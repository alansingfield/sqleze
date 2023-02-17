using Sqleze.DryIoc;
using Sqleze;
using Sqleze.SqlClient;
using Sqleze.Util;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Readers
{

    public interface IColumnProperty<TElement>
    {
        string ColumnName { get; }
        string PropertyName { get; }
        int ColumnOrdinal { get; }
        bool PropertyConsOnly { get; }

        object? GetValue();
    }

    public interface IColumnProperty<TElement, TValue> 
        : IColumnProperty<TElement>
    {
    }
    
    public interface IColumnProperty<TElement, TValue, TSqlDbType>
        : IColumnProperty<TElement, TValue>
        where TSqlDbType : IKnownSqlDbType
    {
    }

    public class ColumnProperty<TElement, TValue, TSqlDbType> 
        : IColumnProperty<TElement, TValue, TSqlDbType>
        where TSqlDbType : IKnownSqlDbType
    {
        private readonly IResolvedPropertyInfo resolvedPropertyInfo;
        private readonly DataReaderFieldInfo dataReaderFieldInfo;
        private readonly IReaderGetValueResolver<TValue, TSqlDbType> readerGetValueResolver;
        private readonly IAdoDataReader adoDataReader;

        public ColumnProperty(
            IResolvedPropertyInfo resolvedPropertyInfo,
            DataReaderFieldInfo dataReaderFieldInfo,
            IReaderGetValueResolver<TValue, TSqlDbType> readerGetValueResolver,
            IAdoDataReader adoDataReader)
        {
            this.resolvedPropertyInfo = resolvedPropertyInfo;
            this.dataReaderFieldInfo = dataReaderFieldInfo;
            this.readerGetValueResolver = readerGetValueResolver;
            this.adoDataReader = adoDataReader;
        }

        public string ColumnName => dataReaderFieldInfo.ColumnName;
        public string PropertyName => resolvedPropertyInfo.PropertyInfo.Name;
        public int ColumnOrdinal => dataReaderFieldInfo.ColumnOrdinal;
        public bool PropertyConsOnly => !resolvedPropertyInfo.PropertyInfo.CanWrite;

        public TValue? GetValue()
        {
            return readerGetValueResolver
                .GetReaderGetValue()
                .GetValue(adoDataReader.SqlDataReader, dataReaderFieldInfo.ColumnOrdinal);
        }

        object? IColumnProperty<TElement>.GetValue()
        {
            return this.GetValue();
        }
    }

    public interface IResolvedPropertyInfo
    {
        PropertyInfo PropertyInfo { get; }
    }

    public class ResolvedPropertyInfo : IResolvedPropertyInfo
    {
        public ResolvedPropertyInfo(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }

        public PropertyInfo PropertyInfo { get; }
    }
}
