namespace Sqleze;

public interface ISqlezeScope
{
    void Use(Type serviceType, object? instance);
}

