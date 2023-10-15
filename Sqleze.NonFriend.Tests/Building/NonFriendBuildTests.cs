using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using TestCommon.Config;
using TestCommon.TestUtil;
using Sqleze;

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
        using var conn = SqlezeRoot.Builder.Connect(TestSettings.ConnectionString);

        conn.Sql("SELECT 1234").ReadSingle<int>().ShouldBe(1234);
    }

    [TestMethod]
    public void NonFriendFactory()
    {
        var builder = SqlezeRoot.Builder.WithConnectionString(TestSettings.ConnectionString);

        var factory1 = builder.Build();
        var factory2 = builder.Build();

        using var conn1 = factory1.Connect();
        using var conn2 = factory2.Connect();

        conn1.Sql("SELECT 1234").ReadSingle<int>().ShouldBe(1234);
    }
    
    [TestMethod]
    public void NonFriendBuild()
    {
        var factory = SqlezeRoot.Builder.WithConnectionString(TestSettings.ConnectionString).Build();

        using var conn = factory.Connect();

        conn.Sql("SELECT 1234").ReadSingle<int>().ShouldBe(1234);
    }
    
    [TestMethod]
    public void NonFriendConfigKeyCeremony()
    {
        var configuration = ConfigurationFactory.New(new[] {"serverSettings.json" });

        var factory = SqlezeRoot.Builder
            .WithConfiguration(configuration)
            .WithConfigKey("ConnectionString").Build();

        using var conn = factory.Connect();

        conn.Sql("SELECT 1234").ReadSingle<int>().ShouldBe(1234);
    }

}
