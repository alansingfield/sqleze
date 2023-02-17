using Sqleze.DryIoc;
using Sqleze;
using Sqleze.SqlClient;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Readers
{
    public interface IColumnPropertyResolver<TElement>
    {
        IColumnProperty<TElement> ResolveSqlTypedColumnProperty();
    }

    public interface IColumnPropertyResolver<TElement, TValue> : IColumnPropertyResolver<TElement> { }

    public class ColumnPropertyResolver<TElement, TValue> : IColumnPropertyResolver<TElement, TValue>
    {
        private readonly DataReaderFieldInfo dataReaderFieldInfo;
        private readonly IGenericResolver<IColumnProperty<TElement, TValue>> genericResolver;
        private readonly IResolvedPropertyInfo resolvedPropertyInfo;
        private readonly IKnownSqlDbTypeFinder knownSqlDbTypeFinder;

        public ColumnPropertyResolver(
            DataReaderFieldInfo dataReaderFieldInfo,
            IGenericResolver<IColumnProperty<TElement, TValue>> genericResolver,
            IResolvedPropertyInfo resolvedPropertyInfo,
            IKnownSqlDbTypeFinder knownSqlDbTypeFinder)
        {
            this.dataReaderFieldInfo = dataReaderFieldInfo;
            this.genericResolver = genericResolver;
            this.resolvedPropertyInfo = resolvedPropertyInfo;
            this.knownSqlDbTypeFinder = knownSqlDbTypeFinder;
        }

        public IColumnProperty<TElement> ResolveSqlTypedColumnProperty()
        {
            // This will be something like nvarchar, datetime2, geometry, etc...
            // Convert to a type which implements IKnownSqlDbType
            Type knownSqlDbType = knownSqlDbTypeFinder.FindKnownSqlDbType(
                dataReaderFieldInfo.SqlDataTypeName);

            // Resolve ColumnProperty<TElement, TValue, TKnownSqlDbType> from the container
            // giving us a result of type IColumnProperty<TElement, TValue>
            // which is assignable to IColumnProperty<TElement>
            return genericResolver.Resolve(knownSqlDbType, new object[]
            {
                resolvedPropertyInfo,
                dataReaderFieldInfo
            });
        }
    }
}
