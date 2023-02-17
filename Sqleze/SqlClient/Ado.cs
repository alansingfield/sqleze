using Sqleze;
using Sqleze.Options;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Sqleze.SqlClient;

public class Ado : IAdo
{
    private readonly IConnectionStringProvider connectionStringProvider;
    private readonly Func<MS.SqlConnection> newSqlConnection;
    private readonly Func<MS.SqlCommand> newSqlCommand;
    private readonly IConnectionPreOpen[] connectionPreOpens;

    public Ado(IConnectionStringProvider connectionStringProvider,
        Func<MS.SqlConnection> newSqlConnection,
        Func<MS.SqlCommand> newSqlCommand,
        IConnectionPreOpen[] connectionPreOpens)
    {
        this.AutoTransaction = true;
        this.connectionStringProvider = connectionStringProvider;
        this.newSqlConnection = newSqlConnection;
        this.newSqlCommand = newSqlCommand;
        this.connectionPreOpens = connectionPreOpens;
    }


    public MS.SqlConnection? SqlConnection { get; private set; }
    public MS.SqlTransaction? SqlTransaction { get; private set; }
    public MS.SqlDataReader? SqlDataReader { get; private set; }
    public MS.SqlCommand? SqlCommand { get; private set; }

    public int RowsetCounter { get; private set; }

    private bool pendingOutputCaptures;

    public bool AutoTransaction
    {
        get { return autoTransaction; }
        set
        {
            if(value != autoTransaction && this.InTransaction)
                throw new Exception("AutoTransaction cannot be changed while within a transaction");

            autoTransaction = value;
        }
    }


    [MemberNotNull(nameof(SqlConnection))]
    public void Connect()
    {
        if(this.SqlConnection == null)
        {
            createSqlConnection();

            this.SqlConnection.Open();
        }
        else
        {
            // If we have an existing connection, but it is in a BROKEN state, don't use it.
            if(connectionIsDead(this.SqlConnection))
            {
                safeDisposeConnection();
                createSqlConnection();

                this.SqlConnection.Open();

                return;
            }
        }
    }

    [MemberNotNull(nameof(SqlConnection))]
    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        if(SqlConnection == null)
        {
            createSqlConnection();

            await this.SqlConnection.OpenAsync(cancellationToken).ConfigureAwait(false);
        }
        else
        {
            // If we have an existing connection, but it is in a BROKEN state, don't use it.
            if(connectionIsDead(this.SqlConnection))
            {
                safeDisposeConnection();
                createSqlConnection();

                await this.SqlConnection.OpenAsync(cancellationToken).ConfigureAwait(false);

                return;
            }
        }
    }

    private void safeDisposeConnection()
    {
        try
        {
            this.SqlConnection?.Dispose();
        }
        catch(Exception) { }

        this.SqlConnection = null;
    }

    private bool connectionIsDead(MS.SqlConnection sqlConnection)
    {
        return sqlConnection.State is ConnectionState.Broken or ConnectionState.Closed;
    }


    [MemberNotNull(nameof(SqlConnection))]
    private void createSqlConnection()
    {
        SqlConnection = newSqlConnection();

        // PreOpen is where we configure the connection string and security token
        foreach(var preOpen in connectionPreOpens)
        {
            preOpen.PreOpen(SqlConnection);
        }
    }

    public bool InTransaction => this.SqlTransaction != null;

    public void BeginTransaction()
    {
        if(this.AutoTransaction)
            throw new Exception($"{nameof(BeginTransaction)} cannot be called if AutoTransaction is set");

        if(this.InTransaction)
            throw new Exception("Already in a transaction; transactions cannot be nested.");

        internalBeginTransaction();
    }

    private bool isBeginTransactionRequired()
    {
        // Transaction already open - nothing to do.
        if(this.SqlTransaction != null)
            return false;

        // If not using auto-transactions, and user didn't call BeginTransaction, nothing to do.
        if(!this.AutoTransaction)
            return false;

        return true;
    }

    private void internalBeginTransaction()
    {
        ensureSqlConnection();

        this.SqlTransaction = this.SqlConnection.BeginTransaction();
    }


    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if(this.AutoTransaction)
            throw new Exception($"{nameof(BeginTransaction)} cannot be called if AutoTransaction is set");

        if(this.InTransaction)
            throw new Exception("Already in a transaction; transactions cannot be nested.");

        await internalBeginTransactionAsync(cancellationToken).ConfigureAwait(false);
    }


    private async Task internalBeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        ensureSqlConnection();

        this.SqlTransaction = (MS.SqlTransaction)await 
            this.SqlConnection.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
    }



    public void Rollback()
    {
        if(this.AutoTransaction)
            throw new Exception($"{nameof(Rollback)} cannot be called if AutoTransaction is set");

        internalRollback();
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if(this.AutoTransaction)
            throw new Exception($"{nameof(Rollback)} cannot be called if AutoTransaction is set");

        await internalRollbackAsync(cancellationToken).ConfigureAwait(false);
    }


    private void internalRollback()
    {
        if(this.SqlTransaction == null)
            throw new Exception("Cannot rollback because not in a transaction");

        this.SqlTransaction.Rollback();
        this.SqlTransaction.Dispose();
        this.SqlTransaction = null;
    }

    private async Task internalRollbackAsync(CancellationToken cancellationToken)
    {
        if(this.SqlTransaction == null)
            throw new Exception("Cannot rollback because not in a transaction");

        await this.SqlTransaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
        await this.SqlTransaction.DisposeAsync().ConfigureAwait(false);
        this.SqlTransaction = null;
    }

    public void Commit()
    {
        if(this.AutoTransaction)
            throw new Exception($"{nameof(Commit)} cannot be called if AutoTransaction is set");

        internalCommit();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if(this.AutoTransaction)
            throw new Exception($"{nameof(CommitAsync)} cannot be called if AutoTransaction is set");

        await internalCommitAsync(cancellationToken).ConfigureAwait(false);
    }

    private void internalCommit()
    {
        if(this.SqlTransaction == null)
            throw new Exception("Cannot commit because not in a transaction");

        this.SqlTransaction.Commit();
        this.SqlTransaction.Dispose();
        this.SqlTransaction = null;
    }

    private async Task internalCommitAsync(CancellationToken cancellationToken)
    {
        if(this.SqlTransaction == null)
            throw new Exception("Cannot commit because not in a transaction");

        await this.SqlTransaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        await this.SqlTransaction.DisposeAsync().ConfigureAwait(false);

        this.SqlTransaction = null;
    }

    /// <summary>
    /// Called when an error occurs, rolls back the transaction if we are in one.
    /// </summary>
    private void rollbackIfInTransaction()
    {
        // Rollback the transaction
        if(this.InTransaction)
        {
            internalRollback();
        }
    }
    
    private async Task rollbackIfInTransactionAsync()
    {
        // Rollback the transaction
        if(this.InTransaction)
        {
            // Note that we don't use a CancellationToken here.
            await internalRollbackAsync(default).ConfigureAwait(false);
        }
    }

    private List<Action>? capturedParams;

    public void StartCommand(
        CommandCreateOptions commandCreateOptions,
        IScopedSqlezeReaderFactory execScope,
        IEnumerable<IAdoParameter> adoParameters)
    {
        if(this.SqlCommand != null)
            throw new Exception("StartCommand called before EndCommand");

        // Create new ADO command
        var sqlCommand = newSqlCommand();

        sqlCommand.CommandType = commandCreateOptions.IsStoredProc ? CommandType.StoredProcedure : CommandType.Text;
        sqlCommand.CommandText = commandCreateOptions.Sql;

        foreach(var adoParameter in adoParameters)
        {
            sqlCommand.Parameters.Add(adoParameter.SqlParameter);
        }

        this.capturedParams = adoParameters
            .Select(x => x.CaptureOutput)
            .OfType<Action>()   // Excludes the nulls
            .ToList();

        this.pendingOutputCaptures = capturedParams.Count > 0;

        // Apply external options (timeout etc)
        foreach(var commandPreOpen in execScope.GetCommandPreExecutes())
        {
            commandPreOpen.PreExecute(sqlCommand);
        }

        this.SqlCommand = sqlCommand;
    }


    public void ReadOutputParams()
    {
        if(capturedParams != null)
        {
            foreach(var outputAction in capturedParams)
            {
                outputAction();
            }
        }
        capturedParams = null;
        pendingOutputCaptures = false;
    }

    public void ThrowIfPendingOutputCaptures()
    {
        if(this.pendingOutputCaptures)
        {
            // Throw this exception only once, it will come round again on Dispose()
            // otherwise.
            this.pendingOutputCaptures = false;
            throw new Exception("Reader was closed before output parameters were read. Be sure to enumerate all rowsets");
        }
    }

    public void EndCommand()
    {
        if(this.SqlCommand == null)
            throw new Exception("EndCommand called before StartCommand");

        this.SqlCommand = null;
    }




    public int ExecuteNonQuery()
    {
        ensureSqlCommand();

        // Open the SQL connection if it hasn't been opened already.
        Connect();

        //// If this is a stored procedure, we can check for missing parameters
        //writeMissingParameterTrace(sqlCommand);

        int result;

        try
        {
            if(isBeginTransactionRequired())
                internalBeginTransaction();

            setCommandConnectionAndTransaction();

            // Execute the command.
            try
            {
                result = this.SqlCommand.ExecuteNonQuery();
            }
            catch(Exception)
            {
                rollbackIfInTransaction();
                throw;
            }

            if(this.AutoTransaction)
                internalCommit();
        }
        catch(Exception e)
        {
            AddExceptionContext(e);
            throw;
        }

        return result;
    }


    public async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken = default)
    {
        ensureSqlCommand();

        // Open the SQL connection if it hasn't been opened already.
        await ConnectAsync(cancellationToken).ConfigureAwait(false);

        //// If this is a stored procedure, we can check for missing parameters
        //writeMissingParameterTrace(sqlCommand);

        int result;

        try
        {
            if(isBeginTransactionRequired())
            {
                await internalBeginTransactionAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            setCommandConnectionAndTransaction();

            // Execute the command.
            try
            {
                result = await this.SqlCommand
                    .ExecuteNonQueryAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch(Exception)
            {
                await rollbackIfInTransactionAsync().ConfigureAwait(false);

                throw;
            }

            if(this.AutoTransaction)
                await internalCommitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch(Exception e)
        {
            AddExceptionContext(e);
            throw;
        }

        return result;
    }

    public void ExecuteReader()
    {
        ensureSqlCommand();

        if(this.SqlDataReader != null)
            throw new Exception("DataReader is already open, (expected call to CompleteReader)");

        // Open the SQL connection if it hasn't been opened already.
        Connect();

        try
        {
            if(isBeginTransactionRequired())
                internalBeginTransaction();

            setCommandConnectionAndTransaction();

            // Invalidate any captured old values of reader to avoid getting the wrong
            // data from GetSqlValueMetadata()
            this.RowsetCounter++;

            // Execute the command.
            try
            {
                this.SqlDataReader = this.SqlCommand.ExecuteReader();
            }
            catch(Exception)
            {
                closeSqlDataReader();
                rollbackIfInTransaction();
                throw;
            }

            // Note that we leave the transaction open until all the rowsets are read.
        }
        catch(Exception e)
        {
            AddExceptionContext(e);
            throw;
        }
    }


    public async Task ExecuteReaderAsync(CancellationToken cancellationToken = default)
    {
        ensureSqlCommand();

        if(this.SqlDataReader != null)
            throw new Exception("DataReader is already open, (expected call to CompleteReader)");

        // Open the SQL connection if it hasn't been opened already.
        await ConnectAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            if(isBeginTransactionRequired())
                await internalBeginTransactionAsync(cancellationToken)
                    .ConfigureAwait(false);

            setCommandConnectionAndTransaction();

            // Invalidate any captured old values of reader to avoid getting the wrong
            // data from GetSqlValueMetadata()
            this.RowsetCounter++;

            // Execute the command.
            try
            {
                this.SqlDataReader = await this.SqlCommand
                    .ExecuteReaderAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            catch(Exception)
            {
                closeSqlDataReader();

                // Note that we don't obey the CancellationToken here, we want to rollback
                // when being cancelled.
                await rollbackIfInTransactionAsync().ConfigureAwait(false);

                throw;
            }
        }
        catch(Exception e)
        {
            AddExceptionContext(e);
            throw;
        }
    }




    public bool NextResult()
    {
        ensureSqlDataReader();

        this.RowsetCounter++;

        // Advance to the next result set.
        var moreResults = this.SqlDataReader.NextResult();

        if(!moreResults)
        {
            finishReader();
        }

        return moreResults;
    }

    public async Task<bool> NextResultAsync(CancellationToken cancellationToken = default)
    {
        ensureSqlDataReader();

        this.RowsetCounter++;

        // Advance to the next result set.
        var moreResults = await this.SqlDataReader
            .NextResultAsync(cancellationToken)
            .ConfigureAwait(false);

        if(!moreResults)
        {
            await finishReaderAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        return moreResults;
    }

    private void finishReader()
    {
        ensureSqlDataReader();

        // If no more result sets, close the reader. Don't dispose it though, we
        // need it to grab the output parameters.
        this.SqlDataReader.Close();

        ReadOutputParams();
        EndCommand();

        CompleteReader();

    }

    private async Task finishReaderAsync(CancellationToken cancellationToken)
    {
        ensureSqlDataReader();

        // If no more result sets, close the reader. Don't dispose it though, we
        // need it to grab the output parameters.
        this.SqlDataReader.Close();

        ReadOutputParams();
        EndCommand();

        await CompleteReaderAsync(cancellationToken).ConfigureAwait(false);
    }


    public void CompleteReader()
    {
        if(this.SqlDataReader == null)
            throw new Exception("AdoState must be ReaderActive (expected call to ExecuteReader)");

        closeSqlDataReader();

        if(this.AutoTransaction)
        {
            internalCommit();
        }
    }

    public async Task CompleteReaderAsync(CancellationToken cancellationToken)
    {
        if(this.SqlDataReader == null)
            throw new Exception("AdoState must be ReaderActive (expected call to ExecuteReader)");

        closeSqlDataReader();

        if(this.AutoTransaction)
        {
            await internalCommitAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public void ReaderFault()
    {
        this.SqlCommand?.Cancel();

        closeSqlDataReader();

        if(this.AutoTransaction)
        {
            internalRollback();
        }
    }

    public async Task ReaderFaultAsync(CancellationToken cancellationToken = default)
    {
        this.SqlCommand?.Cancel();

        closeSqlDataReader();

        if(this.AutoTransaction)
        {
            await internalRollbackAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    private void closeSqlDataReader()
    {
        this.SqlDataReader?.Close();
        this.SqlDataReader?.Dispose();
        this.SqlDataReader = null;
    }

    private void setCommandConnectionAndTransaction()
    {
        ensureSqlCommand();

        this.SqlCommand.Connection = SqlConnection;

        // Transaction COULD be null if AutoTransaction is turned off and they didn't call
        // BeginTransaction() first. This is expected and OK; some DDL statements can't run
        // within a transaction, for example.
        this.SqlCommand.Transaction = SqlTransaction;
    }


    [MemberNotNull(nameof(SqlConnection))]
    private void ensureSqlConnection()
    {
        if(this.SqlConnection == null)
            throw new Exception("Connection is not open");
    }

    [MemberNotNull(nameof(SqlDataReader))]
    private void ensureSqlDataReader()
    {
        if(this.SqlDataReader == null)
            throw new Exception("DataReader is not open");
    }

    [MemberNotNull(nameof(SqlCommand))]
    private void ensureSqlCommand()
    {
        if(this.SqlCommand == null)
            throw new Exception("Call StartCommand() first");
    }


    private void AddExceptionContext(Exception ex)
    {
        // TODO - Abstract this out separately...

        if(this.SqlConnection != null)
        {
            ex.Data.Add("Connection String", SqlConnection.ConnectionString);
        }
        if(this.SqlCommand != null)
        {
            if(this.SqlCommand.CommandType == CommandType.StoredProcedure)
                ex.Data.Add("Stored Procedure", this.SqlCommand.CommandText);
            else
                ex.Data.Add("Command Text", this.SqlCommand.CommandText);

            //if(this.SqlCommand.Parameters.Count > 0)
            //{
            //    SqlParameter[] paramArray = new SqlParameter[sqlCommand.Parameters.Count];
            //    sqlCommand.Parameters.CopyTo(paramArray, 0);
            //    ex.Data.Add("Command Parameters", paramArray);
            //}
        }

        //if(ex is MS.SqlException sqlException)
        //{
        //    ex.Data.Add("Line Number", sqlException.LineNumber);
        //    ex.Data.Add("SQL Exception Number", sqlException.Number);
        //}
    }


    public AdoState AdoState
    {
        get
        {
            if(SqlDataReader != null)
                return AdoState.ReaderActive;
            if(SqlCommand != null)
                return AdoState.CommandOpen;
            if(SqlTransaction != null)
                return AdoState.TransactionStarted;
            if(SqlConnection != null)
                return AdoState.Connected;
            else
                return AdoState.Disconnected;
        }
    }







    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    private bool autoTransaction;

    protected virtual void Dispose(bool disposing)
    {
        if(!disposedValue)
        {
            if(disposing)
            {
                closeSqlDataReader();

                SqlTransaction?.Dispose();
                SqlTransaction = null;

                SqlConnection?.Dispose();
                SqlConnection = null;
            }

            disposedValue = true;
        }
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        Dispose(true);
    }


    #endregion
}