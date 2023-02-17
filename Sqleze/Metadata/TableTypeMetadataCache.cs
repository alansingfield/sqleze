using Sqleze.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Metadata
{
    public interface ITableTypeMetadataCache
    {
        IReadOnlyList<TableTypeColumnDefinition> GetTableTypeColumns(string tableTypeName);
        Task<IReadOnlyList<TableTypeColumnDefinition>> GetTableTypeColumnsAsync(string tableTypeName, CancellationToken cancellationToken = default);
    }

    // TODO - add a higher level to index these by connection string
    public class TableTypeMetadataCache : ITableTypeMetadataCache
    {
        private readonly ITableTypeMetadataQuery tableTypeMetadataQuery;

        private ConcurrentCache<string, IReadOnlyList<TableTypeColumnDefinition>> cache = new();
        private ConcurrentAsyncCache<string, IReadOnlyList<TableTypeColumnDefinition>> asyncCache = new();

        public TableTypeMetadataCache(ITableTypeMetadataQuery tableTypeMetadataQuery)
        {
            this.tableTypeMetadataQuery = tableTypeMetadataQuery;
        }

        public IReadOnlyList<TableTypeColumnDefinition> GetTableTypeColumns(string tableTypeName)
        {
            return cache.Get(tableTypeName,
                x => tableTypeMetadataQuery.Query(x));
        }

        public async Task<IReadOnlyList<TableTypeColumnDefinition>> GetTableTypeColumnsAsync(
            string tableTypeName,
            CancellationToken cancellationToken = default)
        {
            return await asyncCache.GetAsync(
                tableTypeName, x => tableTypeMetadataQuery.QueryAsync(x, cancellationToken))
                .ConfigureAwait(false);
        }
    }
}
