namespace Sqleze;

public interface ISqlezeParameterFactory
{
    public IScopedSqlezeParameterFactory OpenScope();
}
