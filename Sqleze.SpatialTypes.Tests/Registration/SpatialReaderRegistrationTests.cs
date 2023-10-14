using Microsoft.SqlServer.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using Sqleze.Readers;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.TestUtil;

namespace Sqleze.SpatialTypes.Tests.Registration
{
    [TestClass]
    public class SpatialReaderRegistrationTests
    {
        [TestMethod]
        public void ReaderRegistrationGeography()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();
            container.RegisterSpatialReaders();

            container.Resolve<IReader<SqlGeography>>().ShouldBeOfType<ScalarReader<SqlGeography>>();
        }

        [TestMethod]
        public void ReaderRegistrationGeographyNullable()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();
            container.RegisterSpatialReaders();

            container.Resolve<IReader<SqlGeography?>>().ShouldBeOfType<ScalarReader<SqlGeography>>();
        }

        [TestMethod]
        public void ReaderRegistrationGeometry()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();
            container.RegisterSpatialReaders();

            container.Resolve<IReader<SqlGeometry>>().ShouldBeOfType<ScalarReader<SqlGeometry>>();
        }

        [TestMethod]
        public void ReaderRegistrationGeometryNullable()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();
            container.RegisterSpatialReaders();

            container.Resolve<IReader<SqlGeometry?>>().ShouldBeOfType<ScalarReader<SqlGeometry>>();
        }

        [TestMethod]
        public void ReaderRegistrationHierarchyId()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();
            container.RegisterSpatialReaders();

            container.Resolve<IReader<SqlHierarchyId>>().ShouldBeOfType<ScalarReader<SqlHierarchyId>>();
        }

        [TestMethod]
        public void ReaderRegistrationHierarchyIdNullable()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.RegisterSqlezeReaders();
            container.RegisterSpatialReaders();

            container.Resolve<IReader<SqlHierarchyId?>>().ShouldBeOfType<ScalarReader<SqlHierarchyId?>>();
        }
    }
}