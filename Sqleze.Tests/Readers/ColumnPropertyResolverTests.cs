using Sqleze.DryIoc;
using Sqleze.Readers;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

namespace Sqleze.Tests.Readers;

[TestClass]
public class ColumnPropertyResolverTests
{
    [TestMethod]
    public void ColumnPropertyResolverGenericPromotionOK()
    {
        var container = new Container().WithNSubstituteFallback();

        container.RegisterGenericResolver();
        container.RegisterColumnPropertyResolver();

        var resolver = container.Resolve<IGenericResolver<IColumnPropertyResolver<MyClass>>>();

        var mc = new MyClass();
        var drf = new DataReaderFieldInfo(0, "X", "varchar");
        var result = resolver.Resolve(typeof(int), new object[] { mc, drf });

        result.ShouldBeOfType<ColumnPropertyResolver<MyClass, int>>();
    }

    private class MyClass { }
}
