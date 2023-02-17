using Sqleze;
using Sqleze.Options;
using System;
using System.Data.SqlClient;

namespace Sqleze.SqlClient
{
    public interface IAdo : IDisposable
    {
        AdoState AdoState { get; }
        int RowsetCounter { get; }

        MS.SqlConnection? SqlConnection { get; }
        MS.SqlTransaction? SqlTransaction { get; }
        MS.SqlDataReader? SqlDataReader { get; }
        MS.SqlCommand? SqlCommand { get; }

        bool AutoTransaction { get; set; }
        bool InTransaction { get; }

        void Connect();
        Task ConnectAsync(CancellationToken cancellationToken = default);

        void BeginTransaction();
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);
        void Rollback();
        Task RollbackAsync(CancellationToken cancellationToken = default);

        void StartCommand(
            CommandCreateOptions options,
            IScopedSqlezeReaderFactory execScope, 
            IEnumerable<IAdoParameter> parameterDefinitions);

        void ReadOutputParams();
        void ThrowIfPendingOutputCaptures();

        int ExecuteNonQuery();
        Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken = default);
        void ExecuteReader();
        Task ExecuteReaderAsync(CancellationToken cancellationToken = default);

        bool NextResult();
        Task<bool> NextResultAsync(CancellationToken cancellationToken = default);

        void CompleteReader();
        Task CompleteReaderAsync(CancellationToken cancellationToken = default);
        void ReaderFault();
        Task ReaderFaultAsync(CancellationToken cancellationToken = default);

        void EndCommand();

    }
}