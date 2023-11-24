using Sqleze;

namespace Sqleze;

public class SqlezeConnectionFactory : ISqlezeConnector
{
    private readonly Func<IScopedSqlezeConnectionFactory> newScopedSqlezeConnectionFactory;
    private readonly Lazy<ISqlezeBuilder> lazySqlezeBuilder;

    public SqlezeConnectionFactory(Func<IScopedSqlezeConnectionFactory> newScopedSqlezeConnectionFactory,
        Lazy<ISqlezeBuilder> lazySqlezeBuilder)
    {
        this.newScopedSqlezeConnectionFactory = newScopedSqlezeConnectionFactory;
        this.lazySqlezeBuilder = lazySqlezeBuilder;
    }

    public ISqlezeConnection Connect()
    {
        return newScopedSqlezeConnectionFactory().Connect();
    }

    public ISqlezeBuilder Reconfigure() => lazySqlezeBuilder.Value;
}

