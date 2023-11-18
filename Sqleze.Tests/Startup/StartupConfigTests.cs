using Microsoft.Extensions.Configuration;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Config;

namespace Sqleze.Tests.Startup;

[TestClass]
public class StartupConfigTests
{
    [TestMethod]
    public void StartupConfigSimple()
    {
        IConfiguration configuration = getConfiguration();

        using var conn = new Sqleze.Startup()
            .Connect(configuration);

        string result = conn
            .Sql("SELECT 'Hellorld'")
            .ReadSingle<string>();
        
        result.ShouldBe("Hellorld");
    }


    private static IConfiguration getConfiguration()
    {
        return ConfigurationFactory.New(new[] { "serverSettings.json" });
    }
}
