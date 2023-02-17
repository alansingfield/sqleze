using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Integration;

[TestClass]
public class DataSetReadTests
{
    [TestMethod]
    public void DataTablePopulate()
    {
        using var conn = connect();

        var dataSet = new DataSet();
        dataSet.Tables.Add("Table1");

        conn.Sql("SELECT a = 1, b = 2").FillDataTable(dataSet.Tables[0]);

        var table = dataSet.Tables[0];

        table.Rows.Count.ShouldBe(1);
        var row = table.Rows[0];

        row["a"].ShouldBe(1);
        row["b"].ShouldBe(2);
    }

    [TestMethod]
    public void DataTablePopulateDual()
    {
        using var conn = connect();

        var dataSet = new DataSet();
        dataSet.Tables.Add("Table1");
        dataSet.Tables.Add("Table2");

        conn.Sql(@"
            SELECT a = 1, b = 2;
            SELECT c = 3, d = 4;

            ")
            .FillDataTable(dataSet.Tables[0])
            .FillDataTable(dataSet.Tables[1])
            ;

        var table1 = dataSet.Tables[0];

        table1.Rows.Count.ShouldBe(1);
        var row1 = table1.Rows[0];

        row1["a"].ShouldBe(1);
        row1["b"].ShouldBe(2);

        var table2 = dataSet.Tables[1];

        table2.Rows.Count.ShouldBe(1);
        var row2 = table2.Rows[0];

        row2["c"].ShouldBe(3);
        row2["d"].ShouldBe(4);
    }


    [TestMethod]
    public void DataSetPopulate()
    {
        using var conn = connect();

        var dataSet = conn.Sql("SELECT a = 1, b = 2").ExecuteDataSet();

        var table = dataSet.Tables[0];

        table.Rows.Count.ShouldBe(1);
        var row = table.Rows[0];

        row["a"].ShouldBe(1);
        row["b"].ShouldBe(2);
    }

    [TestMethod]
    public void DataSetPopulateWithParams()
    {
        using var conn = connect();

        var dataSet = conn.Sql("SELECT a = @x, b = 2")
            .Parameters.Set("@x", 1)
            .ExecuteDataSet<DataSet>();

        var table = dataSet.Tables[0];

        table.Rows.Count.ShouldBe(1);
        var row = table.Rows[0];

        row["a"].ShouldBe(1);
        row["b"].ShouldBe(2);
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
