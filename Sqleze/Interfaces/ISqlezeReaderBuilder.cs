using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public interface ISqlezeReaderBuilder
    {
        ISqlezeReaderBuilder With<T>(Action<T, ISqlezeScope> configure);
        int ExecuteNonQuery();
        Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken = default);
        ISqlezeReader ExecuteReader();
        Task<ISqlezeReader> ExecuteReaderAsync(CancellationToken cancellationToken = default);
    }
}
