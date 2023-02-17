using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Composition;

public class ScopedSqlezeRowsetBuilder<TConfigRoot> : IScopedSqlezeRowsetBuilder<TConfigRoot>
{
    private readonly ISqlezeScope sqlezeScope;
    private readonly TConfigRoot configRoot;
    private readonly Func<ISqlezeRowsetBuilder> newSqlezeRowsetFactory;

    public ScopedSqlezeRowsetBuilder(
        ISqlezeScope sqlezeScope,
        TConfigRoot configRoot,
        Func<ISqlezeRowsetBuilder> newSqlezeRowsetFactory)
    {
        this.sqlezeScope = sqlezeScope;
        this.configRoot = configRoot;
        this.newSqlezeRowsetFactory = newSqlezeRowsetFactory;
    }

    public ISqlezeRowsetBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure)
    {
        configure?.Invoke(configRoot, sqlezeScope);

        return newSqlezeRowsetFactory();
    }
}
