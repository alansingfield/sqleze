using Sqleze;

namespace Sqleze;

public class SqlezeBuilder : ISqlezeBuilder
{
    private readonly IResolverContext factoryScope;
    private readonly Func<ISqlezeConnector> newSqlezeConnectionFactory;

    public SqlezeBuilder(IResolverContext scope,
        Func<ISqlezeConnector> newSqlezeConnectionFactory)
    {
        this.factoryScope = scope;
        this.newSqlezeConnectionFactory = newSqlezeConnectionFactory;
    }

    public ISqlezeConnector Build()
    {
        // This will create a new scope for the connection factory with all the configured
        // items within.
        return newSqlezeConnectionFactory();
    }

    public ISqlezeBuilder With<T>(Action<T, ISqlezeScope> configure)
    {
        var sfFunc = factoryScope.Resolve<Func<IScopedSqlezeConnectionBuilder<T>>>();

        // Create a new builder which is configured to open a new scope.
        var scopedBuilder = sfFunc();

        // Configure factory scope and create the SqlezeConnectionFactory
        return scopedBuilder.Create(configure);
    }
}
