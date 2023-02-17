using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Params;

namespace Sqleze.Registration;

public static class ValuePrecisionScaleMeasurerRegistrationExtensions
{
    public static void RegisterValuePrecisionScaleMeasurers(this IRegistrator registrator)
    {
        // Fallback is to use a precision/scale measurer which returns null.
        registrator.Register(typeof(IValuePrecisionScaleMeasurer<>), typeof(ValuePrecisionScaleMeasurerDefault<>), Reuse.Singleton);

        registrator.Register<IValuePrecisionScaleMeasurer<decimal?>, ValuePrecisionScaleMeasurerDecimalNullable>(Reuse.Singleton);
        registrator.Register<IValuePrecisionScaleMeasurer<decimal>, ValuePrecisionScaleMeasurerDecimal>(Reuse.Singleton);
        registrator.Register<IValuePrecisionScaleMeasurer<SqlDecimal?>, ValuePrecisionScaleMeasurerSqlDecimalNullable>(Reuse.Singleton);
        registrator.Register<IValuePrecisionScaleMeasurer<SqlDecimal>, ValuePrecisionScaleMeasurerSqlDecimal>(Reuse.Singleton);
    }
}
