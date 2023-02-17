using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze;
using Sqleze.Composition;
using Sqleze.ConnectionStrings;

namespace Sqleze
{
    public static class ConnectionExtensions
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
        /// Specify connection string directly (not via config)
        /// </summary>
        /// <param name="sqlezeConnectionBuilder"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ISqlezeBuilder WithConnectionString(
            this ISqlezeBuilder sqlezeConnectionBuilder,
            string connectionString
            )
        {
            return sqlezeConnectionBuilder.With<VerbatimConnectionRoot>(
                (root, scope) =>
                {
                    scope.Use(new VerbatimConnectionOptions() { ConnectionString = connectionString });
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

        /// <summary>
        /// Specify connection string directly (not via config)
        /// </summary>
        /// <param name="sqlezeConnectionBuilder"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ISqlezeBuilder WithConnectionString(
            this ISqleze sqleze,
            string connectionString
            )
        {
            return sqleze.Reconfigure().WithConnectionString(connectionString);
        }
    }
}

namespace Sqleze
{
    public class ConnectionRoot { }
    public class VerbatimConnectionRoot : ConnectionRoot { }
    public class ConfigConnectionRoot : ConnectionRoot { }
}