using Sqleze.Params;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration;

public static class ValueSizeMeasurerRegistrationExtensions
{
    public static void RegisterValueSizeMeasurers(this IRegistrator registrator)
    {
        // Fallback is to use a value size measurer which returns null.
        registrator.Register(typeof(IValueSizeMeasurer<>), typeof(ValueSizeMeasurerDefault<>), Reuse.Singleton);

        registrator.Register<IValueSizeMeasurer<string?>, ValueSizeMeasurerStringNullable>(Reuse.Singleton);
        registrator.Register<IValueSizeMeasurer<byte[]?>, ValueSizeMeasurerByteArrayNullable>(Reuse.Singleton);
        registrator.Register<IValueSizeMeasurer<SqlString?>, ValueSizeMeasurerSqlStringNullable>(Reuse.Singleton);
        registrator.Register<IValueSizeMeasurer<SqlString>, ValueSizeMeasurerSqlString>(Reuse.Singleton);
        registrator.Register<IValueSizeMeasurer<SqlBinary?>, ValueSizeMeasurerSqlBinaryNullable>(Reuse.Singleton);
        registrator.Register<IValueSizeMeasurer<SqlBinary>, ValueSizeMeasurerSqlBinary>(Reuse.Singleton);
        registrator.Register<IValueSizeMeasurer<SqlBytes?>, ValueSizeMeasurerSqlBytes>(Reuse.Singleton);
        registrator.Register<IValueSizeMeasurer<SqlChars?>, ValueSizeMeasurerSqlChars>(Reuse.Singleton);
    }
}
