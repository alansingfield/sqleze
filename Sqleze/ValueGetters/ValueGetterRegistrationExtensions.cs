using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration;

public static class ValueGetterRegistrationExtensions
{
    public static void RegisterValueGetters(this IRegistrator registrator)
    {
        registrator.Register(typeof(IReaderGetValue<>), typeof(ReaderGetValue<>), Reuse.Singleton);

        registrator.Register<IKnownSqlDbTypeResolver, KnownSqlDbTypeResolver>(Reuse.Singleton);
        registrator.Register<IKnownSqlDbTypeFinder, KnownSqlDbTypeFinder>(Reuse.Singleton);

        registrator.Register<IReaderGetValue<SqlBinary>,    ReaderGetValueSqlBinary>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlBoolean>,   ReaderGetValueSqlBoolean>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlByte>,      ReaderGetValueSqlByte>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlBytes>,     ReaderGetValueSqlBytes>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlChars>,     ReaderGetValueSqlChars>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlDateTime>,  ReaderGetValueSqlDateTime>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlDecimal>,   ReaderGetValueSqlDecimal>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlDouble>,    ReaderGetValueSqlDouble>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlGuid>,      ReaderGetValueSqlGuid>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlInt16>,     ReaderGetValueSqlInt16>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlInt32>,     ReaderGetValueSqlInt32>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlInt64>,     ReaderGetValueSqlInt64>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlMoney>,     ReaderGetValueSqlMoney>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlSingle>,    ReaderGetValueSqlSingle>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlString>,    ReaderGetValueSqlString>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlXml>,       ReaderGetValueSqlXml>(Reuse.Singleton);

        registrator.Register<IReaderGetValue<SqlBinary?>,    ReaderGetValueSqlBinaryNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlBoolean?>,   ReaderGetValueSqlBooleanNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlByte?>,      ReaderGetValueSqlByteNullable>(Reuse.Singleton);
//        registrator.Register<IReaderGetValue<SqlBytes?>,     ReaderGetValueSqlBytesNullable>(Reuse.Singleton);
//        registrator.Register<IReaderGetValue<SqlChars?>,     ReaderGetValueSqlCharsNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlDateTime?>,  ReaderGetValueSqlDateTimeNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlDecimal?>,   ReaderGetValueSqlDecimalNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlDouble?>,    ReaderGetValueSqlDoubleNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlGuid?>,      ReaderGetValueSqlGuidNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlInt16?>,     ReaderGetValueSqlInt16Nullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlInt32?>,     ReaderGetValueSqlInt32Nullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlInt64?>,     ReaderGetValueSqlInt64Nullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlMoney?>,     ReaderGetValueSqlMoneyNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlSingle?>,    ReaderGetValueSqlSingleNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<SqlString?>,    ReaderGetValueSqlStringNullable>(Reuse.Singleton);
        //registrator.Register<IReaderGetValue<SqlXml?>,       ReaderGetValueSqlXmlNullable>(Reuse.Singleton);

        registrator.Register<IReaderGetValue<DateOnly>,      ReaderGetValueDateOnly>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<DateOnly?>,     ReaderGetValueDateOnlyNullable>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<TimeOnly>,      ReaderGetValueTimeOnly>(Reuse.Singleton);
        registrator.Register<IReaderGetValue<TimeOnly?>,     ReaderGetValueTimeOnlyNullable>(Reuse.Singleton);



    }
}
