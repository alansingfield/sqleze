using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Sqleze.Util;

// See https://bryanhelms.com/2021/03/29/thread-safe-auth-token-store-using-concurrentdictionary-and-asynclazy.html

public class AsyncLazy<T> : Lazy<Task<T>>
{
    public AsyncLazy(Func<T> valueFactory)
        : base(() => Task.Factory.StartNew(valueFactory)) { }

    public AsyncLazy(Func<Task<T>> taskFactory)
        : base(() => Task.Factory.StartNew(() => taskFactory()).Unwrap()) { }

    // This allow awaiting the value within the AsyncLazy directly, rather than having to use .Value
    public TaskAwaiter<T> GetAwaiter() { return Value.GetAwaiter(); }
}