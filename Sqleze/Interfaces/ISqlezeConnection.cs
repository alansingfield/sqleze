namespace Sqleze;

public interface ISqlezeConnection : IDisposable
{
    ISqlezeCommandBuilder With<T>(Action<T, ISqlezeScope> configure);
    bool InTransaction { get; }
    bool AutoTransaction { get; set; }

    void BeginTransaction();
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    void Rollback();
    Task RollbackAsync(CancellationToken cancellationToken = default);

    void Commit();
    Task CommitAsync(CancellationToken cancellationToken = default);
}