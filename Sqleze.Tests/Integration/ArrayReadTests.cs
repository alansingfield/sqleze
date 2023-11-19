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
        int[]? l2 = null;

        conn.Sql(@"
            SELECT 1 UNION SELECT 2;
            SELECT 3 UNION SELECT 4;")
            .ReadArray(() => l1!)
            .ReadArray(() => l2!);

        l1.ShouldBe(new int[] { 1, 2 });
        l2.ShouldBe(new int[] { 3, 4 });
    }

    [TestMethod]
    public async Task ArrayReadChainAsync()
    {
        using var conn = Connect();

        int[]? l1 = null;
        int[]? l2 = null;

        await conn.Sql(@"
            SELECT 1 UNION SELECT 2;
            SELECT 3 UNION SELECT 4;")
            .ReadArrayAsync(() => l1!)
            .ReadArrayAsync(() => l2!);

        l1.ShouldBe(new int[] { 1, 2 });
        l2.ShouldBe(new int[] { 3, 4 });
    }

}
