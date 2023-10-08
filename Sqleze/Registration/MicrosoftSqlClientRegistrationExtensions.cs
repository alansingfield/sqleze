using Sqleze.TableValuedParameters;


namespace Sqleze.Registration;

public static class MicrosoftSqlClientRegistrationExtensions
{
#if !MOCK_SQLCLIENT

    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
    static void RegisterMicrosoftSqlClient(this IRegistrator registrator)
    {
        // Register the concrete types for these for the real thing.
        // We use NSubstitute mocks in the mock version.
        // TODO - add tests to ensure these are disposed correctly in the Ado class
        registrator.Register<MS.SqlConnection>(
            made: Made.Of(() => new MS.SqlConnection()),
            setup: Setup.With(allowDisposableTransient: true));

        registrator.Register<MS.SqlCommand>(
            made: Made.Of(() => new MS.SqlCommand()),
            setup: Setup.With(allowDisposableTransient: true));

        registrator.Register<MS.SqlParameter>(
            made: Made.Of(() => new MS.SqlParameter()),
            setup: Setup.With(allowDisposableTransient: true));

        // As these have constructors have parameters, use factories to create them.
        registrator.Register<ISqlMetaDataFactory, SqlMetaDataFactory>(Reuse.Singleton);
        registrator.Register<ISqlDataRecordFactory, SqlDataRecordFactory>(Reuse.Singleton);
    }
#endif
}
