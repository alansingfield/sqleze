using Microsoft.Extensions.Configuration;
using Sqleze.ConnectionStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public static class ConfigConnectionExtensions
    {
        /// <summary>
        /// Specify which key to use within the ConnectionStrings section of your options json
        /// </summary>
        /// <param name="sqlezeConnectionBuilder"></param>
        /// <param name="connectionKey"></param>
        /// <returns></returns>
        public static ISqlezeBuilder WithConfigKey(
            this ISqlezeBuilder sqlezeConnectionBuilder,
            string connectionKey
            )
        {
            return sqlezeConnectionBuilder.With<ConfigConnectionRoot>(
                (root, scope) =>
                {
                    scope.Use(new ConfigConnectionOptions(connectionKey, null));
                });
        }
        
        /// <summary>
        /// Specify which key to use within the ConnectionStrings section of your options json
        /// and key for the password held separately (e.g. via secrets.json)
        /// </summary>
        /// <param name="sqlezeConnectionBuilder"></param>
        /// <param name="connectionKey"></param>
        /// <returns></returns>
        public static ISqlezeBuilder WithConfigKey(
            this ISqlezeBuilder sqlezeConnectionBuilder,
            string connectionKey,
            string passwordKey
            )
        {
            return sqlezeConnectionBuilder.With<ConfigConnectionRoot>(
                (root, scope) =>
                {
                    scope.Use(new ConfigConnectionOptions(connectionKey, passwordKey));
                });
        }
    }

}

namespace Sqleze
{
    public class ConfigConnectionRoot : ConnectionRoot { }
}