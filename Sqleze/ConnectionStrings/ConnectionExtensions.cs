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
}