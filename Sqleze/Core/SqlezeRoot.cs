using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public static class SqlezeRoot
{
    /// <summary>
    /// Opens the configuration chain for customising your SQLEZE connection factory.
    /// </summary>
    /// <returns></returns>
    public static ISqlezeBuilder OpenBuilder()
    {
        var container = new Container();

        container.RegisterSqleze();

        return container.Resolve<ISqlezeBuilder>();
    }

    /// <summary>
    /// Open a SQLEZE database connection with the default options. You must dispose when complete.
    /// </summary>
    /// <returns></returns>
    public static ISqlezeConnection Connect(string connectionString)
    {
        return OpenBuilder()
            .WithConnectionString(connectionString)
            .Connect();
    }
}
