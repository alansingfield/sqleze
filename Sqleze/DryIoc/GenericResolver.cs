using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.DryIoc
{
    public interface IGenericResolver<TService> 
        where TService : notnull
    {
        TService Resolve(Type extraType, object?[]? args = null);
    }

    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
    class GenericResolver<TService> : IGenericResolver<TService>
        where TService: notnull
    {
        private readonly IResolverContext resolverContext;
        private IGenericPromoter? genericPromoter;

        public GenericResolver(IResolverContext scope)
        {
            this.resolverContext = scope;
        }

        public TService Resolve(Type extraType, object?[]? args = null)
        {
            resolvePromoter();

            // Append the extra type onto our type so IService<A> becomes IService<A, int>
            var promotedGeneric = genericPromoter.Promote(typeof(TService), extraType);

            return (TService)resolverContext.Resolve(promotedGeneric, args);
        }

        [MemberNotNull(nameof(genericPromoter))]
        private void resolvePromoter()
        {
            // Already done the work??
            if(this.genericPromoter != null)
                return;

            // RegisterGenericPromotion() will have registered the IGenericPromoter keyed
            // by the open generic type of the service.
            var serviceKey = typeof(TService);

            if(serviceKey.IsClosedGeneric())
                serviceKey = serviceKey.GetGenericTypeDefinition();

            this.genericPromoter = resolverContext.Resolve<IGenericPromoter>(serviceKey: serviceKey);
        }
    }

    public static class GenericResolverExtensions
    {
        public static TResult Resolve<TService, TResult>(
            this IGenericResolver<TService> genericResolver,
            Type extraType,
            object?[]? args = null) 
                where TResult : TService
                where TService : notnull
        {
            return (TResult)genericResolver.Resolve(extraType, args);
        }
    }
}
