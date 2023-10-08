using Sqleze.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Dynamics
{
    public interface IDefaultValueCache<T>
    {
        T? DefaultValue();
    }

    public class DefaultValueCache<T> : IDefaultValueCache<T>
    {
        private readonly Lazy<Func<T?>> defaultValueFunc;
        private readonly IDefaultFallbackExpressionBuilder defaultFallbackExpressionBuilder;

        public DefaultValueCache(IDefaultFallbackExpressionBuilder defaultFallbackExpressionBuilder)
        {
            this.defaultFallbackExpressionBuilder = defaultFallbackExpressionBuilder;

            defaultValueFunc = new Lazy<Func<T?>>(() => createDefaultValueFunc());
        }

        public T? DefaultValue()
        {
            return defaultValueFunc.Value();
        }

        private Func<T?> createDefaultValueFunc()
        {
            var defaultExpr = defaultFallbackExpressionBuilder.Build(typeof(T), NullabilityState.NotNull);

            // Compile the expression into a function and return.
            return Expression.Lambda<Func<T?>>(defaultExpr).Compile();
        }
    }

    public class DefaultValueCacheString : IDefaultValueCache<string>
    {
        public string? DefaultValue() => "";
    }
    public class DefaultValueCacheByteArray : IDefaultValueCache<byte[]>
    {
        public byte[]? DefaultValue() => Array.Empty<byte>();
    }

}

namespace Sqleze.Registration
{
    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
        static class DefaultValueCacheRegistrationExtensions
    {
        public static void RegisterDefaultValueCache(this IRegistrator registrator)
        {
            registrator.Register(typeof(IDefaultValueCache<>), typeof(DefaultValueCache<>), Reuse.Singleton);
            registrator.Register<IDefaultValueCache<string>, DefaultValueCacheString>(Reuse.Singleton);
            registrator.Register<IDefaultValueCache<byte[]>, DefaultValueCacheByteArray>(Reuse.Singleton);

        }
    }
}