using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ConnectionStrings
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ConnectionKey">Key within ConnectionStrings section for SQL server connection string</param>
    /// <param name="PasswordKey">Key within configuration for password (overrides any password in ConnectionStrings)</param>
    public record ConfigConnectionOptions
    (
        string ConnectionKey,
        string? PasswordKey
    );
}
