namespace Sqleze;

public interface IDependencyHolder<T>
{
    T Inner { get; }
}

