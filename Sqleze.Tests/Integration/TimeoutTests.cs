using Microsoft.Data.SqlClient;
using Shouldly;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Integration;

[TestClass]
public class TimeoutTests
{
    [TestMethod]
    public void TimeoutSyncRoot()
    {
        var sqleze = openSqleze();

        using var conn = sqleze.WithCommandTimeout(2)
            .Connect();

        Should.Throw(() =>
        {
            conn.Sql("WAITFOR DELAY '00:00:05'")
                .ExecuteNonQuery();
        }, typeof(SqlException)).Message.ShouldContain("Operation cancelled by user.");
    }

    [TestMethod]
    public void TimeoutSyncConnection()
    {
        var sqleze = openSqleze();

        using var conn = sqleze
            .Connect();

        Should.Throw(() =>
        {
            conn.WithCommandTimeout(2)
                .Sql("WAITFOR DELAY '00:00:05'")
                .ExecuteNonQuery();
        }, typeof(SqlException)).Message.ShouldContain("Operation cancelled by user.");
    }

    [TestMethod]
    public void TimeoutSyncCommand()
    {
        var sqleze = openSqleze();

        using var conn = sqleze
            .Connect();

        Should.Throw(() =>
        {
            conn.Sql("WAITFOR DELAY '00:00:05'")
                .WithCommandTimeout(2)
                .ExecuteNonQuery();
        }, typeof(SqlException)).Message.ShouldContain("Operation cancelled by user.");
    }


    private static ISqlezeBuilder openSqleze()
    {
        var container = new Container();

        container.RegisterSqleze();
        container.RegisterTestSettings();

        return container.Resolve<ISqlezeBuilder>();
    }
}
