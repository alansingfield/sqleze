using Shouldly;
using Sqleze;
using Sqleze.NamingConventions;
using Sqleze.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Integration
{
    [TestClass]
    public class PocoReadRecordTests
    {
        [TestMethod]
        public void PocoReadRecord1()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT name = 'Robot', number = 123")
                .ExecuteReader()
                .ReadSingle<RecordOne>();

            result.Name.ShouldBe("Robot");
            result.Number.ShouldBe(123);
        }

        [TestMethod]
        public void PocoReadRecord2()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT wrong_field = 'Robot', number = 123")
                .WithIgnoreUnmappedProperties()
                .WithUnmappedColumnsPolicy(throwOnNamed: false)
                .ExecuteReader()
                .ReadSingle<RecordOne>();

            // Missing / extra fields are ignored (for now at least)
            result.Name.ShouldBe(null);
            result.Number.ShouldBe(123);
        }

        [TestMethod]
        public void PocoReadRecord2ThroughParameterSet()
        {
            using var connection = connect();

            int arg = 123;
            var result = connection.Sql("SELECT wrong_field = 'Robot', number = @arg")
                .Parameters.Set(() => arg)
                .WithIgnoreUnmappedProperties(true)
                .WithUnmappedColumnsPolicy(throwOnNamed: false)
                .ExecuteReader()
                .ReadSingle<RecordOne>();

            // Missing / extra fields are ignored (for now at least)
            result.Name.ShouldBe(null);
            result.Number.ShouldBe(123);
        }

        [TestMethod]
        public void PocoReadRecord2ThroughParameterCollectionSet()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT wrong_field = 'Robot', number = @arg")
                .Parameters.Set(new { arg = 123 })
                .WithIgnoreUnmappedProperties()
                .WithUnmappedColumnsPolicy(throwOnNamed: false)
                .ReadSingle<RecordOne>();

            // Missing / extra fields are ignored (for now at least)
            result.Name.ShouldBe(null);
            result.Number.ShouldBe(123);
        }

        [TestMethod]
        public void PocoReadRecord3()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT number = 123, name = 'Robot'")
                .ExecuteReader()
                .ReadSingle<RecordOne>();

            // Fields in wrong order don't matter
            result.Name.ShouldBe("Robot");
            result.Number.ShouldBe(123);
        }

        [TestMethod]
        public void PocoReadRecord4()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT number = 123, name = NULL")
                .ExecuteReader()
                .ReadSingle<RecordTwo>();

            // Because Name is a non-nullable string, SQL NULL is converted
            // to empty string.
            result.Name.ShouldBe("");
            result.Number.ShouldBe(123);
        }


        public record RecordOne(
            string? Name,
            int Number
            );
        
        public record RecordTwo(
            string Name,
            int Number
            );

        private ISqlezeConnection connect()
        {
            return openSqleze().Connect();
        }

        private static ISqlezeBuilder openSqleze()
        {
            var container = DI.NewContainer();

            container.RegisterSqleze();
            container.RegisterTestSettings();

            return container.Resolve<ISqlezeBuilder>();
        }
    }
}
