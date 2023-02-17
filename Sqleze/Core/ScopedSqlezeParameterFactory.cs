using Sqleze.DryIoc;
using Sqleze;
using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public class ScopedSqlezeParameterFactory : IScopedSqlezeParameterFactory
{
    private readonly IGenericResolver<ISqlezeParameterProvider> sqlezeParameterProviderResolver;
    private readonly INamingConvention namingConvention;

    public ScopedSqlezeParameterFactory(
        IGenericResolver<ISqlezeParameterProvider> sqlezeParameterProviderResolver,
        INamingConvention namingConvention,
        ISqlezeCommand sqlezeCommand)
    {
        this.sqlezeParameterProviderResolver = sqlezeParameterProviderResolver;
        this.namingConvention = namingConvention;
        this.Command = sqlezeCommand;
    }

    public ISqlezeCommand Command { get; init; }

    public ISqlezeParameterProvider<T> Create<T>(string parameterName)
    {
        var options = resolveName(parameterName);

        return sqlezeParameterProviderResolver
            .Resolve<ISqlezeParameterProvider, ISqlezeParameterProvider<T>>(
                typeof(T),          // Resolve to ISqlezeParameterProvider<T> not ISqlezeParameterProvider
                new[] { options })  // Pass ParameterCreateOptions through to the provider's container so the constructor can use it
            ;
    }

    private ParameterCreateOptions resolveName(string parameterName)
    {
        if(parameterName.StartsWith("@"))
        {
            return new ParameterCreateOptions()
            {
                AdoName = parameterName,
                Name = namingConvention.SqlToDotNet(parameterName.TrimStart('@'))
            };
        }
        else if(parameterName != "")
        {
            return new ParameterCreateOptions()
            {
                AdoName = "@" + namingConvention.DotNetToSql(parameterName),
                Name = parameterName
            };
        }
        else
        {
            // The return value parameter can be passed with no name.
            return new ParameterCreateOptions()
            {
                AdoName = "",
                Name = "",
            };
        }
    }

}


