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
        private readonly Func<MS.SqlConnectionStringBuilder> newSqlConnectionStringBuilder;

        public ConfigConnectionStringProvider(
            IConfiguration configuration, 
            ConfigConnectionOptions options,
            Func<MS.SqlConnectionStringBuilder> newSqlConnectionStringBuilder)
        {
            this.configuration = configuration;
            this.options = options;
            this.newSqlConnectionStringBuilder = newSqlConnectionStringBuilder;
        }

        public string GetConnectionString()
        {
            string connectionString = configuration.GetConnectionString(options.ConnectionKey);

            if(options.PasswordKey is null)
                return connectionString;

            var builder = newSqlConnectionStringBuilder();

            builder.ConnectionString = connectionString;
            builder.Password = configuration[options.PasswordKey];

            return builder.ConnectionString;
        }
    }
}
