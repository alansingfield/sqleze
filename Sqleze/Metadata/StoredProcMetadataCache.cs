using Sqleze.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Metadata
{
    // TODO - convert this to a dictionary
    public interface IStoredProcMetadataCache
    {
        IReadOnlyList<StoredProcParamDefinition> GetStoredProcParams(string storedProcName);
        Task<IReadOnlyList<StoredProcParamDefinition>> GetStoredProcParamsAsync(string storedProcName, CancellationToken cancellationToken = default);
    }

    // TODO - add a higher level to index these by connection string
    public class StoredProcMetadataCache : IStoredProcMetadataCache
    {
        private readonly IStoredProcMetadataQuery storedProcMetadataQuery;

        private ConcurrentCache<string, IReadOnlyList<StoredProcParamDefinition>> cache = new();
        private ConcurrentAsyncCache<string, IReadOnlyList<StoredProcParamDefinition>> asyncCache = new();

        public StoredProcMetadataCache(IStoredProcMetadataQuery storedProcMetadataQuery)
        {
            this.storedProcMetadataQuery = storedProcMetadataQuery;
        }

        public IReadOnlyList<StoredProcParamDefinition> GetStoredProcParams(string storedProcName)
        {
            return cache.Get(storedProcName,
                x => storedProcMetadataQuery.Query(x));
        }

        public async Task<IReadOnlyList<StoredProcParamDefinition>> GetStoredProcParamsAsync(
            string storedProcName,
            CancellationToken cancellationToken = default)
        {
            return await asyncCache.GetAsync(
                storedProcName, x => storedProcMetadataQuery.QueryAsync(x, cancellationToken)
                ).ConfigureAwait(false);
        }
    }
}
