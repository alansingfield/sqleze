using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Sqleze.Registration;
using Shouldly;

namespace Sqleze.Microsoft.DependencyInjection.Tests.MSDI;

[TestClass]
public class MicrosoftDependencyInjectionTests
{
    [TestMethod]
    public void MSDI1()
    {
        var container = new Container();
        var serviceProviderFactory = new DryIocServiceProviderFactory<SqlezeCompositionRoot>(container);
        
        var services = new ServiceCollection();
        var containerBuilder = serviceProviderFactory.CreateBuilder(services);
        var serviceProvider = serviceProviderFactory.CreateServiceProvider(containerBuilder);

        var scope1 = serviceProvider.CreateScope();
        var scope2 = serviceProvider.CreateScope();

        var root1 = scope1.ServiceProvider.GetRequiredService<ISqlezeRoot>();
        var root2 = scope2.ServiceProvider.GetRequiredService<ISqlezeRoot>();

        // ISqlezeRoot is singleton
        root1.ShouldBe(root2);
    }

    [TestMethod]
    public void MSDI2()
    {
        var container = new Container();
        var serviceProviderFactory = new DryIocServiceProviderFactory<SqlezeCompositionRoot>(container);
        
        var services = new ServiceCollection();

        services.AddSingleton<IHomeSql, HomeSql>();

        var containerBuilder = serviceProviderFactory.CreateBuilder(services);
        var serviceProvider = serviceProviderFactory.CreateServiceProvider(containerBuilder);

        var scope1 = serviceProvider.CreateScope();
        var scope2 = serviceProvider.CreateScope();

        var homeSql1 = scope1.ServiceProvider.GetRequiredService<IHomeSql>();
        var homeSql2 = scope2.ServiceProvider.GetRequiredService<IHomeSql>();
    }

    public interface IHomeSql
    {
        ISqlezeConnection Connect();
    }

    public class HomeSql : IHomeSql
    {
        private readonly ISqlezeConnector factory;

        public HomeSql(ISqlezeBuilder builder)
        {
            factory = builder.WithConnectionString("server = 123")
                .Build();
        }

        public ISqlezeConnection Connect()
        {
            return factory.Connect();
        }
    }

    private class DryIocServiceProviderFactory<TCompositionRoot> : IServiceProviderFactory<IContainer>
    {
        private readonly IContainer container;

        public DryIocServiceProviderFactory(IContainer container)
        {
            this.container = container;
        }

        public IContainer CreateBuilder(IServiceCollection services)
            => container.WithDependencyInjectionAdapter(services);

        public IServiceProvider CreateServiceProvider(IContainer containerBuilder)
            => containerBuilder.ConfigureServiceProvider<TCompositionRoot>();
    }
}
