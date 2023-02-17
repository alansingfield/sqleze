using Microsoft.Extensions.Configuration;
using Sqleze.Configuration;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Use IConfiguration to allow reading from ConnectionStrings section of the json config
        /// </summary>
        /// <param name="sqlezeBuilder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ISqlezeBuilder WithConfiguration(this ISqlezeBuilder sqlezeBuilder, IConfiguration configuration)
        {
            return sqlezeBuilder.With<ConfigurationRoot>((root, scope) =>
            {
                scope.Use(configuration);
            });
        }
    }
}

namespace Sqleze.Configuration
{
    public class ConfigurationRoot { }
}