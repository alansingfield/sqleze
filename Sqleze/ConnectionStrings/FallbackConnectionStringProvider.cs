using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ConnectionStrings;
public class FallbackConnectionStringProvider : IConnectionStringProvider
{
    public FallbackConnectionStringProvider()
    {
    }

    public string GetConnectionString()
    {
        throw new Exception("No ConnectionString can be found. Use the WithConnectionString() method on the ISqlezeBuilder class to provide this");
    }
}
