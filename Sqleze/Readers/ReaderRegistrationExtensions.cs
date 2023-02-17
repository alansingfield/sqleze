using Sqleze.DryIoc;
using Sqleze;
using Sqleze.Options;
using Sqleze.Readers;
using Sqleze.Registration;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration
{
    public static class ReaderRegistrationExtensions
    {
        public static void RegisterSqlezeReaders(this IRegistrator registrator)
        {
            // Default is the POCO reader
            registrator.Register(typeof(IReader<>), typeof(PocoReader<>));
            registrator.Register(typeof(IColumnPropertyMapper<>), typeof(ColumnPropertyMapper<>));

            registrator.RegisterCheckedAdoDataReader();
            registrator.RegisterUnmappedColumnsPolicy();
            registrator.RegisterDuplicateColumnsPolicy();
            registrator.RegisterUnmappedPropertiesPolicy();

            registrator.Register(typeof(IColumnPropertyResolver<,>), typeof(ColumnPropertyResolver<,>), Reuse.Transient);
            registrator.RegisterGenericPromotion(typeof(IColumnPropertyResolver<>), typeof(IColumnPropertyResolver<,>));

            registrator.Register(typeof(IColumnProperty<,,>), typeof(ColumnProperty<,,>), Reuse.Transient);
            registrator.RegisterGenericPromotion(typeof(IColumnProperty<,>), typeof(IColumnProperty<,,>),
                trialType: typeof(IKnownSqlDbType));

            registrator.Register(typeof(IReaderGetValueResolver<,>), typeof(ReaderGetValueResolver<,>));
            registrator.RegisterGenericPromotion(typeof(IReaderGetValueResolver<>), typeof(IReaderGetValueResolver<,>),
                trialType: typeof(IKnownSqlDbType));

            // Scalar reader for these specific known types
            registrator.RegisterScalarReaderFallbackPolicy();

            registrator.Register<IReader<bool>,            ScalarReader<bool>>();
            registrator.Register<IReader<bool?>,           ScalarReader<bool?>>();
            registrator.Register<IReader<byte>,            ScalarReader<byte>>();
            registrator.Register<IReader<byte?>,           ScalarReader<byte?>>();
            registrator.Register<IReader<byte[]?>,         ScalarReader<byte[]?>>();
            registrator.Register<IReader<DateTime>,        ScalarReader<DateTime>>();
            registrator.Register<IReader<DateTime?>,       ScalarReader<DateTime?>>();
            registrator.Register<IReader<DateOnly>,        ScalarReader<DateOnly>>();
            registrator.Register<IReader<DateOnly?>,       ScalarReader<DateOnly?>>();
            registrator.Register<IReader<TimeOnly>,        ScalarReader<TimeOnly>>();
            registrator.Register<IReader<TimeOnly?>,       ScalarReader<TimeOnly?>>();
            registrator.Register<IReader<decimal>,         ScalarReader<decimal>>();
            registrator.Register<IReader<decimal?>,        ScalarReader<decimal?>>();
            registrator.Register<IReader<double>,          ScalarReader<double>>();
            registrator.Register<IReader<double?>,         ScalarReader<double?>>();
            registrator.Register<IReader<float>,           ScalarReader<float>>();
            registrator.Register<IReader<float?>,          ScalarReader<float?>>();
            registrator.Register<IReader<int>,             ScalarReader<int>>();
            registrator.Register<IReader<int?>,            ScalarReader<int?>>();
            registrator.Register<IReader<long>,            ScalarReader<long>>();
            registrator.Register<IReader<long?>,           ScalarReader<long?>>();
            registrator.Register<IReader<object?>,         ScalarReader<object?>>();
            registrator.Register<IReader<short>,           ScalarReader<short>>();
            registrator.Register<IReader<short?>,          ScalarReader<short?>>();
            registrator.Register<IReader<string?>,         ScalarReader<string?>>();
            registrator.Register<IReader<DateTimeOffset>,  ScalarReader<DateTimeOffset>>();
            registrator.Register<IReader<DateTimeOffset?>, ScalarReader<DateTimeOffset?>>();
            registrator.Register<IReader<Guid>,            ScalarReader<Guid>>();
            registrator.Register<IReader<Guid?>,           ScalarReader<Guid?>>();
            registrator.Register<IReader<TimeSpan>,        ScalarReader<TimeSpan>>();
            registrator.Register<IReader<TimeSpan?>,       ScalarReader<TimeSpan?>>();
            registrator.Register<IReader<SqlBinary>,       ScalarReader<SqlBinary>>();
            registrator.Register<IReader<SqlBinary?>,      ScalarReader<SqlBinary?>>();
            registrator.Register<IReader<SqlBoolean>,      ScalarReader<SqlBoolean>>();
            registrator.Register<IReader<SqlBoolean?>,     ScalarReader<SqlBoolean?>>();
            registrator.Register<IReader<SqlByte>,         ScalarReader<SqlByte>>();
            registrator.Register<IReader<SqlByte?>,        ScalarReader<SqlByte?>>();
            registrator.Register<IReader<SqlBytes?>,       ScalarReader<SqlBytes?>>();
            registrator.Register<IReader<SqlChars?>,       ScalarReader<SqlChars?>>();
            registrator.Register<IReader<SqlDateTime>,     ScalarReader<SqlDateTime>>();
            registrator.Register<IReader<SqlDateTime?>,    ScalarReader<SqlDateTime?>>();
            registrator.Register<IReader<SqlDecimal>,      ScalarReader<SqlDecimal>>();
            registrator.Register<IReader<SqlDecimal?>,     ScalarReader<SqlDecimal?>>();
            registrator.Register<IReader<SqlDouble>,       ScalarReader<SqlDouble>>();
            registrator.Register<IReader<SqlDouble?>,      ScalarReader<SqlDouble?>>();
            registrator.Register<IReader<SqlGuid>,         ScalarReader<SqlGuid>>();
            registrator.Register<IReader<SqlGuid?>,        ScalarReader<SqlGuid?>>();
            registrator.Register<IReader<SqlInt16>,        ScalarReader<SqlInt16>>();
            registrator.Register<IReader<SqlInt16?>,       ScalarReader<SqlInt16?>>();
            registrator.Register<IReader<SqlInt32>,        ScalarReader<SqlInt32>>();
            registrator.Register<IReader<SqlInt32?>,       ScalarReader<SqlInt32?>>();
            registrator.Register<IReader<SqlInt64>,        ScalarReader<SqlInt64>>();
            registrator.Register<IReader<SqlInt64?>,       ScalarReader<SqlInt64?>>();
            registrator.Register<IReader<SqlMoney>,        ScalarReader<SqlMoney>>();
            registrator.Register<IReader<SqlMoney?>,       ScalarReader<SqlMoney?>>();
            registrator.Register<IReader<SqlSingle>,       ScalarReader<SqlSingle>>();
            registrator.Register<IReader<SqlSingle?>,      ScalarReader<SqlSingle?>>();
            registrator.Register<IReader<SqlString>,       ScalarReader<SqlString>>();
            registrator.Register<IReader<SqlString?>,      ScalarReader<SqlString?>>();
            registrator.Register<IReader<SqlXml?>,         ScalarReader<SqlXml?>>();


            // Classes don't need the non-nullable versions
            //registrator.Register<IReader<byte[]>, ScalarReader<byte[]>>();
            //registrator.Register<IReader<object>, ScalarReader<object>>();
            //registrator.Register<IReader<string>, ScalarReader<string>>();
            //registrator.Register<IReader<SqlBytes>, ScalarReader<SqlBytes>>();
            //registrator.Register<IReader<SqlChars>, ScalarReader<SqlChars>>();
            //registrator.Register<IReader<SqlXml>, ScalarReader<SqlXml>>();


            // Dictionary readers

            registrator.Register<IReader<Dictionary<string, object?>>, DictionaryReader<Dictionary<string, object?>>>();
            registrator.Register<IReader<ExpandoObject>, DictionaryReader<ExpandoObject>>();

        }
    }
}
