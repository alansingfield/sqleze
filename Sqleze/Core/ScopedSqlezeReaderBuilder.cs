using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Composition;

public class ScopedSqlezeReaderBuilder<TConfigRoot> : IScopedSqlezeReaderBuilder<TConfigRoot>
{
    private readonly ISqlezeScope sqlezeScope;
    private readonly TConfigRoot configRoot;
    private readonly Func<ISqlezeReaderBuilder> newSqlezeReaderFactory;

    public ScopedSqlezeReaderBuilder(
        ISqlezeScope sqlezeScope,
        TConfigRoot configRoot,
        Func<ISqlezeReaderBuilder> newSqlezeReaderFactory)
    {
        this.sqlezeScope = sqlezeScope;
        this.configRoot = configRoot;
        this.newSqlezeReaderFactory = newSqlezeReaderFactory;
    }

    public ISqlezeReaderBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure)
    {
        configure?.Invoke(configRoot, sqlezeScope);

        return newSqlezeReaderFactory();
    }
}
