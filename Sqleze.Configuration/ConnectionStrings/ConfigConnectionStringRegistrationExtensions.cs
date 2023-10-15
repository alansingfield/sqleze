using Sqleze.ConnectionStrings;
using Sqleze;
using Sqleze.DryIoc;
using DryIoc;

namespace Sqleze.Configuration.ConnectionStrings;
public static class ConfigConnectionStringRegistrationExtensions
{
    #if DRYIOC_DLL
    public 
    #else
    internal
    #endif
    static void RegisterSqlezeConfigConnectionStringProvider(this IRegistrator registrator)
    {
        registrator.Register<IConfigConnectionStringProvider, ConfigConnectionStringProvider>(
            Reuse.Scoped);

        registrator.Register<IConnectionStringProvider, ConfigConnectionStringProvider>(
            Reuse.Scoped,
            setup: Setup.With(
                asResolutionCall: true,
                condition: Condition.ScopedToGenericArg<ConfigConnectionRoot, ConnectionRoot>()));

        registrator.Register<ConfigConnectionRoot>();

        registrator.Register<ConfigConnectionOptions>(Reuse.ScopedToService<IScopedSqlezeConnectionBuilder<ConfigConnectionRoot>>());
    }
}
