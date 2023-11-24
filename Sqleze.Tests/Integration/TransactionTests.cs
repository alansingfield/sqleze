using Microsoft.Data.SqlClient;
using Shouldly;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sqleze.Tests.Integration;

[TestClass]
public class TransactionTests
{
    [TestMethod]
    public void SqlErrorCausesRollback1()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        // The SQL will make a divide by zero occur when we start to read it.
        var rdr = conn.Sql("SELECT 1/0").ExecuteReader();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        var exception = Should.Throw(() =>
        {
            rdr.ReadSingle<int>();
        }, typeof(SqlException));

        // It will drop out of the transaction.
        conn.InTransaction.ShouldBe(false);

        exception.Message.ShouldContain("Divide by zero");

    }


    [TestMethod]
    public async Task SqlErrorCausesRollback1Async()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        // The SQL will make a divide by zero occur when we start to read it.
        var rdr = await conn.Sql("SELECT 1/0").ExecuteReaderAsync();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        var exception = Should.Throw(async () =>
        {
            await rdr.ReadSingleAsync<int>();
        }, typeof(SqlException));

        // It will drop out of the transaction.
        conn.InTransaction.ShouldBe(false);

        exception.Message.ShouldContain("Divide by zero");

    }

    [TestMethod]
    public void SqlErrorCausesRollback2()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        // The first rowset will execute OK. Second will raise error.
        var rdr = conn.Sql(@"
            SELECT 1;
            SELECT 2/0;

        ").ExecuteReader();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        rdr.ReadSingle<int>().ShouldBe(1);

        // Stays in transaction because there are further rowsets
        conn.InTransaction.ShouldBe(true);

        // Next rowset causes the divide by zero error.
        var exception = Should.Throw(() =>
        {
            rdr.ReadSingle<int>();
        }, typeof(SqlException));

        exception.Message.ShouldContain("Divide by zero");
        conn.InTransaction.ShouldBe(false);
    }

    [TestMethod]
    public async Task SqlErrorCausesRollback2Async()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        // The first rowset will execute OK. Second will raise error.
        var rdr = await conn.Sql(@"
            SELECT 1;
            SELECT 2/0;

        ").ExecuteReaderAsync();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        (await rdr.ReadSingleAsync<int>()).ShouldBe(1);

        // Stays in transaction because there are further rowsets
        conn.InTransaction.ShouldBe(true);

        // Next rowset causes the divide by zero error.
        var exception = Should.Throw(async () =>
        {
            await rdr.ReadSingleAsync<int>();
        }, typeof(SqlException));

        exception.Message.ShouldContain("Divide by zero");
        conn.InTransaction.ShouldBe(false);
    }

    [TestMethod]
    public void SqlErrorCausesRollback3()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        conn.Sql(@"
            CREATE TABLE #tmp (val int);
            INSERT INTO #tmp VALUES (1);
        ").ExecuteNonQuery();

        conn.Sql("SELECT val FROM #tmp")
            .ReadArray<int>()
            .ShouldBe(new[] { 1 });

        var rdr = conn.Sql(@"
            INSERT INTO #tmp VALUES (2);
            SELECT 3;
            INSERT INTO #tmp VALUES (4);
            SELECT 5/0;
            INSERT INTO #tmp VALUES (6);
        ").ExecuteReader();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        // First rowset (the SELECT 3 command)
        rdr.ReadSingle<int>().ShouldBe(3);

        // Stays in transaction because there are further rowsets
        conn.InTransaction.ShouldBe(true);

        // Next rowset causes the divide by zero error. Capture and handle this error.
        Should.Throw(() =>
        {
            rdr.ReadSingle<int>();
        }, typeof(SqlException)).Message.ShouldContain("Divide by zero");

        // The command will have been rolled back automatically because of the exception
        // on ReadSingle()
        conn.InTransaction.ShouldBe(false);

        // Should not get value 2 or 4 in the #tmp table because we rolled back.
        conn.Sql("SELECT val FROM #tmp")
            .ReadArray<int>()
            .ShouldBe(new[] { 1 });

        // But we can still run further commands, and they go into their own txns.
    }

    [TestMethod]
    public async Task SqlErrorCausesRollback3Async()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        await conn.Sql(@"
            CREATE TABLE #tmp (val int);
            INSERT INTO #tmp VALUES (1);
        ").ExecuteNonQueryAsync();

        (await conn.Sql("SELECT val FROM #tmp")
            .ReadArrayAsync<int>())
            .ShouldBe(new[] { 1 });

        var rdr = await conn.Sql(@"
            INSERT INTO #tmp VALUES (2);
            SELECT 3;
            INSERT INTO #tmp VALUES (4);
            SELECT 5/0;
            INSERT INTO #tmp VALUES (6);
        ").ExecuteReaderAsync();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        // First rowset (the SELECT 3 command)
        (await rdr.ReadSingleAsync<int>()).ShouldBe(3);

        // Stays in transaction because there are further rowsets
        conn.InTransaction.ShouldBe(true);

        // Next rowset causes the divide by zero error. Capture and handle this error.
        Should.Throw(async () =>
        {
            await rdr.ReadSingleAsync<int>();
        }, typeof(SqlException)).Message.ShouldContain("Divide by zero");

        // The command will have been rolled back automatically because of the exception
        // on ReadSingle()
        conn.InTransaction.ShouldBe(false);

        // Should not get value 2 or 4 in the #tmp table because we rolled back.
        conn.Sql("SELECT val FROM #tmp")
            .ReadArray<int>()
            .ShouldBe(new[] { 1 });

        // But we can still run further commands, and they go into their own txns.
    }

    [TestMethod]
    public void TransactionAutoCommit1()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        conn.Sql(@"
            CREATE TABLE #tmp (val int);
            INSERT INTO #tmp VALUES (1);
        ").ExecuteNonQuery();

        conn.Sql("SELECT val FROM #tmp")
            .ReadArray<int>()
            .ShouldBe(new[] { 1 });

        var rdr = conn.Sql(@"
            INSERT INTO #tmp VALUES (2);
            SELECT 3;
            INSERT INTO #tmp VALUES (4);
            SELECT 5;
            INSERT INTO #tmp VALUES (6);
        ").ExecuteReader();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        // First rowset (the SELECT 3 command)
        rdr.ReadSingle<int>().ShouldBe(3);

        // Stays in transaction because there are further rowsets
        conn.InTransaction.ShouldBe(true);

        // Next rowset (SELECT 5)
        rdr.ReadSingle<int>().ShouldBe(5);

        // The command will have committed now since that's the last rowset
        conn.InTransaction.ShouldBe(false);

        // Check all the values are written into #tmp
        conn.Sql("SELECT val FROM #tmp")
            .ReadArray<int>()
            .ShouldBe(new[] { 1, 2, 4, 6 });

    }

    [TestMethod]
    public async Task TransactionAutoCommit1Async()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        await conn.Sql(@"
            CREATE TABLE #tmp (val int);
            INSERT INTO #tmp VALUES (1);
        ").ExecuteNonQueryAsync();

        (await conn.Sql("SELECT val FROM #tmp")
            .ReadArrayAsync<int>())
            .ShouldBe(new[] { 1 });

        var rdr = await conn.Sql(@"
            INSERT INTO #tmp VALUES (2);
            SELECT 3;
            INSERT INTO #tmp VALUES (4);
            SELECT 5;
            INSERT INTO #tmp VALUES (6);
        ").ExecuteReaderAsync();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        // First rowset (the SELECT 3 command)
        (await rdr.ReadSingleAsync<int>()).ShouldBe(3);

        // Stays in transaction because there are further rowsets
        conn.InTransaction.ShouldBe(true);

        // Next rowset (SELECT 5)
        (await rdr.ReadSingleAsync<int>()).ShouldBe(5);

        // The command will have committed now since that's the last rowset
        conn.InTransaction.ShouldBe(false);

        // Check all the values are written into #tmp
        conn.Sql("SELECT val FROM #tmp")
            .ReadArray<int>()
            .ShouldBe(new[] { 1, 2, 4, 6 });

    }

    [TestMethod]
    public void TransactionAutoCommitRowsetOmitted()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        conn.Sql(@"
            CREATE TABLE #tmp (val int);
            INSERT INTO #tmp VALUES (1);
        ").ExecuteNonQuery();

        conn.Sql("SELECT val FROM #tmp")
            .ReadArray<int>()
            .ShouldBe(new[] { 1 });

        var rdr = conn.Sql(@"
            INSERT INTO #tmp VALUES (2);
            SELECT 3;
            INSERT INTO #tmp VALUES (4);
            SELECT 5;
            INSERT INTO #tmp VALUES (6);
        ").ExecuteReader();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        // First rowset (the SELECT 3 command)
        rdr.ReadSingle<int>().ShouldBe(3);

        // Stays in transaction because there are further rowsets
        conn.InTransaction.ShouldBe(true);

        // Next command will abandon remaining rowsets from previous
        // query.

        // Check all the values are written into #tmp even though we
        // didn't enumerate all the rowsets.
        conn.Sql("SELECT val FROM #tmp")
            .ReadArray<int>()
            .ShouldBe(new[] { 1, 2, 4, 6 });

        // No longer in a transaction since we read to the end.
        conn.InTransaction.ShouldBe(false);

    }

    [TestMethod]
    public async Task TransactionAutoCommitRowsetOmittedAsync()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.Connect();
        conn.AutoTransaction.ShouldBe(true);
        conn.InTransaction.ShouldBe(false);

        await conn.Sql(@"
            CREATE TABLE #tmp (val int);
            INSERT INTO #tmp VALUES (1);
        ").ExecuteNonQueryAsync();

        (await conn.Sql("SELECT val FROM #tmp")
            .ReadArrayAsync<int>())
            .ShouldBe(new[] { 1 });

        var rdr = await conn.Sql(@"
            INSERT INTO #tmp VALUES (2);
            SELECT 3;
            INSERT INTO #tmp VALUES (4);
            SELECT 5;
            INSERT INTO #tmp VALUES (6);
        ").ExecuteReaderAsync();

        // ExecuteReader causes us to go into a transaction
        conn.InTransaction.ShouldBe(true);

        // First rowset (the SELECT 3 command)
        (await rdr.ReadSingleAsync<int>()).ShouldBe(3);

        // Stays in transaction because there are further rowsets
        conn.InTransaction.ShouldBe(true);

        // Next command will abandon remaining rowsets from previous
        // query.

        // Check all the values are written into #tmp even though we
        // didn't enumerate all the rowsets.
        conn.Sql("SELECT val FROM #tmp")
            .ReadArray<int>()
            .ShouldBe(new[] { 1, 2, 4, 6 });

        // No longer in a transaction since we read to the end.
        conn.InTransaction.ShouldBe(false);

    }

    [TestMethod]
    public void TransactionManualCommitWithError()
    {
        var sqleze = openSqleze();

        using(var conn = sqleze.Connect())
        {
            conn.AutoTransaction = false;

            // Create temp table without using a txn.
            conn.Sql(@"
                CREATE TABLE #tmp (val int);
                INSERT INTO #tmp VALUES (1);
            ").ExecuteNonQuery();

            conn.Sql("SELECT val FROM #tmp")
                .ReadArray<int>()
                .ShouldBe(new[] { 1 });

            conn.BeginTransaction();

            conn.InTransaction.ShouldBe(true);

            var rdr = conn.Sql(@"
                INSERT INTO #tmp VALUES (2);
                SELECT 3/0;
                INSERT INTO #tmp VALUES (4);
                SELECT 5;
            ").ExecuteReader();

            conn.InTransaction.ShouldBe(true);

            Should.Throw(() =>
            {
                rdr.ReadSingle<int>();
            }, typeof(SqlException)).Message.ShouldContain("Divide by zero");

            conn.InTransaction.ShouldBe(true);

            conn.Commit();

            conn.InTransaction.ShouldBe(false);

            // Check all the values are written into #tmp even though we
            // didn't enumerate all the rowsets.
            conn.Sql("SELECT val FROM #tmp")
                .ReadArray<int>()
                .ShouldBe(new[] { 1, 2, 4 });
        }
    }

    [TestMethod]
    public async Task TransactionManualCommitWithErrorAsync()
    {
        var sqleze = openSqleze();

        using(var conn = sqleze.Connect())
        {
            conn.AutoTransaction = false;

            // Create temp table without using a txn.
            await conn.Sql(@"
                CREATE TABLE #tmp (val int);
                INSERT INTO #tmp VALUES (1);
            ").ExecuteNonQueryAsync();

            (await conn.Sql("SELECT val FROM #tmp")
                .ReadArrayAsync<int>())
                .ShouldBe(new[] { 1 });

            conn.BeginTransaction();

            conn.InTransaction.ShouldBe(true);

            var rdr = await conn.Sql(@"
                INSERT INTO #tmp VALUES (2);
                SELECT 3/0;
                INSERT INTO #tmp VALUES (4);
                SELECT 5;
            ").ExecuteReaderAsync();

            conn.InTransaction.ShouldBe(true);

            Should.Throw(async () =>
            {
                await rdr.ReadSingleAsync<int>();
            }, typeof(SqlException)).Message.ShouldContain("Divide by zero");

            conn.InTransaction.ShouldBe(true);

            await conn.CommitAsync();

            conn.InTransaction.ShouldBe(false);

            // Check all the values are written into #tmp even though we
            // didn't enumerate all the rowsets.
            conn.Sql("SELECT val FROM #tmp")
                .ReadArray<int>()
                .ShouldBe(new[] { 1, 2, 4 });
        }
    }


    [TestMethod]
    public void TransactionManualRollbackOnDispose()
    {
        var sqleze = openSqleze();

        // Need a proper table since we are closing the creating session.
        string tabName = $"tmp_{Guid.NewGuid():N}";

        try
        {
            var conn = sqleze.Connect();

            conn.AutoTransaction = false;

            // Create temp table without using a txn.
            conn.Sql(@$"
                CREATE TABLE {tabName} (val int);
                INSERT INTO {tabName} VALUES (1);
            ").ExecuteNonQuery();

            conn.Sql($"SELECT val FROM {tabName}")
                .ReadArray<int>()
                .ShouldBe(new[] { 1 });

            conn.InTransaction.ShouldBe(false);

            conn.BeginTransaction();

            conn.InTransaction.ShouldBe(true);

            conn.Sql($"INSERT INTO {tabName} VALUES (2);").ExecuteNonQuery();

            conn.InTransaction.ShouldBe(true);

            conn.Sql($"SELECT val FROM {tabName}")
                .ReadArray<int>()
                .ShouldBe(new[] { 1, 2 });

            // Dispose should roll back the txn since we didn't commit
            conn.Dispose();
            conn = null;

            var conn2 = sqleze.Connect();

            conn2.Sql($"SELECT val FROM {tabName}")
                .ReadArray<int>()
                .ShouldBe(new[] { 1 });
        }
        finally
        {
            using(var conn3 = sqleze.Connect())
            {
                conn3.Sql($"DROP TABLE IF EXISTS {tabName}").ExecuteNonQuery();
            }
        }
    }

    [TestMethod]
    public async Task TransactionManualRollbackOnDisposeAsync()
    {
        var sqleze = openSqleze();

        // Need a proper table since we are closing the creating session.
        string tabName = $"tmp_{Guid.NewGuid():N}";

        try
        {
            var conn = sqleze.Connect();

            conn.AutoTransaction = false;

            // Create temp table without using a txn.
            await conn.Sql(@$"
                CREATE TABLE {tabName} (val int);
                INSERT INTO {tabName} VALUES (1);
            ").ExecuteNonQueryAsync();

            (await conn.Sql($"SELECT val FROM {tabName}")
                .ReadArrayAsync<int>())
                .ShouldBe(new[] { 1 });

            conn.InTransaction.ShouldBe(false);

            await conn.BeginTransactionAsync();

            conn.InTransaction.ShouldBe(true);

            await conn.Sql($"INSERT INTO {tabName} VALUES (2);").ExecuteNonQueryAsync();

            conn.InTransaction.ShouldBe(true);

            (await conn.Sql($"SELECT val FROM {tabName}")
                .ReadArrayAsync<int>())
                .ShouldBe(new[] { 1, 2 });

            // Dispose should roll back the txn since we didn't commit
            conn.Dispose();
            conn = null;

            var conn2 = sqleze.Connect();

            conn2.Sql($"SELECT val FROM {tabName}")
                .ReadArray<int>()
                .ShouldBe(new[] { 1 });
        }
        finally
        {
            using(var conn3 = sqleze.Connect())
            {
                conn3.Sql($"DROP TABLE IF EXISTS {tabName}").ExecuteNonQuery();
            }
        }
    }


    [TestMethod]
    public async Task TransactionManualRollbackOnCancellationTokenAsync()
    {
        var sqleze = openSqleze();

        // Need a proper table since we are closing the creating session.
        string tabName = $"tmp_{Guid.NewGuid():N}";

        try
        {
            using var conn = sqleze.Connect();

            conn.AutoTransaction = false;

            // Create temp table without using a txn.
            await conn.Sql(@$"
                CREATE TABLE {tabName} (val int);
                INSERT INTO {tabName} VALUES (1);
            ").ExecuteNonQueryAsync();

            (await conn.Sql($"SELECT val FROM {tabName}")
                .ReadArrayAsync<int>())
                .ShouldBe(new[] { 1 });

            conn.InTransaction.ShouldBe(false);

            await conn.BeginTransactionAsync();

            conn.InTransaction.ShouldBe(true);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(3000);

            SqlException? exception = null;

            try
            {
                await conn.Sql(@$"
                    INSERT INTO {tabName} VALUES (2);
                    WAITFOR DELAY '00:00:20'
                    INSERT INTO {tabName} VALUES (3);
                    ").ExecuteNonQueryAsync(cts.Token);
            }
            catch(SqlException ex)
            {
                exception = ex;
            }

            exception.ShouldNotBeNull();
            exception.Class.ShouldBe((byte)11);
            exception.Message.ShouldContain("Operation cancelled by user");

            conn.InTransaction.ShouldBe(false);

            (await conn.Sql($"SELECT val FROM {tabName}")
                .ReadArrayAsync<int>())
                .ShouldBe(new[] { 1 });

            var conn2 = sqleze.Connect();

            conn2.Sql($"SELECT val FROM {tabName}")
                .ReadArray<int>()
                .ShouldBe(new[] { 1 });
        }
        finally
        {
            using(var conn3 = sqleze.Connect())
            {
                conn3.Sql($"DROP TABLE IF EXISTS {tabName}").ExecuteNonQuery();
            }
        }
    }


    [TestMethod]
    public async Task TransactionNoTransactionCancellationTokenAsync()
    {
        var sqleze = openSqleze();

        // Need a proper table since we are closing the creating session.
        string tabName = $"tmp_{Guid.NewGuid():N}";

        try
        {
            using var conn = sqleze.Connect();

            conn.AutoTransaction = false;

            // Create temp table without using a txn.
            await conn.Sql(@$"
                CREATE TABLE {tabName} (val int);
                INSERT INTO {tabName} VALUES (1);
            ").ExecuteNonQueryAsync();

            (await conn.Sql($"SELECT val FROM {tabName}")
                .ReadArrayAsync<int>())
                .ShouldBe(new[] { 1 });

            conn.InTransaction.ShouldBe(false);

            // Cancel execution after 3 seconds
            var cts = new CancellationTokenSource();
            cts.CancelAfter(3000);

            SqlException? exception = null;

            try
            {
                await conn.Sql(@$"
                    INSERT INTO {tabName} VALUES (2);
                    WAITFOR DELAY '00:00:20'
                    INSERT INTO {tabName} VALUES (3);
                    ").ExecuteNonQueryAsync(cts.Token);
            }
            catch(SqlException ex)
            {
                exception = ex;
            }

            exception.ShouldNotBeNull();
            exception.Class.ShouldBe((byte)11);
            exception.Message.ShouldContain("Operation cancelled by user");

            conn.InTransaction.ShouldBe(false);

            // Without using transactions, the cancellation doesn't rollback,
            // you just get the value 2 inserted and 3 isn't.

            (await conn.Sql($"SELECT val FROM {tabName}")
                .ReadArrayAsync<int>())
                .ShouldBe(new[] { 1, 2 });

            var conn2 = sqleze.Connect();

            conn2.Sql($"SELECT val FROM {tabName}")
                .ReadArray<int>()
                .ShouldBe(new[] { 1, 2 });
        }
        finally
        {
            using(var conn3 = sqleze.Connect())
            {
                conn3.Sql($"DROP TABLE IF EXISTS {tabName}").ExecuteNonQuery();
            }
        }
    }



    private static ISqlezeBuilder openSqleze()
    {
        var container = new Container();

        container.RegisterSqleze();
        container.RegisterTestSettings();

        return container.Resolve<ISqlezeBuilder>().WithConfigKey("DefaultConnection");
    }
}
