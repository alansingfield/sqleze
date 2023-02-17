using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests
{
    [TestClass]
    public class PerPropResolveTests
    {
        [TestMethod]
        public void PerPropResolve1()
        {
            var container = new Container();
            container.Register(typeof(MyResolver<,>));
            container.Register(typeof(IOpenGen<>), typeof(OpenGen<>));

            var resolver = container.Resolve<MyResolver<MyClass, IOpenGen>>();

            foreach(var (propInfo, openGen) in resolver.ResolveForEachProperty(typeof(IOpenGen<>)))
            {

            }
        }

        private class MyResolver<T, TService>
        {
            private readonly IResolverContext scope;

            public MyResolver(IResolverContext scope)
            {
                this.scope = scope;
            }

            public IEnumerable<(PropertyInfo, TService)> ResolveForEachProperty(Type openGen)
            {
                if(!openGen.IsOpenGeneric())
                    throw new Exception("openGen must be open generic");

                if(!typeof(TService).IsAssignableFrom(openGen))
                    throw new Exception($"The type {openGen} does not implement service {typeof(TService)}");

                foreach(var prop in typeof(T).GetProperties())
                {
                    var propertyType = prop.PropertyType;

                    var elemType = openGen.MakeGenericType(propertyType);

                    yield return (prop, (TService)scope.Resolve(elemType));
                }
            }
        }

        private class MyClass
        {
            public string? Name { get; set; }
            public int Value { get; set; }
        }

        private interface IOpenGen
        {
            object? Value { get; set; }
        }
        private interface IOpenGen<T> : IOpenGen
        {
            new T? Value { get; set; }
        }

        private class OpenGen<T> : IOpenGen<T>
        {
            public T? Value { get; set; }
            object? IOpenGen.Value
            {
                get => Value;
                set => this.Value = (T?)value;
            }
        }
    }
}
