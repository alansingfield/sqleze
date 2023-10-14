using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.ValueGetters;
using Shouldly;
using Sqleze.SpatialTypes.ValueGetters;
using Microsoft.SqlServer.Types;
using Sqleze.Registration;

namespace Sqleze.SpatialTypes.Tests.ValueGetters;

[TestClass]
public class RegisterSpatialTypeValueGettersTests
{
    [TestMethod]
    public void SpatialTypeValueGettersResolve()
    {
        var container = DI.NewContainer();

        container.RegisterValueGetters();
        container.RegisterSpatialTypeValueGetters();

        container.Resolve<IReaderGetValue<SqlGeography>>().ShouldBeOfType<ReaderGetValueSqlGeography>();
        container.Resolve<IReaderGetValue<SqlGeometry>>().ShouldBeOfType<ReaderGetValueSqlGeometry>();
        container.Resolve<IReaderGetValue<SqlHierarchyId>>().ShouldBeOfType<ReaderGetValueSqlHierarchyId>();
        container.Resolve<IReaderGetValue<SqlHierarchyId?>>().ShouldBeOfType<ReaderGetValueSqlHierarchyIdNullable>();
    }
}
