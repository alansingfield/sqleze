using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.DryIoc;

namespace Sqleze.Registration;

public static class DryIocResolverRegistrationExtensions
{
    
    public static void RegisterMultiResolver(this IRegistrator registrator)
    {
        registrator.Register(typeof(IMultiResolver<,>), typeof(MultiResolver<,>));
    }

    public static void RegisterGenericResolver(this IRegistrator registrator)
    {
        registrator.Register(typeof(IGenericResolver<>), typeof(GenericResolver<>));
    }
}
