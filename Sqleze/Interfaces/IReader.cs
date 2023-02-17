using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public interface IReader<T> : IReader
    {
        IEnumerable<T> Enumerate();
        IAsyncEnumerable<T> EnumerateAsync(CancellationToken cancellationToken);
    }

    public interface IReader
    {
    }

}
