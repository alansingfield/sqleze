using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.TableValuedParameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.TableValuedParameters
{
    [TestClass]
    public class SqlMetaDataFactoryTests
    {
        [TestMethod]
        public void SqlMetaDataFactoryStandard()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.Register<MSS.SqlMetaData>(made: FactoryMethod.ConstructorWithResolvableArguments);

            container.Register<ISqlMetaDataFactory, SqlMetaDataFactory>();

            var factory = container.Resolve<ISqlMetaDataFactory>();

            var intMeta = factory.New("colname", SqlDbType.Int);

            intMeta.Name.ShouldBe("colname");
            intMeta.SqlDbType.ShouldBe(SqlDbType.Int);
        }

        [TestMethod]
        public void SqlMetaDataFactorySized()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.Register<MSS.SqlMetaData>(made: FactoryMethod.ConstructorWithResolvableArguments);

            container.Register<ISqlMetaDataFactory, SqlMetaDataFactory>();

            var factory = container.Resolve<ISqlMetaDataFactory>();

            var intMeta = factory.New("colname", SqlDbType.VarChar, 50);

            intMeta.Name.ShouldBe("colname");
            intMeta.SqlDbType.ShouldBe(SqlDbType.VarChar);
            intMeta.MaxLength.ShouldBe(50);
        }

        [TestMethod]
        public void SqlMetaDataFactoryScaled()
        {
            var container = DI.NewContainer().WithNSubstituteFallback();

            container.Register<MSS.SqlMetaData>(made: FactoryMethod.ConstructorWithResolvableArguments);

            container.Register<ISqlMetaDataFactory, SqlMetaDataFactory>();

            var factory = container.Resolve<ISqlMetaDataFactory>();

            var intMeta = factory.New("colname", SqlDbType.Decimal, (byte)18, (byte)2);

            intMeta.Name.ShouldBe("colname");
            intMeta.SqlDbType.ShouldBe(SqlDbType.Decimal);
            intMeta.Precision.ShouldBe((byte)18);
            intMeta.Scale.ShouldBe((byte)2);
        }
    }
}
