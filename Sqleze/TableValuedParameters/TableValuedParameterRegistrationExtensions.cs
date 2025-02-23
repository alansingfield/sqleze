using Sqleze.DryIoc;
using Sqleze.TableValuedParameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration;

public static class TableValuedParameterRegistrationExtensions
{
    public static void RegisterRecordValueSetters(this IRegistrator registrator)
    {
        // Use this RecordSetValue for normal scalar types, plus strings and byte arrays.
        registrator.Register(
            typeof(IRecordSetValue<>),
            typeof(RecordSetValue<>),
            Reuse.Singleton,
            setup: Setup.With(condition: Condition.GenericArgIs(
                x => !x.IsAssignableTo<IEnumerable>()
                    || x == typeof(string)
                    || x == typeof(byte[])
                    || x == typeof(byte?[])
                )));

        registrator.Register<IRecordSetValue<SqlBinary>, RecordSetSqlValueSqlBinary>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlBoolean>, RecordSetSqlValueSqlBoolean>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlByte>, RecordSetSqlValueSqlByte>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlBytes>, RecordSetSqlValueSqlBytes>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlChars>, RecordSetSqlValueSqlChars>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlDateTime>, RecordSetSqlValueSqlDateTime>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlDecimal>, RecordSetSqlValueSqlDecimal>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlDouble>, RecordSetSqlValueSqlDouble>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlGuid>, RecordSetSqlValueSqlGuid>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlInt16>, RecordSetSqlValueSqlInt16>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlInt32>, RecordSetSqlValueSqlInt32>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlInt64>, RecordSetSqlValueSqlInt64>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlMoney>, RecordSetSqlValueSqlMoney>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlSingle>, RecordSetSqlValueSqlSingle>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlString>, RecordSetSqlValueSqlString>(Reuse.Singleton);
        registrator.Register<IRecordSetValue<SqlXml>, RecordSetSqlValueSqlXml>(Reuse.Singleton);
    }


    public static void RegisterSqlDataRecordWriterFromProperty(this IRegistrator registrator)
    {
        registrator.RegisterGenericPromotion(typeof(ISqlDataRecordWriterFromProperty<>), typeof(ISqlDataRecordWriterFromProperty<,>));
        registrator.Register(typeof(ISqlDataRecordWriterFromProperty<,>), typeof(SqlDataRecordWriterFromProperty<,>));
    }


    public static void RegisterSqlDataRecordMappers(this IRegistrator registrator)
    {
        // By default, map each property in an object to a separate field
        registrator.Register(typeof(ISqlDataRecordMapper<>), typeof(SqlDataRecordPropertyMapper<>), Reuse.ScopedOrSingleton);

        // For scalars, map to the type itself.
        registrator.Register<ISqlDataRecordMapper<bool>,            SqlDataRecordScalarMapper<bool>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<bool?>,           SqlDataRecordScalarMapper<bool?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<byte>,            SqlDataRecordScalarMapper<byte>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<byte?>,           SqlDataRecordScalarMapper<byte?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<byte[]?>,         SqlDataRecordScalarMapper<byte[]?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<DateTime>,        SqlDataRecordScalarMapper<DateTime>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<DateTime?>,       SqlDataRecordScalarMapper<DateTime?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<decimal>,         SqlDataRecordScalarMapper<decimal>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<decimal?>,        SqlDataRecordScalarMapper<decimal?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<double>,          SqlDataRecordScalarMapper<double>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<double?>,         SqlDataRecordScalarMapper<double?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<float>,           SqlDataRecordScalarMapper<float>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<float?>,          SqlDataRecordScalarMapper<float?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<int>,             SqlDataRecordScalarMapper<int>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<int?>,            SqlDataRecordScalarMapper<int?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<long>,            SqlDataRecordScalarMapper<long>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<long?>,           SqlDataRecordScalarMapper<long?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<object?>,         SqlDataRecordScalarMapper<object?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<short>,           SqlDataRecordScalarMapper<short>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<short?>,          SqlDataRecordScalarMapper<short?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<string?>,         SqlDataRecordScalarMapper<string?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<DateTimeOffset>,  SqlDataRecordScalarMapper<DateTimeOffset>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<DateTimeOffset?>, SqlDataRecordScalarMapper<DateTimeOffset?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<Guid>,            SqlDataRecordScalarMapper<Guid>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<Guid?>,           SqlDataRecordScalarMapper<Guid?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<TimeSpan>,        SqlDataRecordScalarMapper<TimeSpan>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<TimeSpan?>,       SqlDataRecordScalarMapper<TimeSpan?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<DateOnly>,        SqlDataRecordScalarMapper<DateOnly>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<DateOnly?>,       SqlDataRecordScalarMapper<DateOnly?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<TimeOnly>,        SqlDataRecordScalarMapper<TimeOnly>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<TimeOnly?>,       SqlDataRecordScalarMapper<TimeOnly?>>(Reuse.Singleton);


        registrator.Register<ISqlDataRecordMapper<SqlBinary>,       SqlDataRecordScalarMapper<SqlBinary>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlBinary?>,      SqlDataRecordScalarMapper<SqlBinary?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlBoolean>,      SqlDataRecordScalarMapper<SqlBoolean>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlBoolean?>,     SqlDataRecordScalarMapper<SqlBoolean?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlByte>,         SqlDataRecordScalarMapper<SqlByte>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlByte?>,        SqlDataRecordScalarMapper<SqlByte?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlBytes?>,       SqlDataRecordScalarMapper<SqlBytes?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlChars?>,       SqlDataRecordScalarMapper<SqlChars?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlDateTime>,     SqlDataRecordScalarMapper<SqlDateTime>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlDateTime?>,    SqlDataRecordScalarMapper<SqlDateTime?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlDecimal>,      SqlDataRecordScalarMapper<SqlDecimal>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlDecimal?>,     SqlDataRecordScalarMapper<SqlDecimal?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlDouble>,       SqlDataRecordScalarMapper<SqlDouble>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlDouble?>,      SqlDataRecordScalarMapper<SqlDouble?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlGuid>,         SqlDataRecordScalarMapper<SqlGuid>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlGuid?>,        SqlDataRecordScalarMapper<SqlGuid?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlInt16>,        SqlDataRecordScalarMapper<SqlInt16>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlInt16?>,       SqlDataRecordScalarMapper<SqlInt16?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlInt32>,        SqlDataRecordScalarMapper<SqlInt32>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlInt32?>,       SqlDataRecordScalarMapper<SqlInt32?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlInt64>,        SqlDataRecordScalarMapper<SqlInt64>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlInt64?>,       SqlDataRecordScalarMapper<SqlInt64?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlMoney>,        SqlDataRecordScalarMapper<SqlMoney>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlMoney?>,       SqlDataRecordScalarMapper<SqlMoney?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlSingle>,       SqlDataRecordScalarMapper<SqlSingle>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlSingle?>,      SqlDataRecordScalarMapper<SqlSingle?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlString>,       SqlDataRecordScalarMapper<SqlString>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlString?>,      SqlDataRecordScalarMapper<SqlString?>>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordMapper<SqlXml?>,         SqlDataRecordScalarMapper<SqlXml?>>(Reuse.Singleton);


        // Classes don't need the non-nullable versions
        //registrator.Register<ISqlDataRecordMapper<byte[]>, SqlDataRecordScalarMapper<byte[]>>();
        //registrator.Register<ISqlDataRecordMapper<object>, SqlDataRecordScalarMapper<object>>();
        //registrator.Register<ISqlDataRecordMapper<string>, SqlDataRecordScalarMapper<string>>();
        //registrator.Register<ISqlDataRecordMapper<SqlBytes>, SqlDataRecordScalarMapper<SqlBytes>>();
        //registrator.Register<ISqlDataRecordMapper<SqlChars>, SqlDataRecordScalarMapper<SqlChars>>();
        //registrator.Register<ISqlDataRecordMapper<SqlXml>, SqlDataRecordScalarMapper<SqlXml>>();



    }

}
