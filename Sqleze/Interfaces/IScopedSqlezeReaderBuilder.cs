namespace Sqleze;

public interface IScopedSqlezeReaderBuilder<TConfigRoot>
{
    ISqlezeReaderBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure);
}
