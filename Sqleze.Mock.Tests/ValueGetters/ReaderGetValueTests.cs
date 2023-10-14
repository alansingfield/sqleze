using Sqleze.Registration;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.Tests.ValueGetters
{
    [TestClass]
    public class ReaderGetValueTests
    {
        [TestMethod]
        public void ReaderGetValueRegistration()
        {
            var container = DI.NewContainer();

            container.RegisterValueGetters();

            container.Resolve<IReaderGetValue<bool?>>()          .ShouldBeOfType<ReaderGetValue<bool?>>();
            container.Resolve<IReaderGetValue<bool>>()           .ShouldBeOfType<ReaderGetValue<bool>>();
            container.Resolve<IReaderGetValue<byte?>>()          .ShouldBeOfType<ReaderGetValue<byte?>>();
            container.Resolve<IReaderGetValue<byte>>()           .ShouldBeOfType<ReaderGetValue<byte>>();
            container.Resolve<IReaderGetValue<byte[]?>>()        .ShouldBeOfType<ReaderGetValue<byte[]?>>();
            container.Resolve<IReaderGetValue<byte[]>>()         .ShouldBeOfType<ReaderGetValue<byte[]>>();
            container.Resolve<IReaderGetValue<DateTime?>>()      .ShouldBeOfType<ReaderGetValue<DateTime?>>();
            container.Resolve<IReaderGetValue<DateTime>>()       .ShouldBeOfType<ReaderGetValue<DateTime>>();
            container.Resolve<IReaderGetValue<DateOnly?>>()      .ShouldBeOfType<ReaderGetFieldValue<DateOnly?>>();
            container.Resolve<IReaderGetValue<DateOnly>>()       .ShouldBeOfType<ReaderGetFieldValue<DateOnly>>();
            container.Resolve<IReaderGetValue<TimeOnly?>>()      .ShouldBeOfType<ReaderGetFieldValue<TimeOnly?>>();
            container.Resolve<IReaderGetValue<TimeOnly>>()       .ShouldBeOfType<ReaderGetFieldValue<TimeOnly>>();
            container.Resolve<IReaderGetValue<decimal?>>()       .ShouldBeOfType<ReaderGetValue<decimal?>>();
            container.Resolve<IReaderGetValue<decimal>>()        .ShouldBeOfType<ReaderGetValue<decimal>>();
            container.Resolve<IReaderGetValue<double?>>()        .ShouldBeOfType<ReaderGetValue<double?>>();
            container.Resolve<IReaderGetValue<double>>()         .ShouldBeOfType<ReaderGetValue<double>>();
            container.Resolve<IReaderGetValue<float?>>()         .ShouldBeOfType<ReaderGetValue<float?>>();
            container.Resolve<IReaderGetValue<float>>()          .ShouldBeOfType<ReaderGetValue<float>>();
            container.Resolve<IReaderGetValue<int?>>()           .ShouldBeOfType<ReaderGetValue<int?>>();
            container.Resolve<IReaderGetValue<int>>()            .ShouldBeOfType<ReaderGetValue<int>>();
            container.Resolve<IReaderGetValue<long?>>()          .ShouldBeOfType<ReaderGetValue<long?>>();
            container.Resolve<IReaderGetValue<long>>()           .ShouldBeOfType<ReaderGetValue<long>>();
            container.Resolve<IReaderGetValue<short?>>()         .ShouldBeOfType<ReaderGetValue<short?>>();
            container.Resolve<IReaderGetValue<short>>()          .ShouldBeOfType<ReaderGetValue<short>>();
            container.Resolve<IReaderGetValue<DateTimeOffset?>>().ShouldBeOfType<ReaderGetValue<DateTimeOffset?>>();
            container.Resolve<IReaderGetValue<DateTimeOffset>>() .ShouldBeOfType<ReaderGetValue<DateTimeOffset>>();
            container.Resolve<IReaderGetValue<Guid?>>()          .ShouldBeOfType<ReaderGetValue<Guid?>>();
            container.Resolve<IReaderGetValue<Guid>>()           .ShouldBeOfType<ReaderGetValue<Guid>>();
            container.Resolve<IReaderGetValue<TimeSpan?>>()      .ShouldBeOfType<ReaderGetValue<TimeSpan?>>();
            container.Resolve<IReaderGetValue<TimeSpan>>()       .ShouldBeOfType<ReaderGetValue<TimeSpan>>();

            container.Resolve<IReaderGetValue<SqlBinary>>()      .ShouldBeOfType<ReaderGetValueSqlBinary>();
            container.Resolve<IReaderGetValue<SqlBoolean>>()     .ShouldBeOfType<ReaderGetValueSqlBoolean>();
            container.Resolve<IReaderGetValue<SqlByte>>()        .ShouldBeOfType<ReaderGetValueSqlByte>();
            container.Resolve<IReaderGetValue<SqlBytes>>()       .ShouldBeOfType<ReaderGetValueSqlBytes>();
            container.Resolve<IReaderGetValue<SqlChars>>()       .ShouldBeOfType<ReaderGetValueSqlChars>();
            container.Resolve<IReaderGetValue<SqlDateTime>>()    .ShouldBeOfType<ReaderGetValueSqlDateTime>();
            container.Resolve<IReaderGetValue<SqlDecimal>>()     .ShouldBeOfType<ReaderGetValueSqlDecimal>();
            container.Resolve<IReaderGetValue<SqlDouble>>()      .ShouldBeOfType<ReaderGetValueSqlDouble>();
            container.Resolve<IReaderGetValue<SqlGuid>>()        .ShouldBeOfType<ReaderGetValueSqlGuid>();
            container.Resolve<IReaderGetValue<SqlInt16>>()       .ShouldBeOfType<ReaderGetValueSqlInt16>();
            container.Resolve<IReaderGetValue<SqlInt32>>()       .ShouldBeOfType<ReaderGetValueSqlInt32>();
            container.Resolve<IReaderGetValue<SqlInt64>>()       .ShouldBeOfType<ReaderGetValueSqlInt64>();
            container.Resolve<IReaderGetValue<SqlMoney>>()       .ShouldBeOfType<ReaderGetValueSqlMoney>();
            container.Resolve<IReaderGetValue<SqlSingle>>()      .ShouldBeOfType<ReaderGetValueSqlSingle>();
            container.Resolve<IReaderGetValue<SqlString>>()      .ShouldBeOfType<ReaderGetValueSqlString>();
            container.Resolve<IReaderGetValue<SqlXml>>()         .ShouldBeOfType<ReaderGetValueSqlXml>();

            container.Resolve<IReaderGetValue<SqlBinary?>>()     .ShouldBeOfType<ReaderGetValueSqlBinaryNullable>();
            container.Resolve<IReaderGetValue<SqlBoolean?>>()    .ShouldBeOfType<ReaderGetValueSqlBooleanNullable>();
            container.Resolve<IReaderGetValue<SqlByte?>>()       .ShouldBeOfType<ReaderGetValueSqlByteNullable>();
            container.Resolve<IReaderGetValue<SqlDateTime?>>()   .ShouldBeOfType<ReaderGetValueSqlDateTimeNullable>();
            container.Resolve<IReaderGetValue<SqlDecimal?>>()    .ShouldBeOfType<ReaderGetValueSqlDecimalNullable>();
            container.Resolve<IReaderGetValue<SqlDouble?>>()     .ShouldBeOfType<ReaderGetValueSqlDoubleNullable>();
            container.Resolve<IReaderGetValue<SqlGuid?>>()       .ShouldBeOfType<ReaderGetValueSqlGuidNullable>();
            container.Resolve<IReaderGetValue<SqlInt16?>>()      .ShouldBeOfType<ReaderGetValueSqlInt16Nullable>();
            container.Resolve<IReaderGetValue<SqlInt32?>>()      .ShouldBeOfType<ReaderGetValueSqlInt32Nullable>();
            container.Resolve<IReaderGetValue<SqlInt64?>>()      .ShouldBeOfType<ReaderGetValueSqlInt64Nullable>();
            container.Resolve<IReaderGetValue<SqlMoney?>>()      .ShouldBeOfType<ReaderGetValueSqlMoneyNullable>();
            container.Resolve<IReaderGetValue<SqlSingle?>>()     .ShouldBeOfType<ReaderGetValueSqlSingleNullable>();
            container.Resolve<IReaderGetValue<SqlString?>>()     .ShouldBeOfType<ReaderGetValueSqlStringNullable>();

            // These are classes rather than structs so we can't have a separate nullable version
            container.Resolve<IReaderGetValue<SqlBytes?>>()      .ShouldBeOfType<ReaderGetValueSqlBytes>();
            container.Resolve<IReaderGetValue<SqlChars?>>()      .ShouldBeOfType<ReaderGetValueSqlChars>();
            container.Resolve<IReaderGetValue<SqlXml?>>()        .ShouldBeOfType<ReaderGetValueSqlXml>();
            container.Resolve<IReaderGetValue<string?>>()        .ShouldBeOfType<ReaderGetValue<string>>();
            container.Resolve<IReaderGetValue<object?>>()        .ShouldBeOfType<ReaderGetValue<object>>();

        }
    }
}


