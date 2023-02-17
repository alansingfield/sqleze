using Sqleze.Readers;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ValueGetters;

public class ReaderGetValueSqlBinary : IReaderGetValue<SqlBinary>
{
    public SqlBinary GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlBinary.Null : sqlDataReader.GetSqlBinary(columnOrdinal);
}

public class ReaderGetValueSqlBoolean : IReaderGetValue<SqlBoolean>
{
    public SqlBoolean GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlBoolean.Null : sqlDataReader.GetSqlBoolean(columnOrdinal);
}

public class ReaderGetValueSqlByte : IReaderGetValue<SqlByte>
{
    public SqlByte GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlByte.Null : sqlDataReader.GetSqlByte(columnOrdinal);
}

public class ReaderGetValueSqlBytes : IReaderGetValue<SqlBytes>
{
    public SqlBytes GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlBytes.Null : sqlDataReader.GetSqlBytes(columnOrdinal);
}

public class ReaderGetValueSqlChars : IReaderGetValue<SqlChars>
{
    public SqlChars GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlChars.Null : sqlDataReader.GetSqlChars(columnOrdinal);
}

public class ReaderGetValueSqlDateTime : IReaderGetValue<SqlDateTime>
{
    public SqlDateTime GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlDateTime.Null : sqlDataReader.GetSqlDateTime(columnOrdinal);
}

public class ReaderGetValueSqlDecimal : IReaderGetValue<SqlDecimal>
{
    public SqlDecimal GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlDecimal.Null : sqlDataReader.GetSqlDecimal(columnOrdinal);
}

public class ReaderGetValueSqlDouble : IReaderGetValue<SqlDouble>
{
    public SqlDouble GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlDouble.Null : sqlDataReader.GetSqlDouble(columnOrdinal);
}

public class ReaderGetValueSqlGuid : IReaderGetValue<SqlGuid>
{
    public SqlGuid GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlGuid.Null : sqlDataReader.GetSqlGuid(columnOrdinal);
}

public class ReaderGetValueSqlInt16 : IReaderGetValue<SqlInt16>
{
    public SqlInt16 GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlInt16.Null : sqlDataReader.GetSqlInt16(columnOrdinal);
}

public class ReaderGetValueSqlInt32 : IReaderGetValue<SqlInt32>
{
    public SqlInt32 GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlInt32.Null : sqlDataReader.GetSqlInt32(columnOrdinal);
}

public class ReaderGetValueSqlInt64 : IReaderGetValue<SqlInt64>
{
    public SqlInt64 GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlInt64.Null : sqlDataReader.GetSqlInt64(columnOrdinal);
}

public class ReaderGetValueSqlMoney : IReaderGetValue<SqlMoney>
{
    public SqlMoney GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlMoney.Null : sqlDataReader.GetSqlMoney(columnOrdinal);
}

public class ReaderGetValueSqlSingle : IReaderGetValue<SqlSingle>
{
    public SqlSingle GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlSingle.Null : sqlDataReader.GetSqlSingle(columnOrdinal);
}

public class ReaderGetValueSqlString : IReaderGetValue<SqlString>
{
    public SqlString GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal) =>
        sqlDataReader.IsDBNull(columnOrdinal) ? SqlString.Null : sqlDataReader.GetSqlString(columnOrdinal);
}

public class ReaderGetValueSqlXml : IReaderGetValue<SqlXml>
{
    public SqlXml GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? SqlXml.Null : sqlDataReader.GetSqlXml(columnOrdinal);
}

public class ReaderGetValueSqlBinaryNullable : IReaderGetValue<SqlBinary?>
{
    public SqlBinary? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlBinary?)null : sqlDataReader.GetSqlBinary(columnOrdinal);
}

public class ReaderGetValueSqlBooleanNullable : IReaderGetValue<SqlBoolean?>
{
    public SqlBoolean? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlBoolean?)null : sqlDataReader.GetSqlBoolean(columnOrdinal);
}

public class ReaderGetValueSqlByteNullable : IReaderGetValue<SqlByte?>
{
    public SqlByte? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlByte?)null : sqlDataReader.GetSqlByte(columnOrdinal);
}

//public class ReaderGetValueSqlBytesNullable : IReaderGetValue<SqlBytes?>
//{
//    public SqlBytes? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
//        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlBytes?)null : sqlDataReader.GetSqlBytes(columnOrdinal);
//}

//public class ReaderGetValueSqlCharsNullable : IReaderGetValue<SqlChars?>
//{
//    public SqlChars? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
//        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlChars?)null : sqlDataReader.GetSqlChars(columnOrdinal);
//}

public class ReaderGetValueSqlDateTimeNullable : IReaderGetValue<SqlDateTime?>
{
    public SqlDateTime? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlDateTime?)null : sqlDataReader.GetSqlDateTime(columnOrdinal);
}

public class ReaderGetValueSqlDecimalNullable : IReaderGetValue<SqlDecimal?>
{
    public SqlDecimal? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlDecimal?)null : sqlDataReader.GetSqlDecimal(columnOrdinal);
}

public class ReaderGetValueSqlDoubleNullable : IReaderGetValue<SqlDouble?>
{
    public SqlDouble? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlDouble?)null : sqlDataReader.GetSqlDouble(columnOrdinal);
}

public class ReaderGetValueSqlGuidNullable : IReaderGetValue<SqlGuid?>
{
    public SqlGuid? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlGuid?)null : sqlDataReader.GetSqlGuid(columnOrdinal);
}

public class ReaderGetValueSqlInt16Nullable : IReaderGetValue<SqlInt16?>
{
    public SqlInt16? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlInt16?)null : sqlDataReader.GetSqlInt16(columnOrdinal);
}

public class ReaderGetValueSqlInt32Nullable : IReaderGetValue<SqlInt32?>
{
    public SqlInt32? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlInt32?)null : sqlDataReader.GetSqlInt32(columnOrdinal);
}

public class ReaderGetValueSqlInt64Nullable : IReaderGetValue<SqlInt64?>
{
    public SqlInt64? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlInt64?)null : sqlDataReader.GetSqlInt64(columnOrdinal);
}

public class ReaderGetValueSqlMoneyNullable : IReaderGetValue<SqlMoney?>
{
    public SqlMoney? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlMoney?)null : sqlDataReader.GetSqlMoney(columnOrdinal);
}

public class ReaderGetValueSqlSingleNullable : IReaderGetValue<SqlSingle?>
{
    public SqlSingle? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlSingle?)null : sqlDataReader.GetSqlSingle(columnOrdinal);
}

public class ReaderGetValueSqlStringNullable : IReaderGetValue<SqlString?>
{
    public SqlString? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlString?)null : sqlDataReader.GetSqlString(columnOrdinal);
}

//public class ReaderGetValueSqlXmlNullable : IReaderGetValue<SqlXml?>
//{
//    public SqlXml? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
//        => sqlDataReader.IsDBNull(columnOrdinal) ? (SqlXml?)null : sqlDataReader.GetSqlXml(columnOrdinal);
//}

