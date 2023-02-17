using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.TableValuedParameters;


public interface IRecordSetValue
{
    void SetValue(MSS.SqlDataRecord sqlDataRecord, int columnIndex, object value);
}

public interface IRecordSetValue<T> : IRecordSetValue
{
}
