using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Composition;

public class ScopedSqlezeCommandBuilder<TConfigRoot> : IScopedSqlezeCommandBuilder<TConfigRoot>
{
    private readonly ISqlezeScope sqlezeScope;
    private readonly TConfigRoot configRoot;
    private readonly Func<ISqlezeCommandBuilder> newSqlezeCommandBuilder;

    public ScopedSqlezeCommandBuilder(
        ISqlezeScope sqlezeScope,
        TConfigRoot configRoot,
        Func<ISqlezeCommandBuilder> newSqlezeCommandBuilder)
    {
        this.sqlezeScope = sqlezeScope;
        this.configRoot = configRoot;
        this.newSqlezeCommandBuilder = newSqlezeCommandBuilder;
    }

    public ISqlezeCommandBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure)
    {
        configure?.Invoke(configRoot, sqlezeScope);

        return newSqlezeCommandBuilder();
    }
}
