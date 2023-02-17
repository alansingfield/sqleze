using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.NamingConventions;

public interface INeutralNamingConvention : INamingConvention { }

public class NeutralNamingConvention : INeutralNamingConvention
{
    public string DotNetToSql(string arg) => arg;
    public string SqlToDotNet(string arg) => arg;
}