using Sqleze.Readers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.RowsetMetadata;
public class RowsetMetadataProviderFieldNames : IRowsetMetadataProvider<IEnumerable<string>>
{
    private readonly IDataReaderFieldNames dataReaderFieldNames;

    public RowsetMetadataProviderFieldNames(IDataReaderFieldNames dataReaderFieldNames)
    {
        this.dataReaderFieldNames = dataReaderFieldNames;
    }

    public IEnumerable<string> GetMetadata()
    {
        return dataReaderFieldNames.GetFieldInfos()
            .Select(x => x.ColumnName)
            .ToImmutableArray();
    }
}
