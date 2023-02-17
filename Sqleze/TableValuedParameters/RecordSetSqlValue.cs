using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.TableValuedParameters;

public class RecordSetSqlValueSqlBinary : IRecordSetValue<SqlBinary>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlBinary(columnIndex, (SqlBinary)val);
    }
}

public class RecordSetSqlValueSqlBoolean : IRecordSetValue<SqlBoolean>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlBoolean(columnIndex, (SqlBoolean)val);
    }
}

public class RecordSetSqlValueSqlByte : IRecordSetValue<SqlByte>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlByte(columnIndex, (SqlByte)val);
    }
}

public class RecordSetSqlValueSqlBytes : IRecordSetValue<SqlBytes>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlBytes(columnIndex, (SqlBytes)val);
    }
}

public class RecordSetSqlValueSqlChars : IRecordSetValue<SqlChars>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlChars(columnIndex, (SqlChars)val);
    }
}

public class RecordSetSqlValueSqlDateTime : IRecordSetValue<SqlDateTime>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlDateTime(columnIndex, (SqlDateTime)val);
    }
}

public class RecordSetSqlValueSqlDecimal : IRecordSetValue<SqlDecimal>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlDecimal(columnIndex, (SqlDecimal)val);
    }
}

public class RecordSetSqlValueSqlDouble : IRecordSetValue<SqlDouble>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlDouble(columnIndex, (SqlDouble)val);
    }
}

public class RecordSetSqlValueSqlGuid : IRecordSetValue<SqlGuid>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlGuid(columnIndex, (SqlGuid)val);
    }
}

public class RecordSetSqlValueSqlInt16 : IRecordSetValue<SqlInt16>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlInt16(columnIndex, (SqlInt16)val);
    }
}

public class RecordSetSqlValueSqlInt32 : IRecordSetValue<SqlInt32>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlInt32(columnIndex, (SqlInt32)val);
    }
}

public class RecordSetSqlValueSqlInt64 : IRecordSetValue<SqlInt64>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlInt64(columnIndex, (SqlInt64)val);
    }
}

public class RecordSetSqlValueSqlMoney : IRecordSetValue<SqlMoney>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlMoney(columnIndex, (SqlMoney)val);
    }
}

public class RecordSetSqlValueSqlSingle : IRecordSetValue<SqlSingle>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlSingle(columnIndex, (SqlSingle)val);
    }
}

public class RecordSetSqlValueSqlString : IRecordSetValue<SqlString>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlString(columnIndex, (SqlString)val);
    }
}

public class RecordSetSqlValueSqlXml : IRecordSetValue<SqlXml>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetSqlXml(columnIndex, (SqlXml)val);
    }
}

