using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public static class StartupRegistrationExtensions
{
    public static void UseSpatialTypes(this IStartupRegistration startupRegistration)
    {
        startupRegistration.Registrator.RegisterSpatialTypes();
    }

}
