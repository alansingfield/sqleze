using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public interface INamingConvention
{
    string DotNetToSql(string arg);
    string SqlToDotNet(string arg);
}
