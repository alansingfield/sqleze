namespace Sqleze;

public interface ISqlezeParameterBuilder
{
    ISqlezeParameterBuilder With<T>(Action<T, ISqlezeScope> configure);
    IScopedSqlezeParameterFactory Build();
}

