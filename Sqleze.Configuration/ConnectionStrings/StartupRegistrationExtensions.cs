using Microsoft.Extensions.Configuration;
using Sqleze.ConnectionStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class StartupRegistrationExtensions
{
    public static void UseConfiguration(this IStartupRegistration startupRegistration)
    {
        startupRegistration.Registrator.RegisterSqlezeConfigKey();
    }
}
