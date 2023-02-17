using Sqleze;

namespace Sqleze;

public class SqlezeReaderBuilder : ISqlezeReaderBuilder
{
    private readonly IResolverContext factoryScope;
    private readonly Func<ISqlezeReaderFactory> newSqlezeReaderFactory;
    private readonly ISqlezeCommand sqlezeCommand;

    public SqlezeReaderBuilder(IResolverContext scope,
        Func<ISqlezeReaderFactory> newSqlezeReaderFactory,
        ISqlezeCommand sqlezeCommand)
    {
        this.factoryScope = scope;
        this.newSqlezeReaderFactory = newSqlezeReaderFactory;
        this.sqlezeCommand = sqlezeCommand;
    }

    public ISqlezeReader ExecuteReader()
    {
        // This will create a new scope for the reader factory with all the configured
        // items within.
        var sqlezeReaderFactory = newSqlezeReaderFactory();

        // Chain back to the SqlezeCommand but pass in our modified context to get a customised
        // reader.
        return sqlezeCommand.ExecuteReader(sqlezeReaderFactory);
    }

    public async Task<ISqlezeReader> ExecuteReaderAsync(CancellationToken cancellationToken = default)
    {
        // This will create a new scope for the reader factory with all the configured
        // items within.
        var sqlezeReaderFactory = newSqlezeReaderFactory();

        return await sqlezeCommand.ExecuteReaderAsync(sqlezeReaderFactory).ConfigureAwait(false);
    }

    public int ExecuteNonQuery()
    {
        // This will create a new scope for the reader factory with all the configured
        // items within.
        var sqlezeReaderFactory = newSqlezeReaderFactory();

        // Chain back to the SqlezeCommand but pass in our modified context to get a customised
        // reader.
        return sqlezeCommand.ExecuteNonQuery(sqlezeReaderFactory);
    }

    public async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken = default)
    {
        // This will create a new scope for the reader factory with all the configured
        // items within.
        var sqlezeReaderFactory = newSqlezeReaderFactory();

        return await sqlezeCommand.ExecuteNonQueryAsync(sqlezeReaderFactory).ConfigureAwait(false);
    }

    public ISqlezeReaderBuilder With<T>(Action<T, ISqlezeScope> configure)
    {
        var sfFunc = factoryScope.Resolve<Func<IScopedSqlezeReaderBuilder<T>>>();

        // Create a new factory which is configured to open a new scope.
        var scopedFactory = sfFunc();

        // Configure factory scope and create the SqlezeReaderFactory
        return scopedFactory.Create(configure);
    }
}
