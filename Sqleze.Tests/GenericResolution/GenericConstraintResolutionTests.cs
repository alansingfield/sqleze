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

namespace Sqleze.Tests.GenericResolution
{
    [TestClass]
    public class GenericConstraintResolutionTests
    {
        public class MyFactory<T> : IMyFactory<T>
        { }

        public class MyFactoryForEnumerables<T, TValue> : IMyFactory<T, TValue>
            where T : IEnumerable<TValue>
        { }

        public interface IMyFactory<T> { }
        public interface IMyFactory<T, TValue> : IMyFactory<T>
            where T : IEnumerable<TValue>
        { }

        private IContainer openContainer()
        {
            var container = DI.NewContainer();

            // Normal impl for non-enumerables.
            container.Register(typeof(IMyFactory<>), typeof(MyFactory<>),
                Reuse.ScopedOrSingleton);

            // Use a different impl for IEnumerables
            container.Register(typeof(IMyFactory<>), typeof(MyFactoryForEnumerables<,>),
                Reuse.ScopedOrSingleton,
                setup: Setup.With(condition: GenericArgIs(
                    x => x.IsAssignableTo<IEnumerable>()
                    )));

            return container;
        }

        private Func<Request, bool> GenericArgIs(Func<Type, bool> argCondition, int argPosition = 0) =>
            request =>
            {
                var serviceType = request.ServiceType;

                if (!serviceType.IsClosedGeneric())
                    return false;

                var genArg = serviceType.GetGenericArguments().Skip(argPosition).FirstOrDefault();

                if (genArg == null)
                    return false;

                return argCondition(genArg);
            };

        [TestMethod]
        public void XXXX()
        {
            var container = DI.NewContainer();

            container.Register(typeof(IMyFactory<>), typeof(MyFactoryForEnumerables<,>),
                Reuse.ScopedOrSingleton);

            container.Resolve<IMyFactory<int[]>>().ShouldBeOfType<MyFactoryForEnumerables<int[], int>>();
        }


        [TestMethod]
        public void MyFactoryForEnumerablesWithArrayConstructs()
        {
            var q = new MyFactoryForEnumerables<int[], int>();
            q.ShouldNotBeNull();
        }

        [TestMethod]
        public void YYYY()
        {
            var container = DI.NewContainer();

            container.Register(typeof(IMyFactory<>), typeof(MyFactoryForEnumerables<,>),
                Reuse.ScopedOrSingleton);

            container.Resolve<IMyFactory<List<int>>>().ShouldBeOfType<MyFactoryForEnumerables<List<int>, int>>();
        }

        [TestMethod]
        public void EnumerableConstraintArray()
        {
            var container = openContainer();

            container.Resolve<IMyFactory<int[]>>().ShouldBeOfType<MyFactoryForEnumerables<int[], int>>();
        }

        [TestMethod]
        public void EnumerableConstraintOK()
        {
            var container = openContainer();

            container.Resolve<IMyFactory<int>>().ShouldBeOfType<MyFactory<int>>();

            container.Resolve<IMyFactory<List<int>>>().ShouldBeOfType<MyFactoryForEnumerables<List<int>, int>>();
        }



        [TestMethod]
        public void GenericArgIsScalarFalse()
        {
            var container = openContainer();

            var r = Request.Create(container, ServiceInfo.Of<IMyFactory<int>>());
            GenericArgIs(x => x.IsAssignableTo<IEnumerable>())(r).ShouldBe(false);
        }

        [TestMethod]
        public void GenericArgIsEnumerableTrue()
        {
            var container = openContainer();

            var r = Request.Create(container, ServiceInfo.Of<IMyFactory<IEnumerable<int>>>());
            GenericArgIs(x => x.IsAssignableTo<IEnumerable>())(r).ShouldBe(true);
        }

        [TestMethod]
        public void GenericArgIsArrayTrue()
        {
            var container = openContainer();

            var r = Request.Create(container, ServiceInfo.Of<IMyFactory<int[]>>());
            GenericArgIs(x => x.IsAssignableTo<IEnumerable>())(r).ShouldBe(true);
        }



    }

}

