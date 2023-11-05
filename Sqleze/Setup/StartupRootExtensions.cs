using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class StartupRootExtensions
{
    public static ISqlezeBuilder OpenBuilder(this Startup startup)
    {
        return startup.Root.OpenBuilder();
    }

    public static ISqlezeBuilder OpenBuilder(this Startup startup, string connectionString)
    {
        return startup.OpenBuilder().WithConnectionString(connectionString);
    }
    public static ISqleze Build(this Startup startup, string connectionString)
    {
        return startup.OpenBuilder(connectionString).Build();
    }

    public static ISqlezeConnection Connect(this Startup startup, string connectionString)
    {
        return startup.Build(connectionString).Connect();
    }
}
