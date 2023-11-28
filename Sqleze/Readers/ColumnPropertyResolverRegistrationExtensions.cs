using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Readers;

namespace Sqleze.Registration;

public static class ColumnPropertyResolverRegistrationExtensions
{
    public static void RegisterColumnPropertyResolver(this IRegistrator registrator)
    {
        registrator.Register(typeof(IColumnPropertyResolver<,>), typeof(ColumnPropertyResolver<,>), Reuse.Transient);
        registrator.RegisterGenericPromotion(typeof(IColumnPropertyResolver<>), typeof(IColumnPropertyResolver<,>));
    }
}
