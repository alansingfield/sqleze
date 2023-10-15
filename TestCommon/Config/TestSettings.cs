using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Config;

namespace TestCommon.TestUtil;
public static class TestSettings
{
    static TestSettings()
    {
        var configuration = ConfigurationFactory.New(new[] { "serverSettings.json" });

        ConnectionString = configuration.GetConnectionString("ConnectionString");
    }

    public static string ConnectionString { get; private set; }
}
