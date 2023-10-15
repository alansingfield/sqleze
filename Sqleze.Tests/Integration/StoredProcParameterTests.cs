using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using Sqleze.NamingConventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Config;
using UnitTestCoder.Shouldly.Gen;

namespace Sqleze.Tests.Integration;

[TestClass]
public class StoredProcParameterTests
{
    [TestMethod]
    public void StoredProcParameterFindsTable()
    {
        using var conn = connect();

        var foos = new List<FooInit>()
        {
            new()
            {
                FooId = 1,
                FooName = "ABC"
            },
            new()
            {
                FooId = 2,
                FooName = "CDE"
            },
        };

        // ALTER PROCEDURE [test].[p_check_parameter_table_entities]
        // (
        //     @foos test.tt_p_check_parameter_table_entities_foos READONLY
        // )
        // AS
        // BEGIN
        //     SELECT  foo_id = foo_id + 100,
        //             foo_name = foo_name + 'X'
        //     FROM    @foos
        //     ;
        // END

        var result = conn.StoredProc("test.p_check_parameter_table_entities")
            .Parameters.Set(() => foos)
            .ReadList<FooInit>();

        //ShouldlyTest.Gen(result, nameof(result));

        {
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result[0].ShouldNotBeNull();
            result[0].FooId.ShouldBe(101);
            result[0].FooName.ShouldBe("ABCX");
            result[1].ShouldNotBeNull();
            result[1].FooId.ShouldBe(102);
            result[1].FooName.ShouldBe("CDEX");
        }
    }


    [TestMethod]
    public void StoredProcParameterRecord()
    {
        using var conn = connect();

        var foos = new List<FooRecord>()
        {
            new(1, "ABC"),
            new(2, "CDE")
        };

        var result = conn.StoredProc("test.p_check_parameter_table_entities")
            .Parameters.Set(() => foos)
            .ReadList<FooRecord>();

        //ShouldlyTest.Gen(result, nameof(result));

        {
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result[0].ShouldNotBeNull();
            result[0].FooId.ShouldBe(101);
            result[0].FooName.ShouldBe("ABCX");
            result[1].ShouldNotBeNull();
            result[1].FooId.ShouldBe(102);
            result[1].FooName.ShouldBe("CDEX");
        }
    }

    [TestMethod]
    public void StoredProcParameterGetSet()
    {
        using var conn = connect();

        var foos = new List<FooGetSet>()
        {
            new()
            {
                FooId = 1,
                FooName = "ABC"
            },
            new()
            {
                FooId = 2,
                FooName = "CDE"
            },
        };

        // ALTER PROCEDURE [test].[p_check_parameter_table_entities]
        // (
        //     @foos test.tt_p_check_parameter_table_entities_foos READONLY
        // )
        // AS
        // BEGIN
        //     SELECT  foo_id = foo_id + 100,
        //             foo_name = foo_name + 'X'
        //     FROM    @foos
        //     ;
        // END

        var result = conn.StoredProc("test.p_check_parameter_table_entities")
            .Parameters.Set(() => foos)
            .ReadList<FooGetSet>();

        //ShouldlyTest.Gen(result, nameof(result));

        {
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result[0].ShouldNotBeNull();
            result[0].FooId.ShouldBe(101);
            result[0].FooName.ShouldBe("ABCX");
            result[1].ShouldNotBeNull();
            result[1].FooId.ShouldBe(102);
            result[1].FooName.ShouldBe("CDEX");
        }
    }

    [TestMethod]
    public void StoredProcParameterConsOnly()
    {
        using var conn = connect();

        var foos = new List<FooConsOnly>()
        {
            new(1, "ABC"),
            new(2, "CDE")
        };

        var result = conn.StoredProc("test.p_check_parameter_table_entities")
            .Parameters.Set(() => foos)
            .ReadList<FooConsOnly>();

        //ShouldlyTest.Gen(result, nameof(result));

        {
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result[0].ShouldNotBeNull();
            result[0].FooId.ShouldBe(101);
            result[0].FooName.ShouldBe("ABCX");
            result[1].ShouldNotBeNull();
            result[1].FooId.ShouldBe(102);
            result[1].FooName.ShouldBe("CDEX");
        }
    }

    [TestMethod]
    public void StoredProcParameterNamingConvention()
    {
        var container = openContainer();

        using var conn = container.Resolve<ISqlezeBuilder>()
            .Connect();

        var foos = new List<FooUnderscore>()
        {
            new()
            {
                foo_id = 1,
                foo_name = "ABC"
            },
            new()
            {
                foo_id = 2,
                foo_name = "CDE"
            },
        };

        // ALTER PROCEDURE [test].[p_check_parameter_table_entities]
        // (
        //     @foos test.tt_p_check_parameter_table_entities_foos READONLY
        // )
        // AS
        // BEGIN
        //     SELECT  foo_id = foo_id + 100,
        //             foo_name = foo_name + 'X'
        //     FROM    @foos
        //     ;
        // END

        var result = conn.StoredProc("test.p_check_parameter_table_entities")
            .Parameters.Set(() => foos)
            .ReadList<FooUnderscore>();

        //ShouldlyTest.Gen(result, nameof(result));

        {
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result[0].ShouldNotBeNull();
            result[0].foo_id.ShouldBe(101);
            result[0].foo_name.ShouldBe("ABCX");
            result[1].ShouldNotBeNull();
            result[1].foo_id.ShouldBe(102);
            result[1].foo_name.ShouldBe("CDEX");
        }
    }


    [TestMethod]
    public void StoredProcParameterNamingConvention2()
    {
        var container = openContainer();
        
        using var conn = container.Resolve<ISqlezeBuilder>()
            .WithCamelUnderscoreNaming()
            .Connect();
        var foos = new List<FooUnderscore>()
        {
            new()
            {
                foo_id = 1,
                foo_name = "ABC"
            },
            new()
            {
                foo_id = 2,
                foo_name = "CDE"
            },
        };

        // ALTER PROCEDURE [test].[p_check_parameter_table_entities]
        // (
        //     @foos test.tt_p_check_parameter_table_entities_foos READONLY
        // )
        // AS
        // BEGIN
        //     SELECT  foo_id = foo_id + 100,
        //             foo_name = foo_name + 'X'
        //     FROM    @foos
        //     ;
        // END

        var result = conn.StoredProc("test.p_check_parameter_table_entities")
            .Parameters.WithNeutralNaming().Set(() => foos)
            .ReadList<FooGetSet>();

        //ShouldlyTest.Gen(result, nameof(result));

        {
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result[0].ShouldNotBeNull();
            result[0].FooId.ShouldBe(101);
            result[0].FooName.ShouldBe("ABCX");
            result[1].ShouldNotBeNull();
            result[1].FooId.ShouldBe(102);
            result[1].FooName.ShouldBe("CDEX");
        }
    }

    [TestMethod]
    public void StoredProcParameterNamingConvention3()
    {
        var container = openContainer();
        
        using var conn = container.Resolve<ISqlezeBuilder>()
            .WithCamelUnderscoreNaming()
            .Connect();

        var foos = new List<FooGetSet>()
        {
            new()
            {
                FooId = 1,
                FooName = "ABC"
            },
            new()
            {
                FooId = 2,
                FooName = "CDE"
            },
        };

        // ALTER PROCEDURE [test].[p_check_parameter_table_entities]
        // (
        //     @foos test.tt_p_check_parameter_table_entities_foos READONLY
        // )
        // AS
        // BEGIN
        //     SELECT  foo_id = foo_id + 100,
        //             foo_name = foo_name + 'X'
        //     FROM    @foos
        //     ;
        // END

        var result = conn.StoredProc("test.p_check_parameter_table_entities")
            .Parameters.WithCamelUnderscoreNaming().Set(() => foos)
            .WithNeutralNaming()
            .ReadList<FooUnderscore>();

        //ShouldlyTest.Gen(result, nameof(result));

        {
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
            result[0].ShouldNotBeNull();
            result[0].foo_id.ShouldBe(101);
            result[0].foo_name.ShouldBe("ABCX");
            result[1].ShouldNotBeNull();
            result[1].foo_id.ShouldBe(102);
            result[1].foo_name.ShouldBe("CDEX");
        }
    }

    private class FooInit
    {
        public int FooId { get; init; }
        public string FooName { get; init; } = "";
    }

    private class FooGetSet
    {
        public int FooId { get; set; }
        public string FooName { get; set; } = "";
    }

    private class FooConsOnly
    {
        public FooConsOnly(int fooId, string fooName)
        {
            FooId = fooId;
            FooName = fooName;
        }
        public int FooId { get; }
        public string FooName { get; }
    }

    private record FooRecord (int FooId, string FooName);

    private class FooUnderscore
    {
        public int foo_id { get; set; }
        public string foo_name { get; set; } = "";
    }

    private ISqlezeConnection connect()
    {
        return openContainer()
            .Resolve<ISqlezeBuilder>()
            .WithCamelUnderscoreNaming()
            .Connect();
    }

    private IContainer openContainer()
    {
        var container = DI.NewContainer();

        container.RegisterSqleze();
        container.RegisterTestSettings();

        return container;
    }
}
