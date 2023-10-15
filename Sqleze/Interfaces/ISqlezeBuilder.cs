namespace Sqleze;

// was originally ISqlezeConnectionBuilder

public interface ISqlezeBuilder
{
    ISqlezeBuilder With<T>(Action<T, ISqlezeScope> configure);
    /// <summary>
    /// Builds the factory which can then be used to create multiple connections
    /// to the database
    /// </summary>
    /// <returns></returns>
    ISqleze Build();
}
