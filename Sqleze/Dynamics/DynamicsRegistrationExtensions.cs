using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Dynamics;

namespace Sqleze.Registration;
public static class DynamicsRegistrationExtensions
{
    public static void RegisterDynamicPropertyCaller(this IRegistrator registrator)
    {
        registrator.Register(typeof(IDynamicPropertyCaller<>), typeof(DynamicPropertyCaller<>), Reuse.Singleton);
    }

    public static void RegisterConstructorCache(this IRegistrator registrator)
    {
        registrator.Register(typeof(IConstructorCache<>), typeof(ConstructorCache<>), Reuse.Singleton);
    }

}
