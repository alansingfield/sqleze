using Sqleze;

namespace Sqleze;

public class SqlezeRowsetFactory : ISqlezeRowsetFactory
{
    private readonly Func<IScopedSqlezeRowsetFactory> newScopedSqlezeRowsetFactory;

    public SqlezeRowsetFactory(Func<IScopedSqlezeRowsetFactory> newScopedSqlezeRowsetFactory)
    {
        this.newScopedSqlezeRowsetFactory = newScopedSqlezeRowsetFactory;
    }

    public ISqlezeRowset<T> OpenRowsetNullable<T>()
    {
        return newScopedSqlezeRowsetFactory().OpenRowsetNullable<T>();
    }
}

