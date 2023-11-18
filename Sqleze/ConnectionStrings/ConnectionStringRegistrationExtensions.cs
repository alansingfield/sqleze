using Sqleze.ConnectionStrings;
using Sqleze;
using Sqleze.DryIoc;

namespace Sqleze.Registration;

public static class ConnectionStringRegistrationExtensions
{
    public static void RegisterSqlezeConnectionStringProviders(this IRegistrator registrator)
    {
        // If no connection string provided, raise an error.
        registrator.Register<IConnectionStringProvider, FallbackConnectionStringProvider>(
            Reuse.Scoped);

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


}
