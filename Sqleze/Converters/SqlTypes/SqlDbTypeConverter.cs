using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Converters.SqlTypes
{
    public static class SqlDbTypeConverter
    {
        public static SqlDbType? ToSqlDbType(string dataTypeName)
        {
            return (dataTypeName?.ToLowerInvariant()) switch
            {
                "bigint"            => SqlDbType.BigInt,
                "binary"            => SqlDbType.Binary,
                "bit"               => SqlDbType.Bit,
                "char"              => SqlDbType.Char,
                "date"              => SqlDbType.Date,
                "datetime"          => SqlDbType.DateTime,
                "datetime2"         => SqlDbType.DateTime2,
                "datetimeoffset"    => SqlDbType.DateTimeOffset,
                "decimal"           => SqlDbType.Decimal,
                "numeric"           => SqlDbType.Decimal,
                "float"             => SqlDbType.Float,
                "image"             => SqlDbType.Image,
                "int"               => SqlDbType.Int,
                "money"             => SqlDbType.Money,
                "nchar"             => SqlDbType.NChar,
                "ntext"             => SqlDbType.NText,
                "nvarchar"          => SqlDbType.NVarChar,
                "real"              => SqlDbType.Real,
                "smalldatetime"     => SqlDbType.SmallDateTime,
                "smallint"          => SqlDbType.SmallInt,
                "smallmoney"        => SqlDbType.SmallMoney,
                "text"              => SqlDbType.Text,
                "time"              => SqlDbType.Time,
                "timestamp"         => SqlDbType.Timestamp,
                "tinyint"           => SqlDbType.TinyInt,
                "uniqueidentifier"  => SqlDbType.UniqueIdentifier,
                "varbinary"         => SqlDbType.VarBinary,
                "varchar"           => SqlDbType.VarChar,
                "variant"           => SqlDbType.Variant,
                "xml"               => SqlDbType.Xml,
                //case "structured": return SqlDbType.Structured;
                //case "udt": return SqlDbType.Udt;
                _ => null,
            };
        }

        public static SqlDbType ToSqlDbTypeKnown(string dataTypeName)
        {
            var sqlDbType = ToSqlDbType(dataTypeName);

            if(sqlDbType == null)
                throw new Exception($"Unknown SQL scalar type {dataTypeName}");

            return sqlDbType.Value;
        }
    }
}
