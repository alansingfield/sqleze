using Sqleze.ConnectionStrings;
using Sqleze;
using Sqleze.DryIoc;

namespace Sqleze.Registration;

    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
    static class ConnectionStringRegistrationExtensions
{
    public static void RegisterSqlezeConnectionStringProviders(this IRegistrator registrator)
    {
        // Fallback to using ConnectionString in appsettings.json
        registrator.Register<IConnectionStringProvider, FallbackConnectionStringProvider>(
            Reuse.Scoped);

        registrator.RegisterSqlezeConfigConnectionStringProvider();
        registrator.RegisterSqlezeVerbatimConnectionStringProvider();
    }

    public static void RegisterSqlezeVerbatimConnectionStringProvider(this IRegistrator registrator)
    {
        registrator.Register<IVerbatimConnectionStringProvider, VerbatimConnectionStringProvider>(
            Reuse.Scoped);

        registrator.Register<IConnectionStringProvider, VerbatimConnectionStringProvider>(
            Reuse.Scoped,
            setup: Setup.With(
                asResolutionCall: true,
                condition: Condition.ScopedToGenericArg<VerbatimConnectionRoot, ConnectionRoot>()));

        registrator.Register<VerbatimConnectionRoot>();

        registrator.Register<VerbatimConnectionOptions>(Reuse.ScopedToService<IScopedSqlezeConnectionBuilder<VerbatimConnectionRoot>>());
    }

    public static void RegisterSqlezeConfigConnectionStringProvider(this IRegistrator registrator)
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
