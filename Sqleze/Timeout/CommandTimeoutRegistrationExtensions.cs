using Sqleze.SqlClient;
using Sqleze.Timeout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration;

public static class CommandTimeoutRegistrationExtensions
{
    public static void RegisterCommandTimeout(this IRegistrator registrator)
    {
        registrator.Register<ICommandPreExecute, CommandPreExecuteTimeout>(Reuse.Scoped, setup: Setup.With(asResolutionCall: true));
        registrator.RegisterInstance(new CommandTimeoutOptions());
        registrator.Register<CommandTimeoutRoot>(setup: Setup.With(asResolutionCall: true));
    }
}
