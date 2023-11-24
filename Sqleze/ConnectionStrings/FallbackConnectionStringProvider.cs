using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ConnectionStrings;
public class FallbackConnectionStringProvider : IConnectionStringProvider
{
    public string GetConnectionString()
    {
        throw new Exception("No connection string specified");
    }
}
