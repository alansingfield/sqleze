using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Config;
using Sqleze;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Types;
using Shouldly;

namespace Sqleze.SpatialTypes.Tests.Startup;

[TestClass]
public class StartupSpatialTypesTests
{
    [TestMethod]
    public void StartupSpatialTypesSimple()
    {
        using var conn = Connect();

        var result = conn.Sql(@"
                DECLARE @x hierarchyid;
                SET @x = '/1/';
                SELECT result = @x;
            ")
            .ReadSingle<SqlHierarchyId>();

        result.ToString().ShouldBe("/1/");
    }
}
