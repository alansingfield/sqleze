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


public class ReaderGetValueDateOnlyNullable : IReaderGetValue<DateOnly?>
{
    private readonly IReaderGetValue<DateTime?> inner;

    public ReaderGetValueDateOnlyNullable(IReaderGetValue<DateTime?> inner)
    {
        this.inner = inner;
    }

    public DateOnly? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
    {
        var dttm = inner.GetValue(sqlDataReader, columnOrdinal);
        return dttm is null ? null : DateOnly.FromDateTime(dttm.Value);
    }
}

public class ReaderGetValueDateOnly : IReaderGetValue<DateOnly>
{
    private readonly IReaderGetValue<DateOnly?> inner;

    public ReaderGetValueDateOnly(IReaderGetValue<DateOnly?> inner)
    {
        this.inner = inner;
    }

    public DateOnly GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => inner.GetValue(sqlDataReader, columnOrdinal) ?? DateOnly.MinValue;
}

public class ReaderGetValueTimeOnlyNullable : IReaderGetValue<TimeOnly?>
{
    private readonly IReaderGetValue<TimeSpan?> inner;

    public ReaderGetValueTimeOnlyNullable(IReaderGetValue<TimeSpan?> inner)
    {
        this.inner = inner;
    }

    public TimeOnly? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
    {
        var timespan = inner.GetValue(sqlDataReader, columnOrdinal);
        return timespan is null ? null : TimeOnly.FromTimeSpan(timespan.Value);
    }
}

public class ReaderGetValueTimeOnly : IReaderGetValue<TimeOnly>
{
    private readonly IReaderGetValue<TimeOnly?> inner;

    public ReaderGetValueTimeOnly(IReaderGetValue<TimeOnly?> inner)
    {
        this.inner = inner;
    }

    public TimeOnly GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal)
        => inner.GetValue(sqlDataReader, columnOrdinal) ?? TimeOnly.MinValue;
}

