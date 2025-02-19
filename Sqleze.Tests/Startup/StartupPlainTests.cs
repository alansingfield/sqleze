using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Config;

namespace Sqleze.Tests.Startup;

[TestClass]
public class StartupPlainTests
{
    [TestMethod]
    public void StartupSimple()
    {
        string connStr = getConnectionString();

        using var conn = new Sqleze.Startup()
            .Connect(connStr);

        string result = conn
            .Sql("SELECT 'Hellorld'")
            .ReadSingle<string>();
        
        result.ShouldBe("Hellorld");
    }

    [TestMethod]
    public void StartupFactory()
    {
        string connStr = getConnectionString();

        var connector = new Sqleze.Startup()
            .Build(connStr);

        using var conn = connector.Connect();

        conn.Sql("SELECT 'Hellorld'")
            .ReadSingle<string>()
            .ShouldBe("Hellorld");


    }

    private static string getConnectionString()
    {
        var configuration = ConfigurationFactory.New(new[] { "serverSettings.json" });

        return configuration.GetConnectionString("DefaultConnection") 
            ?? throw new NullReferenceException("Unable to read DefaultConnection from config");
    }
}
