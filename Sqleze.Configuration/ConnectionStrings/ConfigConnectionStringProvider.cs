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
        private readonly ConfigConnectionOptions options;

        public ConfigConnectionStringProvider(
            IConfiguration configuration, 
            ConfigConnectionOptions options)
        {
            this.configuration = configuration;
            this.options = options;
        }

        public string GetConnectionString()
        {
            string connectionString = configuration.GetConnectionString(options.ConnectionKey);

            if(
        }
    }
}
