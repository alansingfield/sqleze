using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Integration;

[TestClass]
public class DictionaryReadTests
{

    [TestMethod]
    public void DictionaryRead1()
    {
        using var connection = connect();

        var result = connection.Sql("SELECT name = 'John', age = 20")
            .ReadSingle<Dictionary<string, object?>>();

        result.Keys.ShouldBe(new[] { "name", "age" });
        result["name"].ShouldBe("John");
        result["age"].ShouldBe(20);

    }

    [TestMethod]
    public void DictionaryDynamic()
    {
        using var connection = connect();

        dynamic result = connection.Sql("SELECT name = 'John', age = 20")
            .ReadSingle<ExpandoObject>();

        ((string)result.name).ShouldBe("John");
        ((int)result.age).ShouldBe(20);

    }

    private ISqlezeConnection connect()
    {
        var container = new Container();

        container.RegisterSqleze();
        container.RegisterTestSettings();

        return container.Resolve<ISqlezeBuilder>()
            .WithConfigKey("DefaultConnection")
            .Connect();
    }
}
