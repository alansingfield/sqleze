using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Collections;

public class ConcurrentCache<TKey, TValue> 
    where TKey: notnull 
    where TValue: notnull
{
    private ConcurrentDictionary<TKey, Lazy<TValue>> _dict;

    public ConcurrentCache()
    {
        _dict = new ConcurrentDictionary<TKey, Lazy<TValue>>();
    }

    public ConcurrentCache(IEqualityComparer<TKey> comparer)
    {
        _dict = new ConcurrentDictionary<TKey, Lazy<TValue>>(comparer);
    }

    public TValue Get(TKey key, Func<TKey, TValue> populateFunc)
    {
        Lazy<TValue> lazyValue;

        // Either pull an existing item from the dictionary or create a new Lazy value which
        // will call the populateFunc when its .Value() is accessed.
        lazyValue = _dict.GetOrAdd(key,
            new Lazy<TValue>(
                () => populateFunc(key))
                );

        try
        {
            // Accessing the Value of the Lazy<TValue> will cause the first thread to run the
            // population function. Any exception will be propagated to here.
            // All other threads that try to access this Value at the same time will ALSO get
            // the exception.
            return lazyValue.Value;
        }
        catch(Exception)
        {
            // Remove the faulted item from the dictionary so that subsequent callers
            // don't pick it up.
            _dict.TryRemove(key, out _);
            throw;
        }
    }
    
    public bool Remove(TKey key)
    {
        return _dict.TryRemove(key, out _);
    }

    public void Clear()
    {
        _dict.Clear();
    }

    public ICollection<TKey> Keys()
    {
        return _dict.Keys;
    }
}
