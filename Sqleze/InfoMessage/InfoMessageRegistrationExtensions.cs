using Sqleze.InfoMessage;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration;
public static class InfoMessageRegistrationExtensions
{
    public static void RegisterInfoMessage(this IRegistrator registrator)
    {
        registrator.Register<IConnectionPreOpen, ConnectionPreOpenInfoMessageAction>(Reuse.Scoped);
        registrator.RegisterInstance(new InfoMessageActionOptions(null));
        registrator.Register<InfoMessageRoot>(setup: Setup.With(asResolutionCall: true));
    }
}
