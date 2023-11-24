using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Config;

namespace Sqleze.Tests.TestUtil;
public static class TestUtilGlobal
{
    public static ISqlezeBuilder OpenBuilder()
    {
        var configuration = ConfigurationFactory.New(new[] { "serverSettings.json" });

        return new Sqleze.Startup()
            .OpenBuilder(configuration);
    }


    public static ISqlezeConnection Connect()
    {
        var configuration = ConfigurationFactory.New(new[] { "serverSettings.json" });

        return new Sqleze.Startup()
            .Connect(configuration);
    }
}
