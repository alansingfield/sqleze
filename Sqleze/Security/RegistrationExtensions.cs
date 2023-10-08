using Sqleze.Security;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration;

#if DRYIOC_DLL
public
#else
internal
#endif
static class RegistrationExtensions
{
    public static void RegisterSqlCredential(this IRegistrator registrator)
    {
        registrator.Register<IConnectionPreOpen, ConnectionPreOpenSqlCredential>(Reuse.Scoped);
        registrator.RegisterInstance(new SqlCredentialOptions(null));
        registrator.Register<SqlCredentialRoot>(setup: Setup.With(asResolutionCall: true));
    }

    public static void RegisterAccessToken(this IRegistrator registrator)
    {
        registrator.Register<IConnectionPreOpen, ConnectionPreOpenAccessToken>(Reuse.Scoped);
        registrator.RegisterInstance(new AccessTokenOptions(null));
        registrator.Register<AccessTokenRoot>(setup: Setup.With(asResolutionCall: true));
    }
}
