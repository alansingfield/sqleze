namespace Sqleze;

public interface IScopedSqlezeConnectionBuilder<TConfigRoot>
{
    ISqlezeBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure);
}
