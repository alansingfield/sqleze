namespace Sqleze;

// was originally ISqlezeConnectionBuilder

public interface ISqlezeBuilder
{
    ISqlezeBuilder With<T>(Action<T, ISqlezeScope> configure);
    ISqleze Build();
}
