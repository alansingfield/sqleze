using Sqleze;
using Sqleze.Options;
using Sqleze.SqlClient;

namespace Sqleze;

public class SqlezeReader : ISqlezeReader
{
    private readonly IResolverContext readerScope;
    private readonly ISqlezeRowsetFactory sqlezeRowsetFactory;

    public SqlezeReader(IResolverContext readerScope, ISqlezeRowsetFactory sqlezeRowsetFactory)
    {
        this.readerScope = readerScope;
        this.sqlezeRowsetFactory = sqlezeRowsetFactory;
    }

    public ISqlezeRowset<T> OpenRowsetNullable<T>()
    {
        return sqlezeRowsetFactory.OpenRowsetNullable<T>();
    }


    public ISqlezeRowsetBuilder With<T>(Action<T, ISqlezeScope> configure)
    {
        var scopedFactoryFunc = readerScope.Resolve<Func<IScopedSqlezeRowsetBuilder<T>>>();

        // Create a new factory which is configured to open a new scope
        var scopedFactory = scopedFactoryFunc();

        // Configure factory scope and create the SqlezeRowsetFactory
        return scopedFactory.Create(configure);
    }

}