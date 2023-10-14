using Microsoft.SqlServer.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.TestUtil;
using UnitTestCoder.Shouldly.Gen;

namespace Sqleze.SpatialTypes.Tests.Integration
{
    [TestClass]
    public class SpatialScalarReadTests
    {

        [TestMethod]
        public void ScalarReadSqlHierarchyId()
        {
            using var connection = connect();

            var result = connection
                .Sql(@"
                    DECLARE @x hierarchyid;
                    SET @x = '/1/';
                    SELECT result = @x;
                ")
                .ReadSingle<SqlHierarchyId>();

            result.ToString().ShouldBe("/1/");
        }

        [TestMethod]
        public void ScalarReadSqlHierarchyIdAsString()
        {
            using var connection = connect();

            var result = connection
                .Sql(@"
                    DECLARE @x hierarchyid;
                    SET @x = '/1/';
                    SELECT result = @x;
                ")
                .ReadSingle<string>();

            result.ShouldBe("/1/");
        }

        [TestMethod]
        public void ScalarReadSqlHierarchyIdAsByteArray()
        {
            using var connection = connect();

            var result = connection
                .Sql(@"
                    DECLARE @x hierarchyid;
                    SET @x = '/1/';
                    SELECT result = @x;
                ")
                .ReadSingle<byte[]>();

            //ShouldlyTest.Gen(result, nameof(result));

            {
                result.ShouldBe(new byte[] { 0x58, });
            }
        }

        private ISqlezeConnection connect()
        {
            var container = DI.NewContainer();

            container.RegisterSqleze();
            container.RegisterSpatialTypes();
            container.RegisterTestSettings();

            return container.Resolve<ISqlezeBuilder>()
                .Connect();
        }
    }
}
