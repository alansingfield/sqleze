using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.TableValuedParameters
{
    public interface ISqlDataRecordFactory
    {
        MSS.SqlDataRecord New(params MSS.SqlMetaData[] metaData);
    }

    #if !MOCK_SQLCLIENT
    public class SqlDataRecordFactory : ISqlDataRecordFactory
    {
        public MSS.SqlDataRecord New(params MSS.SqlMetaData[] metaData) => new MSS.SqlDataRecord(metaData);
    }
    #endif
}
