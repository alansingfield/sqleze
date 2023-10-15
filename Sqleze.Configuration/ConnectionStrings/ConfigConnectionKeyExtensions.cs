using Sqleze.ConnectionStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class ConfigConnectionKeyExtensions
{
    /// <summary>
    /// Specify which key to use within the ConnectionStrings section of your options json
    /// </summary>
    /// <param name="sqlezeConnectionBuilder"></param>
    /// <param name="configKey"></param>
    /// <returns></returns>
    public static ISqlezeBuilder WithConfigKey(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        string configKey
        )
    {
        return sqlezeConnectionBuilder.With<ConfigConnectionRoot>(
            (root, scope) =>
            {
                scope.Use(new ConfigConnectionOptions() { ConnectionKey = configKey });
            });
    }

    /// <summary>
    /// Specify which key to use within the ConnectionStrings section of your options json
    /// </summary>
    /// <param name="sqlezeConnectionBuilder"></param>
    /// <param name="configKey"></param>
    /// <returns></returns>
    public static ISqlezeBuilder WithConfigKey(
        this ISqleze sqleze,
        string configKey
        )
    {
        return sqleze.Reconfigure().WithConfigKey(configKey);
    }
}
