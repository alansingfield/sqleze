using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon.Config;
public class ConfigurationFactory
{
    public static IConfiguration New(IEnumerable<string> filenames,
        IEnumerable<string>? optionalFilenames = null)
    {
        var builder = new ConfigurationBuilder();

        foreach(var fn in filenames)
        {
            builder.AddJsonFile(fn, optional: false, reloadOnChange: false);
        }

        if(optionalFilenames != null)
        {
            foreach(var fn in optionalFilenames)
            {
                builder.AddJsonFile(fn, optional: true, reloadOnChange: false);
            }
        }

        return builder.Build();
    }
}
