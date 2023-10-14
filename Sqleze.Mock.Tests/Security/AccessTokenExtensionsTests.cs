using DryIoc;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.Tests.Security;

[TestClass]
public class AccessTokenExtensionsTests
{
    [TestMethod]
    public void AccessTokenAtRoot()
    {
        using var container = DI.NewContainer().WithNSubstituteFallback();

        container.RegisterSqlezeCore();
        container.RegisterSqlezeConnectionStringProviders();

        // Make a singleton MS.SqlConnection
        var sqlConnection = Substitute.For<MS.SqlConnection>();
        container.RegisterInstance<MS.SqlConnection>(sqlConnection);

        _ = sqlConnection.DidNotReceive().AccessToken;

        var sqlezeBuilder = container.Resolve<ISqlezeBuilder>();

        sqlezeBuilder
            .WithConnectionString("FAKE")
            .WithAccessToken("XXX111")
            .Connect().Sql("").ExecuteNonQuery();

        Received.InOrder(() =>
        {
            _ = sqlConnection.Received(1).AccessToken = "XXX111";
            sqlConnection.Open();
        });
    }

    [TestMethod]
    public void AccessTokenAfterBuilder()
    {
        using var container = DI.NewContainer().WithNSubstituteFallback();

        container.RegisterSqlezeCore();
        container.RegisterSqlezeConnectionStringProviders();

        // Make a singleton MS.SqlConnection
        var sqlConnection = Substitute.For<MS.SqlConnection>();
        container.RegisterInstance<MS.SqlConnection>(sqlConnection);

        _ = sqlConnection.DidNotReceive().AccessToken;

        var sqlezeBuilder = container.Resolve<ISqlezeBuilder>();

        sqlezeBuilder
            .WithConnectionString("FAKE")
            .Build()
            .WithAccessToken("XXX111")
            .Connect().Sql("").ExecuteNonQuery();

        Received.InOrder(() =>
        {
            _ = sqlConnection.Received(1).AccessToken = "XXX111";
            sqlConnection.Open();
        });
    }
}
