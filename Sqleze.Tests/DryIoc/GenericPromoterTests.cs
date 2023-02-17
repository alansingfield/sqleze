using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Shouldly;
using Sqleze.DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.DryIoc
{
    [TestClass]
    public class GenericPromoterTests
    {

        [TestMethod]
        public void GenericPromoterUni()
        {
            var promoter = GenericPromoter.For(typeof(IUniService<>));

            var result = promoter.Promote(typeof(IUniService), typeof(int));

            result.ShouldBe(typeof(IUniService<int>));
        }

        [TestMethod]
        public void GenericPromoterDuo()
        {
            var promoter = GenericPromoter.For(typeof(IDuoService<,>));

            var result = promoter.Promote(typeof(IDuoService<string>), typeof(int));

            result.ShouldBe(typeof(IDuoService<string, int>));
        }

        [TestMethod]
        public void GenericPromoterWrongImpl()
        {
            var promoter = GenericPromoter.For(typeof(IDuoService<>));

            Should.Throw(() =>
            {
                var result = promoter.Promote(typeof(IUniService), typeof(int));
            }, typeof(Exception)).Message.ShouldBe(
                "Generic promotion of type IUniService resulted in type IDuoService`1 which does not implement the former interface.");
        }

        public interface IUniService { }

        public interface IUniService<TValue> : IUniService { }

        public class UniService<TValue> : IUniService<TValue> { }

        public class UniServiceConsumer
        {
            public UniServiceConsumer(IUniService uniService)
            {
            }
        }

        public interface IDuoService<TSomething> { }

        public interface IDuoService<TSomething, TValue> : IDuoService<TSomething> { }

        public class DuoService<TValue> : IDuoService<TValue> { }

    }
}
