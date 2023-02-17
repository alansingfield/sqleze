using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ValueGetters;

public interface IKnownSqlDbTypeResolver
{
    Type? ResolveKnownSqlDbType(string sqlTypeName);
}

public class KnownSqlDbTypeResolver : IKnownSqlDbTypeResolver
{
    // When we read a rowset, we get the column type name as a string. We want to convert
    // this to a Type so we can then resolve the correct kind of reader from the container.
    public Type? ResolveKnownSqlDbType(string sqlTypeName)
    {
        return (sqlTypeName.ToLowerInvariant()) switch
        {
            "bigint" => typeof(IKnownSqlDbTypeBigInt),
            "binary" => typeof(IKnownSqlDbTypeBinary),
            "bit" => typeof(IKnownSqlDbTypeBit),
            "char" => typeof(IKnownSqlDbTypeChar),
            "date" => typeof(IKnownSqlDbTypeDate),
            "datetime" => typeof(IKnownSqlDbTypeDateTime),
            "datetime2" => typeof(IKnownSqlDbTypeDateTime2),
            "datetimeoffset" => typeof(IKnownSqlDbTypeDateTimeOffset),
            "decimal" => typeof(IKnownSqlDbTypeDecimal),
            "numeric" => typeof(IKnownSqlDbTypeDecimal),
            "float" => typeof(IKnownSqlDbTypeFloat),
            "image" => typeof(IKnownSqlDbTypeImage),
            "int" => typeof(IKnownSqlDbTypeInt),
            "money" => typeof(IKnownSqlDbTypeMoney),
            "nchar" => typeof(IKnownSqlDbTypeNChar),
            "ntext" => typeof(IKnownSqlDbTypeNText),
            "nvarchar" => typeof(IKnownSqlDbTypeNVarChar),
            "real" => typeof(IKnownSqlDbTypeReal),
            "smalldatetime" => typeof(IKnownSqlDbTypeSmallDateTime),
            "smallint" => typeof(IKnownSqlDbTypeSmallInt),
            "smallmoney" => typeof(IKnownSqlDbTypeSmallMoney),
            // "structured" - not relevant for table types
            // "udt" - not supplied 
            "text" => typeof(IKnownSqlDbTypeText),
            "time" => typeof(IKnownSqlDbTypeTime),
            "timestamp" => typeof(IKnownSqlDbTypeTimestamp),
            "tinyint" => typeof(IKnownSqlDbTypeTinyInt),
            "uniqueidentifier" => typeof(IKnownSqlDbTypeUniqueIdentifier),
            "varbinary" => typeof(IKnownSqlDbTypeVarBinary),
            "varchar" => typeof(IKnownSqlDbTypeVarChar),
            "variant" => typeof(IKnownSqlDbTypeVariant), // ?????
            "xml" => typeof(IKnownSqlDbTypeXml),
            _ => null
        };
    }
}

public interface IKnownSqlDbType { }

public interface IKnownSqlDbTypeBigInt : IKnownSqlDbType { }
public interface IKnownSqlDbTypeBinary : IKnownSqlDbType { }
public interface IKnownSqlDbTypeBit : IKnownSqlDbType { }
public interface IKnownSqlDbTypeChar : IKnownSqlDbType { }
public interface IKnownSqlDbTypeDate : IKnownSqlDbType { }
public interface IKnownSqlDbTypeDateTime : IKnownSqlDbType { }
public interface IKnownSqlDbTypeDateTime2 : IKnownSqlDbType { }
public interface IKnownSqlDbTypeDateTimeOffset : IKnownSqlDbType { }
public interface IKnownSqlDbTypeDecimal : IKnownSqlDbType { }
public interface IKnownSqlDbTypeFloat : IKnownSqlDbType { }
public interface IKnownSqlDbTypeImage : IKnownSqlDbType { }
public interface IKnownSqlDbTypeInt : IKnownSqlDbType { }
public interface IKnownSqlDbTypeMoney : IKnownSqlDbType { }
public interface IKnownSqlDbTypeNChar : IKnownSqlDbType { }
public interface IKnownSqlDbTypeNText : IKnownSqlDbType { }
public interface IKnownSqlDbTypeNVarChar : IKnownSqlDbType { }
public interface IKnownSqlDbTypeReal : IKnownSqlDbType { }
public interface IKnownSqlDbTypeSmallDateTime : IKnownSqlDbType { }
public interface IKnownSqlDbTypeSmallInt : IKnownSqlDbType { }
public interface IKnownSqlDbTypeSmallMoney : IKnownSqlDbType { }
public interface IKnownSqlDbTypeStructured : IKnownSqlDbType { }
public interface IKnownSqlDbTypeText : IKnownSqlDbType { }
public interface IKnownSqlDbTypeTime : IKnownSqlDbType { }
public interface IKnownSqlDbTypeTimestamp : IKnownSqlDbType { }
public interface IKnownSqlDbTypeTinyInt : IKnownSqlDbType { }
public interface IKnownSqlDbTypeUdt : IKnownSqlDbType { }
public interface IKnownSqlDbTypeUniqueIdentifier : IKnownSqlDbType { }
public interface IKnownSqlDbTypeVarBinary : IKnownSqlDbType { }
public interface IKnownSqlDbTypeVarChar : IKnownSqlDbType { }
public interface IKnownSqlDbTypeVariant : IKnownSqlDbType { }
public interface IKnownSqlDbTypeXml : IKnownSqlDbType { }

