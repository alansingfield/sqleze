using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Util;
using System.Linq.Expressions;
using Shouldly;
using Sqleze.Dynamics;
using TestCommon.TestUtil;
using System.Net.Http.Headers;

namespace Sqleze.Tests.Dynamics
{
    [TestClass]
    public class RecordConstructorCacheTests
    {
        [TestMethod]
        public void ReadRecordConstructor()
        {
            var t = typeof(Example);

            var fieldnames = new string[] { "Name", "Number" };

            var dictFieldNameIndexes = fieldnames
                .SelectIndexed()
                .ToDictionary(x => x.Item, x => x.Index);

            var cons = t.GetConstructors()[0];

            var parms = cons.GetParameters();

            if(parms.Any(x => x.Name == null))
                throw new Exception($"Unable to build constructor for type {t.Name} because it has nameless parameters");

            var paramArgsDict = parms.Select((parm, idx) => new
            {
                Name = parm.Name ?? "",
                Index = idx,
                ParameterInfo = parm
            })
                .ToDictionary(x => x.Name, x => new { x.Index, x.ParameterInfo });

            if(paramArgsDict.Any(x => !fieldnames.Contains(x.Key)))
                throw new Exception($"No value was supplied for constructor parameter");

            // Input parameter to the required Action is object?[]
            var values = Expression.Parameter(typeof(object?[]), "values");

            Expression[] consArgExprs = parms.Select(x => new
            {
                ParameterInfo = x,
                Index = dictFieldNameIndexes[x.Name ?? ""]  // Position within the object[] arg
            })
                .Select(x =>
                    // Convert.ChangeType(input[fieldIdx], parameterType)
                    Expression.Convert(
                        Expression.ArrayAccess(
                            values,
                            Expression.Constant(x.Index)),
                        x.ParameterInfo.ParameterType))
                .ToArray();

            var newExpression = Expression.New(cons, consArgExprs);

            var lambdaFunc = (Func<object?[], Example>)
                Expression.Lambda(newExpression,
                new ParameterExpression[] { values })
                .Compile();

            var args1 = new object?[] { "XXXX", 1234 };

            var example = lambdaFunc(args1);

            example.ShouldNotBeNull();
            example.Name.ShouldBe("XXXX");
            example.Number.ShouldBe(1234);
        }

        [TestMethod]
        public void RecordConstructor2()
        {
            var container = openContainer();
            var c = container.Resolve<IConstructorLambdaBuilder<Example>>();

            var fn = c.GetConstructorFunc(typeof(Example).GetConstructors()[0]);

            var example = fn(
                new object?[] { "XXXX", 1234 },
                new int[] { 0, 1 });

            example.ShouldNotBeNull();
            example.Name.ShouldBe("XXXX");
            example.Number.ShouldBe(1234);
        }

        [TestMethod]
        public void RecordConstructor3()
        {
            var container = openContainer();
            var c = container.Resolve<IConstructorLambdaBuilder<Example>>();

            var fn = c.GetConstructorFunc(typeof(Example).GetConstructors()[0]);

            var example = fn(
                new object?[] { "XXXX", 1234 },
                new int[] { -1, 1 });

            example.ShouldNotBeNull();
            example.Name.ShouldBe("");
            example.Number.ShouldBe(1234);
        }
        [TestMethod]
        public void RecordConstructor4()
        {
            var container = openContainer();
            var c = container.Resolve<IConstructorLambdaBuilder<Example>>();

            var fn = c.GetConstructorFunc(typeof(Example).GetConstructors()[0]);

            var example = fn(
                new object?[] { null, 1234 },
                new int[] { 0, 1 });

            example.ShouldNotBeNull();
            example.Name.ShouldBe("");
            example.Number.ShouldBe(1234);
        }

        [TestMethod]
        public void RecordConstructor5()
        {
            var container = openContainer();
            var c = container.Resolve<IConstructorLambdaBuilder<Example>>();

            var fn = c.GetConstructorFunc(typeof(Example).GetConstructors()[0]);

            var example = fn(
                new object?[] { "XXXX", null },
                new int[] { 0, 1 });

            example.ShouldNotBeNull();
            example.Name.ShouldBe("XXXX");
            example.Number.ShouldBe(0);
        }

        [TestMethod]
        public void RecordConstructor6()
        {
            var container = openContainer();
            var c = container.Resolve<IConstructorLambdaBuilder<ByteArrExample>>();

            var fn = c.GetConstructorFunc(typeof(ByteArrExample).GetConstructors()[0]);

            var example = fn(
                new object?[] { null, null },
                new int[] { 0, 1 });

            example.ShouldNotBeNull();
            example.NonNullableByteArray.ShouldNotBeNull();
            example.NonNullableByteArray.Length.ShouldBe(0);
            example.NullableByteArray.ShouldBeNull();
        }


        private IContainer openContainer()
        {
            var container = new Container().WithNSubstituteFallback();
            container.Register(typeof(IConstructorLambdaBuilder<>), typeof(ConstructorLambdaBuilder<>));
            container.Register<IDefaultFallbackExpressionBuilder, DefaultFallbackExpressionBuilder>(Reuse.Singleton);

            return container;
        }


        public record Example
            (string Name, int Number);

        public record ByteArrExample
        (
            byte[] NonNullableByteArray,
            byte[]? NullableByteArray
        );
    }
}
