using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using Sqleze.Params;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Params
{
    [TestClass]
    public class ParameterDefaultSqlTypeTests
    {
        [TestMethod]
        public void ParameterDefaultSqlResolutionNVarChar()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<string>>();
            options1.SqlTypeName.ShouldBe("nvarchar");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<string?>>();
            options2.SqlTypeName.ShouldBe("nvarchar");
        }

        //[TestMethod]
        //public void ParameterDefaultSqlResolutionInt()
        //{
        //    var container = new Container();

        //    container.RegisterParameterDefaultSqlTypes();

        //    var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<int>>();
        //    options1.SqlTypeName.ShouldBe("int");

        //    var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<int?>>();
        //    options2.SqlTypeName.ShouldBe("int");
        //}

        [TestMethod]
        public void ParameterDefaultSqlResolutionObject()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            // object falls back to nvarchar just like ADO does.
            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<object>>();
            options1.SqlTypeName.ShouldBe("nvarchar");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<object?>>();
            options2.SqlTypeName.ShouldBe("nvarchar");
        }


        [TestMethod]
        public void ParameterDefaultSqlResolutionString()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<string>>();
            options1.SqlTypeName.ShouldBe("nvarchar");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<string?>>();
            options2.SqlTypeName.ShouldBe("nvarchar");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionByteArray()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<byte[]>>();
                options1.SqlTypeName.ShouldBe("varbinary");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<byte[]?>>();
                options2.SqlTypeName.ShouldBe("varbinary");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionInt()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<int>>();
            options1.SqlTypeName.ShouldBe("int");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<int?>>();
            options2.SqlTypeName.ShouldBe("int");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionBool()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<bool>>();
            options1.SqlTypeName.ShouldBe("bit");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<bool?>>();
            options2.SqlTypeName.ShouldBe("bit");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionByte()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<byte>>();
            options1.SqlTypeName.ShouldBe("tinyint");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<byte?>>();
            options2.SqlTypeName.ShouldBe("tinyint");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionShort()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<short>>();
            options1.SqlTypeName.ShouldBe("smallint");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<short?>>();
            options2.SqlTypeName.ShouldBe("smallint");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionLong()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<long>>();
            options1.SqlTypeName.ShouldBe("bigint");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<long?>>();
            options2.SqlTypeName.ShouldBe("bigint");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSingle()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<float>>();
            options1.SqlTypeName.ShouldBe("real");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<float?>>();
            options2.SqlTypeName.ShouldBe("real");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionDouble()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<double>>();
            options1.SqlTypeName.ShouldBe("float");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<double?>>();
            options2.SqlTypeName.ShouldBe("float");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutiondecimal()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<decimal>>();
            options1.SqlTypeName.ShouldBe("decimal");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<decimal?>>();
            options2.SqlTypeName.ShouldBe("decimal");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionDateTime()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<DateTime>>();
            options1.SqlTypeName.ShouldBe("datetime2");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<DateTime?>>();
            options2.SqlTypeName.ShouldBe("datetime2");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionDateTimeOffset()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<DateTimeOffset>>();
            options1.SqlTypeName.ShouldBe("datetimeoffset");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<DateTimeOffset?>>();
            options2.SqlTypeName.ShouldBe("datetimeoffset");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionTimeSpan()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<TimeSpan>>();
            options1.SqlTypeName.ShouldBe("time");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<TimeSpan?>>();
            options2.SqlTypeName.ShouldBe("time");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionGuid()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<Guid>>();
            options1.SqlTypeName.ShouldBe("uniqueidentifier");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<Guid?>>();
            options2.SqlTypeName.ShouldBe("uniqueidentifier");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlBinary()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlBinary>>();
            options1.SqlTypeName.ShouldBe("varbinary");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlBinary?>>();
            options2.SqlTypeName.ShouldBe("varbinary");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlBoolean()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlBoolean>>();
            options1.SqlTypeName.ShouldBe("bit");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlBoolean?>>();
            options2.SqlTypeName.ShouldBe("bit");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlByte()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlByte>>();
            options1.SqlTypeName.ShouldBe("tinyint");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlByte?>>();
            options2.SqlTypeName.ShouldBe("tinyint");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlBytes()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlBytes>>();
            options1.SqlTypeName.ShouldBe("varbinary");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlBytes?>>();
            options2.SqlTypeName.ShouldBe("varbinary");
        }

        //[TestMethod]
        //public void ParameterDefaultSqlResolutionSqlChars()
        //{
        //    var container = new Container();

        //    container.RegisterParameterDefaultSqlTypes();

        //    var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlChars>>();
        //    options1.SqlTypeName.ShouldBe("SqlChars");

        //    var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlChars?>>();
        //    options2.SqlTypeName.ShouldBe("SqlChars");
        //}

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlDateTime()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlDateTime>>();
            options1.SqlTypeName.ShouldBe("datetime");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlDateTime?>>();
            options2.SqlTypeName.ShouldBe("datetime");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlDouble()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlDouble>>();
            options1.SqlTypeName.ShouldBe("float");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlDouble?>>();
            options2.SqlTypeName.ShouldBe("float");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlGuid()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlGuid>>();
            options1.SqlTypeName.ShouldBe("uniqueidentifier");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlGuid?>>();
            options2.SqlTypeName.ShouldBe("uniqueidentifier");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlInt16()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlInt16>>();
            options1.SqlTypeName.ShouldBe("smallint");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlInt16?>>();
            options2.SqlTypeName.ShouldBe("smallint");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlInt32()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlInt32>>();
            options1.SqlTypeName.ShouldBe("int");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlInt32?>>();
            options2.SqlTypeName.ShouldBe("int");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlInt64()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlInt64>>();
            options1.SqlTypeName.ShouldBe("bigint");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlInt64?>>();
            options2.SqlTypeName.ShouldBe("bigint");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlMoney()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlMoney>>();
            options1.SqlTypeName.ShouldBe("money");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlMoney?>>();
            options2.SqlTypeName.ShouldBe("money");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlDecimal()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlDecimal>>();
            options1.SqlTypeName.ShouldBe("decimal");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlDecimal?>>();
            options2.SqlTypeName.ShouldBe("decimal");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlSingle()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlSingle>>();
            options1.SqlTypeName.ShouldBe("real");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlSingle?>>();
            options2.SqlTypeName.ShouldBe("real");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlString()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlString>>();
            options1.SqlTypeName.ShouldBe("nvarchar");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlString?>>();
            options2.SqlTypeName.ShouldBe("nvarchar");
        }

        [TestMethod]
        public void ParameterDefaultSqlResolutionSqlXml()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options1 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlXml>>();
            options1.SqlTypeName.ShouldBe("xml");

            var options2 = container.Resolve<IParameterDefaultSqlTypeOptions<SqlXml?>>();
            options2.SqlTypeName.ShouldBe("xml");
        }


        [TestMethod]
        public void ParameterDefaultSqlResolutionTableTypeNotKnown()
        {
            var container = new Container();

            container.RegisterParameterDefaultSqlTypes();

            var options = container.Resolve<IParameterDefaultSqlTypeOptions<IEnumerable<object>>>();
            options.SqlTypeName.ShouldBe("");
            options.Mode.ShouldBe(SqlezeParameterMode.TableType);
        }





    }
}
