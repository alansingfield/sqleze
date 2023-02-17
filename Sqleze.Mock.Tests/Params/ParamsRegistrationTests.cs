using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using Sqleze.Options;
using Sqleze.Params;
using Sqleze.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Params
{
    [TestClass]
    public class ParamsRegistrationTests
    {
        [TestMethod]
        public void ParamsRegistrationChooseValueParamInt()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterParameterPreparation();
            container.Use(CommandCreateOptions.Default);

            using var scope = container.OpenScope();

            scope.Resolve<IAdoParameterFactory<int?>>().ShouldBeOfType<AdoParameterFactory<int?>>();
        }
        [TestMethod]
        public void ParamsRegistrationChooseValueParamString()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterParameterPreparation();
            container.Use(CommandCreateOptions.Default);

            using var scope = container.OpenScope();

            scope.Resolve<IAdoParameterFactory<string>>().ShouldBeOfType<AdoParameterFactory<string>>();
            scope.Resolve<IAdoParameterFactory<string?>>().ShouldBeOfType<AdoParameterFactory<string?>>();
        }

        [TestMethod]
        public void ParamsRegistrationChooseValueParamByteArray()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterParameterPreparation();
            container.Use(CommandCreateOptions.Default);

            using var scope = container.OpenScope();

            scope.Resolve<IAdoParameterFactory<byte[]>>().ShouldBeOfType<AdoParameterFactory<byte[]>>();
            scope.Resolve<IAdoParameterFactory<byte?[]>>().ShouldBeOfType<AdoParameterFactory<byte?[]>>();
            scope.Resolve<IAdoParameterFactory<byte[]?>>().ShouldBeOfType<AdoParameterFactory<byte[]?>>();
            scope.Resolve<IAdoParameterFactory<byte?[]?>>().ShouldBeOfType<AdoParameterFactory<byte?[]?>>();
        }

        [TestMethod]
        public void ParamsRegistrationChooseTableTypeParamList()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterParameterPreparation();

            using var scope = container.OpenScope();

            scope.Resolve<IAdoParameterFactory<List<MyClass>>>()
                .ShouldBeOfType<TableTypeAdoParameterFactory<List<MyClass>, MyClass>>();
        }

        [TestMethod]
        public void ParamsRegistrationChooseTableTypeParamArray()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterParameterPreparation();

            using var scope = container.OpenScope();

            scope.Resolve<IAdoParameterFactory<MyClass[]>>()
                .ShouldBeOfType<TableTypeAdoParameterFactory<MyClass[], MyClass>>();
        }

        [TestMethod]
        public void ParamsRegistrationChooseTableTypeParamScalarList()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterParameterPreparation();

            using var scope = container.OpenScope();

            scope.Resolve<IAdoParameterFactory<List<int>>>()
                .ShouldBeOfType<TableTypeAdoParameterFactory<List<int>, int>>();
        }

        [TestMethod]
        public void ParamsRegistrationChooseTableTypeParamScalarArray()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterParameterPreparation();

            using var scope = container.OpenScope();

            scope.Resolve<IAdoParameterFactory<int[]>>()
                .ShouldBeOfType<TableTypeAdoParameterFactory<int[], int>>();
        }


        [TestMethod]
        public void ParamsRegistrationChooseTableTypeParamEnumerable()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterParameterPreparation();

            using var scope = container.OpenScope();

            scope.Resolve<IAdoParameterFactory<IEnumerable<MyClass>>>()
                .ShouldBeOfType<TableTypeAdoParameterFactory<IEnumerable<MyClass>, MyClass>>();
        }

        [TestMethod]
        public void ParamsRegistrationChooseTableTypeParamScalarEnumerable()
        {
            var container = new Container().WithNSubstituteFallback();

            container.RegisterParameterPreparation();

            using var scope = container.OpenScope();

            scope.Resolve<IAdoParameterFactory<IEnumerable<int>>>()
                .ShouldBeOfType<TableTypeAdoParameterFactory<IEnumerable<int>, int>>();
        }

        public class MyClass { }
    }
}
