using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.Readers;
using Sqleze.Registration;
using Sqleze.TableValuedParameters;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.TableValuedParameters
{
    [TestClass]
    public class SqlDataRecordMapperRegistrationTests
    {
        [TestMethod]
        public void SqlDataRecordMapperRegistrationPoco()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            // Fallback is always the POCO reader
            container.Resolve<ISqlDataRecordMapper<MyEntity>>().ShouldBeOfType<SqlDataRecordPropertyMapper<MyEntity>>();
        }


        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarBool()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<bool>>().ShouldBeOfType<SqlDataRecordScalarMapper<bool>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableBool()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<bool?>>().ShouldBeOfType<SqlDataRecordScalarMapper<bool?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarByte()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<byte>>().ShouldBeOfType<SqlDataRecordScalarMapper<byte>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableByte()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<byte?>>().ShouldBeOfType<SqlDataRecordScalarMapper<byte?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarByteArray()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<byte[]>>().ShouldBeOfType<SqlDataRecordScalarMapper<byte[]>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableByteArray()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<byte[]?>>().ShouldBeOfType<SqlDataRecordScalarMapper<byte[]?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarDateTime()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<DateTime>>().ShouldBeOfType<SqlDataRecordScalarMapper<DateTime>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableDateTime()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<DateTime?>>().ShouldBeOfType<SqlDataRecordScalarMapper<DateTime?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarDecimal()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<decimal>>().ShouldBeOfType<SqlDataRecordScalarMapper<decimal>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableDecimal()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<decimal?>>().ShouldBeOfType<SqlDataRecordScalarMapper<decimal?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarDouble()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<double>>().ShouldBeOfType<SqlDataRecordScalarMapper<double>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableDouble()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<double?>>().ShouldBeOfType<SqlDataRecordScalarMapper<double?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarFloat()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<float>>().ShouldBeOfType<SqlDataRecordScalarMapper<float>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableFloat()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<float?>>().ShouldBeOfType<SqlDataRecordScalarMapper<float?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarInt()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<int>>().ShouldBeOfType<SqlDataRecordScalarMapper<int>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableInt()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<int?>>().ShouldBeOfType<SqlDataRecordScalarMapper<int?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarLong()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<long>>().ShouldBeOfType<SqlDataRecordScalarMapper<long>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableLong()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<long?>>().ShouldBeOfType<SqlDataRecordScalarMapper<long?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarObject()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<object>>().ShouldBeOfType<SqlDataRecordScalarMapper<object>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableObject()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<object?>>().ShouldBeOfType<SqlDataRecordScalarMapper<object?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarShort()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<short>>().ShouldBeOfType<SqlDataRecordScalarMapper<short>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableShort()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<short?>>().ShouldBeOfType<SqlDataRecordScalarMapper<short?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarString()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<string>>().ShouldBeOfType<SqlDataRecordScalarMapper<string>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableString()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<string?>>().ShouldBeOfType<SqlDataRecordScalarMapper<string?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarDateTimeOffset()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<DateTimeOffset>>().ShouldBeOfType<SqlDataRecordScalarMapper<DateTimeOffset>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableDateTimeOffset()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<DateTimeOffset?>>().ShouldBeOfType<SqlDataRecordScalarMapper<DateTimeOffset?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarGuid()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<Guid>>().ShouldBeOfType<SqlDataRecordScalarMapper<Guid>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableGuid()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<Guid?>>().ShouldBeOfType<SqlDataRecordScalarMapper<Guid?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarTimeSpan()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<TimeSpan>>().ShouldBeOfType<SqlDataRecordScalarMapper<TimeSpan>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableTimeSpan()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<TimeSpan?>>().ShouldBeOfType<SqlDataRecordScalarMapper<TimeSpan?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlBinary()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlBinary>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlBinary>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlBinary()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlBinary?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlBinary?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlBoolean()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlBoolean>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlBoolean>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlBoolean()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlBoolean?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlBoolean?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlByte()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlByte>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlByte>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlByte()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlByte?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlByte?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlBytes()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlBytes>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlBytes>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlBytes()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlBytes?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlBytes?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlChars()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlChars>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlChars>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlChars()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlChars?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlChars?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlDateTime()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlDateTime>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlDateTime>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlDateTime()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlDateTime?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlDateTime?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlDecimal()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlDecimal>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlDecimal>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlDecimal()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlDecimal?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlDecimal?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlDouble()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlDouble>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlDouble>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlDouble()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlDouble?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlDouble?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlGuid()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlGuid>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlGuid>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlGuid()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlGuid?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlGuid?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlInt16()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlInt16>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlInt16>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlInt16()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlInt16?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlInt16?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlInt32()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlInt32>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlInt32>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlInt32()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlInt32?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlInt32?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlInt64()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlInt64>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlInt64>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlInt64()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlInt64?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlInt64?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlMoney()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlMoney>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlMoney>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlMoney()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlMoney?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlMoney?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlSingle()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlSingle>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlSingle>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlSingle()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlSingle?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlSingle?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlString()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlString>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlString>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlString()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlString?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlString?>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarSqlXml()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlXml>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlXml>>();
        }

        [TestMethod]
        public void SqlDataRecordMapperRegistrationScalarNullableSqlXml()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlDataRecordMappers();

            container.Resolve<ISqlDataRecordMapper<SqlXml?>>().ShouldBeOfType<SqlDataRecordScalarMapper<SqlXml?>>();
        }

        // TODO - implement a dictionary mapper
        //[TestMethod]
        //public void SqlDataRecordMapperRegistrationDictionaryObject()
        //{
        //    var container = DI.NewContainer().WithNSubstituteFallback();

        //    container.RegisterSqlDataRecordMappers();

        //    container.Resolve<ISqlDataRecordMapper<Dictionary<string, object>>>().ShouldBeOfType<DictionaryReader<Dictionary<string, object?>>>();
        //}

        //[TestMethod]
        //public void SqlDataRecordMapperRegistrationDictionaryNullableObject()
        //{
        //    var container = DI.NewContainer().WithNSubstituteFallback();

        //    container.RegisterSqlDataRecordMappers();

        //    container.Resolve<ISqlDataRecordMapper<Dictionary<string, object?>>>().ShouldBeOfType<DictionaryReader<Dictionary<string, object?>>>();
        //}


        public class MyEntity { }
    }
}
