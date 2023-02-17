using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Composition;

public class ScopedSqlezeConnectionBuilder<TConfigRoot> : IScopedSqlezeConnectionBuilder<TConfigRoot>
{
    private readonly ISqlezeScope sqlezeScope;
    private readonly TConfigRoot configRoot;
    private readonly Func<ISqlezeBuilder> newSqlezeConnectionBuilder;

    public ScopedSqlezeConnectionBuilder(
        ISqlezeScope sqlezeScope,
        TConfigRoot configRoot,
        Func<ISqlezeBuilder> newSqlezeConnectionBuilder)
    {
        this.sqlezeScope = sqlezeScope;
        this.configRoot = configRoot;
        this.newSqlezeConnectionBuilder = newSqlezeConnectionBuilder;
    }

    public ISqlezeBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure)
    {
        configure?.Invoke(configRoot, sqlezeScope);

        return newSqlezeConnectionBuilder();
    }
}
