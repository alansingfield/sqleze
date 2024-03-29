﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Sqleze;
using Sqleze.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestCoder.Shouldly.Gen;

namespace Sqleze.Tests.Metadata
{
    [TestClass]
    public class TableTypeMetadataQueryTests
    {

        [TestMethod]
        public void TableTypeMetadataQuery1()
        {
            var q = buildQuery();

            var result = q.Query("dbo.tt_table_type_all_types");

            //ShouldlyTest.Gen(result, nameof(result));

            {
                result.ShouldNotBeNull();
                result.Count().ShouldBe(38);
                result[0].ShouldNotBeNull();
                result[0].ColumnName.ShouldBe("col_bigint");
                result[0].ColumnOrdinal.ShouldBe(0);
                result[0].Datatype.ShouldBe("bigint");
                result[0].Length.ShouldBe(8);
                result[0].Precision.ShouldBe(19);
                result[0].Scale.ShouldBe(0);
                result[0].IsNullable.ShouldBe(false);
                result[1].ShouldNotBeNull();
                result[1].ColumnName.ShouldBe("col_binary");
                result[1].ColumnOrdinal.ShouldBe(1);
                result[1].Datatype.ShouldBe("binary");
                result[1].Length.ShouldBe(100);
                result[1].Precision.ShouldBe(0);
                result[1].Scale.ShouldBe(0);
                result[1].IsNullable.ShouldBe(false);
                result[2].ShouldNotBeNull();
                result[2].ColumnName.ShouldBe("col_bit");
                result[2].ColumnOrdinal.ShouldBe(2);
                result[2].Datatype.ShouldBe("bit");
                result[2].Length.ShouldBe(1);
                result[2].Precision.ShouldBe(1);
                result[2].Scale.ShouldBe(0);
                result[2].IsNullable.ShouldBe(false);
                result[3].ShouldNotBeNull();
                result[3].ColumnName.ShouldBe("col_char");
                result[3].ColumnOrdinal.ShouldBe(3);
                result[3].Datatype.ShouldBe("char");
                result[3].Length.ShouldBe(1);
                result[3].Precision.ShouldBe(0);
                result[3].Scale.ShouldBe(0);
                result[3].IsNullable.ShouldBe(false);
                result[4].ShouldNotBeNull();
                result[4].ColumnName.ShouldBe("col_date");
                result[4].ColumnOrdinal.ShouldBe(4);
                result[4].Datatype.ShouldBe("date");
                result[4].Length.ShouldBe(3);
                result[4].Precision.ShouldBe(10);
                result[4].Scale.ShouldBe(0);
                result[4].IsNullable.ShouldBe(false);
                result[5].ShouldNotBeNull();
                result[5].ColumnName.ShouldBe("col_datetime");
                result[5].ColumnOrdinal.ShouldBe(5);
                result[5].Datatype.ShouldBe("datetime");
                result[5].Length.ShouldBe(8);
                result[5].Precision.ShouldBe(23);
                result[5].Scale.ShouldBe(3);
                result[5].IsNullable.ShouldBe(false);
                result[6].ShouldNotBeNull();
                result[6].ColumnName.ShouldBe("col_datetime2");
                result[6].ColumnOrdinal.ShouldBe(6);
                result[6].Datatype.ShouldBe("datetime2");
                result[6].Length.ShouldBe(8);
                result[6].Precision.ShouldBe(27);
                result[6].Scale.ShouldBe(7);
                result[6].IsNullable.ShouldBe(false);
                result[7].ShouldNotBeNull();
                result[7].ColumnName.ShouldBe("col_datetimeoffset_0");
                result[7].ColumnOrdinal.ShouldBe(7);
                result[7].Datatype.ShouldBe("datetimeoffset");
                result[7].Length.ShouldBe(8);
                result[7].Precision.ShouldBe(26);
                result[7].Scale.ShouldBe(0);
                result[7].IsNullable.ShouldBe(false);
                result[8].ShouldNotBeNull();
                result[8].ColumnName.ShouldBe("col_datetimeoffset_7");
                result[8].ColumnOrdinal.ShouldBe(8);
                result[8].Datatype.ShouldBe("datetimeoffset");
                result[8].Length.ShouldBe(10);
                result[8].Precision.ShouldBe(34);
                result[8].Scale.ShouldBe(7);
                result[8].IsNullable.ShouldBe(false);
                result[9].ShouldNotBeNull();
                result[9].ColumnName.ShouldBe("col_decimal_38_4");
                result[9].ColumnOrdinal.ShouldBe(9);
                result[9].Datatype.ShouldBe("decimal");
                result[9].Length.ShouldBe(17);
                result[9].Precision.ShouldBe(38);
                result[9].Scale.ShouldBe(4);
                result[9].IsNullable.ShouldBe(false);
                result[10].ShouldNotBeNull();
                result[10].ColumnName.ShouldBe("col_numeric_38_4");
                result[10].ColumnOrdinal.ShouldBe(10);
                result[10].Datatype.ShouldBe("numeric");
                result[10].Length.ShouldBe(17);
                result[10].Precision.ShouldBe(38);
                result[10].Scale.ShouldBe(4);
                result[10].IsNullable.ShouldBe(false);
                result[11].ShouldNotBeNull();
                result[11].ColumnName.ShouldBe("col_float");
                result[11].ColumnOrdinal.ShouldBe(11);
                result[11].Datatype.ShouldBe("float");
                result[11].Length.ShouldBe(8);
                result[11].Precision.ShouldBe(53);
                result[11].Scale.ShouldBe(0);
                result[11].IsNullable.ShouldBe(false);
                result[12].ShouldNotBeNull();
                result[12].ColumnName.ShouldBe("col_image");
                result[12].ColumnOrdinal.ShouldBe(12);
                result[12].Datatype.ShouldBe("image");
                result[12].Length.ShouldBe(256);
                result[12].Precision.ShouldBe(0);
                result[12].Scale.ShouldBe(0);
                result[12].IsNullable.ShouldBe(false);
                result[13].ShouldNotBeNull();
                result[13].ColumnName.ShouldBe("col_int");
                result[13].ColumnOrdinal.ShouldBe(13);
                result[13].Datatype.ShouldBe("int");
                result[13].Length.ShouldBe(4);
                result[13].Precision.ShouldBe(10);
                result[13].Scale.ShouldBe(0);
                result[13].IsNullable.ShouldBe(false);
                result[14].ShouldNotBeNull();
                result[14].ColumnName.ShouldBe("col_money");
                result[14].ColumnOrdinal.ShouldBe(14);
                result[14].Datatype.ShouldBe("money");
                result[14].Length.ShouldBe(8);
                result[14].Precision.ShouldBe(19);
                result[14].Scale.ShouldBe(4);
                result[14].IsNullable.ShouldBe(false);
                result[15].ShouldNotBeNull();
                result[15].ColumnName.ShouldBe("col_nchar");
                result[15].ColumnOrdinal.ShouldBe(15);
                result[15].Datatype.ShouldBe("nchar");
                result[15].Length.ShouldBe(1);
                result[15].Precision.ShouldBe(0);
                result[15].Scale.ShouldBe(0);
                result[15].IsNullable.ShouldBe(false);
                result[16].ShouldNotBeNull();
                result[16].ColumnName.ShouldBe("col_ntext");
                result[16].ColumnOrdinal.ShouldBe(16);
                result[16].Datatype.ShouldBe("ntext");
                result[16].Length.ShouldBe(256);
                result[16].Precision.ShouldBe(0);
                result[16].Scale.ShouldBe(0);
                result[16].IsNullable.ShouldBe(false);
                result[17].ShouldNotBeNull();
                result[17].ColumnName.ShouldBe("col_nvarchar_30");
                result[17].ColumnOrdinal.ShouldBe(17);
                result[17].Datatype.ShouldBe("nvarchar");
                result[17].Length.ShouldBe(30);
                result[17].Precision.ShouldBe(0);
                result[17].Scale.ShouldBe(0);
                result[17].IsNullable.ShouldBe(false);
                result[18].ShouldNotBeNull();
                result[18].ColumnName.ShouldBe("col_nvarchar_max");
                result[18].ColumnOrdinal.ShouldBe(18);
                result[18].Datatype.ShouldBe("nvarchar");
                result[18].Length.ShouldBe(-1);
                result[18].Precision.ShouldBe(0);
                result[18].Scale.ShouldBe(0);
                result[18].IsNullable.ShouldBe(false);
                result[19].ShouldNotBeNull();
                result[19].ColumnName.ShouldBe("col_real");
                result[19].ColumnOrdinal.ShouldBe(19);
                result[19].Datatype.ShouldBe("real");
                result[19].Length.ShouldBe(4);
                result[19].Precision.ShouldBe(24);
                result[19].Scale.ShouldBe(0);
                result[19].IsNullable.ShouldBe(false);
                result[20].ShouldNotBeNull();
                result[20].ColumnName.ShouldBe("col_smalldatetime");
                result[20].ColumnOrdinal.ShouldBe(20);
                result[20].Datatype.ShouldBe("smalldatetime");
                result[20].Length.ShouldBe(4);
                result[20].Precision.ShouldBe(16);
                result[20].Scale.ShouldBe(0);
                result[20].IsNullable.ShouldBe(false);
                result[21].ShouldNotBeNull();
                result[21].ColumnName.ShouldBe("col_smallint");
                result[21].ColumnOrdinal.ShouldBe(21);
                result[21].Datatype.ShouldBe("smallint");
                result[21].Length.ShouldBe(2);
                result[21].Precision.ShouldBe(5);
                result[21].Scale.ShouldBe(0);
                result[21].IsNullable.ShouldBe(false);
                result[22].ShouldNotBeNull();
                result[22].ColumnName.ShouldBe("col_smallmoney");
                result[22].ColumnOrdinal.ShouldBe(22);
                result[22].Datatype.ShouldBe("smallmoney");
                result[22].Length.ShouldBe(4);
                result[22].Precision.ShouldBe(10);
                result[22].Scale.ShouldBe(4);
                result[22].IsNullable.ShouldBe(false);
                result[23].ShouldNotBeNull();
                result[23].ColumnName.ShouldBe("col_text");
                result[23].ColumnOrdinal.ShouldBe(23);
                result[23].Datatype.ShouldBe("text");
                result[23].Length.ShouldBe(256);
                result[23].Precision.ShouldBe(0);
                result[23].Scale.ShouldBe(0);
                result[23].IsNullable.ShouldBe(false);
                result[24].ShouldNotBeNull();
                result[24].ColumnName.ShouldBe("col_time_0");
                result[24].ColumnOrdinal.ShouldBe(24);
                result[24].Datatype.ShouldBe("time");
                result[24].Length.ShouldBe(3);
                result[24].Precision.ShouldBe(8);
                result[24].Scale.ShouldBe(0);
                result[24].IsNullable.ShouldBe(false);
                result[25].ShouldNotBeNull();
                result[25].ColumnName.ShouldBe("col_time_7");
                result[25].ColumnOrdinal.ShouldBe(25);
                result[25].Datatype.ShouldBe("time");
                result[25].Length.ShouldBe(5);
                result[25].Precision.ShouldBe(16);
                result[25].Scale.ShouldBe(7);
                result[25].IsNullable.ShouldBe(false);
                result[26].ShouldNotBeNull();
                result[26].ColumnName.ShouldBe("col_timestamp");
                result[26].ColumnOrdinal.ShouldBe(26);
                result[26].Datatype.ShouldBe("timestamp");
                result[26].Length.ShouldBe(8);
                result[26].Precision.ShouldBe(0);
                result[26].Scale.ShouldBe(0);
                result[26].IsNullable.ShouldBe(false);
                result[27].ShouldNotBeNull();
                result[27].ColumnName.ShouldBe("col_tinyint");
                result[27].ColumnOrdinal.ShouldBe(27);
                result[27].Datatype.ShouldBe("tinyint");
                result[27].Length.ShouldBe(1);
                result[27].Precision.ShouldBe(3);
                result[27].Scale.ShouldBe(0);
                result[27].IsNullable.ShouldBe(false);
                result[28].ShouldNotBeNull();
                result[28].ColumnName.ShouldBe("col_uniqueidentifier");
                result[28].ColumnOrdinal.ShouldBe(28);
                result[28].Datatype.ShouldBe("uniqueidentifier");
                result[28].Length.ShouldBe(16);
                result[28].Precision.ShouldBe(0);
                result[28].Scale.ShouldBe(0);
                result[28].IsNullable.ShouldBe(false);
                result[29].ShouldNotBeNull();
                result[29].ColumnName.ShouldBe("col_varbinary_100");
                result[29].ColumnOrdinal.ShouldBe(29);
                result[29].Datatype.ShouldBe("varbinary");
                result[29].Length.ShouldBe(100);
                result[29].Precision.ShouldBe(0);
                result[29].Scale.ShouldBe(0);
                result[29].IsNullable.ShouldBe(false);
                result[30].ShouldNotBeNull();
                result[30].ColumnName.ShouldBe("col_varbinary_max");
                result[30].ColumnOrdinal.ShouldBe(30);
                result[30].Datatype.ShouldBe("varbinary");
                result[30].Length.ShouldBe(-1);
                result[30].Precision.ShouldBe(0);
                result[30].Scale.ShouldBe(0);
                result[30].IsNullable.ShouldBe(false);
                result[31].ShouldNotBeNull();
                result[31].ColumnName.ShouldBe("col_varchar_30");
                result[31].ColumnOrdinal.ShouldBe(31);
                result[31].Datatype.ShouldBe("varchar");
                result[31].Length.ShouldBe(30);
                result[31].Precision.ShouldBe(0);
                result[31].Scale.ShouldBe(0);
                result[31].IsNullable.ShouldBe(false);
                result[32].ShouldNotBeNull();
                result[32].ColumnName.ShouldBe("col_varchar_max");
                result[32].ColumnOrdinal.ShouldBe(32);
                result[32].Datatype.ShouldBe("varchar");
                result[32].Length.ShouldBe(-1);
                result[32].Precision.ShouldBe(0);
                result[32].Scale.ShouldBe(0);
                result[32].IsNullable.ShouldBe(false);
                result[33].ShouldNotBeNull();
                result[33].ColumnName.ShouldBe("col_sql_variant");
                result[33].ColumnOrdinal.ShouldBe(33);
                result[33].Datatype.ShouldBe("sql_variant");
                result[33].Length.ShouldBe(8016);
                result[33].Precision.ShouldBe(0);
                result[33].Scale.ShouldBe(0);
                result[33].IsNullable.ShouldBe(false);
                result[34].ShouldNotBeNull();
                result[34].ColumnName.ShouldBe("col_xml");
                result[34].ColumnOrdinal.ShouldBe(34);
                result[34].Datatype.ShouldBe("xml");
                result[34].Length.ShouldBe(-1);
                result[34].Precision.ShouldBe(0);
                result[34].Scale.ShouldBe(0);
                result[34].IsNullable.ShouldBe(false);
                result[35].ShouldNotBeNull();
                result[35].ColumnName.ShouldBe("col_geography");
                result[35].ColumnOrdinal.ShouldBe(35);
                result[35].Datatype.ShouldBe("geography");
                result[35].Length.ShouldBe(-1);
                result[35].Precision.ShouldBe(0);
                result[35].Scale.ShouldBe(0);
                result[35].IsNullable.ShouldBe(false);
                result[36].ShouldNotBeNull();
                result[36].ColumnName.ShouldBe("col_geometry");
                result[36].ColumnOrdinal.ShouldBe(36);
                result[36].Datatype.ShouldBe("geometry");
                result[36].Length.ShouldBe(-1);
                result[36].Precision.ShouldBe(0);
                result[36].Scale.ShouldBe(0);
                result[36].IsNullable.ShouldBe(false);
                result[37].ShouldNotBeNull();
                result[37].ColumnName.ShouldBe("col_hierarchyid");
                result[37].ColumnOrdinal.ShouldBe(37);
                result[37].Datatype.ShouldBe("hierarchyid");
                result[37].Length.ShouldBe(892);
                result[37].Precision.ShouldBe(0);
                result[37].Scale.ShouldBe(0);
                result[37].IsNullable.ShouldBe(false);
            }
        }

        private static ITableTypeMetadataQuery buildQuery()
        {
            var container = new Container();

            container.RegisterSqleze();
            container.RegisterTestSettings();

            var builder = container.Resolve<ISqlezeBuilder>().WithConfigKey("DefaultConnection");

            var factory = builder.Build();

            var container2 = new Container();
            container2.Register<ITableTypeMetadataQuery, TableTypeMetadataQuery>();
            container2.Use<ISqlezeConnector>(factory);

            var q = container2.Resolve<ITableTypeMetadataQuery>();
            return q;
        }

        [TestMethod]
        public void TableTypeMetadataQuery2()
        {
            var q = buildQuery();

            var result = q.Query("dbo.tt_table_type_all_types_nullable");

            //ShouldlyTest.Gen(result, nameof(result));

            {
                result.ShouldNotBeNull();
                result.Count().ShouldBe(38);
                result[0].ShouldNotBeNull();
                result[0].ColumnName.ShouldBe("col_bigint");
                result[0].ColumnOrdinal.ShouldBe(0);
                result[0].Datatype.ShouldBe("bigint");
                result[0].Length.ShouldBe(8);
                result[0].Precision.ShouldBe(19);
                result[0].Scale.ShouldBe(0);
                result[0].IsNullable.ShouldBe(true);
                result[1].ShouldNotBeNull();
                result[1].ColumnName.ShouldBe("col_binary");
                result[1].ColumnOrdinal.ShouldBe(1);
                result[1].Datatype.ShouldBe("binary");
                result[1].Length.ShouldBe(100);
                result[1].Precision.ShouldBe(0);
                result[1].Scale.ShouldBe(0);
                result[1].IsNullable.ShouldBe(true);
                result[2].ShouldNotBeNull();
                result[2].ColumnName.ShouldBe("col_bit");
                result[2].ColumnOrdinal.ShouldBe(2);
                result[2].Datatype.ShouldBe("bit");
                result[2].Length.ShouldBe(1);
                result[2].Precision.ShouldBe(1);
                result[2].Scale.ShouldBe(0);
                result[2].IsNullable.ShouldBe(true);
                result[3].ShouldNotBeNull();
                result[3].ColumnName.ShouldBe("col_char");
                result[3].ColumnOrdinal.ShouldBe(3);
                result[3].Datatype.ShouldBe("char");
                result[3].Length.ShouldBe(1);
                result[3].Precision.ShouldBe(0);
                result[3].Scale.ShouldBe(0);
                result[3].IsNullable.ShouldBe(true);
                result[4].ShouldNotBeNull();
                result[4].ColumnName.ShouldBe("col_date");
                result[4].ColumnOrdinal.ShouldBe(4);
                result[4].Datatype.ShouldBe("date");
                result[4].Length.ShouldBe(3);
                result[4].Precision.ShouldBe(10);
                result[4].Scale.ShouldBe(0);
                result[4].IsNullable.ShouldBe(true);
                result[5].ShouldNotBeNull();
                result[5].ColumnName.ShouldBe("col_datetime");
                result[5].ColumnOrdinal.ShouldBe(5);
                result[5].Datatype.ShouldBe("datetime");
                result[5].Length.ShouldBe(8);
                result[5].Precision.ShouldBe(23);
                result[5].Scale.ShouldBe(3);
                result[5].IsNullable.ShouldBe(true);
                result[6].ShouldNotBeNull();
                result[6].ColumnName.ShouldBe("col_datetime2");
                result[6].ColumnOrdinal.ShouldBe(6);
                result[6].Datatype.ShouldBe("datetime2");
                result[6].Length.ShouldBe(8);
                result[6].Precision.ShouldBe(27);
                result[6].Scale.ShouldBe(7);
                result[6].IsNullable.ShouldBe(true);
                result[7].ShouldNotBeNull();
                result[7].ColumnName.ShouldBe("col_datetimeoffset_0");
                result[7].ColumnOrdinal.ShouldBe(7);
                result[7].Datatype.ShouldBe("datetimeoffset");
                result[7].Length.ShouldBe(8);
                result[7].Precision.ShouldBe(26);
                result[7].Scale.ShouldBe(0);
                result[7].IsNullable.ShouldBe(true);
                result[8].ShouldNotBeNull();
                result[8].ColumnName.ShouldBe("col_datetimeoffset_7");
                result[8].ColumnOrdinal.ShouldBe(8);
                result[8].Datatype.ShouldBe("datetimeoffset");
                result[8].Length.ShouldBe(10);
                result[8].Precision.ShouldBe(34);
                result[8].Scale.ShouldBe(7);
                result[8].IsNullable.ShouldBe(true);
                result[9].ShouldNotBeNull();
                result[9].ColumnName.ShouldBe("col_decimal_38_4");
                result[9].ColumnOrdinal.ShouldBe(9);
                result[9].Datatype.ShouldBe("decimal");
                result[9].Length.ShouldBe(17);
                result[9].Precision.ShouldBe(38);
                result[9].Scale.ShouldBe(4);
                result[9].IsNullable.ShouldBe(true);
                result[10].ShouldNotBeNull();
                result[10].ColumnName.ShouldBe("col_numeric_38_4");
                result[10].ColumnOrdinal.ShouldBe(10);
                result[10].Datatype.ShouldBe("numeric");
                result[10].Length.ShouldBe(17);
                result[10].Precision.ShouldBe(38);
                result[10].Scale.ShouldBe(4);
                result[10].IsNullable.ShouldBe(true);
                result[11].ShouldNotBeNull();
                result[11].ColumnName.ShouldBe("col_float");
                result[11].ColumnOrdinal.ShouldBe(11);
                result[11].Datatype.ShouldBe("float");
                result[11].Length.ShouldBe(8);
                result[11].Precision.ShouldBe(53);
                result[11].Scale.ShouldBe(0);
                result[11].IsNullable.ShouldBe(true);
                result[12].ShouldNotBeNull();
                result[12].ColumnName.ShouldBe("col_image");
                result[12].ColumnOrdinal.ShouldBe(12);
                result[12].Datatype.ShouldBe("image");
                result[12].Length.ShouldBe(256);
                result[12].Precision.ShouldBe(0);
                result[12].Scale.ShouldBe(0);
                result[12].IsNullable.ShouldBe(true);
                result[13].ShouldNotBeNull();
                result[13].ColumnName.ShouldBe("col_int");
                result[13].ColumnOrdinal.ShouldBe(13);
                result[13].Datatype.ShouldBe("int");
                result[13].Length.ShouldBe(4);
                result[13].Precision.ShouldBe(10);
                result[13].Scale.ShouldBe(0);
                result[13].IsNullable.ShouldBe(true);
                result[14].ShouldNotBeNull();
                result[14].ColumnName.ShouldBe("col_money");
                result[14].ColumnOrdinal.ShouldBe(14);
                result[14].Datatype.ShouldBe("money");
                result[14].Length.ShouldBe(8);
                result[14].Precision.ShouldBe(19);
                result[14].Scale.ShouldBe(4);
                result[14].IsNullable.ShouldBe(true);
                result[15].ShouldNotBeNull();
                result[15].ColumnName.ShouldBe("col_nchar");
                result[15].ColumnOrdinal.ShouldBe(15);
                result[15].Datatype.ShouldBe("nchar");
                result[15].Length.ShouldBe(1);
                result[15].Precision.ShouldBe(0);
                result[15].Scale.ShouldBe(0);
                result[15].IsNullable.ShouldBe(true);
                result[16].ShouldNotBeNull();
                result[16].ColumnName.ShouldBe("col_ntext");
                result[16].ColumnOrdinal.ShouldBe(16);
                result[16].Datatype.ShouldBe("ntext");
                result[16].Length.ShouldBe(256);
                result[16].Precision.ShouldBe(0);
                result[16].Scale.ShouldBe(0);
                result[16].IsNullable.ShouldBe(true);
                result[17].ShouldNotBeNull();
                result[17].ColumnName.ShouldBe("col_nvarchar_30");
                result[17].ColumnOrdinal.ShouldBe(17);
                result[17].Datatype.ShouldBe("nvarchar");
                result[17].Length.ShouldBe(30);
                result[17].Precision.ShouldBe(0);
                result[17].Scale.ShouldBe(0);
                result[17].IsNullable.ShouldBe(true);
                result[18].ShouldNotBeNull();
                result[18].ColumnName.ShouldBe("col_nvarchar_max");
                result[18].ColumnOrdinal.ShouldBe(18);
                result[18].Datatype.ShouldBe("nvarchar");
                result[18].Length.ShouldBe(-1);
                result[18].Precision.ShouldBe(0);
                result[18].Scale.ShouldBe(0);
                result[18].IsNullable.ShouldBe(true);
                result[19].ShouldNotBeNull();
                result[19].ColumnName.ShouldBe("col_real");
                result[19].ColumnOrdinal.ShouldBe(19);
                result[19].Datatype.ShouldBe("real");
                result[19].Length.ShouldBe(4);
                result[19].Precision.ShouldBe(24);
                result[19].Scale.ShouldBe(0);
                result[19].IsNullable.ShouldBe(true);
                result[20].ShouldNotBeNull();
                result[20].ColumnName.ShouldBe("col_smalldatetime");
                result[20].ColumnOrdinal.ShouldBe(20);
                result[20].Datatype.ShouldBe("smalldatetime");
                result[20].Length.ShouldBe(4);
                result[20].Precision.ShouldBe(16);
                result[20].Scale.ShouldBe(0);
                result[20].IsNullable.ShouldBe(true);
                result[21].ShouldNotBeNull();
                result[21].ColumnName.ShouldBe("col_smallint");
                result[21].ColumnOrdinal.ShouldBe(21);
                result[21].Datatype.ShouldBe("smallint");
                result[21].Length.ShouldBe(2);
                result[21].Precision.ShouldBe(5);
                result[21].Scale.ShouldBe(0);
                result[21].IsNullable.ShouldBe(true);
                result[22].ShouldNotBeNull();
                result[22].ColumnName.ShouldBe("col_smallmoney");
                result[22].ColumnOrdinal.ShouldBe(22);
                result[22].Datatype.ShouldBe("smallmoney");
                result[22].Length.ShouldBe(4);
                result[22].Precision.ShouldBe(10);
                result[22].Scale.ShouldBe(4);
                result[22].IsNullable.ShouldBe(true);
                result[23].ShouldNotBeNull();
                result[23].ColumnName.ShouldBe("col_text");
                result[23].ColumnOrdinal.ShouldBe(23);
                result[23].Datatype.ShouldBe("text");
                result[23].Length.ShouldBe(256);
                result[23].Precision.ShouldBe(0);
                result[23].Scale.ShouldBe(0);
                result[23].IsNullable.ShouldBe(true);
                result[24].ShouldNotBeNull();
                result[24].ColumnName.ShouldBe("col_time_0");
                result[24].ColumnOrdinal.ShouldBe(24);
                result[24].Datatype.ShouldBe("time");
                result[24].Length.ShouldBe(3);
                result[24].Precision.ShouldBe(8);
                result[24].Scale.ShouldBe(0);
                result[24].IsNullable.ShouldBe(true);
                result[25].ShouldNotBeNull();
                result[25].ColumnName.ShouldBe("col_time_7");
                result[25].ColumnOrdinal.ShouldBe(25);
                result[25].Datatype.ShouldBe("time");
                result[25].Length.ShouldBe(5);
                result[25].Precision.ShouldBe(16);
                result[25].Scale.ShouldBe(7);
                result[25].IsNullable.ShouldBe(true);
                result[26].ShouldNotBeNull();
                result[26].ColumnName.ShouldBe("col_timestamp");
                result[26].ColumnOrdinal.ShouldBe(26);
                result[26].Datatype.ShouldBe("timestamp");
                result[26].Length.ShouldBe(8);
                result[26].Precision.ShouldBe(0);
                result[26].Scale.ShouldBe(0);
                result[26].IsNullable.ShouldBe(true);
                result[27].ShouldNotBeNull();
                result[27].ColumnName.ShouldBe("col_tinyint");
                result[27].ColumnOrdinal.ShouldBe(27);
                result[27].Datatype.ShouldBe("tinyint");
                result[27].Length.ShouldBe(1);
                result[27].Precision.ShouldBe(3);
                result[27].Scale.ShouldBe(0);
                result[27].IsNullable.ShouldBe(true);
                result[28].ShouldNotBeNull();
                result[28].ColumnName.ShouldBe("col_uniqueidentifier");
                result[28].ColumnOrdinal.ShouldBe(28);
                result[28].Datatype.ShouldBe("uniqueidentifier");
                result[28].Length.ShouldBe(16);
                result[28].Precision.ShouldBe(0);
                result[28].Scale.ShouldBe(0);
                result[28].IsNullable.ShouldBe(true);
                result[29].ShouldNotBeNull();
                result[29].ColumnName.ShouldBe("col_varbinary_100");
                result[29].ColumnOrdinal.ShouldBe(29);
                result[29].Datatype.ShouldBe("varbinary");
                result[29].Length.ShouldBe(100);
                result[29].Precision.ShouldBe(0);
                result[29].Scale.ShouldBe(0);
                result[29].IsNullable.ShouldBe(true);
                result[30].ShouldNotBeNull();
                result[30].ColumnName.ShouldBe("col_varbinary_max");
                result[30].ColumnOrdinal.ShouldBe(30);
                result[30].Datatype.ShouldBe("varbinary");
                result[30].Length.ShouldBe(-1);
                result[30].Precision.ShouldBe(0);
                result[30].Scale.ShouldBe(0);
                result[30].IsNullable.ShouldBe(true);
                result[31].ShouldNotBeNull();
                result[31].ColumnName.ShouldBe("col_varchar_30");
                result[31].ColumnOrdinal.ShouldBe(31);
                result[31].Datatype.ShouldBe("varchar");
                result[31].Length.ShouldBe(30);
                result[31].Precision.ShouldBe(0);
                result[31].Scale.ShouldBe(0);
                result[31].IsNullable.ShouldBe(true);
                result[32].ShouldNotBeNull();
                result[32].ColumnName.ShouldBe("col_varchar_max");
                result[32].ColumnOrdinal.ShouldBe(32);
                result[32].Datatype.ShouldBe("varchar");
                result[32].Length.ShouldBe(-1);
                result[32].Precision.ShouldBe(0);
                result[32].Scale.ShouldBe(0);
                result[32].IsNullable.ShouldBe(true);
                result[33].ShouldNotBeNull();
                result[33].ColumnName.ShouldBe("col_sql_variant");
                result[33].ColumnOrdinal.ShouldBe(33);
                result[33].Datatype.ShouldBe("sql_variant");
                result[33].Length.ShouldBe(8016);
                result[33].Precision.ShouldBe(0);
                result[33].Scale.ShouldBe(0);
                result[33].IsNullable.ShouldBe(true);
                result[34].ShouldNotBeNull();
                result[34].ColumnName.ShouldBe("col_xml");
                result[34].ColumnOrdinal.ShouldBe(34);
                result[34].Datatype.ShouldBe("xml");
                result[34].Length.ShouldBe(-1);
                result[34].Precision.ShouldBe(0);
                result[34].Scale.ShouldBe(0);
                result[34].IsNullable.ShouldBe(true);
                result[35].ShouldNotBeNull();
                result[35].ColumnName.ShouldBe("col_geography");
                result[35].ColumnOrdinal.ShouldBe(35);
                result[35].Datatype.ShouldBe("geography");
                result[35].Length.ShouldBe(-1);
                result[35].Precision.ShouldBe(0);
                result[35].Scale.ShouldBe(0);
                result[35].IsNullable.ShouldBe(true);
                result[36].ShouldNotBeNull();
                result[36].ColumnName.ShouldBe("col_geometry");
                result[36].ColumnOrdinal.ShouldBe(36);
                result[36].Datatype.ShouldBe("geometry");
                result[36].Length.ShouldBe(-1);
                result[36].Precision.ShouldBe(0);
                result[36].Scale.ShouldBe(0);
                result[36].IsNullable.ShouldBe(true);
                result[37].ShouldNotBeNull();
                result[37].ColumnName.ShouldBe("col_hierarchyid");
                result[37].ColumnOrdinal.ShouldBe(37);
                result[37].Datatype.ShouldBe("hierarchyid");
                result[37].Length.ShouldBe(892);
                result[37].Precision.ShouldBe(0);
                result[37].Scale.ShouldBe(0);
                result[37].IsNullable.ShouldBe(true);
            }
        }



        [TestMethod]
        public async Task TableTypeMetadataQueryAsync()
        {
            var q = buildQuery();

            var result = (await q.QueryAsync("dbo.tt_int_vals")).ToList();

            //ShouldlyTest.Gen(result, nameof(result));

            {
                result.ShouldNotBeNull();
                result.Count().ShouldBe(1);
                result[0].ShouldNotBeNull();
                result[0].ColumnName.ShouldBe("val");
                result[0].ColumnOrdinal.ShouldBe(0);
                result[0].Datatype.ShouldBe("int");
                result[0].Length.ShouldBe(4);
                result[0].Precision.ShouldBe(10);
                result[0].Scale.ShouldBe(0);
                result[0].IsNullable.ShouldBe(false);
            }

        }

    }
}
