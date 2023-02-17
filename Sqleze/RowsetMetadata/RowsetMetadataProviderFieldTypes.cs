using Sqleze.Readers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.RowsetMetadata;

public record FieldType
(
    string Name,
    int ColumnOrdinal,
    string SqlDataTypeName
);

public class RowsetMetadataProviderFieldTypes : IRowsetMetadataProvider<IEnumerable<FieldType>>
{
    private readonly IDataReaderFieldNames dataReaderFieldNames;

    public RowsetMetadataProviderFieldTypes(IDataReaderFieldNames dataReaderFieldNames)
    {
        this.dataReaderFieldNames = dataReaderFieldNames;
    }

    public IEnumerable<FieldType> GetMetadata()
    {
        return dataReaderFieldNames.GetFieldInfos()
            .Select(x => new FieldType(
                x.ColumnName,
                x.ColumnOrdinal,
                x.SqlDataTypeName
             ))
            .ToImmutableArray();
    }
}
