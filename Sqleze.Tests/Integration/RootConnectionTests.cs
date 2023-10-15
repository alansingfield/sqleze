using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIOC = DryIoc;

namespace Sqleze.Tests.Integration;

[TestClass]
public class RootConnectionTests
{
    //[TestMethod]
    //public void SecondaryContainerTest()
    //{
    //    var container = DI.NewContainer().WithNSubstituteFallback();

    //    //var configuration = Substitute.For<IConfiguration>();
    //    container.RegisterTestSettings();

    //    container.Register<ISqlezeBuilder>(Reuse.Singleton,
    //        made: Made.Of(() => sqlezeFactory(DIOC.Arg.Of<IConfiguration>())
    //        ));

    //    using var scope = container.OpenScope();

    //    var sqleze = scope.Resolve<ISqlezeBuilder>();

    //    using var conn = sqleze.Connect();

    //    var cmd = conn.Sql("SELECT @arg");
    //    cmd.Parameters.Set("@arg", 123);
    //    cmd.ReadSingleOrDefault<int>().ShouldBe(123);
    //}

    //private ISqlezeBuilder sqlezeFactory(IConfiguration configuration)
    //{
    //    return opencontainer
    //        .WithConfiguration(configuration)
    //        .WithConfigKey("ConnectionString");
    //}

    //private IContainer openContainer()
    //{
    //    var container = DI.NewContainer();

    //    container.RegisterSqleze();
    //    container.RegisterTestSettings();

    //    return container;
    //}
}
