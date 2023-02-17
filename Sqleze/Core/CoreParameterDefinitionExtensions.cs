using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public static class CoreParameterDefinitionExtensions
{
    public static ISqlezeParameter<T> Value<T>(this ISqlezeParameter<T> sqlezeParameter, T value)
    {
        sqlezeParameter.Value = value;
        sqlezeParameter.OmitInput = false;
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> OmitInputValue<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Value = default;
        sqlezeParameter.OmitInput = true;
        return sqlezeParameter;
    }

    /// <summary>
    /// When using an output parameter, put the parameter into Output mode rather than
    /// the normal InputOutput. Only really useful for calling stored procedures with
    /// an output parameter which has a default
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sqlezeParameter"></param>
    /// <param name="outputOnly"></param>
    /// <returns></returns>
    public static ISqlezeParameter<T> OutputOnly<T>(this ISqlezeParameter<T> sqlezeParameter, bool outputOnly = true)
    {
        sqlezeParameter.OmitInput = true;
        return sqlezeParameter;
    }

    private static ISqlezeParameter<T> scalar<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        return sqlezeParameter;
    }

    //public static ISqlezeParameter<T> SqlType<T>(this ISqlezeParameter<T> sqlezeParameter, string sqlType)
    //{
    //    sqlezeParameter.SqlType(sqlType);
    //    return sqlezeParameter;
    //}

    public static ISqlezeParameter<T> Length<T>(this ISqlezeParameter<T> sqlezeParameter, int length)
    {
        sqlezeParameter.Length = length;
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> Scale<T>(this ISqlezeParameter<T> sqlezeParameter, int scale)
    {
        sqlezeParameter.Scale = (byte)scale;
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> Precision<T>(this ISqlezeParameter<T> sqlezeParameter, int precision)
    {
        sqlezeParameter.Precision = (byte)precision;
        return sqlezeParameter;
    }


    public static ISqlezeParameter<T> AsTableType<T>(this ISqlezeParameter<T> sqlezeParameter, string tableTypeName)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.TableType;
        sqlezeParameter.SqlTypeName = tableTypeName;
        return sqlezeParameter;
    }


    public static ISqlezeParameter<T> AsDecimal<T>(this ISqlezeParameter<T> sqlezeParameter, int precision, int scale)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "decimal";
        sqlezeParameter.Precision(precision);
        sqlezeParameter.Scale(scale);
        return sqlezeParameter;
    }
    public static ISqlezeParameter<T> AsDecimal<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "decimal";
        return sqlezeParameter;
    }
    public static ISqlezeParameter<T> AsNumeric<T>(this ISqlezeParameter<T> sqlezeParameter, int precision, int scale)
    {
         return sqlezeParameter.AsDecimal<T>(precision, scale);
    }
    public static ISqlezeParameter<T> AsNumeric<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        return sqlezeParameter.AsDecimal<T>();
    }


    public static ISqlezeParameter<T> AsVarChar<T>(this ISqlezeParameter<T> sqlezeParameter, int? length = null)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "varchar";

        if(length != null)
            sqlezeParameter.Length(length.Value);

        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsVarCharMax<T>(this ISqlezeParameter<T> sqlezeParameter)
        => sqlezeParameter.AsVarChar(length: -1);

    public static ISqlezeParameter<T> AsNVarChar<T>(this ISqlezeParameter<T> sqlezeParameter, int? length = null)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "nvarchar";

        if(length != null)
            sqlezeParameter.Length(length.Value);

        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsNVarCharMax<T>(this ISqlezeParameter<T> sqlezeParameter)
        => sqlezeParameter.AsNVarChar(length: -1);

    public static ISqlezeParameter<T> AsChar<T>(this ISqlezeParameter<T> sqlezeParameter, int? length = null)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "char";

        if(length != null)
            sqlezeParameter.Length(length.Value);

        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsNChar<T>(this ISqlezeParameter<T> sqlezeParameter, int? length = null)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "nchar";

        if(length != null)
            sqlezeParameter.Length(length.Value);

        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsBinary<T>(this ISqlezeParameter<T> sqlezeParameter, int? length = null)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "binary";

        if(length != null)
            sqlezeParameter.Length(length.Value);

        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsVarBinary<T>(this ISqlezeParameter<T> sqlezeParameter, int? length = null)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "varbinary";

        if(length != null)
            sqlezeParameter.Length(length.Value);

        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsVarBinaryMax<T>(this ISqlezeParameter<T> sqlezeParameter)
        => sqlezeParameter.AsVarBinary(length: -1);

    public static ISqlezeParameter<T> AsDateTime<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "datetime";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsDateTime2<T>(this ISqlezeParameter<T> sqlezeParameter, int? scale = null)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "datetime2";
        sqlezeParameter.Scale(scale ?? 7);
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsDateTimeOffset<T>(this ISqlezeParameter<T> sqlezeParameter, int? scale = null)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "datetimeoffset";
        sqlezeParameter.Scale(scale ?? 7);
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsTime<T>(this ISqlezeParameter<T> sqlezeParameter, int? scale = null)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "time";
        sqlezeParameter.Scale(scale ?? 7);
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsBigInt<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "bigint";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsBit<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "bit";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsFloat<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "float";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsImage<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "image";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsInt<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "int";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsMoney<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "money";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsReal<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "real";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsUniqueIdentifier<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "uniqueidentifier";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsSmallDateTime<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "smalldatetime";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsSmallInt<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "smallint";
        return sqlezeParameter;
    }
    public static ISqlezeParameter<T> AsSmallMoney<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "smallmoney";
        return sqlezeParameter;
    }
    public static ISqlezeParameter<T> AsText<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "text";
        return sqlezeParameter;
    }
    public static ISqlezeParameter<T> AsTimestamp<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "timestamp";
        return sqlezeParameter;
    }
    public static ISqlezeParameter<T> AsTinyInt<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "tinyint";
        return sqlezeParameter;
    }
    public static ISqlezeParameter<T> AsSqlVariant<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "sql_variant";
        return sqlezeParameter;
    }

    // TODO - add XML schema etc to this...
    public static ISqlezeParameter<T> AsXml<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "xml";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsDate<T>(this ISqlezeParameter<T> sqlezeParameter)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;
        sqlezeParameter.SqlTypeName = "date";
        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> AsUdt<T>(this ISqlezeParameter<T> sqlezeParameter,
        string udtTypeName)
    {
        sqlezeParameter.Mode = SqlezeParameterMode.AssemblyType;
        sqlezeParameter.SqlTypeName = udtTypeName;
        return sqlezeParameter;
    }

}
