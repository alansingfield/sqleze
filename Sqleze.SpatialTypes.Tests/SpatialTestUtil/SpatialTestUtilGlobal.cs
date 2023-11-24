using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Config;

namespace Sqleze.SpatialTypes.Tests.SpatialTestUtil;
public static class SpatialTestUtilGlobal
{
    public static ISqlezeConnection Connect()
    {
        var configuration = ConfigurationFactory.New(new[] { "serverSettings.json" });

        return new Sqleze
            .Startup(r => r.UseSpatialTypes())
            .Connect(configuration);
    }
}
