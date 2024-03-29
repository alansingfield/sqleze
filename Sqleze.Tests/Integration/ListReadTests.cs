﻿using Shouldly;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Integration;

[TestClass]
public class ListReadTests
{
    [TestMethod]
    public void ListReadChain()
    {
        using var conn = Connect();

        List<int>? l1 = null;
        List<int>? l2 = null;

        conn.Sql(@"
            SELECT 1 UNION SELECT 2;
            SELECT 3 UNION SELECT 4;")
            .ReadList(() => l1!)
            .ReadList(() => l2!);

        l1.ShouldBe(new List<int> { 1, 2 });
        l2.ShouldBe(new List<int> { 3, 4 });
    }

    [TestMethod]
    public async Task ListReadChainAsync()
    {
        using var conn = Connect();

        List<int>? l1 = null;
        List<int>? l2 = null;

        await conn.Sql(@"
            SELECT 1 UNION SELECT 2;
            SELECT 3 UNION SELECT 4;")
            .ReadListAsync(() => l1!)
            .ReadListAsync(() => l2!);

        l1.ShouldBe(new List<int> { 1, 2 });
        l2.ShouldBe(new List<int> { 3, 4 });
    }


    [TestMethod]
    public void ListReadChainOut()
    {
        using var conn = Connect();

        conn.Sql(@"

            SELECT 1 UNION SELECT 2;

            SELECT 'A' UNION SELECT 'B';

        ")
            .ReadList<int>(out var l1)
            .ReadList<string>(out var l2);

        l1.ShouldBe(new List<int> { 1, 2 });
        l2.ShouldBe(new List<string> { "A", "B" });
    }
}
