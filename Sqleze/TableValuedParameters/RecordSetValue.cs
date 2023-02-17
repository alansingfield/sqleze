using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.TableValuedParameters;

public class RecordSetValue<T> : IRecordSetValue<T>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        sqlDataRecord.SetValue(columnIndex, val);
    }
}
