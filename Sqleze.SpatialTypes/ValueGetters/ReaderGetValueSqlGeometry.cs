using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using Sqleze;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SpatialTypes.ValueGetters
{
    internal static class ReaderGetValueSqlGeometryCore
    {
        public static SqlGeometry GetSqlGeometryValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            return SqlGeometry.Deserialize(sqlDataReader.GetSqlBytes(columnOrdinal));
        }
        public static string GetStringValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            return new string(GetSqlGeometryValue(sqlDataReader, columnOrdinal).STAsText().Value);
        }
        public static byte[] GetByteArrayValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            return sqlDataReader.GetSqlBytes(columnOrdinal).Value;
        }
    }

    public class ReaderGetValueSqlGeometry : IReaderGetValue<SqlGeometry>
    {
        public SqlGeometry GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return SqlGeometry.Null;

            return ReaderGetValueSqlGeometryCore.GetSqlGeometryValue(sqlDataReader, columnOrdinal);
        }
    }

    public class ReaderGetValueSqlGeometryToByteArray : IReaderGetValue<byte[], IKnownSqlDbTypeGeometry>
    {
        public byte[] GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return Array.Empty<byte>();

            return ReaderGetValueSqlGeometryCore.GetByteArrayValue(sqlDataReader, columnOrdinal);
        }
    }

    public class ReaderGetValueSqlGeometryToByteArrayNullable : IReaderGetValue<byte[]?, IKnownSqlDbTypeGeometry>
    {
        public byte[]? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return null;

            return ReaderGetValueSqlGeometryCore.GetByteArrayValue(sqlDataReader, columnOrdinal);
        }
    }

    public class ReaderGetValueSqlGeometryToString : IReaderGetValue<string, IKnownSqlDbTypeGeometry>
    {
        public string GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return "";

            return ReaderGetValueSqlGeometryCore.GetStringValue(sqlDataReader, columnOrdinal);
        }
    }
    public class ReaderGetValueSqlGeometryToStringNullable : IReaderGetValue<string?, IKnownSqlDbTypeGeometry>
    {
        public string? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return null;

            return ReaderGetValueSqlGeometryCore.GetStringValue(sqlDataReader, columnOrdinal);
        }
    }

    public class ReaderGetValueSqlGeometryToObject : IReaderGetValue<object, IKnownSqlDbTypeGeometry>
    {
        public object GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return SqlGeometry.Null;

            return ReaderGetValueSqlGeometryCore.GetSqlGeometryValue(sqlDataReader, columnOrdinal);
        }
    }
    public class ReaderGetValueSqlGeometryToObjectNullable : IReaderGetValue<object?, IKnownSqlDbTypeGeometry>
    {
        public object? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return null;

            return ReaderGetValueSqlGeometryCore.GetSqlGeometryValue(sqlDataReader, columnOrdinal);
        }
    }

}
