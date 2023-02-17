using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Sqleze.Readers;
using System.Xml.Linq;
using Sqleze;
using Sqleze.NamingConventions;
using Sqleze.DryIoc;
using Newtonsoft.Json.Linq;
using UnitTestCoder.Shouldly.Gen;
using Sqleze.ValueGetters;
using Sqleze.Registration;

namespace Sqleze.Tests.Readers
{
    [TestClass]
    public class ColumnPropertyMapperTests
    {
//        [TestMethod]
//        public void ColumnPropertyMapperEmpty()
//        {
//            var container = new Container().WithNSubstituteFallback();

//            container.Register(typeof(IColumnPropertyMapper<>), typeof(ColumnPropertyMapper<>));
//            container.Register(typeof(IGenericResolver<>), typeof(GenericResolver<>));
//            container.Register(typeof(IMultiResolver<,>), typeof(MultiResolver<,>));
////            container.Register(typeof(IColumnProperty<,>), typeof(ColumnProperty<,>));

//            container.Register<INamingConvention, NeutralNamingConvention>();

//            var mapper = container.Resolve<IColumnPropertyMapper<EntityOne>>();

//            var cols = mapper.MapColumnsToProperties().ToList();

//            cols.Count.ShouldBe(0);
//        }

        [TestMethod]
        public void ColumnPropertyMapperMatchingFields()
        {
            IContainer container = openContainer();

            container.Register<INamingConvention, NeutralNamingConvention>();

            var dataReaderFieldNames = container.Resolve<IDataReaderFieldNames>();
            dataReaderFieldNames.GetFieldInfos().ReturnsForAnyArgs(
                new DataReaderFieldInfo[]
                {
                    new DataReaderFieldInfo
                    (
                        ColumnOrdinal: 1,
                        ColumnName: "Name",
                        SqlDataTypeName: ""
                    ),
                    new DataReaderFieldInfo
                    (
                        ColumnOrdinal: 2,
                        ColumnName:"Number",
                        SqlDataTypeName: ""
                    ),

                });

            var mapper = container.Resolve<IColumnPropertyMapper<EntityOne>>();

            var cols = mapper.MapColumnsToProperties().ToList();

            //ShouldlyTest.Gen(cols, nameof(cols));

            {
                cols.ShouldNotBeNull();
                cols.Count().ShouldBe(2);
                cols[0].ShouldNotBeNull();
                cols[0].ColumnName.ShouldBe("Name");
                cols[0].PropertyName.ShouldBe("Name");
                cols[0].ColumnOrdinal.ShouldBe(1);
                cols[0].PropertyConsOnly.ShouldBe(false);
                cols[1].ShouldNotBeNull();
                cols[1].ColumnName.ShouldBe("Number");
                cols[1].PropertyName.ShouldBe("Number");
                cols[1].ColumnOrdinal.ShouldBe(2);
                cols[1].PropertyConsOnly.ShouldBe(false);
            }
        }

        [TestMethod]
        public void ColumnPropertyMapperMatchingRecord()
        {
            IContainer container = openContainer();

            container.Register<INamingConvention, NeutralNamingConvention>();

            var dataReaderFieldNames = container.Resolve<IDataReaderFieldNames>();
            dataReaderFieldNames.GetFieldInfos().ReturnsForAnyArgs(
                new DataReaderFieldInfo[]
                {
                    new DataReaderFieldInfo
                    (
                        ColumnOrdinal: 1,
                        ColumnName: "Name",
                        SqlDataTypeName: ""
                    ),
                    new DataReaderFieldInfo
                    (
                        ColumnOrdinal: 2,
                        ColumnName:"Number",
                        SqlDataTypeName: ""
                    ),

                });

            var mapper = container.Resolve<IColumnPropertyMapper<RecordOne>>();

            var cols = mapper.MapColumnsToProperties().ToList();

            //ShouldlyTest.Gen(cols, nameof(cols));

            {
                cols.ShouldNotBeNull();
                cols.Count().ShouldBe(2);
                cols[0].ShouldNotBeNull();
                cols[0].ColumnName.ShouldBe("Name");
                cols[0].PropertyName.ShouldBe("Name");
                cols[0].ColumnOrdinal.ShouldBe(1);
                cols[0].PropertyConsOnly.ShouldBe(false);
                cols[1].ShouldNotBeNull();
                cols[1].ColumnName.ShouldBe("Number");
                cols[1].PropertyName.ShouldBe("Number");
                cols[1].ColumnOrdinal.ShouldBe(2);
                cols[1].PropertyConsOnly.ShouldBe(false);
            }
        }

        private static IContainer openContainer()
        {
            var container = new Container().WithNSubstituteFallback();

            container.Register(typeof(IColumnPropertyMapper<>), typeof(ColumnPropertyMapper<>));
            container.Register(typeof(IGenericResolver<>), typeof(GenericResolver<>));
            container.Register(typeof(IGenericPromoter), typeof(GenericPromoter));
            container.Register(typeof(IMultiResolver<,>), typeof(MultiResolver<,>));

            container.Register(typeof(IColumnPropertyResolver<,>), typeof(ColumnPropertyResolver<,>), Reuse.Transient);
            container.RegisterGenericPromotion(typeof(IColumnPropertyResolver<>), typeof(IColumnPropertyResolver<,>));

            container.Register(typeof(IColumnProperty<,,>), typeof(ColumnProperty<,,>), Reuse.Transient);
            container.RegisterGenericPromotion(typeof(IColumnProperty<,>), typeof(IColumnProperty<,,>),
                trialType: typeof(IKnownSqlDbType));

            container.Register<IKnownSqlDbTypeFinder, KnownSqlDbTypeFinder>(Reuse.Singleton);
            //container.Register<IKnownSqlDbTypeResolver, KnownSqlDbTypeResolver>(Reuse.Singleton);


            return container;
        }

        public class EntityOne
        {
            public string? Name { get; set; }
            public int Number { get; set; }
        }
        public class EntityTwo
        {
            public EntityTwo(string name)
            {
                Name = name;
            }

            public string Name { get; init; } = "";
            public int Number { get; set; }
        }

        public record RecordOne
        (
            string Name,
            int Number
        );
    }
}
