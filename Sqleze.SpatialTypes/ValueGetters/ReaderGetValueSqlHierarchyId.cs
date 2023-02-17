using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using Sqleze;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SpatialTypes.ValueGetters;

internal static class ReaderGetValueSqlHierarchyIdCore
{
    public static SqlHierarchyId GetSqlHierarchyIdValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        using var stream = sqlDataReader.GetStream(columnOrdinal);
        using var reader = new BinaryReader(stream);

        var result = new SqlHierarchyId();
        result.Read(reader);

        return result;
    }

    public static string GetStringValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        return GetSqlHierarchyIdValue(sqlDataReader, columnOrdinal).ToString();
    }
    public static byte[] GetByteArrayValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        return sqlDataReader.GetSqlBytes(columnOrdinal).Value;
    }
}

public class ReaderGetValueSqlHierarchyId : IReaderGetValue<SqlHierarchyId>
{
    public SqlHierarchyId GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return SqlHierarchyId.Null;

        return ReaderGetValueSqlHierarchyIdCore.GetSqlHierarchyIdValue(sqlDataReader, columnOrdinal);
    }
}

public class ReaderGetValueSqlHierarchyIdNullable : IReaderGetValue<SqlHierarchyId?>
{
    public SqlHierarchyId? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return null;

        return ReaderGetValueSqlHierarchyIdCore.GetSqlHierarchyIdValue(sqlDataReader, columnOrdinal);
    }
}


public class ReaderGetValueSqlHierarchyIdToByteArray : IReaderGetValue<byte[], IKnownSqlDbTypeHierarchyId>
{
    public byte[] GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return Array.Empty<byte>();

        return ReaderGetValueSqlHierarchyIdCore.GetByteArrayValue(sqlDataReader, columnOrdinal);
    }
}

public class ReaderGetValueSqlHierarchyIdToByteArrayNullable : IReaderGetValue<byte[]?, IKnownSqlDbTypeHierarchyId>
{
    public byte[]? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return null;

        return ReaderGetValueSqlHierarchyIdCore.GetByteArrayValue(sqlDataReader, columnOrdinal);
    }
}

public class ReaderGetValueSqlHierarchyIdToString : IReaderGetValue<string, IKnownSqlDbTypeHierarchyId>
{
    public string GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return "";

        return ReaderGetValueSqlHierarchyIdCore.GetStringValue(sqlDataReader, columnOrdinal);
    }
}
public class ReaderGetValueSqlHierarchyIdToStringNullable : IReaderGetValue<string?, IKnownSqlDbTypeHierarchyId>
{
    public string? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return null;

        return ReaderGetValueSqlHierarchyIdCore.GetStringValue(sqlDataReader, columnOrdinal);
    }
}

public class ReaderGetValueSqlHierarchyIdToObject : IReaderGetValue<object, IKnownSqlDbTypeHierarchyId>
{
    public object GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return SqlHierarchyId.Null;

        return ReaderGetValueSqlHierarchyIdCore.GetSqlHierarchyIdValue(sqlDataReader, columnOrdinal);
    }
}

public class ReaderGetValueSqlHierarchyIdToObjectNullable : IReaderGetValue<object?, IKnownSqlDbTypeHierarchyId>
{
    public object? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    {
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return null;

        return ReaderGetValueSqlHierarchyIdCore.GetSqlHierarchyIdValue(sqlDataReader, columnOrdinal);
    }
}
