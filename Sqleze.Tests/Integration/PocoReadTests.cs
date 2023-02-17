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
    public class PocoReadTests
    {
        [TestMethod]
        public void PocoRead1()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT name = 'Robot', number = 123")
                .ExecuteReader()
                .ReadSingle<ClassOne>();

            result.Name.ShouldBe("Robot");
            result.Number.ShouldBe(123);
        }

        [TestMethod]
        public void PocoReadNamingConvention()
        {
            using var connection = openSqleze()
                .WithCamelUnderscoreNaming()
                .Connect();

            var result = connection.Sql("SELECT first_name = 'Robot', last_name = 'X'")
                .ExecuteReader()
                .ReadSingle<ClassTwo>();

            result.FirstName.ShouldBe("Robot");
            result.LastName.ShouldBe("X");
        }

        [TestMethod]
        public void PocoReadNamingConventionFlipCommand()
        {
            using var connection = openSqleze()
                .WithCamelUnderscoreNaming()
                .Connect();

            var result = connection
                .WithNeutralNaming()    // At command builder level
                .Sql("SELECT FirstName = 'Robot', LastName = 'X'")
                .ExecuteReader()
                .ReadSingle<ClassTwo>();

            result.FirstName.ShouldBe("Robot");
            result.LastName.ShouldBe("X");
        }

        [TestMethod]
        public void PocoReadNamingConventionFlipReader()
        {
            using var connection = openSqleze()
                .WithCamelUnderscoreNaming()
                .Connect();

            var result = connection
                .Sql("SELECT FirstName = 'Robot', LastName = 'X'")
                .WithNeutralNaming()    // At reader level
                .ExecuteReader()
                .ReadSingle<ClassTwo>();

            result.FirstName.ShouldBe("Robot");
            result.LastName.ShouldBe("X");
        }


        [TestMethod]
        public void PocoReadBlankColumnNames()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT name = 'Robot', number = 123, 'Blank1', 'No column name'")
                .ExecuteReader()
                .ReadSingle<ClassOne>();

            result.Name.ShouldBe("Robot");
            result.Number.ShouldBe(123);
        }

        [TestMethod]
        public void PocoReadBlankColumnNamesFailOnUnnamedUmapped()
        {
            using var connection = connect();

            Should.Throw(() =>
            {
                connection.Sql("SELECT name = 'Robot', number = 123, 'Blank1', 'No column name'")
                    .WithUnmappedColumnsPolicy(throwOnUnnamed: true)
                    .ExecuteReader()
                    .ReadSingle<ClassOne>();
            }, typeof(Exception)).Message.ShouldBe("Column '' at position 3 was returned but no property maps to it.\r\nColumn '' at position 4 was returned but no property maps to it.");
        }

        [TestMethod]
        public void PocoReadBlankColumnNamesFailOnNamedUnmapped()
        {
            using var connection = connect();

            Should.Throw(() =>
            {
                connection.Sql("SELECT name = 'Robot', number = 123, bad_column = 999")
                    .WithUnmappedColumnsPolicy(throwOnNamed: true)
                    .ExecuteReader()
                    .ReadSingle<ClassOne>();
            }, typeof(Exception)).Message.ShouldBe("Column 'bad_column' at position 3 was returned but no property maps to it.");
        }

        [TestMethod]
        public void PocoReadDuplicateMap()
        {
            using var connection = connect();

            Should.Throw(() =>
            {
                connection.Sql("SELECT name = 'Robot', name = 'Banana', number = 123")
                    .WithDuplicateColumnsPolicy(throwIfDuplicate: true)
                    .ExecuteReader()
                    .ReadSingle<ClassOne>();
            }, typeof(Exception)).Message.ShouldBe("Multiple columns cannot map to the same property [Name] -> name, name.");
        }

        [TestMethod]
        public void PocoReadDuplicateMapIgnore()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT name = 'Robot', name = 'Banana', number = 123")
                .WithDuplicateColumnsPolicy(throwIfDuplicate: false)
                .ExecuteReader()
                .ReadSingle<ClassOne>();

            // We ignore duplicate columns and get the value from the leftmost one.
            result.Name.ShouldBe("Robot");
            result.Number.ShouldBe(123);
        }

        [TestMethod]
        public void PocoReadStringNotNull()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT first_name = '', last_name = NULL")
                .WithCamelUnderscoreNaming()
                .ExecuteReader()
                .ReadSingle<ClassThree>();

            // These are non-nullable strings so we will get empty string instead of null.
            result.FirstName.ShouldBe("");
            result.LastName.ShouldBe("");
        }

        [TestMethod]
        public void PocoReadStringValuesNotNull()
        {
            using var connection = connect();

            var result = connection.Sql("SELECT first_name = 'A', last_name = 'B'")
                .WithCamelUnderscoreNaming()
                .ExecuteReader()
                .ReadSingle<ClassThree>();

            // Make sure we can read populated values into non-nullables.
            result.FirstName.ShouldBe("A");
            result.LastName.ShouldBe("B");
        }

        public class ClassOne
        {
            public string? Name { get; set; }

            public int Number { get; set; }
        }

        public class ClassTwo
        {
            public string? FirstName { get; set; }

            public string? LastName { get; set; }
        }
        public class ClassThree
        {
            public string FirstName { get; set; } = "";

            public string LastName { get; set; } = "";
        }



        private ISqlezeConnection connect()
        {
            return openSqleze().Connect();
        }

        private static ISqlezeBuilder openSqleze()
        {
            var container = new Container();

            container.RegisterSqleze();
            container.RegisterTestSettings();

            return container.Resolve<ISqlezeBuilder>();
        }
    }
}
