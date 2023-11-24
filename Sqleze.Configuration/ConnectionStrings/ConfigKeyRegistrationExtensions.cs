using DryIoc;
using Sqleze.Configuration;
using Sqleze.DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ConnectionStrings;
public static class ConfigKeyRegistrationExtensions
{
    public static void RegisterSqlezeConfigKey(this IRegistrator registrator)
    {
        registrator.Register<IConfigConnectionStringProvider, ConfigConnectionStringProvider>(
            Reuse.Scoped);

        registrator.Register<IConnectionStringProvider, ConfigConnectionStringProvider>(
            Reuse.Scoped,
            setup: Setup.With(
                asResolutionCall: true,
                condition: Condition.ScopedToGenericArg<ConfigConnectionRoot, ConnectionRoot>()));

        registrator.Register<ConfigConnectionRoot>();
        registrator.Register<ConfigurationRoot>();

        registrator.Register<ConfigConnectionOptions>(Reuse.ScopedToService<IScopedSqlezeConnectionBuilder<ConfigConnectionRoot>>());
    }

}
