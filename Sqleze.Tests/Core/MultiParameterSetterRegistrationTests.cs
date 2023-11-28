using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.DryIoc;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Core;

[TestClass]
public class MultiParameterSetterRegistrationTests
{
    [TestMethod]
    public void MultiParameterSetterGenericPromotionOK()
    {
        var container = new Container().WithNSubstituteFallback();

        container.RegisterGenericResolver();
        container.RegisterMultiParameterSetter();

        var resolver = container.Resolve<IGenericResolver<IParameterSetter<MyClass>>>();

        var mc = new MyClass();
        var result = resolver.Resolve(typeof(int), new[] { mc });

        result.ShouldBeOfType<ParameterSetter<MyClass, int>>();
    }

    private class MyClass { }
}
