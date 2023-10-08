using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.DryIoc
{
    public interface IMultiResolver<TService, TElement>
        where TService : notnull
    {
        IEnumerable<TService> ResolvePerProperty(Func<PropertyInfo, (bool, object?[]?)>? filter = null);
    }

    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
    class MultiResolver<TService, TElement> : IMultiResolver<TService, TElement>
        where TService : notnull
    {
        private readonly IResolverContext scope;
        private readonly IGenericResolver<TService> genericResolver;

        public MultiResolver(IResolverContext scope,
            IGenericResolver<TService> genericResolver)
        {
            this.scope = scope;
            this.genericResolver = genericResolver;
        }

        public IEnumerable<TService> ResolvePerProperty(
            Func<PropertyInfo, (bool, object?[]?)>? filter = null)
        {
            // For each property in our element type
            foreach(var propertyInfo in typeof(TElement).GetProperties())
            {
                bool required = true;
                object?[]? argValues = null;

                if(filter != null)
                {
                    (required, argValues) = filter(propertyInfo);
                }

                if(required)
                {
                    // Resolve with extra type, pass args and return.
                    yield return genericResolver.Resolve(propertyInfo.PropertyType, argValues);
                }
            }
        }
    }
}
