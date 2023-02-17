namespace Sqleze;

public interface IScopedSqlezeParameterBuilder<TConfigRoot>
{
    ISqlezeParameterBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure);
}
