using Sqleze.Composition;
using Sqleze;
using Sqleze.Options;
using Sqleze.SqlClient;

namespace Sqleze;

public class SqlezeConnection : ISqlezeConnection
{
    private readonly IResolverContext connectionScope;
    private readonly IConnectionStringProvider connectionStringProvider;
    private readonly ISqlezeCommandFactory sqlezeCommandFactory;
    private readonly IAdo ado;
    private bool disposedValue;

    public SqlezeConnection(IResolverContext scope,
        IConnectionStringProvider connectionStringProvider,
        ISqlezeCommandFactory sqlezeCommandFactory,
        IAdo ado)
    {
        this.connectionScope = scope;
        this.connectionStringProvider = connectionStringProvider;
        this.sqlezeCommandFactory = sqlezeCommandFactory;
        this.ado = ado;
    }


    public ISqlezeCommandBuilder With<T>(Action<T, ISqlezeScope> configure)
    {
        var scopedFactoryFunc = connectionScope.Resolve<Func<IScopedSqlezeCommandBuilder<T>>>();

        // Create a new factory which is configured to open a new scope
        var scopedFactory = scopedFactoryFunc();

        // Configure factory scope and create the SqlezeCommandFactory
        return scopedFactory.Create(configure);
    }

    public bool InTransaction => this.ado.InTransaction;

    public bool AutoTransaction 
    { 
        get => this.ado.AutoTransaction; 
        set => this.ado.AutoTransaction = value; 
    }

    public void BeginTransaction()
    {
        ado.BeginTransaction();
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await ado.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
    }

    public void Commit()
    {
        ado.Commit();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await ado.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public void Rollback()
    {
        ado.Rollback();
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await ado.RollbackAsync(cancellationToken).ConfigureAwait(false);
    }

    protected void Dispose(bool disposing)
    {
        if(disposedValue)
            return;

        if(disposing)
        {
            this.connectionScope?.Dispose();
        }

        disposedValue = true;
    }

    public void Dispose() => Dispose(true);
}
