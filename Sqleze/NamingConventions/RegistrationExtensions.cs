using Sqleze.DryIoc;
using Sqleze;
using Sqleze.NamingConventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration;

public static class NamingConventionRegistrationExtensions
{
    public static void RegisterSqlezeNamingConventions(this IRegistrator registrator)
    {
        // NeutralNamingConvention is the default naming convention
        registrator.Register<INamingConvention, NeutralNamingConvention>(
            Reuse.Scoped);

        registrator.RegisterSqlezeNeutralNamingConvention();
        registrator.RegisterSqlezeCamelUnderscoreNamingConvention();
    }

    public static void RegisterSqlezeNeutralNamingConvention(this IRegistrator registrator)
    {
        registrator.Register<INeutralNamingConvention, NeutralNamingConvention>(
            Reuse.Scoped);

        registrator.Register<INamingConvention, NeutralNamingConvention>(
            Reuse.Scoped,
            setup: Setup.With(
                asResolutionCall: true,
                condition: Condition.ScopedToGenericArg<NeutralNamingConventionRoot, NamingConventionCategory>()));

        registrator.Register<NeutralNamingConventionRoot>();
    }

    public static void RegisterSqlezeCamelUnderscoreNamingConvention(this IRegistrator registrator)
    {
        registrator.Register<ICamelUnderscoreNamingConvention, CamelUnderscoreNamingConvention>(
            Reuse.Scoped);

        registrator.Register<INamingConvention, CamelUnderscoreNamingConvention>(
            Reuse.Scoped,
            setup: Setup.With(
                asResolutionCall: true,
                condition: Condition.ScopedToGenericArg<CamelUnderscoreNamingConventionRoot, NamingConventionCategory>()));

        registrator.Register<CamelUnderscoreNamingConventionRoot>();
    }
}
