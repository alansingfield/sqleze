using Sqleze;

namespace Sqleze;

public class SqlezeScope : ISqlezeScope
{
    private readonly IResolverContext scope;

    private bool disposedValue;

    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
    SqlezeScope(IResolverContext scope)
    {
        this.scope = scope;
    }

    public void Use(Type serviceType, object? instance)
    {
        scope.Use(serviceType, instance);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(disposedValue)
            return;

        if(disposing)
        {
            this.scope?.Dispose();
        }

        disposedValue = true;
    }

    public void Dispose() => Dispose(true);
}
