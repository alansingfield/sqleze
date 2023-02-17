namespace Sqleze;

public interface IScopedSqlezeRowsetBuilder<TConfigRoot>
{
    ISqlezeRowsetBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure);
}
