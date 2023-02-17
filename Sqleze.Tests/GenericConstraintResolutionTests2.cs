using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.DryIoc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sqleze.Tests
{
    [TestClass]
    public class GenericConstraintResolutionTests2
    {

        public interface IMyThing<T> { }
        public interface IMyThing<T, TValue> : IMyThing<T>
            where T : IEnumerable<TValue>
        { }
        public class MyThing<T, TValue> : IMyThing<T, TValue>
            where T : IEnumerable<TValue>
        { }


        [TestMethod]
        public void ListDoesMatch()
        {
            var container = new Container();

            container.Register(typeof(IMyThing<>), typeof(MyThing<,>),
                Reuse.ScopedOrSingleton);

            container.Resolve<IMyThing<List<int>>>().ShouldBeOfType<MyThing<List<int>, int>>();
        }

        [TestMethod]
        public void ArrayDoesntMatch()
        {
            var container = new Container();

            container.Register(typeof(IMyThing<>), typeof(MyThing<,>),
                Reuse.ScopedOrSingleton);

            // Fails with DryIoc.ContainerException: code: Error.NoMatchedGenericParamConstraints
            container.Resolve<IMyThing<int[]>>().ShouldBeOfType<MyThing<int[], int>>();
        }



        [TestMethod]
        public void ArrayThingIsConstructable()
        {
            var q = new MyThing<int[], int>();
            q.ShouldNotBeNull();
        }


    }

}

