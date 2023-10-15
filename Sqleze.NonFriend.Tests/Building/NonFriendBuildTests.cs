using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using TestCommon.Config;
using TestCommon.TestUtil;

namespace Sqleze.NonFriend.Tests.Building;

[TestClass]
public class NonFriendBuildTests
{
    // Think I should rename ISqleze to ISqlezeFactory
    // Rename SqlezeRoot to Sqleze ?
    // Would run in to namespace problems?

    [TestMethod]
    public void NonFriendConnect()
    {
        using var conn = Sqleze.Root.Connect(TestSettings.ConnectionString);

        conn.Sql("SELECT 1234").ReadSingle<int>().ShouldBe(1234);
    }

    [TestMethod]
    public void NonFriendBuild()
    {
        var sqleze = Sqleze.Root.Builder.Build();

        using var conn = sqleze.Connect();

        conn.Sql("SELECT 1234").ReadSingle<int>().ShouldBe(1234);
    }


    [TestMethod]
    public void NonFriendFactory()
    {
        var sqleze = Sqleze.Root.Factory;

        using var conn = sqleze.Connect();

        conn.Sql("SELECT 1234").ReadSingle<int>().ShouldBe(1234);
    }

}
