using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqleze.Microsoft.DependencyInjection.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Microsoft.DependencyInjection.Tests.MSDI;

[TestClass]
public class LowCeremonyTests
{
    //[TestMethod]
    //public void MyTestMethod()
    //{
    //    var services = new ServiceCollection();

    //   // services.AddSqleze();

    //    var serviceProvider = services.BuildServiceProvider();

    //    var scope1 = serviceProvider.CreateScope();
    //    var scope2 = serviceProvider.CreateScope();

    //    var homeSql1 = scope1.ServiceProvider.GetRequiredService<ISqleze>();
    //    var homeSql2 = scope2.ServiceProvider.GetRequiredService<ISqleze>();

    //}
}
