namespace Sqleze.ValueGetters;

public class ReaderGetValue<T> : IReaderGetValue<T>
{
    public T? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
    {
        // Don't use DBNull - use a normal null instead.
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return default;

        object value = sqlDataReader.GetValue(columnOrdinal);

        Type type = typeof(T);

        // Convert.ChangeType needs the underlying value type for nullables.
        type = Nullable.GetUnderlyingType(type) ?? type;

        return (T?)Convert.ChangeType(value, type);
    }
}


public class ReaderGetFieldValue<T> : IReaderGetValue<T>
{
    public T? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
    {
        // Don't use DBNull - use a normal null instead.
        if(sqlDataReader.IsDBNull(columnOrdinal))
            return default;

        object? value = sqlDataReader.GetFieldValue<T>(columnOrdinal);

        Type type = typeof(T);

        // Convert.ChangeType needs the underlying value type for nullables.
        type = Nullable.GetUnderlyingType(type) ?? type;

        return (T?)Convert.ChangeType(value, type);
    }
}