using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.DryIoc;
using Sqleze.Readers;
using Sqleze.Registration;
using Shouldly;
using Sqleze.ValueGetters;

namespace Sqleze.Tests.Readers;

[TestClass]
public class ColumnPropertyRegistrationTests
{
    [TestMethod]
    public void ColumnPropertyGenericPromotionOK()
    {
        var container = new Container().WithNSubstituteFallback();

        container.RegisterGenericResolver();
        container.RegisterColumnProperty();

        var resolver = container.Resolve<IGenericResolver<IColumnProperty<MyClass, int>>>();

        var mc = new MyClass();
        var drf = new DataReaderFieldInfo(0, "X", "int");
        var sqldbtype = Substitute.For<IKnownSqlDbType>();

        var result = resolver.Resolve(typeof(IKnownSqlDbTypeInt), new object[] { mc, drf });

        result.ShouldBeOfType<ColumnProperty<MyClass, int, IKnownSqlDbTypeInt>>();
    }

    private class MyClass { }
}
