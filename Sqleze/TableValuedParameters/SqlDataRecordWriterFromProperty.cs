using Sqleze.Dynamics;
using Sqleze.Metadata;
using Sqleze.Readers;

namespace Sqleze.TableValuedParameters;

public interface ISqlDataRecordWriterFromProperty<T, TValue> : ISqlDataRecordWriterFromProperty<T>
{
}

public interface ISqlDataRecordWriterFromProperty<T>
{
    void WriteField(T item, MSS.SqlDataRecord sqlDataRecord);
}

public class SqlDataRecordWriterFromProperty<T, TValue> : ISqlDataRecordWriterFromProperty<T, TValue>
{
    private readonly IRecordSetValue<TValue> recordSetValue;
    private readonly TableTypeColumnDefinition tableTypeColumnDefinition;
    private readonly IResolvedPropertyInfo resolvedPropertyInfo;
    private readonly IDynamicPropertyCaller<T> dynamicPropertyCaller;

    public SqlDataRecordWriterFromProperty(
        IRecordSetValue<TValue> recordSetValue,
        TableTypeColumnDefinition tableTypeColumnDefinition,
        IResolvedPropertyInfo resolvedPropertyInfo,
        IDynamicPropertyCaller<T> dynamicPropertyCaller
        )
    {
        this.recordSetValue = recordSetValue;
        this.tableTypeColumnDefinition = tableTypeColumnDefinition;
        this.resolvedPropertyInfo = resolvedPropertyInfo;
        this.dynamicPropertyCaller = dynamicPropertyCaller;
    }

    public void WriteField(T item, MSS.SqlDataRecord sqlDataRecord)
    {
        var propertyInfo = resolvedPropertyInfo.PropertyInfo;
        
        var getter = dynamicPropertyCaller.CompilePropertyGetFunc(propertyInfo.Name);

        object? value = getter(item);

        if(value == null)
            sqlDataRecord.SetDBNull(tableTypeColumnDefinition.ColumnOrdinal);
        else
            recordSetValue.SetValue(sqlDataRecord,
                tableTypeColumnDefinition.ColumnOrdinal,
                value);
    }
}
