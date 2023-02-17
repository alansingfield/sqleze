using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze.Params;
using Sqleze.Registration;
using Sqleze.Sizing;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Tests.Params;

[TestClass]
public class ValueSizeMeasurerRegistrationTests
{
    [TestMethod]
    public void SizeMeasurerRegistrationBaseTypes()
    {
        var container = new Container();

        container.RegisterValueSizeMeasurers();

        container.Resolve<IValueSizeMeasurer<string>>().ShouldBeOfType<ValueSizeMeasurerStringNullable>();
        container.Resolve<IValueSizeMeasurer<string?>>().ShouldBeOfType<ValueSizeMeasurerStringNullable>();

        container.Resolve<IValueSizeMeasurer<byte[]>>().ShouldBeOfType<ValueSizeMeasurerByteArrayNullable>();
        container.Resolve<IValueSizeMeasurer<byte[]?>>().ShouldBeOfType<ValueSizeMeasurerByteArrayNullable>();
    }

    [TestMethod]
    public void SizeMeasurerRegistrationSqlTypes()
    {
        var container = new Container();

        container.RegisterValueSizeMeasurers();

        container.Resolve<IValueSizeMeasurer<SqlString>>().ShouldBeOfType<ValueSizeMeasurerSqlString>();
        container.Resolve<IValueSizeMeasurer<SqlString?>>().ShouldBeOfType<ValueSizeMeasurerSqlStringNullable>();
        container.Resolve<IValueSizeMeasurer<SqlBinary>>().ShouldBeOfType<ValueSizeMeasurerSqlBinary>();
        container.Resolve<IValueSizeMeasurer<SqlBinary?>>().ShouldBeOfType<ValueSizeMeasurerSqlBinaryNullable>();

        container.Resolve<IValueSizeMeasurer<SqlBytes>>().ShouldBeOfType<ValueSizeMeasurerSqlBytes>();
        container.Resolve<IValueSizeMeasurer<SqlBytes?>>().ShouldBeOfType<ValueSizeMeasurerSqlBytes>();

        container.Resolve<IValueSizeMeasurer<SqlChars>>().ShouldBeOfType<ValueSizeMeasurerSqlChars>();
        container.Resolve<IValueSizeMeasurer<SqlChars?>>().ShouldBeOfType<ValueSizeMeasurerSqlChars>();
    }

    [TestMethod]
    public void SizeMeasurerRegistrationFallback()
    {
        var container = new Container();

        container.RegisterValueSizeMeasurers();

        container.Resolve<IValueSizeMeasurer<int>>().ShouldBeOfType<ValueSizeMeasurerDefault<int>>();
        container.Resolve<IValueSizeMeasurer<int?>>().ShouldBeOfType<ValueSizeMeasurerDefault<int?>>();

        container.Resolve<IValueSizeMeasurer<DateTime>>().ShouldBeOfType<ValueSizeMeasurerDefault<DateTime>>();
        container.Resolve<IValueSizeMeasurer<DateTime?>>().ShouldBeOfType<ValueSizeMeasurerDefault<DateTime?>>();
    }
}
