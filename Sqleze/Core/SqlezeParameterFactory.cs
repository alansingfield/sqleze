using Sqleze;

namespace Sqleze;

public class SqlezeParameterFactory : ISqlezeParameterFactory
{
    private readonly Func<IScopedSqlezeParameterFactory> newScopedSqlezeParameterFactory;

    public SqlezeParameterFactory(Func<IScopedSqlezeParameterFactory> newScopedSqlezeParameterFactory)
    {
        this.newScopedSqlezeParameterFactory = newScopedSqlezeParameterFactory;
    }

    public IScopedSqlezeParameterFactory OpenScope() => newScopedSqlezeParameterFactory();
}

