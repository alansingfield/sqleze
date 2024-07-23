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


public class RecordSetValueDateOnly : IRecordSetValue<DateOnly>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        DateOnly dateOnly = (DateOnly)val;
        DateTime dateTime = dateOnly.ToDateTime(TimeOnly.MinValue);
        sqlDataRecord.SetValue(columnIndex, dateTime);
    }
}

public class RecordSetValueDateOnlyNullable : IRecordSetValue<DateOnly?>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        if(val == null)
        {
            sqlDataRecord.SetDBNull(columnIndex);
        }
        else
        {
            DateOnly? dateOnly = (DateOnly?)val;
            DateTime dateTime = dateOnly.Value.ToDateTime(TimeOnly.MinValue);
            sqlDataRecord.SetValue(columnIndex, dateTime);
        }
    }
}

public class RecordSetValueTimeOnly : IRecordSetValue<TimeOnly>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        TimeOnly timeOnly = (TimeOnly)val;
        sqlDataRecord.SetValue(columnIndex, timeOnly.ToTimeSpan());
    }
}

public class RecordSetValueTimeOnlyNullable : IRecordSetValue<TimeOnly?>
{
    public void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object val)
    {
        if(val == null)
        {
            sqlDataRecord.SetDBNull(columnIndex);
        }
        else
        {
            TimeOnly? timeOnly = (TimeOnly?)val;
            sqlDataRecord.SetValue(columnIndex, timeOnly.Value.ToTimeSpan());
        }
    }
}
