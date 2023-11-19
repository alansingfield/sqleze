using Microsoft.Extensions.Configuration;
using Shouldly;
using Sqleze.ConnectionStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.ConnectionStrings;

[TestClass] 
public class ConfigConnectionStringProviderTests
{
    [TestMethod]
    public void ConfigConnectionStringProviderChangesPassword()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IConfigConnectionStringProvider, ConfigConnectionStringProvider>();
        container.Register<MS.SqlConnectionStringBuilder>(made: Made.Of(() => new MS.SqlConnectionStringBuilder()));
        
        container.Use(new ConfigConnectionOptions(
            ConnectionKey:  "ConnKey",
            PasswordKey:    "PassKey"));

        var configurationSection = Substitute.For<IConfigurationSection>();

        var configuration = container.Resolve<IConfiguration>();
        configuration.GetSection("ConnectionStrings").Returns(configurationSection);

        configurationSection["ConnKey"].Returns("Server=Example;User Id=AnyUser;Password=WillBeIgnored");

        configuration["PassKey"].Returns("ReplacementPassword");

        var provider = container.Resolve<IConfigConnectionStringProvider>();

        // SqlConnectionStringBuilder normalises "Server" to "Data Source" and "User Id" to "User ID".
        provider.GetConnectionString().ShouldBe("Data Source=Example;User ID=AnyUser;Password=ReplacementPassword");
    }
    
    [TestMethod]
    public void ConfigConnectionStringProviderNoPasswordKey()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IConfigConnectionStringProvider, ConfigConnectionStringProvider>();
        container.Register<MS.SqlConnectionStringBuilder>(made: Made.Of(() => new MS.SqlConnectionStringBuilder()));
        
        container.Use(new ConfigConnectionOptions(
            ConnectionKey:  "ConnKey",
            PasswordKey:    null));

        var configurationSection = Substitute.For<IConfigurationSection>();

        var configuration = container.Resolve<IConfiguration>();
        configuration.GetSection("ConnectionStrings").Returns(configurationSection);

        configurationSection["ConnKey"].Returns("Server=Example;User Id=AnyUser;Password=WillRemain");

        configuration["PassKey"].Returns("ReplacementPassword");

        var provider = container.Resolve<IConfigConnectionStringProvider>();

        // Wasn't passed through the SqlConnectionStringBuilder so comes back without modification
        provider.GetConnectionString().ShouldBe("Server=Example;User Id=AnyUser;Password=WillRemain");
    }

    [TestMethod]
    public void ConfigConnectionStringProviderPasswordKeyNotFound()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IConfigConnectionStringProvider, ConfigConnectionStringProvider>();
        container.Register<MS.SqlConnectionStringBuilder>(made: Made.Of(() => new MS.SqlConnectionStringBuilder()));
        
        container.Use(new ConfigConnectionOptions(
            ConnectionKey:  "ConnKey",
            PasswordKey:    "PassKey"));

        var configurationSection = Substitute.For<IConfigurationSection>();

        var configuration = container.Resolve<IConfiguration>();
        configuration.GetSection("ConnectionStrings").Returns(configurationSection);

        configurationSection["ConnKey"].Returns("Server=Example;User Id=AnyUser;Password=*****");

        configuration["PassKey"].Returns((string?)null);

        var provider = container.Resolve<IConfigConnectionStringProvider>();

        var error = Should.Throw<KeyNotFoundException>(() =>
        {
            provider.GetConnectionString();
        });

        error.Message.ShouldBe("Key not found within configuration file. (PasswordKey)");
    }


    [TestMethod]
    public void ConfigConnectionStringProviderNoConfigSetting()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IConfigConnectionStringProvider, ConfigConnectionStringProvider>();
        container.Register<MS.SqlConnectionStringBuilder>(made: Made.Of(() => new MS.SqlConnectionStringBuilder()));
        
        container.Use(new ConfigConnectionOptions(
            ConnectionKey:  "ConnKey",
            PasswordKey:    null));

        var configurationSection = Substitute.For<IConfigurationSection>();

        var configuration = container.Resolve<IConfiguration>();
        configuration.GetSection("ConnectionStrings").Returns(configurationSection);

        configurationSection["ConnKey"].Returns((string?)null);

        var provider = container.Resolve<IConfigConnectionStringProvider>();

        var error = Should.Throw<KeyNotFoundException>(() =>
        {
            provider.GetConnectionString();
        });

        error.Message.ShouldStartWith("Key not found within ConnectionStrings section of configuration file. (ConnectionKey)");
    }

    
    [TestMethod]
    public void ConfigConnectionStringProviderBlankConfigSetting()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IConfigConnectionStringProvider, ConfigConnectionStringProvider>();
        container.Register<MS.SqlConnectionStringBuilder>(made: Made.Of(() => new MS.SqlConnectionStringBuilder()));
        
        container.Use(new ConfigConnectionOptions(
            ConnectionKey:  "ConnKey",
            PasswordKey:    null));

        var configurationSection = Substitute.For<IConfigurationSection>();

        var configuration = container.Resolve<IConfiguration>();
        configuration.GetSection("ConnectionStrings").Returns(configurationSection);

        configurationSection["ConnKey"].Returns("");

        var provider = container.Resolve<IConfigConnectionStringProvider>();

        var error = Should.Throw<ArgumentException>(() =>
        {
            provider.GetConnectionString();
        });

        error.Message.ShouldStartWith("Blank connection string found in ConnectionStrings section of configuration file.");
        error.ParamName.ShouldBe("ConnectionKey");
    }


    [TestMethod]
    public void ConfigConnectionStringProviderBlankPassword()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IConfigConnectionStringProvider, ConfigConnectionStringProvider>();
        container.Register<MS.SqlConnectionStringBuilder>(made: Made.Of(() => new MS.SqlConnectionStringBuilder()));
        
        container.Use(new ConfigConnectionOptions(
            ConnectionKey:  "ConnKey",
            PasswordKey:    "PassKey"));

        var configurationSection = Substitute.For<IConfigurationSection>();

        var configuration = container.Resolve<IConfiguration>();
        configuration.GetSection("ConnectionStrings").Returns(configurationSection);

        configurationSection["ConnKey"].Returns("Server=Example;User Id=AnyUser;Password=WillBeIgnored");

        // Configured for a blank password, it should blindly use this.
        configuration["PassKey"].Returns("");

        var provider = container.Resolve<IConfigConnectionStringProvider>();

        // SqlConnectionStringBuilder normalises "Server" to "Data Source" and "User Id" to "User ID".
        provider.GetConnectionString().ShouldBe("Data Source=Example;User ID=AnyUser;Password=");
    }
}
