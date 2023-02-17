using Microsoft.Extensions.Configuration;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ConnectionStrings;
public class FallbackConnectionStringProvider : IConnectionStringProvider
{
    private readonly IConfiguration configuration;

    public FallbackConnectionStringProvider(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GetConnectionString()
    {
        return configuration.GetConnectionString("ConnectionString");
    }
}
