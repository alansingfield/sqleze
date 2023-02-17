using Sqleze.Collations;
using Sqleze.Interfaces;
using Sqleze.NamingConventions;
using Sqleze.Options;
using Sqleze.Readers;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sqleze.Mock.Tests.Readers;

[TestClass]
public class ColumnPropertyMapperTests
{
    [TestMethod]
    public void ColumnPropertyMapperSingle()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IColumnPropertyMapper, ColumnPropertyMapper>();

        container.Register<INamingConvention, NeutralNamingConvention>();
        container.Register<ICollation, Collation>();
        container.Register<CollationOptions>();

        var ado = container.Resolve<IAdo>();
        ado.SqlDataReader.Returns(Substitute.For<MS.SqlDataReader>());
        ado.SqlDataReader.VisibleFieldCount.Returns(1);
        ado.SqlDataReader.GetName(0).Returns("field");

        var cpm = container.Resolve<IColumnPropertyMapper>();

        var map = cpm.MapColumnsToProperties<CPM1>();

        map.ShouldNotBeNull();
        map.Count.ShouldBe(1);

        var itm = map[0];
        itm.PropertyInfo.Name.ShouldBe("Field");
        itm.ColumnOrdinal.ShouldBe(0);
        itm.PropertyOrdinal.ShouldBe(0);
        itm.ConverterArgs.ColumnName.ShouldBe("field");
        itm.ConverterArgs.PropertyName.ShouldBe("Field");
        itm.ConverterArgs.TargetType.ShouldBe(typeof(int));
        itm.ConverterArgs.DeclaringType.ShouldBe(typeof(CPM1));
    }

    [TestMethod]
    public void ColumnPropertyMapperExtra()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IColumnPropertyMapper, ColumnPropertyMapper>();

        container.Register<INamingConvention, NeutralNamingConvention>();
        container.Register<ICollation, Collation>();
        container.Register<CollationOptions>();

        var ado = container.Resolve<IAdo>();
        ado.SqlDataReader.Returns(Substitute.For<MS.SqlDataReader>());
        ado.SqlDataReader.VisibleFieldCount.Returns(2);
        ado.SqlDataReader.GetName(0).Returns("different_field");
        ado.SqlDataReader.GetName(1).Returns("field");

        var cpm = container.Resolve<IColumnPropertyMapper>();

        var map = cpm.MapColumnsToProperties<CPM1>();

        map.ShouldNotBeNull();
        map.Count.ShouldBe(1);

        var itm = map[0];
        itm.PropertyInfo.Name.ShouldBe("Field");
        itm.ColumnOrdinal.ShouldBe(1);
        itm.PropertyOrdinal.ShouldBe(0);
        itm.ConverterArgs.ColumnName.ShouldBe("field");
        itm.ConverterArgs.PropertyName.ShouldBe("Field");
        itm.ConverterArgs.TargetType.ShouldBe(typeof(int));
        itm.ConverterArgs.DeclaringType.ShouldBe(typeof(CPM1));
    }

    [TestMethod]
    public void ColumnPropertyMapperDouble()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IColumnPropertyMapper, ColumnPropertyMapper>();

        container.Register<INamingConvention, NeutralNamingConvention>();
        container.Register<ICollation, Collation>();
        container.Register<CollationOptions>();

        var ado = container.Resolve<IAdo>();
        ado.SqlDataReader.Returns(Substitute.For<MS.SqlDataReader>());
        ado.SqlDataReader.VisibleFieldCount.Returns(2);
        ado.SqlDataReader.GetName(0).Returns("fieldA");
        ado.SqlDataReader.GetName(1).Returns("fieldB");

        var cpm = container.Resolve<IColumnPropertyMapper>();

        var map = cpm.MapColumnsToProperties<CPM2>();

        map.ShouldNotBeNull();
        map.Count.ShouldBe(2);

        map[0].PropertyInfo.Name.ShouldBe("FieldA");
        map[0].ColumnOrdinal.ShouldBe(0);
        map[0].PropertyOrdinal.ShouldBe(0);
        map[0].ConverterArgs.ColumnName.ShouldBe("fieldA");
        map[0].ConverterArgs.PropertyName.ShouldBe("FieldA");
        map[0].ConverterArgs.TargetType.ShouldBe(typeof(int));
        map[0].ConverterArgs.DeclaringType.ShouldBe(typeof(CPM2));

        map[1].PropertyInfo.Name.ShouldBe("FieldB");
        map[1].ColumnOrdinal.ShouldBe(1);
        map[1].PropertyOrdinal.ShouldBe(1);
        map[1].ConverterArgs.ColumnName.ShouldBe("fieldB");
        map[1].ConverterArgs.PropertyName.ShouldBe("FieldB");
        map[1].ConverterArgs.TargetType.ShouldBe(typeof(string));
        map[1].ConverterArgs.DeclaringType.ShouldBe(typeof(CPM2));
    }


    [TestMethod]
    public void ColumnPropertyMapperDupeInObject()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IColumnPropertyMapper, ColumnPropertyMapper>();

        container.Register<INamingConvention, NeutralNamingConvention>();
        container.Register<ICollation, Collation>();
        container.Register<CollationOptions>();

        var ado = container.Resolve<IAdo>();
        ado.SqlDataReader.Returns(Substitute.For<MS.SqlDataReader>());
        ado.SqlDataReader.VisibleFieldCount.Returns(2);
        ado.SqlDataReader.GetName(0).Returns("field");
        ado.SqlDataReader.GetName(1).Returns("FIELD");

        var cpm = container.Resolve<IColumnPropertyMapper>();


        Should.Throw(() =>
        {
            cpm.MapColumnsToProperties<CPMDupe>();
        }, typeof(Exception)).Message.ShouldBe("Multiple properties cannot map to the same database field [field] -> field, FIELD");
    }


    [TestMethod]
    public void ColumnPropertyMapperDupeInResultSet()
    {
        var container = new Container().WithNSubstituteFallback();

        container.Register<IColumnPropertyMapper, ColumnPropertyMapper>();

        container.Register<INamingConvention, NeutralNamingConvention>();
        container.Register<ICollation, Collation>();
        container.Register<CollationOptions>();

        var ado = container.Resolve<IAdo>();
        ado.SqlDataReader.Returns(Substitute.For<MS.SqlDataReader>());
        ado.SqlDataReader.VisibleFieldCount.Returns(2);
        ado.SqlDataReader.GetName(0).Returns("field");
        ado.SqlDataReader.GetName(1).Returns("FIELD");

        var cpm = container.Resolve<IColumnPropertyMapper>();


        Should.Throw(() =>
        {
            cpm.MapColumnsToProperties<CPM1>();
        }, typeof(Exception)).Message.ShouldBe("Cannot map to column 'field' because it is duplicated in the result set.");
    }

    private class CPM1
    {
        public int Field { get; set; }
    }
    private class CPM2
    {
        public int FieldA { get; set; }
        public string FieldB { get; set; }
    }
    private class CPMDupe
    {
        public int field {get;set; }
        public int FIELD {get;set; }
    }
}
