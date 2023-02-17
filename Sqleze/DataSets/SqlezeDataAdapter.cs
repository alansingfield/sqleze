using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.DataSets;
public class SqlezeDataAdapter : DataAdapter
{
    public SqlezeDataAdapter()
    {
    }

    public int Fill(DataTable[] dataTables, MS.SqlDataReader dataReader, int startRecord, int maxRecords)
    {
#if !MOCK_SQLCLIENT
        return base.Fill(dataTables, dataReader, startRecord, maxRecords);
#else
        return 0;
#endif
    }
}
