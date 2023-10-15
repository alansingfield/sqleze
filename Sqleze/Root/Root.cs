
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class SqlezeRoot
{
    private static readonly IContainer container;
    private static readonly Lazy<ISqlezeBuilder> lazySqlezeBuilder;

    static SqlezeRoot()
    {
        var rules = Rules.Default;

        #if !DRYIOC_DLL
        // Override the rules to allow using internal constructors.
        rules = rules.With(FactoryMethod.ConstructorWithResolvableArgumentsIncludingNonPublic);
        #endif

        container = new Container(rules);
        container.RegisterSqleze();

        lazySqlezeBuilder = new Lazy<ISqlezeBuilder>(
            () => registerAndResolve());
    }

    internal static void ModuleHandshake(Action<IRegistrator> registrator)
    {
        if(lazySqlezeBuilder.IsValueCreated)
            throw new Exception("module handshake was too late");

        registrator(container);
    }

    private static ISqlezeBuilder registerAndResolve()
    {
        return container.Resolve<ISqlezeBuilder>();
    }

    public static ISqlezeBuilder Builder => lazySqlezeBuilder.Value;
}
