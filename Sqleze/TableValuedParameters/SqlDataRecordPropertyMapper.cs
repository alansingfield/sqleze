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

public class SqlDataRecordPropertyMapper<T> : ISqlDataRecordMapper<T>
{
    private readonly IMultiResolver<ISqlDataRecordWriterFromProperty<T>, T> mappingsResolver;
    private readonly INamingConvention namingConvention;
    private readonly ICollation collation;
    private readonly ITableTypeDuplicateColumnsPolicy tableTypeDuplicateColumnsPolicy;
    private readonly ITableTypeUnmappedColumnsPolicy tableTypeUnmappedColumnsPolicy;
    private readonly ITableTypeUnmappedPropertiesPolicy tableTypeUnmappedPropertiesPolicy;

    public SqlDataRecordPropertyMapper(
        IMultiResolver<ISqlDataRecordWriterFromProperty<T>, T> mappingsResolver,
        INamingConvention namingConvention,
        ICollation collation,
        ITableTypeDuplicateColumnsPolicy tableTypeDuplicateColumnsPolicy,
        ITableTypeUnmappedColumnsPolicy tableTypeUnmappedColumnsPolicy,
        ITableTypeUnmappedPropertiesPolicy tableTypeUnmappedPropertiesPolicy)
    {
        this.mappingsResolver = mappingsResolver;
        this.namingConvention = namingConvention;
        this.collation = collation;
        this.tableTypeDuplicateColumnsPolicy = tableTypeDuplicateColumnsPolicy;
        this.tableTypeUnmappedColumnsPolicy = tableTypeUnmappedColumnsPolicy;
        this.tableTypeUnmappedPropertiesPolicy = tableTypeUnmappedPropertiesPolicy;
    }

    public Action<T, MSS.SqlDataRecord> Map(IEnumerable<TableTypeColumnDefinition> tableTypeColumnDefinitions)
    {
        // Get field names from table type. There normally won't be duplicate field names unless
        // something odd is happening with case-sensitivity (case-sensitive SQL server and
        // we set the collation to case-insensitive)
        var colDefDict = tableTypeColumnDefinitions
            .GroupBy(x => x.ColumnName, x => x, collation.Comparer)
            .ToDictionary(x => x.Key, x => x.ToList(),
                collation.Comparer);

        // Tick off each Column name as we map it; the remainder will be the unmapped fields.
        var unmappedColumns = new HashSet<string>(colDefDict.Keys, collation.Comparer);

        var duplicateColumns = new List<(TableTypeColumnDefinition, PropertyInfo)>();
        var unmappedProps = new List<(PropertyInfo, string)>();

        var mappings = mappingsResolver.ResolvePerProperty(prop =>
        {
            // Compute the SQL table type column name based on the C# property name.
            string columnName = namingConvention.DotNetToSql(prop.Name);

            // Look up the column name, do we get a match?
            if(!colDefDict.TryGetValue(columnName, out var colDef))
            {
                unmappedProps.Add((prop, columnName));
            }
            else
            {
                // We've mapped this column, remove it from the unmapped set.
                unmappedColumns.Remove(columnName);

                // It was a match, but is is unambiguous? Keep track of these but
                // keep going, we raise an error later if needed.
                if(colDef.Count > 1)
                {
                    duplicateColumns.AddRange(colDef.Select(x => (x, prop)));
                }

                // If we get to here, we want to resolve a scope for the property/column
                // binding. Pass a holder for the PropertyInfo, and the first ColumnDef
                // into that new scope.
                return (true, new object[]
                {
                    new ResolvedPropertyInfo(prop),
                    colDef.First()
                });
            }

            // No match, don't want it
            return (false, null);

        }).ToList();

        tableTypeDuplicateColumnsPolicy.Handle(duplicateColumns);

        // Did we get a column for every property in our object?
        tableTypeUnmappedPropertiesPolicy.Handle(unmappedProps);

        // Table parameters must have all fields mapped; if not raise an error.
        tableTypeUnmappedColumnsPolicy.Handle(
            tableTypeColumnDefinitions.Where(x => unmappedColumns.Contains(x.ColumnName)));

        // Action populates each field in turn.
        return (itm, sqlDataRecord) =>
        {
            foreach(var mapping in mappings)
            {
                mapping.WriteField(itm, sqlDataRecord);
            }
        };
    }
}



//public class SqlDataRecordWriterScalar<T, TValue> where T: TValue
//{
//    private readonly IRecordSetValue<TValue> recordSetValue;
//    private readonly TableTypeColumnDefinition tableTypeColumnDefinition;

//    public SqlDataRecordWriterScalar(
//        IRecordSetValue<TValue> recordSetValue,
//        TableTypeColumnDefinition tableTypeColumnDefinition
//        )
//    {
//        this.recordSetValue = recordSetValue;
//        this.tableTypeColumnDefinition = tableTypeColumnDefinition;
//    }

//    public void WriteField(T item, MSS.SqlDataRecord sqlDataRecord)
//    {
//        if(item == null)
//            sqlDataRecord.SetDBNull(0);
//        else
//            recordSetValue.SetValue(sqlDataRecord,
//                0,
//                item);
//    }
//}
