using Sqleze;

namespace Sqleze;

public class SqlezeRowsetBuilder : ISqlezeRowsetBuilder
{
    private readonly IResolverContext factoryScope;
    private readonly Func<ISqlezeRowsetFactory> newSqlezeRowsetFactory;

    public SqlezeRowsetBuilder(IResolverContext scope,
        Func<ISqlezeRowsetFactory> newSqlezeRowsetFactory)
    {
        this.factoryScope = scope;
        this.newSqlezeRowsetFactory = newSqlezeRowsetFactory;
    }

    public ISqlezeRowsetFactory Build()
    {
        // This will create a new scope for the connection factory with all the configured
        // items within.
        return newSqlezeRowsetFactory();
    }

    public ISqlezeRowsetBuilder With<T>(Action<T, ISqlezeScope> configure)
    {
        var sfFunc = factoryScope.Resolve<Func<IScopedSqlezeRowsetBuilder<T>>>();

        // Create a new factory which is configured to open a new scope.
        var scopedFactory = sfFunc();

        // Configure factory scope and create the SqlezeRowsetFactory
        return scopedFactory.Create(configure);
    }
}
