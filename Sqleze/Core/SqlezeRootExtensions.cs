using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class SqlezeRootExtensions
{
    public static ISqlezeConnection Connect(this ISqlezeRoot sqlezeRoot, string connectionString)
    {
        return sqlezeRoot.Factory(connectionString).Connect();
    }
}
