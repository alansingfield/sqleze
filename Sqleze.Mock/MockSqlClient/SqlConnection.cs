using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.MockSqlClient
{
    public interface SqlConnection : IDisposable
    {
        string? ConnectionString { get; set; }
        public string Database { get; }
        ConnectionState State { get; set; }
        event SqlInfoMessageEventHandler InfoMessage;
        bool FireInfoMessageEventOnUserErrors { get; set; }

        void Open();
        Task OpenAsync(CancellationToken cancellationToken = default);
        SqlTransaction? BeginTransaction();
        ValueTask<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        string AccessToken { get; set; }
        SqlCredential Credential { get; set; }
    }
}
