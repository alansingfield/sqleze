using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.ConnectionStrings;
using Sqleze;
using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sqleze.Tests.Integration
{
    [TestClass]
    public class ScalarReadTests
    {
        [TestMethod]
        public void ScalarRead1()
        {
            var container = new Container();

            container.RegisterSqleze();
            container.RegisterTestSettings();

            var builder = container.Resolve<ISqlezeBuilder>();

            using var connection = builder
                .WithConfigKey("ConnectionString")
                .Build()
                .Connect();

            var result = connection.Sql("SELECT 'Hello World'")
                .ExecuteReader()
                .ReadSingle<string>();

            result.ShouldBe("Hello World");
        }

        [TestMethod]
        public void ScalarReadSimpleConnection()
        {
            using var connection = connect();

            var result = connection
                .Sql("SELECT 'Hello World'")
                .ReadSingle<string>();

            result.ShouldBe("Hello World");
        }

        [TestMethod]
        public void ScalarReadSimpleConnection2()
        {
            var container = new Container();

            container.RegisterSqleze();
            container.RegisterTestSettings();

            using var connection = container.Resolve<ISqlezeBuilder>()
                .WithConfigKey("ConnectionString")
                .Connect();

            var result = connection
                .Sql("SELECT 'Hello World'")
                .ReadSingle<string>();

            result.ShouldBe("Hello World");
        }

        [TestMethod]
        public void ScalarReadSimpleNull()
        {
            using var connection = connect();

            var result = connection
                .Sql("SELECT NULL")
                .ReadSingleNullable<string?>();

            result.ShouldBe(null);
        }

        [TestMethod]
        public void ScalarReadSimpleStringNotNull()
        {
            using var connection = connect();

            var result = connection
                .Sql("SELECT NULL")
                .ReadSingle<string>();

            result.ShouldBe("");
        }

        [TestMethod]
        public void ScalarReadSimpleInt()
        {
            using var connection = connect();

            var result = connection
                .Sql("SELECT NULL")
                .ReadSingle<int>();

            result.ShouldBe(0);
        }

        [TestMethod]
        public void ScalarReadSimpleNullableInt()
        {
            using var connection = connect();

            var result = connection
                .Sql("SELECT NULL")
                .ReadSingleNullable<int?>();

            result.ShouldBe(null);
        }

        [TestMethod]
        public void ScalarReadSimpleNullableIntAnomoly()
        {
            using var connection = connect();

            // We're saying we want nullable, but we get int not int?
            var result = connection
                .Sql("SELECT NULL")
                .ReadSingleNullable<int>();

            result.ShouldBe(0);
        }

        [TestMethod]
        public async Task ScalarReadListAsync()
        {
            using var connection = connect();

            var rdr = await connection
                .Sql("SELECT 'Hello World'")
                .ExecuteReaderAsync();

            var result = await rdr.OpenRowsetNullable<string>()
                .EnumerateAsync()
                .ToListAsync();

            result.Single().ShouldBe("Hello World");
        }

        [TestMethod]
        public async Task ScalarReadListAsync2()
        {
            using var connection = connect();

            var rdr = await connection
                .Sql("SELECT 'Hello World'")
                .ExecuteReaderAsync();

            var result = await rdr.ReadListAsync<string>();

            result.Single().ShouldBe("Hello World");
        }

        [TestMethod]
        public async Task ScalarReadSingleAsync()
        {
            using var connection = connect();

            var result = await connection
                .Sql("SELECT 'Hello World'")
                .ReadSingleAsync<string>();

            result.ShouldBe("Hello World");
        }


        [TestMethod]
        public void ScalarReadSqlString()
        {
            using var connection = connect();

            var result = connection
                .Sql("SELECT 'Hello World'")
                .ReadSingle<SqlString>();

            result.Value.ShouldBe("Hello World");
            result.IsNull.ShouldBe(false);
        }

        [TestMethod]
        public void ScalarReadSqlStringDbNull()
        {
            using var connection = connect();

            var result = connection
                .Sql("SELECT NULL")
                .ReadSingle<SqlString>();

            result.IsNull.ShouldBe(true);
        }

        [TestMethod]
        public void ScalarReadSqlStringNull()
        {
            using var connection = connect();

            var result = connection
                .Sql("SELECT NULL")
                .ReadSingleNullable<SqlString?>();

            result.ShouldBeNull();
        }

        [TestMethod]
        public void ScalarReadMultiRowset()
        {
            using var connection = connect();

            var rdr = connection
                .Sql("SELECT 'A'; SELECT 'B'")
                .ExecuteReader();

            var resultA = rdr.ReadSingle<string>();
            var resultB = rdr.ReadSingle<string>();

            resultA.ShouldBe("A");
            resultB.ShouldBe("B");
        }

        [TestMethod]
        public async Task ScalarReadMultiRowsetAsync()
        {
            using var connection = connect();

            var rdr = await connection
                .Sql("SELECT 'A'; SELECT 'B'")
                .ExecuteReaderAsync();

            var resultA = await rdr.ReadSingleAsync<string>();
            var resultB = await rdr.ReadSingleAsync<string>();

            resultA.ShouldBe("A");
            resultB.ShouldBe("B");
        }



        [TestMethod]
        public void ScalarReadDateOnly()
        {
            using var connection = connect();

            DateOnly arg = DateOnly.Parse("2022-12-22");

            var rowset = connection
                .Sql("SELECT @arg")
                .Parameters.Set(() => arg)
                .ExecuteReader()
                .OpenRowset<DateOnly>();

            var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
            var result = rowset.Enumerate().Single();

            result.ShouldBe(DateOnly.Parse("2022-12-22"));
            dataType.ShouldBe("date");
        }

        [TestMethod]
        public void ScalarReadDateOnlyNullable()
        {
            using var connection = connect();

            DateOnly? arg = null;

            var rowset = connection
                .Sql("SELECT @arg")
                .Parameters.Set(() => arg)
                .ExecuteReader()
                .OpenRowsetNullable<DateOnly?>();

            var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
            var result = rowset.Enumerate().Single();

            result.ShouldBe(null);
            dataType.ShouldBe("date");
        }
        [TestMethod]
        public void ScalarReadTimeOnly()
        {
            using var connection = connect();

            TimeOnly arg = TimeOnly.Parse("23:59:30");

            var rowset = connection
                .Sql("SELECT @arg")
                .Parameters.Set(() => arg)
                .ExecuteReader()
                .OpenRowset<TimeOnly>();

            var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
            var result = rowset.Enumerate().Single();

            result.ShouldBe(TimeOnly.Parse("23:59:30"));
            dataType.ShouldBe("time(7)");
        }

        [TestMethod]
        public void ScalarReadTimeOnlyNullable()
        {
            using var connection = connect();

            TimeOnly? arg = null;

            var rowset = connection
                .Sql("SELECT @arg")
                .Parameters.Set(() => arg)
                .ExecuteReader()
                .OpenRowsetNullable<TimeOnly?>();

            var dataType = rowset.GetFieldSchemas().Single().SqlDataType();
            var result = rowset.Enumerate().Single();

            result.ShouldBe(null);
            dataType.ShouldBe("time(7)");
        }


        [TestMethod]
        public void ScalarReadLargeResult()
        {
            using var connection = connect();

            int len = 10_000_000;

            var result = connection
                .Sql($"SELECT REPLICATE(CONVERT(nvarchar(max), 'X'), {len})")
                .ReadSingle<string>();

            result.ShouldBe(new string('X', len));
        }

        //Commenting this out for now, it proves the issue which WithForceSynchronousRead fixes.

        //[TestMethod]
        //public async Task ScalarReadLargeResultAsync()
        //{
        //    // Demonstrates that ReadAsync is super-slow - let's create an option to prefer
        //    // Read() over ReadAsync();
        //    using var connection = sqleze()
        //        .Connect();

        //    int len = 10_000_000;   // 10MB takes 2 minutes...

        //    var result = await connection
        //        .WithForceSynchronousRead(false)
        //        .Sql($"SELECT REPLICATE(CONVERT(nvarchar(max), 'X'), {len})")
        //        .ReadSingleAsync<string>();

        //    result.ShouldBe(new string('X', len));
        //}

        [TestMethod]
        public async Task ScalarReadLargeResultForceSynchronousReadAsync()
        {
            // Demonstrates that ReadAsync is super-slow - let's create an option to prefer
            // Read() over ReadAsync();
            using var connection = connect();

            int len = 10_000_000;

            var result = await connection
                .WithForceSynchronousRead(true) // this fixes the problem.
                .Sql($"SELECT REPLICATE(CONVERT(nvarchar(max), 'X'), {len})")
                .ReadSingleAsync<string>();

            result.ShouldBe(new string('X', len));
        }

        [TestMethod]
        public void ReadSingleChain()
        {
            int a1 = 0;
            int a2 = 0;
            int a3 = 0;

            using var connection = connect();

            connection.Sql("SELECT 1; SELECT 2; SELECT 3;")
                .ReadSingle(() => a1)
                .ReadSingle(() => a2)
                .ReadSingle(() => a3);

            a1.ShouldBe(1);
            a2.ShouldBe(2);
            a3.ShouldBe(3);
        }

        [TestMethod]
        public async Task ReadSingleChainAsync()
        {
            int a1 = 0;
            int a2 = 0;
            int a3 = 0;

            using var connection = connect();

            await connection.Sql("SELECT 1; SELECT 2; SELECT 3;")
                .ReadSingleAsync(() => a1)
                .ReadSingleAsync(() => a2)
                .ReadSingleAsync(() => a3);

            a1.ShouldBe(1);
            a2.ShouldBe(2);
            a3.ShouldBe(3);
        }

        private ISqlezeBuilder sqleze()
        {
            var container = new Container();

            container.RegisterSqleze();
            container.RegisterTestSettings();

            return container.Resolve<ISqlezeBuilder>();
        }


        private ISqlezeConnection connect()
        {
            var container = new Container();

            container.RegisterSqleze();
            container.RegisterTestSettings();

            return container.Resolve<ISqlezeBuilder>()
                .Connect();
        }
    }
}
