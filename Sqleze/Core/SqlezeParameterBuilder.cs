using Sqleze;

namespace Sqleze;

public class SqlezeParameterBuilder : ISqlezeParameterBuilder
{
    private readonly IResolverContext factoryScope;
    private readonly Func<ISqlezeParameterFactory> newSqlezeParameterFactory;

    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
    SqlezeParameterBuilder(IResolverContext scope,
        Func<ISqlezeParameterFactory> newSqlezeParameterFactory)
    {
        this.factoryScope = scope;
        this.newSqlezeParameterFactory = newSqlezeParameterFactory;
    }

    public IScopedSqlezeParameterFactory Build()
    {
        // This will create a new scope for the parameter factory with all the configured
        // items within.
        return newSqlezeParameterFactory().OpenScope();
    }
    public ISqlezeParameterBuilder With<T>(Action<T, ISqlezeScope> configure)
    {
        var sfFunc = factoryScope.Resolve<Func<IScopedSqlezeParameterBuilder<T>>>();

        // Create a new factory which is configured to open a new scope.
        var scopedFactory = sfFunc();

        // Configure factory scope and create the inner SqlezeParameterBuilder
        return scopedFactory.Create(configure);
    }


}
