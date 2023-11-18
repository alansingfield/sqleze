using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class StartupConfigurationExtensions
{
    public static ISqlezeBuilder OpenBuilder(this Startup startup, IConfiguration configuration, string configKey = "DefaultConnection")
    {
        return startup.OpenBuilder().WithConfigKey(configKey).WithConfiguration(configuration);
    }

    public static ISqleze Build(this Startup startup, IConfiguration configuration, string configKey = "DefaultConnection")
    {
        return startup.OpenBuilder(configuration, configKey).Build();
    }

    public static ISqlezeConnection Connect(this Startup startup, IConfiguration configuration, string configKey = "DefaultConnection")
    {
        return startup.Build(configuration, configKey).Connect();
    }
}
