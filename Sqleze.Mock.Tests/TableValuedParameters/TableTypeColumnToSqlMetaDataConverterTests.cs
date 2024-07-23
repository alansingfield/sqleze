using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqleze.Metadata;
using Sqleze.TableValuedParameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.Tests.TableValuedParameters
{
    [TestClass]
    public class TableTypeColumnToSqlMetaDataConverterTests
    {

        [TestMethod]
        public void TableTypeColumnToSqlMetaDataConverterStandard()
        {
            var container = new Container().WithNSubstituteFallback();

            container.Register<ITableTypeColumnToSqlMetaDataConverter, TableTypeColumnToSqlMetaDataConverter>();

            var sqlMetaDataFactory = container.Resolve<ISqlMetaDataFactory>();

            var foo = container.Resolve<Func<string, SqlDbType, int, MSS.SqlMetaData>>();

            var conv = container.Resolve<ITableTypeColumnToSqlMetaDataConverter>();

            var coldef = new TableTypeColumnDefinition(
                ColumnName: "col",
                ColumnOrdinal: 2,
                Datatype: "float",
                Length: 0,
                Precision: (byte)0,
                Scale: (byte)0,
                IsNullable: false);

            var sqlMetaData = Substitute.For<MSS.SqlMetaData>();

            sqlMetaDataFactory.New("", default(SqlDbType))
                .ReturnsForAnyArgs(sqlMetaData);

            var result = conv.Convert(coldef);
            result.ShouldBe(sqlMetaData);

            sqlMetaDataFactory.Received(1).New("col", SqlDbType.Float);

            sqlMetaDataFactory.DidNotReceiveWithAnyArgs().New("", default(SqlDbType), 0);
            sqlMetaDataFactory.DidNotReceiveWithAnyArgs().New("", default(SqlDbType), (byte)0, (byte)0);
        }

        [TestMethod]
        public void TableTypeColumnToSqlMetaDataConverterSized()
        {
            var container = new Container().WithNSubstituteFallback();

            container.Register<ITableTypeColumnToSqlMetaDataConverter, TableTypeColumnToSqlMetaDataConverter>();

            var sqlMetaDataFactory = container.Resolve<ISqlMetaDataFactory>();

            var foo = container.Resolve<Func<string, SqlDbType, int, MSS.SqlMetaData>>();

            var conv = container.Resolve<ITableTypeColumnToSqlMetaDataConverter>();

            var coldef = new TableTypeColumnDefinition(
                ColumnName: "col",
                ColumnOrdinal: 2,
                Datatype: "varchar",
                Length: 30,
                Precision: (byte)0,
                Scale: (byte)0,
                IsNullable: false);

            var sqlMetaData = Substitute.For<MSS.SqlMetaData>();

            sqlMetaDataFactory.New("", default(SqlDbType), 0)
                .ReturnsForAnyArgs(sqlMetaData);

            var result = conv.Convert(coldef);
            result.ShouldBe(sqlMetaData);

            sqlMetaDataFactory.Received(1).New("col", SqlDbType.VarChar, 30);

            sqlMetaDataFactory.DidNotReceiveWithAnyArgs().New("", default(SqlDbType));
            sqlMetaDataFactory.DidNotReceiveWithAnyArgs().New("", default(SqlDbType), (byte)0, (byte)0);
        }

        [TestMethod]
        public void TableTypeColumnToSqlMetaDataConverterScaled()
        {
            var container = new Container().WithNSubstituteFallback();

            container.Register<ITableTypeColumnToSqlMetaDataConverter, TableTypeColumnToSqlMetaDataConverter>();

            var sqlMetaDataFactory = container.Resolve<ISqlMetaDataFactory>();

            var foo = container.Resolve<Func<string, SqlDbType, int, MSS.SqlMetaData>>();

            var conv = container.Resolve<ITableTypeColumnToSqlMetaDataConverter>();

            var coldef = new TableTypeColumnDefinition(
                ColumnName: "col",
                ColumnOrdinal: 2,
                Datatype: "numeric",
                Length: 0,
                Precision: (byte)18,
                Scale: (byte)2,
                IsNullable: false);

            var sqlMetaData = Substitute.For<MSS.SqlMetaData>();

            sqlMetaDataFactory.New("", default(SqlDbType), (byte)0, (byte)0)
                .ReturnsForAnyArgs(sqlMetaData);

            var result = conv.Convert(coldef);
            result.ShouldBe(sqlMetaData);

            sqlMetaDataFactory.Received(1).New("col", SqlDbType.Decimal, (byte)18, (byte)2);

            sqlMetaDataFactory.DidNotReceiveWithAnyArgs().New("", default(SqlDbType), 0);
            sqlMetaDataFactory.DidNotReceiveWithAnyArgs().New("", default(SqlDbType));
        }

        [TestMethod]
        public void TableTypeColumnToSqlMetaDataConverterScaledNoPrecision()
        {
            var container = new Container().WithNSubstituteFallback();

            container.Register<ITableTypeColumnToSqlMetaDataConverter, TableTypeColumnToSqlMetaDataConverter>();

            var sqlMetaDataFactory = container.Resolve<ISqlMetaDataFactory>();

            var foo = container.Resolve<Func<string, SqlDbType, int, MSS.SqlMetaData>>();

            var conv = container.Resolve<ITableTypeColumnToSqlMetaDataConverter>();

            // Special case, even though Precision of a Time is 16, we want this to be passed as zero
            var coldef = new TableTypeColumnDefinition(
                ColumnName: "col",
                ColumnOrdinal: 2,
                Datatype: "time",
                Length: 0,
                Precision: (byte)16,
                Scale: (byte)7,
                IsNullable: false);

            var sqlMetaData = Substitute.For<MSS.SqlMetaData>();

            sqlMetaDataFactory.New("", default(SqlDbType), (byte)0, (byte)0)
                .ReturnsForAnyArgs(sqlMetaData);

            var result = conv.Convert(coldef);
            result.ShouldBe(sqlMetaData);

            // Precision is zero but scale is 7.
            sqlMetaDataFactory.Received(1).New("col", SqlDbType.Time, (byte)0, (byte)7);

            sqlMetaDataFactory.DidNotReceiveWithAnyArgs().New("", default(SqlDbType), 0);
            sqlMetaDataFactory.DidNotReceiveWithAnyArgs().New("", default(SqlDbType));
        }
    }
}
