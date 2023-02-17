using Sqleze.DryIoc;
using Sqleze;
using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public class ScopedSqlezeRowsetFactory : IScopedSqlezeRowsetFactory
{
    private readonly IGenericResolver<ISqlezeRowsetProvider> sqlezeRowsetProviderResolver;

    public ScopedSqlezeRowsetFactory(
        IGenericResolver<ISqlezeRowsetProvider> sqlezeRowsetProviderResolver)
    {
        this.sqlezeRowsetProviderResolver = sqlezeRowsetProviderResolver;
    }

    public ISqlezeRowset<T> OpenRowsetNullable<T>()
    {
        return sqlezeRowsetProviderResolver
            .Resolve<ISqlezeRowsetProvider, ISqlezeRowsetProvider<T>>(typeof(T))
            .SqlezeRowset;
    }
}


