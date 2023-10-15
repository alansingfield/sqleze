using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon.DependencyInjection;
public static class DI
{
    #if !DRYIOC_ABSENT

    #if DRYIOC_DLL
    public 
    #else
    internal
    #endif
    static IContainer NewContainer()
    {
        var rules = Rules.Default;

        #if! DRYIOC_DLL
        // Allow use of internal constructors
        rules = rules.With(FactoryMethod.ConstructorWithResolvableArgumentsIncludingNonPublic);
        #endif

        return new Container(rules);
    }

    #endif
}
