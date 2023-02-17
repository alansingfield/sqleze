using Sqleze.Options;
using Sqleze.SqlClient;

namespace Sqleze;

public interface ISqlezeCommand
{
    int ExecuteNonQuery(
        ISqlezeReaderFactory? sqlezeReaderFactory = null);
    Task<int> ExecuteNonQueryAsync(
        ISqlezeReaderFactory? sqlezeReaderFactory = null, 
        CancellationToken cancellationToken = default);

    ISqlezeReader ExecuteReader(
        ISqlezeReaderFactory? sqlezeReaderFactory = null);
    Task<ISqlezeReader> ExecuteReaderAsync(
        ISqlezeReaderFactory? sqlezeReaderFactory = null, 
        CancellationToken cancellationToken = default);

    ISqlezeReaderBuilder With<T>(Action<T, ISqlezeScope> configure);

    ISqlezeParameterCollection Parameters { get; }
}

