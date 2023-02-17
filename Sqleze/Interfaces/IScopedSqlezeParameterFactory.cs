using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public interface IScopedSqlezeParameterFactory
    {
        ISqlezeParameterProvider<T> Create<T>(string parameterName);
        ISqlezeCommand Command { get; }
    }
}
