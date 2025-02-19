using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestCommon.Config;

public static class ConfigurationExtensions
{
    ///// <summary>
    ///// Sets up configuration binding to a section of the appsettings.json file.
    ///// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#use-di-services-to-configure-options 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="configuration">from ConfigurationBuilder</param>
    ///// <param name="sectionName">Name of section in appsettings.json file</param>
    ///// <returns></returns>
    //public static T BuildOption<T>(this IConfiguration configuration, string sectionName)
    //{
    //    return configuration.GetSection(sectionName).Get<T>();
    //}

    public static TService BuildOption<TService, TImplementation>
        (this IConfiguration configuration, string sectionName)
        where TImplementation : TService
    {
        var options = configuration.GetSection(sectionName).Get<TImplementation>();
        if(options == null)
            throw new Exception("section not found");

        return options;
    }

    public static void RegisterSettingsFiles(this IRegistrator container,
        IEnumerable<string> filenames,
        IEnumerable<string>? optionalFilenames = null)
    {
        container.Register<IConfiguration>(reuse: Reuse.Singleton, made: Made.Of(() => ConfigurationFactory.New(filenames, optionalFilenames)));
    }


}
