using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.ConnectionStrings;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sqleze.Tests.Building.CommandBuilderTests;

namespace Sqleze.Tests.Building
{
    [TestClass]
    public class ReaderBuilderTests
    {
        [TestMethod]
        public void ReaderBuilder1()
        {

            var container = new Container().WithNSubstituteFallback();

            container.RegisterSqleze();
            container.RegisterTestSettings();

            var factory = container.Resolve<ISqlezeBuilder>().WithConfigKey("ConnectionString");

            var connection = factory
                .Build()
                .Connect();

            var rdr = connection
                .Sql("SELECT 0; SELECT 'Foo';")
                .ExecuteReader();

            var result1 = rdr.OpenRowset<int>().Enumerate().ToArray();
            var result2 = rdr.OpenRowset<string>().Enumerate().ToArray();

        }
    }
}
