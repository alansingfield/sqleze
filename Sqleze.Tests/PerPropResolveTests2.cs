using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests
{
    [TestClass]
    public class PerPropResolveTests2
    {
        [TestMethod]
        public void PerPropResolve2()
        {
            var container = new Container();
            container.Register(typeof(OpenGenResolver<>));
            container.Register(typeof(IOpenGen<,>), typeof(OpenGen<,>));
            container.Register(typeof(IOpenGenResolverInnards<>), typeof(OpenGenResolverInnards<>));

            var resolver = container.Resolve<OpenGenResolver<MyClass>>();

            var openGens = new List<(PropertyInfo, IOpenGen<MyClass>)>();

            var q = resolver.ResolveForEachProperty().ToList();

            q.Count.ShouldBe(2);
            q[0].Item2.ShouldBeOfType<OpenGen<MyClass, string>>();
            q[1].Item2.ShouldBeOfType<OpenGen<MyClass, int>>();
        }

        private class OpenGenResolver<TElement>
        {
            private readonly IOpenGenResolverInnards<TElement> innards;

            public OpenGenResolver(IOpenGenResolverInnards<TElement> innards)
            {
                this.innards = innards;
            }

            public IEnumerable<(PropertyInfo, IOpenGen<TElement>)> ResolveForEachProperty()
            {
                return innards.Run<IOpenGen<TElement>>(
                    propertyType => typeof(IOpenGen<,>)
                        .MakeGenericType(typeof(TElement), propertyType));
            }
        }

        private interface IOpenGenResolverInnards<TElement>
        {
            IEnumerable<(PropertyInfo, TResult)> Run<TResult>(Func<Type, Type> makeGenericFunc);
        }
        private class OpenGenResolverInnards<TElement> : IOpenGenResolverInnards<TElement>
        {
            private readonly IResolverContext scope;

            public OpenGenResolverInnards(IResolverContext scope)
            {
                this.scope = scope;
            }

            public IEnumerable<(PropertyInfo, TResult)> Run<TResult>(Func<Type, Type> makeGenericFunc)
            {
                foreach(var prop in typeof(TElement).GetProperties())
                {
                    var propertyType = prop.PropertyType;

                    var elemType = makeGenericFunc(propertyType);

                    var q = scope.Resolve(elemType);

                    yield return (prop, (TResult)q);
                }

            }
        }

        private class MyClass
        {
            public string? Name { get; set; }
            public int Value { get; set; }
        }

        private interface IOpenGen<TElement>
        {
            object? GetValue(TElement element);
        }
        private interface IOpenGen<TElement, TValue> : IOpenGen<TElement>
        {
            new TValue? GetValue(TElement element);
        }

        private class OpenGen<TElement, TValue> : IOpenGen<TElement, TValue>
        {
            public TValue? GetValue(TElement element)
            {
                return default;
            }

            object? IOpenGen<TElement>.GetValue(TElement element)
            {
                return (object?)this.GetValue(element);
            }
        }
    }
}
