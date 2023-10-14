using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestCoder.Shouldly.Gen;

namespace Sqleze.Tests.Integration;

[TestClass]
public class InfoMessageTests
{
    [TestMethod]
    public void InfoMessageOutput()
    {
        var container = DI.NewContainer();

        container.RegisterSqleze();
        container.RegisterTestSettings();

        List<string> messages = new List<string>();

        using var conn = container.Resolve<ISqlezeBuilder>()
            .WithInfoMessagesTo(x => messages.Add(x.Message))
            .Connect();

        var cmd = conn.Sql("PRINT 'Bananas'")
            .ExecuteNonQuery();

        messages.Count.ShouldBe(1);
        messages[0].ShouldBe("Bananas");
    }

    [TestMethod]
    public void InfoMessageOutputDual()
    {
        var container = DI.NewContainer();

        container.RegisterSqleze();
        container.RegisterTestSettings();

        List<string> messages1 = new List<string>();
        List<string> messages2 = new List<string>();

        var sqleze = container.Resolve<ISqlezeBuilder>();

        var sqleze1 = sqleze
            .WithInfoMessagesTo(x => messages1.Add(x.Message));

        using var conn1 = sqleze1.Connect();

        // Note that we stack our InfoMessages onto the existing one
        // therefore messages1 sees both messages from both connections
        // and messages2 just sees messages from connection 2.
        using var conn2 = sqleze1
            .WithInfoMessagesTo(x => messages2.Add(x.Message))
            .Connect();

        conn1
            .Sql("PRINT 'Bananas'")
            .ExecuteNonQuery();

        conn2
            .Sql("PRINT 'Apples'")
            .ExecuteNonQuery();

        //ShouldlyTest.Gen(messages1, nameof(messages1));
        //ShouldlyTest.Gen(messages2, nameof(messages2));

        {
            messages1.ShouldBe(new[] { "Bananas", "Apples" });
        }

        {
            messages2.ShouldBe(new[] { "Apples" });
        }
    }
}
