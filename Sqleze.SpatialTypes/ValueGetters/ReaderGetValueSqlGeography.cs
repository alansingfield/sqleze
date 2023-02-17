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
    internal static class ReaderGetValueSqlGeographyCore
    {
        public static SqlGeography GetSqlGeographyValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            return SqlGeography.Deserialize(sqlDataReader.GetSqlBytes(columnOrdinal));
        }
        public static string GetStringValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            return new string(GetSqlGeographyValue(sqlDataReader, columnOrdinal).STAsText().Value);
        }
        public static byte[] GetByteArrayValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            return sqlDataReader.GetSqlBytes(columnOrdinal).Value;
        }
    }

    public class ReaderGetValueSqlGeography : IReaderGetValue<SqlGeography>
    {
        public SqlGeography GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return SqlGeography.Null;

            return ReaderGetValueSqlGeographyCore.GetSqlGeographyValue(sqlDataReader, columnOrdinal);
        }
    }

    public class ReaderGetValueSqlGeographyToByteArray : IReaderGetValue<byte[], IKnownSqlDbTypeGeography>
    {
        public byte[] GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return Array.Empty<byte>();

            return ReaderGetValueSqlGeographyCore.GetByteArrayValue(sqlDataReader, columnOrdinal);
        }
    }

    public class ReaderGetValueSqlGeographyToByteArrayNullable : IReaderGetValue<byte[]?, IKnownSqlDbTypeGeography>
    {
        public byte[]? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return null;

            return ReaderGetValueSqlGeographyCore.GetByteArrayValue(sqlDataReader, columnOrdinal);
        }
    }

    public class ReaderGetValueSqlGeographyToString : IReaderGetValue<string, IKnownSqlDbTypeGeography>
    {
        public string GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return "";

            return ReaderGetValueSqlGeographyCore.GetStringValue(sqlDataReader, columnOrdinal);
        }
    }
    public class ReaderGetValueSqlGeographyToStringNullable : IReaderGetValue<string?, IKnownSqlDbTypeGeography>
    {
        public string? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return null;

            return ReaderGetValueSqlGeographyCore.GetStringValue(sqlDataReader, columnOrdinal);
        }
    }


    public class ReaderGetValueSqlGeographyToObject : IReaderGetValue<object, IKnownSqlDbTypeGeography>
    {
        public object GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return SqlGeography.Null;

            return ReaderGetValueSqlGeographyCore.GetSqlGeographyValue(sqlDataReader, columnOrdinal);
        }
    }
    public class ReaderGetValueSqlGeographyToObjectNullable : IReaderGetValue<object?, IKnownSqlDbTypeGeography>
    {
        public object? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return null;

            return ReaderGetValueSqlGeographyCore.GetSqlGeographyValue(sqlDataReader, columnOrdinal);
        }
    }
}
