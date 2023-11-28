using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.DryIoc;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sqleze.Tests.DryIoc.ForEachPropertyTests;

namespace Sqleze.Tests.DryIoc
{
    [TestClass]
    public class ForEachPropertyTests
    {
        [TestMethod]
        public void MultiResolver1()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterGenericResolver();
            container.RegisterMultiResolver();
            container.Register(typeof(IMyService<,>), typeof(MyService<,>));
            container.RegisterGenericPromotion(typeof(IMyService<>), typeof(IMyService<,>));

            var resolver = container.Resolve<IMultiResolver<IMyService<MyEntity>, MyEntity>>();

            var entity = new MyEntity();

            var results = resolver.ResolvePerProperty()
                .ToList();

            results[0].ShouldBeOfType<MyService<MyEntity, string?>>();
            results[1].ShouldBeOfType<MyService<MyEntity, int>>();
        }


        public class MyEntity
        {
            public string? Name { get; set; }
            public int Number { get; set; }
        }

        public interface IMyService<TSomething>
        {
            TSomething? Something { get; set; }
            object? Value { get; set; }
        }

        public interface IMyService<TSomething, TValue> : IMyService<TSomething>
        {
            new TValue? Value { get; set; }
        }

        public class MyService<TSomething, TValue> : IMyService<TSomething, TValue>
        {
            public TValue? Value { get; set; }
            public TSomething? Something { get; set; }
            object? IMyService<TSomething>.Value { get => this.Value; set => this.Value = (TValue?)value; }
        }

    }
}
