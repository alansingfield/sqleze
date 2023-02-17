using Sqleze.Readers;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ValueGetters
{
    public interface IReaderGetValue<T>
    {
        T? GetValue(MS.SqlDataReader sqlDataReader, int columnOrdinal);
    }

    public interface IReaderGetValue<T, TKnownSqlDbType>
        : IReaderGetValue<T> 
        where TKnownSqlDbType : IKnownSqlDbType { }
}
