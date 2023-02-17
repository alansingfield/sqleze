using Shouldly;
using Sqleze.Dynamics;

namespace Sqleze.Tests.Dynamics;

[TestClass]
public class DynamicPropertyCallerTests
{
    [TestMethod]
    public void DynamicPropertyCallerGet()
    {
        var cont = openContainer();

        var dynamicPropertyCaller = cont.Resolve<IDynamicPropertyCaller<Foo>>();

        var foo = new Foo() { FooId = 10, FooName = "Blah" };
        var getFunc = dynamicPropertyCaller.CompilePropertyGetFunc(new[] { "FooId", "FooName" });

        var data = getFunc(foo);

        data.Length.ShouldBe(2);
        data[0].ShouldBe(10);
        data[1].ShouldBe("Blah");

    }

    [TestMethod]
    public void DynamicPropertyCallerGetSingle()
    {
        var cont = openContainer();

        var dynamicPropertyCaller = cont.Resolve<IDynamicPropertyCaller<Foo>>();

        var foo = new Foo() { FooId = 10, FooName = "Blah" };
        var getFunc = dynamicPropertyCaller.CompilePropertyGetFunc("FooId");

        var data = getFunc(foo);

        data.ShouldBe(10);
        

    }

    [TestMethod]
    public void DynamicPropertyCallerSet()
    {
        var cont = openContainer();

        var dynamicPropertyCaller = cont.Resolve<IDynamicPropertyCaller<Foo>>();

        var foo = new Foo() { FooId = 10, FooName = "Blah" };
        var setAction = dynamicPropertyCaller.CompilePropertySetAction(new[] { "FooId", "FooName" });

        setAction(foo, new object[] { 20, "Yellow" });

        foo.FooId.ShouldBe(20);
        foo.FooName.ShouldBe("Yellow");

    }

    [TestMethod]
    public void DynamicPropertyCallerOmitProp()
    {
        var cont = openContainer();

        var dynamicPropertyCaller = cont.Resolve<IDynamicPropertyCaller<Foo>>();

        var foo = new Foo() { FooId = 10, FooName = "Blah" };
        var setAction = dynamicPropertyCaller.CompilePropertySetAction(new[]
        {
            null,       // Passing null for a property name skips over that position in the array
            "FooId",
            "FooName"
            });

        setAction(foo, new object[] { 9999, 20, "Yellow" });

        foo.FooId.ShouldBe(20);
        foo.FooName.ShouldBe("Yellow");

    }

    [TestMethod]
    public void DynamicPropertyCallerNotNull()
    {
        var cont = openContainer();

        var dynamicPropertyCaller = cont.Resolve<IDynamicPropertyCaller<FooNotNull>>();

        var foo = new FooNotNull() { FooId = 10, FooName = "Blah" };
        var setAction = dynamicPropertyCaller.CompilePropertySetAction(new[]
        {
            "FooId",
            "FooName"
        });

        setAction(foo, new object?[] { null, null });

        foo.FooId.ShouldBe(0);
        foo.FooName.ShouldBe("");

    }

    private IContainer openContainer()
    {
        var container = new Container().WithNSubstituteFallback();
        container.Register(typeof(IDynamicPropertyCaller<>), typeof(DynamicPropertyCaller<>), Reuse.Singleton);
        container.Register<IDefaultFallbackExpressionBuilder, DefaultFallbackExpressionBuilder>(Reuse.Singleton);

        return container;
    }


    private class Foo
    {
        public int FooId { get; set; }
        public string? FooName { get; set; }
    }
    private class FooNotNull
    {
        public int FooId { get; set; }
        public string FooName { get; set; } = "initial value";
    }

}
