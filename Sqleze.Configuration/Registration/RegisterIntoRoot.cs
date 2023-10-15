using Sqleze.Configuration.ConnectionStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Configuration.Registration;
public static class RegisterIntoRoot
{
    [ModuleInitializer]
    internal static void PushToRootContainer()
    {
        SqlezeRoot.ModuleHandshake(reg => reg.RegisterSqlezeConfigConnectionStringProvider());
    }
}
