using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.TableValuedParameters
{
    public interface ISqlMetaDataFactory
    {
        MSS.SqlMetaData New(string name, SqlDbType sqlDbType);
        MSS.SqlMetaData New(string name, SqlDbType sqlDbType, byte precision, byte scale);
        MSS.SqlMetaData New(string name, SqlDbType sqlDbType, int maxLength);
    }

#if !MOCK_SQLCLIENT
    public class SqlMetaDataFactory : ISqlMetaDataFactory
    {
        public MSS.SqlMetaData New(string name, SqlDbType sqlDbType, int maxLength)
            => new MSS.SqlMetaData(name, sqlDbType, maxLength);
        public MSS.SqlMetaData New(string name, SqlDbType sqlDbType, byte precision, byte scale)
            => new MSS.SqlMetaData(name, sqlDbType, precision, scale);
        public MSS.SqlMetaData New(string name, SqlDbType sqlDbType)
            => new MSS.SqlMetaData(name, sqlDbType);
    }
#endif
}
