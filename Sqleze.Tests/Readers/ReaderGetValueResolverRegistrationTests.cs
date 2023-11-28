using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.DryIoc;
using Sqleze.Registration;
using Shouldly;
using Sqleze.Readers;
using Sqleze.ValueGetters;

namespace Sqleze.Tests.Readers;

[TestClass]
public class ReaderGetValueResolverRegistrationTests
{
    [TestMethod]
    public void ReaderGetValueResolverGenericPromotionOK()
    {
        var container = new Container().WithNSubstituteFallback();

        container.RegisterGenericResolver();
        container.RegisterReaderGetValueResolver();

        var resolver = container.Resolve<IGenericResolver<IReaderGetValueResolver<MyClass>>>();

        var mc = new MyClass();
        var result = resolver.Resolve(typeof(IKnownSqlDbTypeInt), new[] { mc });

        result.ShouldBeOfType<ReaderGetValueResolver<MyClass, IKnownSqlDbTypeInt>>();
    }

    public class MyClass { }
}
