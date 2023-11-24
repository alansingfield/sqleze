using Shouldly;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.ConnectionStrings;

namespace Sqleze.Tests.Integration;

[TestClass]
public class ArrayReadTests
{
    [TestMethod]
    public void ArrayReadChain()
    {
        using var conn = Connect();

        int[]? l1 = null;
        string[]? l2 = null;

        conn.Sql(@"

            SELECT 1 UNION SELECT 2;

            SELECT 'A' UNION SELECT 'B';

        ")
            .ReadArray(() => l1!)
            .ReadArray(() => l2!);

        l1.ShouldBe(new int[] { 1, 2 });
        l2.ShouldBe(new string[] { "A", "B" });
    }

    [TestMethod]
    public async Task ArrayReadChainAsync()
    {
        using var conn = Connect();

        int[]? l1 = null;
        string[]? l2 = null;

        await conn.Sql(@"

            SELECT 1 UNION SELECT 2;

            SELECT 'A' UNION SELECT 'B';

        ")
            .ReadArrayAsync(() => l1!)
            .ReadArrayAsync(() => l2!);

        l1.ShouldBe(new int[] { 1, 2 });
        l2.ShouldBe(new string[] { "A", "B" });
    }
    
    [TestMethod]
    public void ArrayReadChainOut()
    {
        using var conn = Connect();

        conn.Sql(@"

            SELECT 1 UNION SELECT 2;

            SELECT 'A' UNION SELECT 'B';

        ")
            .ReadArray<int>(out var l1)
            .ReadArray<string>(out var l2);

        l1.ShouldBe(new int[] { 1, 2 });
        l2.ShouldBe(new string[] { "A", "B" });
    }

    private class ReadChainModel
    {
        public int[] L1 { get; set; } = new int[0];
        public string[] L2 { get; set; } = new string[0];
    }

    [TestMethod]
    public void ArrayReadChainModel()
    {
        using var conn = Connect();

        var model = new ReadChainModel();

        conn.Sql(@"

            SELECT 1 UNION SELECT 2;

            SELECT 'A' UNION SELECT 'B';

        ")
            .ReadArray(() => model.L1)
            .ReadArray(() => model.L2);

        model.L1.ShouldBe(new int[] { 1, 2 });
        model.L2.ShouldBe(new string[] { "A", "B" });
    }
}
