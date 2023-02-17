namespace Sqleze;

public interface ISqlezeRowsetProvider { }

public interface ISqlezeRowsetProvider<T> : ISqlezeRowsetProvider
{
    ISqlezeRowset<T> SqlezeRowset { get; }
}
