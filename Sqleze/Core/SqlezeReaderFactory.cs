using Sqleze;

namespace Sqleze;

public class SqlezeReaderFactory : ISqlezeReaderFactory
{
    private readonly Func<IScopedSqlezeReaderFactory> newScopedSqlezeReaderFactory;

    public SqlezeReaderFactory(Func<IScopedSqlezeReaderFactory> newScopedSqlezeReaderFactory)
    {
        this.newScopedSqlezeReaderFactory = newScopedSqlezeReaderFactory;
    }

    public IScopedSqlezeReaderFactory OpenScope() => newScopedSqlezeReaderFactory();
}

