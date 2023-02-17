using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Registration
{
    [TestClass]
    public class MicrosoftSqlClientRegistrationTests
    {
        [TestMethod]
        public void MicrosoftSqlClientRegistered()
        {
            var container = new Container();

            container.RegisterMicrosoftSqlClient();

            // The container takes over new() for these two objects, make sure
            // we can do this successfully (chooses the parameterless constructor)
            container.Resolve<MS.SqlConnection>();
            container.Resolve<MS.SqlCommand>();
        }
    }
}
