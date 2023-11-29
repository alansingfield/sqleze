using Shouldly;
using Sqleze;
using Sqleze.NamingConventions;
using Sqleze.RowsetMetadata;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestCoder.Shouldly.Gen;

namespace Sqleze.Tests.Integration
{
    [TestClass]
    public class ParameterTests
    {
        [TestMethod]
        public void ParameterSet1()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT arg = @arg");

            cmd.Parameters
                .Set("@arg", 123);

            cmd.ReadSingle<int>().ShouldBe(123);
        }

        [TestMethod]
        public void ParameterSet2()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT foo = @foo, bar = @bar");

            cmd.Parameters
                .Set("@foo", 123)
                .Set("@bar", "Hello");

            var foobar = cmd.ReadSingle<FooBar>();

            foobar.Foo.ShouldBe(123);
            foobar.Bar.ShouldBe("Hello");
        }

        [TestMethod]
        public void ParameterSet3()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT foo = @foo, bar = @bar");

            cmd.Parameters
                .Set(new
                {
                    Foo = 123,
                    Bar = "Hello"
                });

            var foobar = cmd.ReadSingle<FooBar>();

            foobar.Foo.ShouldBe(123);
            foobar.Bar.ShouldBe("Hello");
        }

        [TestMethod]
        public void ParameterSet4()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT foo = @foo, bar = @bar");

            cmd.Parameters
                .Set(new
                {
                    Foo = 123,
                    Bar = "Hello"
                })
                .Set(new
                {
                    Foo = 5455,
                })
                ;

            var foobar = cmd.ReadSingle<FooBar>();

            foobar.Foo.ShouldBe(5455);
            foobar.Bar.ShouldBe("Hello");
        }

        [TestMethod]
        public void ParameterSet5()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT foo = @foo, bar = @bar");

            var foo = 5641;
            var bar = "hello";

            // Name is taken from lambda expression
            cmd.Parameters
                .Set(() => foo)
                .Set(() => bar)
                ;

            var foobar = cmd.ReadSingle<FooBar>();

            foobar.Foo.ShouldBe(5641);
            foobar.Bar.ShouldBe("hello");
        }

        [TestMethod]
        public void ParameterSet6()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT FirstName = @first_name, LastName = @last_name");

            var scopedSqlezeParameterFactory =
                cmd.Parameters
                    .WithCamelUnderscoreNaming()
                    .Build()
                    .Set("FirstName", "John")
                    .Set("LastName", "Doe");

            var foobar = cmd.ReadSingle<NamingTest>();

            foobar.FirstName.ShouldBe("John");
            foobar.LastName.ShouldBe("Doe");
        }

        [TestMethod]
        public void ParameterSet7()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT FirstName = @first_name, LastName = @last_name");

            var scopedSqlezeParameterFactory =
                cmd.Parameters
                    .WithCamelUnderscoreNaming()
                    .Build()
                    .Set(new 
                    {
                        FirstName = "John",
                        LastName = "Doe",
                    });

            var foobar = cmd.ReadSingle<NamingTest>();

            foobar.FirstName.ShouldBe("John");
            foobar.LastName.ShouldBe("Doe");
        }

        [TestMethod]
        public void ParameterSet8()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT FirstName = @first_name, LastName = @last_name");

            var sqlezeParameter =
                cmd.Parameters
                    .WithCamelUnderscoreNaming()
                    .Build()
                    .Set("FirstName", "John", exitContext: true)
                    .Set("Last_Name", "Doe");

            var foobar = cmd.ReadSingle<NamingTest>();

            foobar.FirstName.ShouldBe("John");
            foobar.LastName.ShouldBe("Doe");
        }

        [TestMethod]
        public void ParameterSet9()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT FirstName = @first_name, LastName = @last_name");

            var foo = cmd.Parameters
                .WithCamelUnderscoreNaming()
                .Set(new
                {
                    FirstName = "John",
                    LastName = "Doe",
                });

            var foobar = cmd.ReadSingle<NamingTest>();

            foobar.FirstName.ShouldBe("John");
            foobar.LastName.ShouldBe("Doe");
        }

        [TestMethod]
        public void ParameterSet10()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT FirstName = @first_name, LastName = @last_name");

            var foo = cmd.Parameters
                .WithCamelUnderscoreNaming()
                .Set(new
                {
                    FirstName = "John",
                    LastName = "Doe",
                })
                .Set(new
                {
                    FirstName = "Jane",
                }
                );

            var foobar = cmd.ReadSingle<NamingTest>();

            foobar.FirstName.ShouldBe("Jane");
            foobar.LastName.ShouldBe("Doe");
        }

        [TestMethod]
        public void ParameterSetSqlValue()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT @arg");

            SqlString arg = "Hello";    // implicit conversion
            var foo = cmd.Parameters
                .Set(() => arg);

            var result = cmd.ReadSingle<string>();

            result.ShouldBe("Hello");
        }

        [TestMethod]
        public void ParameterSetSqlValueDbNull()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT @arg");

            SqlString arg = SqlString.Null;
            
            cmd.Parameters
                .Set(() => arg);

            var result = cmd.ReadSingleNullable<string?>();

            result.ShouldBe(null);
        }

        [TestMethod]
        public void ParameterSetSqlValueNull()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT @arg");

            SqlString? arg = null;
            var result = connection
                .Sql("SELECT @arg")
                .Parameters.Set(() => arg)
                .ReadSingleNullable<string?>();

            result.ShouldBe(null);
        }


        [TestMethod]
        public void ParameterSetValueReadAtCorrectPoint()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT @arg");

            string arg = "Hello";
            var foo = cmd.Parameters
                .Set(() => arg);

            arg = "Changing after set should keep original value";

            var result = cmd.ReadSingle<string>();

            result.ShouldBe("Hello");
        }


        [TestMethod]
        public void ParameterSetValueAmendAfterSet()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT @arg");

            string arg = "Hello";
            var foo = cmd.Parameters
                .Set(() => arg);

            // Retrieve the parameter from the collection and change it.
            cmd.Parameters.Get(nameof(arg)).Value = "Bye";

            var result = cmd.ReadSingle<string>();

            result.ShouldBe("Bye");
        }


        [TestMethod]
        public void QuantizeSize1()
        {
            var builder = OpenBuilder();

            using var connection = builder
                .WithPreferredNVarCharSizes(new[] { 10, 20, 50, 100, 4000 })
                .Connect();

            string arg = "Hello";

            var cmd = connection.Sql("SELECT @arg");
            cmd.Parameters.Set(new { arg });

            var rdr = cmd.ExecuteReader();
            var rowset = rdr.OpenRowset<string>();

            var schema = rowset.GetFieldSchemas().First();
            schema.Size.ShouldBe(10);

            var result = rowset.Enumerate().Single();

            result.ShouldBe("Hello");
        }

        [TestMethod]
        public void QuantizeSize2()
        {
            var builder = OpenBuilder();

            // Setting sizes at the connection level will be overridden
            using var connection = builder
                .WithPreferredNVarCharSizes(new[] { 10, 20, 50, 100, 4000 })
                .Connect();

            var cmd = connection.Sql("SELECT @arg");

            string arg = "Hello";
            cmd.Parameters
                // override at the parameter collection level.
                .WithPreferredNVarCharSizes(new[] { 100, 200 })
                .Set(() => arg);

            var rdr = cmd.ExecuteReader();
            var rowset = rdr.OpenRowset<string>();

            var schema = rowset.GetFieldSchemas().First();
            schema.Size.ShouldBe(100);

            var result = rowset.Enumerate().Single();

            result.ShouldBe("Hello");
        }

        [TestMethod]
        public void QuantizeSizeMaxNVarChar()
        {
            var builder = OpenBuilder();

            // Note - we've not put in 4000 as a preferred size. This will
            // be put in for us automatically.
            using var connection = builder
                .WithPreferredNVarCharSizes(new[] { 10, 20, 50, 100 })
                .Connect();

            var cmd = connection.Sql("SELECT @arg");

            string arg = new string('X', 4001);
            cmd.Parameters.Set(() => arg);

            var rdr = cmd.ExecuteReader();
            var rowset = rdr.OpenRowset<string>();

            var schema = rowset.GetFieldSchemas().First();
            schema.SqlDataType().ShouldBe("nvarchar(max)");

            var result = rowset.Enumerate().Single();

            result.Length.ShouldBe(4001);
        }

        [TestMethod]
        public void QuantizeSizeMaxVarChar()
        {
            var builder = OpenBuilder();

            // Note - we've not put in 8000 as a preferred size. This will
            // be put in for us automatically.
            using var connection = builder
                .WithPreferredVarCharSizes(new[] { 10, 20, 50, 100 })
                .Connect();

            var cmd = connection.Sql("SELECT @arg");

            string arg = new string('X', 8001);
            cmd.Parameters.Set(() => arg).AsVarChar();

            var rdr = cmd.ExecuteReader();
            var rowset = rdr.OpenRowset<string>();

            var schema = rowset.GetFieldSchemas().First();
            schema.SqlDataType().ShouldBe("varchar(max)");

            var result = rowset.Enumerate().Single();

            result.Length.ShouldBe(8001);
        }


        [TestMethod]
        public void QuantizeSizeDecimal()
        {
            var builder = OpenBuilder();

            using var connection = builder
                .WithPreferredNumericPrecisionScales(new[] { (2, 0), (5, 3), (10, 5) })
                .Connect();

            var cmd = connection.Sql("SELECT @arg");

            decimal arg = 1.000m;
            cmd.Parameters.Set(() => arg);

            var rdr = cmd.ExecuteReader();
            var rowset = rdr.OpenRowset<decimal>();

            var schema = rowset.GetFieldSchemas().First();
            schema.SqlDataType().ShouldBe("decimal(5,3)");

            var result = rowset.Enumerate().Single();

            result.ShouldBe(1.000m);
        }

        [TestMethod]
        public void QuantizeSizeDecimalBadSizes1()
        {
            var builder = OpenBuilder();

            Should.Throw(() =>
            {
                builder
                    .WithPreferredNumericPrecisionScales(new[] { (0, 0) });
            }, typeof(Exception))
                .Message.ShouldBe("A precision / scale of (0,0) is not valid");
        }

        [TestMethod]
        public void QuantizeSizeDecimalBadSizes2()
        {
            var builder = OpenBuilder();

            Should.Throw(() =>
            {
                builder
                    .WithPreferredNumericPrecisionScales(new[] { (3, 0), (39, 3) });
            }, typeof(Exception))
                .Message.ShouldBe("A precision / scale of (39,3) is not valid");
        }

        [TestMethod]
        public void QuantizeSizeDecimalBadSizes3()
        {
            var builder = OpenBuilder();

            Should.Throw(() =>
            {
                builder
                    .WithPreferredNumericPrecisionScales(new[] { (-1, 0) });
            }, typeof(Exception))
                .Message.ShouldBe("A precision / scale of (-1,0) is not valid");
        }

        [TestMethod]
        public void QuantizeSizeDecimalBadSizes4()
        {
            var builder = OpenBuilder();

            Should.Throw(() =>
            {
                builder
                    .WithPreferredNumericPrecisionScales(new[] { (6, 7) });
            }, typeof(Exception))
                .Message.ShouldBe("A precision / scale of (6,7) is not valid");
        }



        [TestMethod]
        public void PreferDateTimeTest1()
        {
            var builder = OpenBuilder();

            using var connection = builder
                .WithPreferDateTime()
                .Connect();

            var cmd = connection.Sql("SELECT @arg");

            DateTime arg = DateTime.Parse("2023-01-20T19:01:55.1244567");
            cmd.Parameters.Set(() => arg);

            var rdr = cmd.ExecuteReader();
            var rowset = rdr.OpenRowset<DateTime>();

            var schema = rowset.GetFieldSchemas().First();
            schema.SqlDataType().ShouldBe("datetime");

            var result = rowset.Enumerate().Single();

            result.ShouldBe(DateTime.Parse("2023-01-20T19:01:55.1230000"));
        }

        [TestMethod]
        public void PreferDateTimeTest2()
        {
            var builder = OpenBuilder();

            using var connection = builder
                .WithPreferDateTime2(5)
                .Connect();

            var cmd = connection.Sql("SELECT @arg");

            DateTime arg = DateTime.Parse("2023-01-20T19:01:55.1244567");
            cmd.Parameters.Set(() => arg);

            var rdr = cmd.ExecuteReader();
            var rowset = rdr.OpenRowset<DateTime>();

            var schema = rowset.GetFieldSchemas().First();
            schema.SqlDataType().ShouldBe("datetime2(5)");

            var result = rowset.Enumerate().Single();

            result.ShouldBe(DateTime.Parse("2023-01-20T19:01:55.1244500"));
        }

        [TestMethod]
        public void PreferDateTimeTest3()
        {
            var builder = OpenBuilder();

            using var connection = builder
                .WithPreferDateTime2()
                .Connect();

            var cmd = connection.Sql("SELECT @arg");

            DateTime arg = DateTime.Parse("2023-01-20T19:01:55.1244567");
            cmd.Parameters.Set(() => arg);

            var rdr = cmd.ExecuteReader();
            var rowset = rdr.OpenRowset<DateTime>();

            var schema = rowset.GetFieldSchemas().First();
            schema.SqlDataType().ShouldBe("datetime2(7)");

            var result = rowset.Enumerate().Single();

            result.ShouldBe(DateTime.Parse("2023-01-20T19:01:55.1244567"));
        }


        [TestMethod]
        public void ParameterSetAndChange()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT arg = @arg");

            int arg = 123;
            cmd.Parameters.Set(() => arg);

            cmd.ReadSingle<int>().ShouldBe(123);

            arg = 456;

            // Parameter value was copied in Set() call, so new value not
            // used yet.
            cmd.ReadSingle<int>().ShouldBe(123);

            // Now we update the value
            cmd.Parameters.Set(() => arg);

            // New value is retrieved.
            cmd.ReadSingle<int>().ShouldBe(456);
        }

        [TestMethod]
        public void ParameterSetChainRead()
        {
            using var connection = Connect();

            var cmd = connection.Sql("SELECT arg = @arg");

            int arg = 123;
            int result = cmd.Parameters.Set(() => arg).ReadSingle<int>();

            result.ShouldBe(123);
        }

        [TestMethod]
        public async Task ParameterSetChainReadAsync()
        {
            using var connection = Connect();

            var fooBar = new FooBar() { Foo = 1, Bar = "A" };

            var model = new FooBarModel();

            await connection.Sql(
                "SELECT foo = @foo + 1, bar = @bar + 'B' "
                )
                .Parameters
                    .Set(() => fooBar.Foo)
                    .Set(() => fooBar.Bar)
                .ReadSingleOrDefaultAsync(() => model.FooBarInstance);

            model.FooBarInstance.ShouldNotBeNull();
            model.FooBarInstance.Foo.ShouldBe(2);
            model.FooBarInstance.Bar.ShouldBe("AB");
        }

        private class FooBar
        {
            public int Foo { get; set; }
            public string? Bar { get; set; }
        }

        private class FooBarModel
        {
            public FooBar? FooBarInstance { get; set; }
        }

        private class NamingTest
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
        }

    }
}
