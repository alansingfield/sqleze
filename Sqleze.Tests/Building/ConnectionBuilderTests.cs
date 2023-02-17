using Sqleze;
using Sqleze.ConnectionStrings;
using Shouldly;
using Sqleze.Composition;
using System.Diagnostics;
using Sqleze.DryIoc;
using Microsoft.Extensions.Configuration;

namespace Sqleze.Tests.Building;

[TestClass]
public class ConnectionBuilderTests
{

    [TestMethod]
    public void ConnectionBuilderVerbatim()
    {
        var container = new Container().WithNSubstituteFallback();

        container.RegisterSqleze();

        container.Register<ConnectionInterceptor>(Reuse.ScopedToService<IScopedSqlezeConnectionBuilder<ConnectionInterceptor>>());

        var builder = container.Resolve<ISqlezeBuilder>();

        int testIdx = 0;
        var connection = builder
            .WithConnectionString("apple")
            .With<ConnectionInterceptor>((inc, scope) =>
            {
                inc.ConnectionStringProvider.GetConnectionString().ShouldBe("apple");
                inc.ConnectionStringProvider.ShouldBeOfType<VerbatimConnectionStringProvider>();

                testIdx++;
            })
            .Connect();

        testIdx.ShouldBe(1);
    }

    [TestMethod]
    public void ConnectionBuilderConfig()
    {
        var container = new Container().WithNSubstituteFallback();

        container.RegisterSqleze();

        container.Register<ConnectionInterceptor>(Reuse.ScopedToService<IScopedSqlezeConnectionBuilder<ConnectionInterceptor>>());

        var configuration = Substitute.For<IConfiguration>();
        configuration.GetConnectionString("key1").Returns("Server=XYZ");
        container.Use(configuration);

        var builder = container.Resolve<ISqlezeBuilder>();

        int testIdx = 0;
        var connection = builder
            .WithConfigKey("key1")
            .With<ConnectionInterceptor>((inc, scope) =>
            {
                inc.ConnectionStringProvider.GetConnectionString().ShouldBe("Server=XYZ");
                inc.ConnectionStringProvider.ShouldBeOfType<ConfigConnectionStringProvider>();

                testIdx++;
            })
            .Connect();

        testIdx.ShouldBe(1);
    }


    [TestMethod]
    public void ConnectionBuilderSharedScope()
    {
        var container = new Container().WithNSubstituteFallback();

        container.RegisterSqleze();

        container.Register<ConnectionInterceptor>(Reuse.ScopedToService<IScopedSqlezeConnectionBuilder<ConnectionInterceptor>>());


        var connectionBuilder = container.Resolve<ISqlezeBuilder>();

        int testIdx = 0;
        var connectionFactory = connectionBuilder
            .With<ConnectionInterceptor>((inc, scope) =>
            {
                testIdx++;
            })
            .Build();

        testIdx.ShouldBe(1);

        var conn1 = connectionFactory.Connect();

        var conn2 = connectionFactory.Connect();

        conn1.ShouldNotBe(conn2);

        testIdx.ShouldBe(1);
    }


    public class ConnectionInterceptor 
    {
        public ConnectionInterceptor(IConnectionStringProvider connectionStringProvider)
        {
            ConnectionStringProvider = connectionStringProvider;
        }

        public IConnectionStringProvider ConnectionStringProvider { get; }
    }



}
