using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.ConnectionStrings;
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
        var configuration = ConfigurationFactory.New(new[] { "serverSettings.json" });
        var connStr = configuration.GetConnectionString("ConnectionString");

        var conn = new Sqleze.Startup()
            .Connect(connStr);

        conn.Sql("SELECT 'Hellorld'").ReadSingle<string>().ShouldBe("Hellorld");
            
    }
}
