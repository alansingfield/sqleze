namespace Sqleze;

public interface ISqlezeRowsetBuilder
{
    ISqlezeRowsetBuilder With<T>(Action<T, ISqlezeScope> configure);
    ISqlezeRowsetFactory Build();
}
