using Sqleze.DryIoc;
using Sqleze;
using Sqleze.SqlClient;
using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sqleze.Readers
{
    public interface IColumnPropertyMapper<T>
    {
        IReadOnlyList<IColumnProperty<T>> MapColumnsToProperties();
    }

    public class ColumnPropertyMapper<T> : IColumnPropertyMapper<T>
    {
        private readonly IDataReaderFieldNames dataReaderFieldNames;
        private readonly INamingConvention namingConvention;
        private readonly ICollation collation;
        private readonly IMultiResolver<IColumnPropertyResolver<T>, T> columnPropertyResolver;
        private readonly IDuplicateColumnsPolicy duplicateColumnsPolicy;
        private readonly IUnmappedPropertiesPolicy unmappedPropertiesPolicy;
        private readonly IUnmappedColumnsPolicy unmappedColumnsPolicy;

        public ColumnPropertyMapper(
            IDataReaderFieldNames dataReaderFieldNames,
            INamingConvention namingConvention,
            ICollation collation,
            IMultiResolver<IColumnPropertyResolver<T>, T> columnPropertyResolver,
            IDuplicateColumnsPolicy duplicateColumnsPolicy,
            IUnmappedPropertiesPolicy unmappedPropertiesPolicy,
            IUnmappedColumnsPolicy unmappedColumnsPolicy)
        {
            this.dataReaderFieldNames = dataReaderFieldNames;
            this.namingConvention = namingConvention;
            this.collation = collation;
            this.columnPropertyResolver = columnPropertyResolver;
            this.duplicateColumnsPolicy = duplicateColumnsPolicy;
            this.unmappedPropertiesPolicy = unmappedPropertiesPolicy;
            this.unmappedColumnsPolicy = unmappedColumnsPolicy;
        }

        public IReadOnlyList<IColumnProperty<T>> MapColumnsToProperties()
        {
            // Get array of field names from data reader including ordinals and the field data type.
            var fieldInfos = dataReaderFieldNames.GetFieldInfos();

            // There could be duplicate field names so we need a dictionary keyed by
            // name with each value being a list.
            var fieldInfoDict = fieldInfos
                .GroupBy(x => x.ColumnName, x => x, collation.Comparer)
                .ToDictionary(x => x.Key, x => (IReadOnlyCollection<DataReaderFieldInfo>) x.ToList().AsReadOnly(),
                    collation.Comparer);

            // Tick off each field name as we map it; the remainder will be the unmapped fields.
            var unmappedColumns = new HashSet<string>(fieldInfoDict.Keys, collation.Comparer);

            // Keep track of duplicate columns and unmapped properties
            var duplicateColumns = new List<(DataReaderFieldInfo, PropertyInfo)>();
            var unmappedProps = new List<(PropertyInfo, string)>();

            // Resolve a scope for each property in our object which has a matching column.
            var columnProperties = columnPropertyResolver.ResolvePerProperty(
                prop =>
                {
                    // Compute the SQL column name based on the C# property name.
                    string columnName = namingConvention.DotNetToSql(prop.Name);

                    // Look up the column name, do we get a match?
                    if(!fieldInfoDict.TryGetValue(columnName, out var fieldInfo))
                    {
                        unmappedProps.Add((prop, columnName));
                    }
                    else
                    {
                        // We've mapped this column, remove it from the unmapped set.
                        unmappedColumns.Remove(columnName);

                        // It was a match, but is is unambiguous? Keep track of these but
                        // keep going, we raise an error later if needed.
                        if(fieldInfo.Count > 1)
                        {
                            duplicateColumns.AddRange(fieldInfo.Select(x => (x, prop)));
                        }

                        // If we get to here, we want to resolve a scope for the property/field
                        // binding. Pass a holder for the PropertyInfo, and the first FieldInfo
                        // into that new scope.
                        return (true, new object[]
                        {
                            new ResolvedPropertyInfo(prop),
                            fieldInfo.First()
                        });
                    }

                    // No match, don't want it
                    return (false, null);
                })
                .Select(x => x.ResolveSqlTypedColumnProperty())
                .ToList();

            // If the query returns two columns with the same name, flag this as a problem.
            // Note that we don't care about duplicates for items which don't match to our
            // properties. This can often occur with "(No column name)" columns.
            // This may not choose to raise an error and if so, we get the leftmost column
            // bound as a fallback.
            duplicateColumnsPolicy.Handle(duplicateColumns);

            // Did we get a column for every property in our object?
            unmappedPropertiesPolicy.Handle(unmappedProps);

            // Did we map every column we received?
            unmappedColumnsPolicy.Handle(
                fieldInfos.Where(x => unmappedColumns.Contains(x.ColumnName)));

            return columnProperties;
        }
    }
}
