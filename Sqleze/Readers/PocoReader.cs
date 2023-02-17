//using Sqleze.Config;
//using Sqleze.Converters;
//using Sqleze.Dynamics;
//using Sqleze.Metadata;
//using Sqleze.Sqleze;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
//using Sqleze.NamingConventions;
//using Sqleze.Collations;
using System.Reflection;
using Sqleze.SqlClient;
using Sqleze;
using System.Runtime.CompilerServices;
using Sqleze.Util;
using Sqleze.Dynamics;
using System.Net.NetworkInformation;

namespace Sqleze.Readers;

public interface IPocoReader<T> : IReader<T> { }
/// <summary>
/// Enumerates through the returned ResultSet, returning each row as a new POCO
/// (Plain Old CLR Object)
/// </summary>
public class PocoReader<T> : IPocoReader<T>
{
    private readonly ICheckedAdoDataReader checkedAdoDataReader;
    private readonly IColumnPropertyMapper<T> columnPropertyMapper;
    private readonly IDynamicPropertyCaller<T> dynamicPropertyCaller;
    private readonly IBestMatchConstructor<T> bestMatchConstructor;

    public PocoReader(
        ICheckedAdoDataReader checkedAdoDataReader,
        IColumnPropertyMapper<T> columnPropertyMapper,
        IDynamicPropertyCaller<T> dynamicPropertyCaller,
        IBestMatchConstructor<T> bestMatchConstructor
        )
    {
        this.checkedAdoDataReader = checkedAdoDataReader;
        this.columnPropertyMapper = columnPropertyMapper;
        this.dynamicPropertyCaller = dynamicPropertyCaller;
        this.bestMatchConstructor = bestMatchConstructor;
    }

    public IEnumerable<T> Enumerate()
    {
        prepare(out var columnPropertyMappings, 
                out var propertySetAction, 
                out var values,
                out var consFunc);

        // Read row by row until rowset completed
        while(checkedAdoDataReader.Read())
        {
            yield return process(columnPropertyMappings, propertySetAction, values, consFunc);
        }
    }

    public async IAsyncEnumerable<T> EnumerateAsync(
        [EnumeratorCancellation]
        CancellationToken cancellationToken)
    {
        prepare(out var columnPropertyMappings,
                out var propertySetAction,
                out var values,
                out var consFunc);

        // Read row by row until rowset completed
        while(await checkedAdoDataReader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            yield return process(columnPropertyMappings, propertySetAction, values, consFunc);
        }
    }

    private void prepare(
        out IReadOnlyList<IColumnProperty<T>> columnPropertyMappings,
        out Action<T, object?[]> propertySetAction,
        out object?[] values,
        out Func<object?[], T> consFunc)
    {
        // Produce a mapping between SQL Server fields and .NET Properties
        columnPropertyMappings = columnPropertyMapper.MapColumnsToProperties();

        // Find the best constructor to use based on the .NET properties we want to write
        // to and the available constructors.
        consFunc = bestMatchConstructor.Find(columnPropertyMappings);

        // Compile a property setter lambda expression which will write to the properties
        // in order of PropertyOrdinal. Leave null gaps for InitOnly properties.
        string?[] propertyNamesInOrder = columnPropertyMappings
            .Select(x => x.PropertyConsOnly ? null : x.PropertyName)
            .ToArray();

        propertySetAction = dynamicPropertyCaller.CompilePropertySetAction(propertyNamesInOrder);

        // Set up a new array to hold the values; we re-use the same array for each row.
        values = new object[columnPropertyMappings.Count];
    }


    private T process(
        IReadOnlyList<IColumnProperty<T>> columnPropertyMappings, 
        Action<T, object?[]> propertySetAction, 
        object?[] values,
        Func<object?[], T> consFunc)
    {
        // Write to the values array
        foreach(var (cpm, idx) in columnPropertyMappings.SelectIndexed())
        {
            values[idx] = cpm.GetValue();
        }

        // Construct the object supplying any constructor params.
        T entity = consFunc(values);

        // Set the relevant properties on the entity object using the values array.
        propertySetAction(entity, values);

        return entity;
    }

}
