using Sqleze.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Dynamics;

public interface IConstructorLambdaCache<T>
{
    Func<object?[], int[], T> GetConstructorFunc(ConstructorInfo constructorInfo);
}

public class ConstructorLambdaCache<T> : IConstructorLambdaCache<T>
{
    private readonly ConcurrentCache<ConstructorInfo, Func<object?[], int[], T>> cache = new();
    private readonly IConstructorLambdaBuilder<T> constructorLambdaBuilder;

    public ConstructorLambdaCache(IConstructorLambdaBuilder<T> constructorLambdaBuilder)
    {
        this.constructorLambdaBuilder = constructorLambdaBuilder;
    }

    public Func<object?[], int[], T> GetConstructorFunc(ConstructorInfo constructorInfo)
    {
        return cache.Get(constructorInfo, x => constructorLambdaBuilder.GetConstructorFunc(x));
    }
}
