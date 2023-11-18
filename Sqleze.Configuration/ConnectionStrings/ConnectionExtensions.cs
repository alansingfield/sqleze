using Microsoft.Extensions.Configuration;
using Sqleze.ConnectionStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    scope.Use(new ConfigConnectionOptions(configKey, null));
                });
        }
        
        /// <summary>
        /// Specify which key to use within the ConnectionStrings section of your options json
        /// and key for the password held separately (secrets.json)
        /// </summary>
        /// <param name="sqlezeConnectionBuilder"></param>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public static ISqlezeBuilder WithConfigKey(
            this ISqlezeBuilder sqlezeConnectionBuilder,
            string configKey,
            string passwordKey
            )
        {
            return sqlezeConnectionBuilder.With<ConfigConnectionRoot>(
                (root, scope) =>
                {
                    scope.Use(new ConfigConnectionOptions(configKey, passwordKey));
                });
        }
    }

}

namespace Sqleze
{
    public class ConfigConnectionRoot : ConnectionRoot { }
}