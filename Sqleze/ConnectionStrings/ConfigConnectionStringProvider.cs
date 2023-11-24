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
            string? connectionString = configuration.GetConnectionString(options.ConnectionKey);

            if(connectionString is null)
                throw new KeyNotFoundException(
                    "Key not found within ConnectionStrings section of configuration file. (ConnectionKey)");

            if(String.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException(
                    "Blank connection string found in ConnectionStrings section of configuration file.",
                    nameof(options.ConnectionKey));

            if(options.PasswordKey is null)
                return connectionString;

            string? newPassword = configuration[options.PasswordKey];

            if(newPassword is null)
                throw new KeyNotFoundException(
                    "Key not found within configuration file. (PasswordKey)");
            
            var builder = newSqlConnectionStringBuilder();

            builder.ConnectionString = connectionString;
            builder.Password = newPassword;

            return builder.ConnectionString;
        }
    }
}
