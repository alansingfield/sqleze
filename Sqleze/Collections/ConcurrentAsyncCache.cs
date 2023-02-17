using Sqleze.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Collections;

public class ConcurrentAsyncCache<TKey, TValue> 
    where TKey: notnull 
    where TValue: notnull
{
    private ConcurrentDictionary<TKey, AsyncLazy<TValue>> _dict;

    public ConcurrentAsyncCache()
    {
        _dict = new ConcurrentDictionary<TKey, AsyncLazy<TValue>>();
    }

    public ConcurrentAsyncCache(IEqualityComparer<TKey> comparer)
    {
        _dict = new ConcurrentDictionary<TKey, AsyncLazy<TValue>>(comparer);
    }

    public async Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> populateFunc)
    {
        AsyncLazy<TValue> lazyValue;

        // Either pull an existing item from the dictionary or create a new Lazy value which
        // will call the populateFunc when its .Value() is accessed.
        lazyValue = _dict.GetOrAdd(key,
            new AsyncLazy<TValue>(
                () => populateFunc(key))
                );

        try
        {
            // Accessing the Value of the AsyncLazy<TValue> will cause the first thread to run the
            // population function. Any exception will be propagated to here.
            // All other threads that try to access this Value at the same time will ALSO get
            // the exception.
            return await lazyValue;
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
