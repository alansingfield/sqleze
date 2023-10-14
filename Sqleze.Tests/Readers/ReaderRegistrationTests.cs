using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using Sqleze.Readers;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Readers
{
    [TestClass]
    public class ReaderRegistrationTests
    {
        [TestMethod]
        public void ReaderRegistrationPoco()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            // Not sure why I NEED to open a scope for this one, but I guess it's ok
            // as in practice we always resolve the IReader<> within a scope.
            using var scope = container.OpenScope();

            // Fallback is always the POCO reader
            scope.Resolve<IReader<MyEntity>>().ShouldBeOfType<PocoReader<MyEntity>>();
        }


        [TestMethod]
        public void ReaderRegistrationScalarBool()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<bool>>().ShouldBeOfType<ScalarReader<bool>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableBool()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<bool?>>().ShouldBeOfType<ScalarReader<bool?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarByte()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<byte>>().ShouldBeOfType<ScalarReader<byte>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableByte()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<byte?>>().ShouldBeOfType<ScalarReader<byte?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarByteArray()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<byte[]>>().ShouldBeOfType<ScalarReader<byte[]>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableByteArray()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<byte[]?>>().ShouldBeOfType<ScalarReader<byte[]?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarDateTime()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<DateTime>>().ShouldBeOfType<ScalarReader<DateTime>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableDateTime()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<DateTime?>>().ShouldBeOfType<ScalarReader<DateTime?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarDecimal()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<decimal>>().ShouldBeOfType<ScalarReader<decimal>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableDecimal()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<decimal?>>().ShouldBeOfType<ScalarReader<decimal?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarDouble()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<double>>().ShouldBeOfType<ScalarReader<double>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableDouble()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<double?>>().ShouldBeOfType<ScalarReader<double?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarFloat()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<float>>().ShouldBeOfType<ScalarReader<float>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableFloat()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<float?>>().ShouldBeOfType<ScalarReader<float?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarInt()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<int>>().ShouldBeOfType<ScalarReader<int>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableInt()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<int?>>().ShouldBeOfType<ScalarReader<int?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarLong()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<long>>().ShouldBeOfType<ScalarReader<long>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableLong()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<long?>>().ShouldBeOfType<ScalarReader<long?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarObject()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<object>>().ShouldBeOfType<ScalarReader<object>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableObject()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<object?>>().ShouldBeOfType<ScalarReader<object?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarShort()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<short>>().ShouldBeOfType<ScalarReader<short>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableShort()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<short?>>().ShouldBeOfType<ScalarReader<short?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarString()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<string>>().ShouldBeOfType<ScalarReader<string>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableString()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<string?>>().ShouldBeOfType<ScalarReader<string?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarDateTimeOffset()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<DateTimeOffset>>().ShouldBeOfType<ScalarReader<DateTimeOffset>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableDateTimeOffset()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<DateTimeOffset?>>().ShouldBeOfType<ScalarReader<DateTimeOffset?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarGuid()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<Guid>>().ShouldBeOfType<ScalarReader<Guid>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableGuid()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<Guid?>>().ShouldBeOfType<ScalarReader<Guid?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarTimeSpan()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<TimeSpan>>().ShouldBeOfType<ScalarReader<TimeSpan>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableTimeSpan()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<TimeSpan?>>().ShouldBeOfType<ScalarReader<TimeSpan?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlBinary()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlBinary>>().ShouldBeOfType<ScalarReader<SqlBinary>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlBinary()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlBinary?>>().ShouldBeOfType<ScalarReader<SqlBinary?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlBoolean()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlBoolean>>().ShouldBeOfType<ScalarReader<SqlBoolean>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlBoolean()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlBoolean?>>().ShouldBeOfType<ScalarReader<SqlBoolean?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlByte()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlByte>>().ShouldBeOfType<ScalarReader<SqlByte>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlByte()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlByte?>>().ShouldBeOfType<ScalarReader<SqlByte?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlBytes()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlBytes>>().ShouldBeOfType<ScalarReader<SqlBytes>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlBytes()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlBytes?>>().ShouldBeOfType<ScalarReader<SqlBytes?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlChars()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlChars>>().ShouldBeOfType<ScalarReader<SqlChars>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlChars()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlChars?>>().ShouldBeOfType<ScalarReader<SqlChars?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlDateTime()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlDateTime>>().ShouldBeOfType<ScalarReader<SqlDateTime>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlDateTime()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlDateTime?>>().ShouldBeOfType<ScalarReader<SqlDateTime?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlDecimal()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlDecimal>>().ShouldBeOfType<ScalarReader<SqlDecimal>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlDecimal()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlDecimal?>>().ShouldBeOfType<ScalarReader<SqlDecimal?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlDouble()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlDouble>>().ShouldBeOfType<ScalarReader<SqlDouble>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlDouble()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlDouble?>>().ShouldBeOfType<ScalarReader<SqlDouble?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlGuid()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlGuid>>().ShouldBeOfType<ScalarReader<SqlGuid>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlGuid()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlGuid?>>().ShouldBeOfType<ScalarReader<SqlGuid?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlInt16()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlInt16>>().ShouldBeOfType<ScalarReader<SqlInt16>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlInt16()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlInt16?>>().ShouldBeOfType<ScalarReader<SqlInt16?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlInt32()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlInt32>>().ShouldBeOfType<ScalarReader<SqlInt32>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlInt32()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlInt32?>>().ShouldBeOfType<ScalarReader<SqlInt32?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlInt64()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlInt64>>().ShouldBeOfType<ScalarReader<SqlInt64>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlInt64()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlInt64?>>().ShouldBeOfType<ScalarReader<SqlInt64?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlMoney()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlMoney>>().ShouldBeOfType<ScalarReader<SqlMoney>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlMoney()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlMoney?>>().ShouldBeOfType<ScalarReader<SqlMoney?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlSingle()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlSingle>>().ShouldBeOfType<ScalarReader<SqlSingle>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlSingle()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlSingle?>>().ShouldBeOfType<ScalarReader<SqlSingle?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlString()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlString>>().ShouldBeOfType<ScalarReader<SqlString>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlString()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlString?>>().ShouldBeOfType<ScalarReader<SqlString?>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarSqlXml()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlXml>>().ShouldBeOfType<ScalarReader<SqlXml>>();
        }

        [TestMethod]
        public void ReaderRegistrationScalarNullableSqlXml()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<SqlXml?>>().ShouldBeOfType<ScalarReader<SqlXml?>>();
        }


        [TestMethod]
        public void ReaderRegistrationDictionaryObject()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<Dictionary<string, object>>>().ShouldBeOfType<DictionaryReader<Dictionary<string, object?>>>();
        }

        [TestMethod]
        public void ReaderRegistrationDictionaryNullableObject()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();

            container.Resolve<IReader<Dictionary<string, object?>>>().ShouldBeOfType<DictionaryReader<Dictionary<string, object?>>>();
        }


        public class MyEntity { }
    }
}
