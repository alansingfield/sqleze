using Sqleze;
using Sqleze.Options;

namespace Sqleze;

public class SqlezeCommandBuilder : ISqlezeCommandBuilder
{
    private readonly IResolverContext factoryScope;
    private readonly Func<ISqlezeCommandFactory> newSqlezeCommandFactory;

    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
    SqlezeCommandBuilder(IResolverContext scope,
        Func<ISqlezeCommandFactory> newSqlezeCommandFactory)
    {
        this.factoryScope = scope;
        this.newSqlezeCommandFactory = newSqlezeCommandFactory;
    }

    public ISqlezeCommandFactory Build() => newSqlezeCommandFactory();

    public ISqlezeCommandBuilder With<T>(Action<T, ISqlezeScope> configure)
    {
        var sfFunc = factoryScope.Resolve<Func<IScopedSqlezeCommandBuilder<T>>>();

        // Create a new builder which is configured to open a new scope.
        var scopedBuilder = sfFunc();

        // Configure the new scope and create the inner builder
        return scopedBuilder.Create(configure);
    }
}
