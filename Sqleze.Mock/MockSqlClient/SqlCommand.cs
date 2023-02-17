using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.MockSqlClient
{
    public interface SqlCommand : IDisposable
    {
        string? CommandText { get; set; }
        CommandType CommandType { get; set; }
        SqlConnection? Connection { get; set; }
        SqlTransaction? Transaction { get; set; }

        SqlDataReader? ExecuteReader();
        Task<SqlDataReader?> ExecuteReaderAsync(CancellationToken cancellationToken);
        int ExecuteNonQuery();
        Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken);
        void Cancel();
        SqlParameterCollection Parameters { get; }
        int CommandTimeout { get; set; }
    }
}
