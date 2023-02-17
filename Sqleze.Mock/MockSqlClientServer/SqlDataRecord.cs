using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.MockSqlClientServer
{
    public interface SqlDataRecord
    {
        //public SqlDataRecord(params SqlMetaData[] metaData);
        int FieldCount { get; }
        string GetName(int ordinal);
        //bool IsDBNull(int ordinal);
        //void SetBoolean(int ordinal, bool value);
        //void SetByte(int ordinal, byte value);
        //void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);
        //void SetChar(int ordinal, char value);
        //void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);
        //void SetDateTime(int ordinal, DateTime value);
        //void SetDateTimeOffset(int ordinal, DateTimeOffset value);
        void SetDBNull(int ordinal);
        //void SetDecimal(int ordinal, decimal value);
        //void SetDouble(int ordinal, double value);
        //void SetFloat(int ordinal, float value);
        //void SetGuid(int ordinal, Guid value);
        //void SetInt16(int ordinal, short value);
        //void SetInt32(int ordinal, int value);
        //void SetInt64(int ordinal, long value);
        void SetSqlBinary(int ordinal, SqlBinary value);
        void SetSqlBoolean(int ordinal, SqlBoolean value);
        void SetSqlByte(int ordinal, SqlByte value);
        void SetSqlBytes(int ordinal, SqlBytes value);
        void SetSqlChars(int ordinal, SqlChars value);
        void SetSqlDateTime(int ordinal, SqlDateTime value);
        void SetSqlDecimal(int ordinal, SqlDecimal value);
        void SetSqlDouble(int ordinal, SqlDouble value);
        void SetSqlGuid(int ordinal, SqlGuid value);
        void SetSqlInt16(int ordinal, SqlInt16 value);
        void SetSqlInt32(int ordinal, SqlInt32 value);
        void SetSqlInt64(int ordinal, SqlInt64 value);
        void SetSqlMoney(int ordinal, SqlMoney value);
        void SetSqlSingle(int ordinal, SqlSingle value);
        void SetSqlString(int ordinal, SqlString value);
        void SetSqlXml(int ordinal, SqlXml value);
        //void SetString(int ordinal, string value);
        //void SetTimeSpan(int ordinal, TimeSpan value);
        void SetValue(int ordinal, object value);
        //int SetValues(params object[] values);
    }
}
