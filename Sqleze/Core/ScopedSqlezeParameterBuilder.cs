using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Composition;

public class ScopedSqlezeParameterBuilder<TConfigRoot> : IScopedSqlezeParameterBuilder<TConfigRoot>
{
    private readonly ISqlezeScope sqlezeScope;
    private readonly TConfigRoot configRoot;
    private readonly Func<ISqlezeParameterBuilder> newSqlezeParameterBuilder;

    public ScopedSqlezeParameterBuilder(
        ISqlezeScope sqlezeScope,
        TConfigRoot configRoot,
        Func<ISqlezeParameterBuilder> newSqlezeParameterBuilder)
    {
        this.sqlezeScope = sqlezeScope;
        this.configRoot = configRoot;
        this.newSqlezeParameterBuilder = newSqlezeParameterBuilder;
    }

    public ISqlezeParameterBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure)
    {
        configure?.Invoke(configRoot, sqlezeScope);

        return newSqlezeParameterBuilder();
    }
}
