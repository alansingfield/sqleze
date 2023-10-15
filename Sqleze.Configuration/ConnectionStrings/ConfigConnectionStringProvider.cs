using Microsoft.Extensions.Configuration;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sqleze.ConnectionStrings
{
    public interface IConfigConnectionStringProvider : IConnectionStringProvider { }
    public class ConfigConnectionStringProvider : IConfigConnectionStringProvider
    {
        private readonly IConfiguration configuration;
        private readonly string connectionKey;

        public ConfigConnectionStringProvider(
            IConfiguration configuration, 
            ConfigConnectionOptions configConnectionOptions)
        {
            this.configuration = configuration;
            this.connectionKey = configConnectionOptions.ConnectionKey;
        }

        public string GetConnectionString()
        {
            return configuration.GetConnectionString(this.connectionKey);
        }
    }
}
