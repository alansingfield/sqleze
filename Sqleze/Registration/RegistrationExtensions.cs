
using Sqleze.ConnectionStrings;
using Sqleze;
using Sqleze.Registration;

namespace Sqleze;

public static class SqlezeRegistrationExtensions
{
    public static void RegisterSqleze(this IRegistrator registrator)
    {
        registrator.RegisterSqlezeCore();
        registrator.RegisterSqlezeConnectionStringProviders();

#if !MOCK_SQLCLIENT
        registrator.RegisterMicrosoftSqlClient();
#endif
    }
}
