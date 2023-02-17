using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.MockSqlClient
{
    public interface SqlDataReader : IDisposable
    {
        bool IsClosed { get; }
        int VisibleFieldCount { get; }

        void Close();
        Task CloseAsync(CancellationToken cancellationToken);
        bool Read();
        Task<bool> ReadAsync(CancellationToken cancellationToken);

        bool NextResult();
        Task<bool> NextResultAsync(CancellationToken cancellationToken);
        string GetName(int i);
        string GetDataTypeName(int i);

        object GetValue(int i);
        T? GetFieldValue<T>(int i);
        bool IsDBNull(int i);
        SqlBinary GetSqlBinary(int i);
        SqlBoolean GetSqlBoolean(int i);
        SqlByte GetSqlByte(int i);
        SqlBytes GetSqlBytes(int i);
        SqlChars GetSqlChars(int i);
        SqlDateTime GetSqlDateTime(int i);
        SqlDecimal GetSqlDecimal(int i);
        SqlDouble GetSqlDouble(int i);
        SqlGuid GetSqlGuid(int i);
        SqlInt16 GetSqlInt16(int i);
        SqlInt32 GetSqlInt32(int i);
        SqlInt64 GetSqlInt64(int i);
        SqlMoney GetSqlMoney(int i);
        SqlSingle GetSqlSingle(int i);
        SqlString GetSqlString(int i);
        SqlXml GetSqlXml(int i);

        DataTable GetSchemaTable();
    }
}
