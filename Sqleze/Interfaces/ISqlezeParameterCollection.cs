using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public interface ISqlezeParameterCollection : 
    IReadOnlyCollection<ISqlezeParameter>
{
    ISqlezeParameter<T> AddOrReplace<T>(string parameterName,
        IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null);

    ISqlezeParameterBuilder With<T>(Action<T, ISqlezeScope> configure);

    bool TryGet(
        string parameterName, 
        [NotNullWhen(true)] out ISqlezeParameter? sqlezeParameter);

    bool Remove(string parameterName);

    void Clear();

    ISqlezeCommand Command { get; }
}
