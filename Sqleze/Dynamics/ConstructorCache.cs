using Sqleze.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Dynamics
{
    public interface IConstructorCache<T>
    {
        T Create();
    }

    public class ConstructorCache<T> : IConstructorCache<T>
    {
        private readonly Lazy<Func<T>> constructorFunc;

        public ConstructorCache()
        {
            constructorFunc = new Lazy<Func<T>>(() => createConstructorFunc());
        }

        public T Create()
        {
            return constructorFunc.Value();
        }

        private static Func<T> createConstructorFunc()
        {
            // Get the parameterless constructor
            var constructorInfo = typeof(T).GetConstructor(System.Type.EmptyTypes);
            if(constructorInfo == null)
                throw new InvalidOperationException(
                    $"Unable to find parameterless constructor for type {typeof(T).Name}");

            // Get the LINQ expression equivalent to "= new T()"
            var newExpression = Expression.New(constructorInfo);

            // Compile the expression into a function and return.
            return Expression.Lambda<Func<T>>(newExpression).Compile();
        }
    }
}
