namespace Sqleze;

public interface ISqlezeRowsetFactory
{
    ISqlezeRowset<T> OpenRowsetNullable<T>();
}
