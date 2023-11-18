using Microsoft.SqlServer.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.TestUtil;

namespace Sqleze.SpatialTypes.Tests.Integration
{
    
    [TestClass]
    public class SpatialDictionaryReadTests
    {
        [TestMethod]
        public void DictionaryReadSqlHierarchyId()
        {
            using var connection = connect();

            var result = connection
                .Sql(@"
                    DECLARE @x hierarchyid;
                    SET @x = '/1/';
                    SELECT result = @x;
                ")
                .ReadSingle<Dictionary<string, object?>>();

            result["result"].ShouldBeOfType<SqlHierarchyId>();
            var x = (SqlHierarchyId?)result["result"];
            x.ShouldNotBeNull();
            x.ToString().ShouldBe("/1/");
        }

        [TestMethod]
        public void DictionaryReadSqlHierarchyIdMultiField()
        {
            using var connection = connect();

            var result = connection
                .Sql(@"
                    DECLARE @x hierarchyid;
                    SET @x = '/1/';
                    SELECT other = 'Hello', result = @x;
                ")
                .ReadSingle<Dictionary<string, object?>>();

            result["result"].ShouldBeOfType<SqlHierarchyId>();
            var x = (SqlHierarchyId?)result["result"];
            x.ShouldNotBeNull();
            x.ToString().ShouldBe("/1/");

            result["other"].ShouldBeOfType<string>();
            var y = (string?)result["other"];
            y.ShouldNotBeNull();
            y.ShouldBe("Hello");

        }



        private ISqlezeConnection connect()
        {
            var container = new Container();

            container.RegisterSqleze();
            container.RegisterSpatialTypes();
            container.RegisterTestSettings();

            return container.Resolve<ISqlezeBuilder>()
                .WithConfigKey("DefaultConnection")
                .Connect();
        }
    }
}
