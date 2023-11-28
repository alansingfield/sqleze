using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Readers;

namespace Sqleze.Registration;

public static class ColumnPropertyRegistrationExtensions
{
    public static void RegisterColumnProperty(this IRegistrator registrator)
    {
        registrator.Register(typeof(IColumnProperty<,,>), typeof(ColumnProperty<,,>), Reuse.Transient);
        registrator.RegisterGenericPromotion(typeof(IColumnProperty<,>), typeof(IColumnProperty<,,>));
    }
}
