using Sqleze;
using Sqleze.RowsetMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class RowsetMetadataExtensions
{
    public static IEnumerable<string> GetFieldNames(this ISqlezeRowset sqlezeRowset)
    {
        return sqlezeRowset.GetMetadata<IEnumerable<string>>();
    }

    public static IEnumerable<FieldType> GetFieldTypes(this ISqlezeRowset sqlezeRowset)
    {
        return sqlezeRowset.GetMetadata<IEnumerable<FieldType>>();
    }

    public static IEnumerable<FieldSchema> GetFieldSchemas(this ISqlezeRowset sqlezeRowset)
    {
        return sqlezeRowset.GetMetadata<IEnumerable<FieldSchema>>();
    }
}
