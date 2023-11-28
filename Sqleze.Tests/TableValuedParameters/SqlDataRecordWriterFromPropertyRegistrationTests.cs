using Sqleze.DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Sqleze.Registration;
using Sqleze.TableValuedParameters;
using Sqleze.Metadata;

namespace Sqleze.Tests.TableValuedParameters;
[TestClass]
public class SqlDataRecordWriterFromPropertyRegistrationTests
{
    [TestMethod]
    public void SqlDataRecordWriterFromPropertyGenericPromotionOK()
    {
        var container = new Container().WithNSubstituteFallback();

        container.RegisterGenericResolver();
        container.RegisterSqlDataRecordWriterFromProperty();
        container.RegisterDynamicPropertyCaller();

        var resolver = container.Resolve<IGenericResolver<ISqlDataRecordWriterFromProperty<MyClass>>>();

        var mc = new MyClass();
        var tt = new TableTypeColumnDefinition("Z", 0, "varchar", 20, 0, 0, true);

        var result = resolver.Resolve(typeof(int), new object[] { mc, tt });

        result.ShouldBeOfType<SqlDataRecordWriterFromProperty<MyClass, int>>();
    }

    private class MyClass { }
}
