using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Integration
{
    [TestClass]
    public class TableValuedParameterTests
    {

        [TestMethod]
        public void TableValuedParameterEntity()
        {
            var arg = new List<ValClass>()
            {
                new ValClass() { Val = 100 },
                new ValClass() { Val = 200 },
            };

            using var conn = connect();

            var result = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .AsTableType("dbo.tt_int_vals")
                .ReadList<int>();

            result.ShouldNotBeNull();
            result[0].ShouldBe(100);
            result[1].ShouldBe(200);
        }

        [TestMethod]
        public void TableValuedParameterEntityNoRows()
        {
            var arg = new List<ValClass>();

            using var conn = connect();

            var result = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .AsTableType("dbo.tt_int_vals")
                .ReadList<int>();

            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
        }

        [TestMethod]
        public void TableValuedParameterEntityNull()
        {
            var arg = new List<ValClass>();

            using var conn = connect();

            var result = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set<List<ValClass>?>("@arg", null)
                .AsTableType("dbo.tt_int_vals")
                .ReadList<int>();

            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
        }

        [TestMethod]
        public void TableValuedParameterScalar()
        {
            var arg = new List<int>()
            {
                100, 200
            };

            using var conn = connect();

            var result = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .AsTableType("dbo.tt_int_vals")
                .ReadList<int>();

            result.ShouldNotBeNull();
            result[0].ShouldBe(100);
            result[1].ShouldBe(200);
        }


        [TestMethod]
        public async Task TableValuedParameterEntityAsync()
        {
            var arg = new List<ValClass>()
            {
                new ValClass() { Val = 100 },
                new ValClass() { Val = 200 },
            };

            using var conn = connect();

            var result = await conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .AsTableType("dbo.tt_int_vals")
                .ReadListAsync<int>();

            result.ShouldNotBeNull();
            result[0].ShouldBe(100);
            result[1].ShouldBe(200);
        }


        [TestMethod]
        public void TableValuedParameterEntityMissingColumns()
        {
            var arg = new List<BadClass>()
            {
                new BadClass() { Bad = 100 },
                new BadClass() { Bad = 200 },
            };

            using var conn = connect();

            var cmd = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .AsTableType("dbo.tt_int_vals");

            Should.Throw(() =>
            {
                cmd.ReadList<int>();
            }, typeof(Exception)).Message.ShouldBe("No table-type parameter columns were found to map to property Bad, was expecting Bad");
        }

        [TestMethod]
        public void TableValuedParameterEntityExtraColumns()
        {
            var arg = new List<ValPlusClass>()
            {
                new ValPlusClass() { Val = 100, Plus = 1 },
                new ValPlusClass() { Val = 200, Plus = 1 },
            };

            using var conn = connect();

            var cmd = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .AsTableType("dbo.tt_int_vals");

            Should.Throw(() =>
            {
                cmd.ReadList<int>();
            }, typeof(Exception)).Message.ShouldBe("No table-type parameter columns were found to map to property Plus, was expecting Plus");
        }

        [TestMethod]
        public void TableValuedParameterKnownTypeEnumerable()
        {
            var conn = getBuilder()
                .WithTableTypeFor<string>("dbo.tt_nvarchar_vals")
                .Connect();

            var arg = new List<string>() { "a", "b" }.AsEnumerable();

            var result = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .ReadList<string>();

            result.ShouldBe(new[] { "a", "b" });
        }

        [TestMethod]
        public void TableValuedParameterKnownTypeList()
        {
            var conn = getBuilder()
                .WithTableTypeFor<string>("dbo.tt_nvarchar_vals")
                .Connect();

            var arg = new List<string>() { "a", "b" };

            var result = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .ReadList<string>();

            result.ShouldBe(new[] { "a", "b" });
        }

        [TestMethod]
        public void TableValuedParameterKnownTypeArray()
        {
            var conn = getBuilder()
                .WithTableTypeFor<string>("dbo.tt_nvarchar_vals")
                .Connect();

            var arg = new string[] { "a", "b" };

            var result = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .ReadList<string>();

            result.ShouldBe(new[] { "a", "b" });
        }

        [TestMethod]
        public void TableValuedParameterKnownMulti()
        {
            var conn = getBuilder()
                .WithTableTypeFor<string>("dbo.tt_nvarchar_vals")
                .WithTableTypeFor<int>("dbo.tt_int_vals")
                .WithTableTypeFor<int?>("dbo.tt_int_foos")
                .Connect();

            var arg1 = new string[] { "a", "b" };
            var arg2 = new int[] { 1, 2 };
            var arg3 = new int?[] { 3, null, 4 };

            var rdr = conn.Sql(@"
                SELECT val FROM @arg1;
                SELECT val FROM @arg2;
                SELECT foo FROM @arg3;
                ")
                .Parameters.Set(new { arg1, arg2, arg3 })
                .ExecuteReader();

            var result1 = rdr.ReadList<string>();
            var result2 = rdr.ReadList<int>();
            var result3 = rdr.ReadListNullable<int?>();

            result1.ShouldBe(new string[] { "a", "b", });
            result2.ShouldBe(new int[] { 1, 2 });
            result3.ShouldBe(new int?[] { 3, null, 4 });
        }


        [TestMethod]
        public void TableValuedParameterKnownTypeByteArrayEnumerable()
        {
            var conn = getBuilder()
                .WithTableTypeFor<byte[]>("dbo.tt_varbinary_vals")
                .Connect();

            var arg = new List<byte[]>() 
            { 
                new byte[] { 0x01, 0x02 }, 
                new byte[] { 0x03, 0x04 }
            }.AsEnumerable();

            var result = conn.Sql("SELECT val FROM @arg")
                .Parameters.Set(() => arg)
                .ReadList<byte[]>();

            result.ShouldBe(new List<byte[]>()
            {
                new byte[] { 0x01, 0x02 },
                new byte[] { 0x03, 0x04 }
            });
        }

        private class ValClass
        {
            public int Val { get; set; }
        }
        private class BadClass
        {
            public int Bad { get; set; }
        }
        private class ValPlusClass
        {
            public int Val { get; set; }
            public int Plus { get; set; }
        }

        private ISqlezeBuilder getBuilder()
        {
            var container = new Container();

            container.RegisterSqleze();
            container.RegisterTestSettings();

            return container.Resolve<ISqlezeBuilder>()
                .WithConfigKey("ConnectionString");
        }

        private ISqlezeConnection connect()
        {
            return getBuilder().Connect();
        }
    }
}
