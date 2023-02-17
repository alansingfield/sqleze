using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.MockSqlClient
{
    public interface SqlTransaction : DbTransaction
    {
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);
        void Rollback();
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
