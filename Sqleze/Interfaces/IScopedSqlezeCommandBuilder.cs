using Sqleze;

namespace Sqleze;

public interface IScopedSqlezeCommandBuilder<TConfigRoot>
{
    ISqlezeCommandBuilder Create(Action<TConfigRoot, ISqlezeScope>? configure);
}
