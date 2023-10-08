using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Integration;

[TestClass]
public class OutputParameterTests
{
    [TestMethod]
    public void OutputParameterNVarChar()
    {
        using var conn = connect();

        string? foo = null;

        var rdr = conn.Sql(@"
            SET @foo = 'hello'
            SELECT result = @foo
            ")
            .Parameters.Set(() => foo).OutputTo(x => foo = x)
            .ExecuteReader();

        var rowset = rdr.OpenRowset<string>();
        var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
        var result = rowset.Enumerate().Single();

        foo.ShouldBe("hello");
        result.ShouldBe("hello");

        // Output params get promoted to MAX
        dataType.ShouldBe("nvarchar(max)");
    }

    [TestMethod]
    public void OutputParameterNVarCharKnown()
    {
        using var conn = connect();

        string? foo = null;

        var rdr = conn.Sql(@"
            SET @foo = 'hello'
            SELECT result = @foo
            ")
            .Parameters.Set(() => foo).AsNVarChar(4).OutputTo(x => foo = x)
            .ExecuteReader();

        var rowset = rdr.OpenRowset<string>();
        var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
        var result = rowset.Enumerate().Single();

        foo.ShouldBe("hell");
        result.ShouldBe("hell");

        // Explicitly specified it's a nvarchar(4) so that's what you get,
        // including the truncation.
        dataType.ShouldBe("nvarchar(4)");
    }

    [TestMethod]
    public void OutputParameterNVarCharMaxUnknown()
    {
        using var conn = connect();

        string? foo = null;

        var rowset = conn.Sql(@"
            SET @foo = REPLICATE(CAST(N'X' AS nvarchar(max)), 4001);
            SELECT result = @foo;
        ").Parameters
            .Set(() => foo).OutputTo(x => foo = x)
            .ExecuteReader()
            .OpenRowset<string>();

        var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
        var result = rowset.Enumerate().Single();

        result.ShouldBe(new string('X', 4001));
        foo.ShouldBe(new string('X', 4001));
        dataType.ShouldBe("nvarchar(max)");
    }

    [TestMethod]
    public void OutputParameterVarCharMaxUnknown()
    {
        using var conn = connect();

        string? foo = null;

        var rowset = conn.Sql(@"
            SET @foo = REPLICATE(CAST(N'X' AS varchar(max)), 8001);
            SELECT result = @foo;
        ").Parameters
            .Set(() => foo).OutputTo(x => foo = x).AsVarChar()
            .ExecuteReader()
            .OpenRowset<string>();

        var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
        var result = rowset.Enumerate().Single();

        result.ShouldBe(new string('X', 8001));
        foo.ShouldBe(new string('X', 8001));
        dataType.ShouldBe("varchar(max)");
    }

    [TestMethod]
    public void OutputParameterDecimalNotSpecified()
    {
        using var conn = connect();

        decimal? foo = null;

        Should.Throw(() =>
        {
            var rdr = conn.Sql(@"
                SET @foo = 12.345
                SELECT result = @foo
                ")
                .Parameters.Set(() => foo).OutputTo(x => foo = x)
                .ExecuteReader();
        }, typeof(Exception))
           .Message.ShouldBe("Decimal output parameter @foo must have precision and scale specified. Use .AsDecimal(p,s)");
    }

    [TestMethod]
    public void OutputParameterDecimalSpecified()
    {
        using var conn = connect();

        decimal? foo = 34.5m;

        var rdr = conn.Sql(@"
            SET @foo = 12.34
            SELECT result = @foo
            ")
            .Parameters.Set(() => foo).OutputTo(x => foo = x).AsDecimal(10,3)
            .ExecuteReader();

        var rowset = rdr.OpenRowset<decimal>();
        var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
        var result = rowset.Enumerate().Single();

        foo.ShouldBe(12.34m);
        result.ShouldBe(12.34m);

        foo.Scale().ShouldBe((byte)3);
        result.Scale().ShouldBe((byte)3);

        dataType.ShouldBe("decimal(10,3)");
    }

    [TestMethod]
    public void OutputParameterMemberNVarChar()
    {
        using var conn = connect();

        string? foo = null;

        var rdr = conn.Sql(@"
            SET @foo = 'hello'
            SELECT result = @foo
            ")
            .Parameters.Set(() => foo).OutputTo(() => foo)
            .ExecuteReader();

        var rowset = rdr.OpenRowset<string>();
        var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
        var result = rowset.Enumerate().Single();

        foo.ShouldBe("hello");
        result.ShouldBe("hello");

        // Output params get promoted to MAX
        dataType.ShouldBe("nvarchar(max)");
    }

    [TestMethod]
    public void OutputParameterDirectNVarChar()
    {
        using var conn = connect();

        string? foo = null;

        var rdr = conn.Sql(@"
            SET @foo = 'hello'
            SELECT result = @foo
            ")
            .Parameters.OutputTo(() => foo)
            .ExecuteReader();

        var rowset = rdr.OpenRowset<string>();
        var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
        var result = rowset.Enumerate().Single();

        foo.ShouldBe("hello");
        result.ShouldBe("hello");

        // Output params get promoted to MAX
        dataType.ShouldBe("nvarchar(max)");
    }

    [TestMethod]
    public void OutputParameterReturnToProc()
    {
        using var conn = connect();

        int retval = 0;
        conn.StoredProc("dbo.p_return_int")
            .Parameters.ReturnTo<int>(x => retval = x)
            .ExecuteNonQuery();

        retval.ShouldBe(123);

    }

    [TestMethod]
    public void OutputParameterReturnToFunctionWithDefault()
    {
        using var conn = connect();

        // ALTER FUNCTION[dbo].[fn_return_nvarchar] (@arg nvarchar(20) = 'DEFAULT')
        // RETURNS nvarchar(20)
        // AS
        // BEGIN
        //    RETURN @arg;
        // END

        string? retval = null;
        conn.StoredProc("dbo.fn_return_nvarchar")
            .Parameters.ReturnTo<string>(x => retval = x)
            .ExecuteNonQuery();

        retval.ShouldBe("DEFAULT");
    }

    [TestMethod]
    public void OutputParameterReturnToFunctionWithArg()
    {
        using var conn = connect();

        string arg = "Banana";
        string? retval = null;

        conn.StoredProc("dbo.fn_return_nvarchar")
            .Parameters.ReturnTo<string>(x => retval = x)
            .Set(() => arg)
            .ExecuteNonQuery();

        retval.ShouldBe("Banana");
    }

    [TestMethod]
    public void OutputParameterReturnToFunctionWithArgSetter()
    {
        using var conn = connect();

        string arg = "Banana";
        string? retval = null;

        conn.StoredProc("dbo.fn_return_nvarchar")
            .Parameters.ReturnTo(() => retval)
            .Set(() => arg)
            .ExecuteNonQuery();

        retval.ShouldBe("Banana");
    }



    [TestMethod]
    public void OutputParameterReturnMember()
    {
        using var conn = connect();

        int retval = 0;
        conn.StoredProc("dbo.p_return_int")
            .Parameters.ReturnTo(() => retval)
            .ExecuteNonQuery();

        retval.ShouldBe(123);

    }

    private ISqlezeConnection connect()
    {
        var container = new Container();

        container.RegisterSqleze();
        container.RegisterTestSettings();

        return container.Resolve<ISqlezeBuilder>()
            .Connect();
    }
}
