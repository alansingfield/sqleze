
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class Root
{
    private static readonly IContainer container;
    private static readonly ISqlezeRoot sqlezeRoot;

    static Root()
    {
        var rules = Rules.Default;

        #if !DRYIOC_DLL
        // Override the rules to allow using internal constructors.
        rules = rules.With(FactoryMethod.ConstructorWithResolvableArgumentsIncludingNonPublic);
        #endif

        container = new Container(rules);
        container.RegisterSqleze();

        sqlezeRoot = container.Resolve<ISqlezeRoot>();
    }

    public static ISqlezeBuilder Builder => sqlezeRoot.Builder;

    /// <summary>
    /// 
    /// Note that this caches the factory by connectionString, so if you are calling this with many
    /// different values over time it may be better to use Sqleze.Root.Builder.WithConnectionString().Build()
    /// instead.
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static ISqleze Factory(string connectionString) => sqlezeRoot.Factory(connectionString);

    public static ISqlezeConnection Connect(string connectionString) => sqlezeRoot.Connect(connectionString);
}
