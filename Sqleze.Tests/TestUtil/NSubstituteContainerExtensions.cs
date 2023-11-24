using NSubstitute;
using System.Collections.Concurrent;

namespace TestCommon.TestUtil;

public static class NSubstituteContainerExtensions
{
    /// <summary>
    /// Configures the container to create any unregistered types through NSubstitute.
    /// </summary>
    /// <param name="container"></param>
    /// <param name="reuse">Default is Reuse.ScopedOrSingleton</param>
    /// <returns></returns>
    public static IContainer WithNSubstituteFallback(this IContainer container, IReuse? reuse = null)
    {
        // See: https://github.com/dadhi/DryIoc/blob/master/docs/DryIoc.Docs/UsingInTestsWithMockingLibrary.md
        var dict = new ConcurrentDictionary<Type, DynamicRegistration>();

        return container.With(rules => rules.WithDynamicRegistration(

            (serviceType, serviceKey) =>
            {
                if(!serviceType.IsAbstract) // Mock interface or abstract class only.
                    return null;

                if(serviceType.IsOpenGeneric())
                    return null;

                var registration = dict.GetOrAdd(
                    serviceType,
                    type => new DynamicRegistration(
                        DelegateFactory.Of(r =>
                            Substitute.For(new[] { serviceType }, null),
                            reuse ?? Reuse.ScopedOrSingleton)));

                return new[] { registration };
            },

            DynamicRegistrationFlags.Service | DynamicRegistrationFlags.AsFallback)
        );
    }
}
