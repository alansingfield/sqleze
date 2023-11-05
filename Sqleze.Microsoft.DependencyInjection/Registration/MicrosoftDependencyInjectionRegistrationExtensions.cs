using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Microsoft.DependencyInjection.Registration;
public static class MicrosoftDependencyInjectionRegistrationExtensions
{
    //public static void ConfigureSqlezeProvider<ISqlezeProvider, TSqlezeProvider>(
    //    this IServiceCollection services, 
    //    Func<ISqlezeBuilder, ISqleze> buildAction) where TSqlezeProvider : ISqlezeProvider
    //{
    //    services.AddSingleton<ISqlezeProvider, TSqlezeProvider>(services =>
    //    {

    //    });
    //}
    //public static void ConfigureSqleze(this IServiceCollection services, Func<ISqlezeBuilder, ISqleze> buildAction)
    //{
    //    var container = new Container();
    //    container.RegisterSqleze();
        
    //    services.AddSingleton<ISqleze>(services => {

    //        var builder = container.Resolve<ISqlezeBuilder>();

    //        var sqlezeFactory = buildAction(builder);
    //        return sqlezeFactory;
    //    });
    //}

    //public static void AddSqleze(this IServiceCollection services)
    //{
    //    services.ConfigureSqleze(builder => builder.Build());
    //}

    public static void AddSqlezeBuilder(this IServiceCollection services)
    {
        services.AddSingleton<ISqlezeBuilder, SqlezeBuilder>();
    }


}

public interface ISqlezeProvider
{
    ISqlezeConnection Connect();
}

public class DefaultSqlezeProvider : ISqlezeProvider
{
    private readonly ISqlezeBuilder builder;
    private readonly ISqleze factory;

    public DefaultSqlezeProvider(ISqlezeBuilder builder)
    {
        this.builder = builder;
        factory = builder.Build();
    }

    public ISqlezeConnection Connect()
    {
        return factory.Connect();
    }
}