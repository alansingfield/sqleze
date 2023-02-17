using Sqleze.Collations;
using Sqleze.DryIoc;
using Sqleze.Dynamics;
using Sqleze;
using Sqleze.Metadata;
using Sqleze.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.TableValuedParameters;

public class SqlDataRecordScalarMapper<T> : ISqlDataRecordMapper<T>
{
    private readonly IRecordSetValue<T> recordSetValue;

    public SqlDataRecordScalarMapper(
        IRecordSetValue<T> recordSetValue)
    {
        this.recordSetValue = recordSetValue;
    }

    public Action<T, MSS.SqlDataRecord> Map(
        IEnumerable<TableTypeColumnDefinition> tableTypeColumnDefinitions)
    {
        return (itm, sqlDataRecord) =>
        {
            if(itm == null)
                sqlDataRecord.SetDBNull(0);
            else
                recordSetValue.SetValue(sqlDataRecord,
                    0,
                    itm);
        };
    }
}

