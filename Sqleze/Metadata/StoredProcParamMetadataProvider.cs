using Sqleze;
using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Metadata
{
    public interface IStoredProcParamDefinitionProvider<T>
    {
        StoredProcParamDefinition? GetDefinition();
        Task<StoredProcParamDefinition?> GetDefinitionAsync(CancellationToken cancellationToken = default);
    }

    public class StoredProcParamDefinitionProvider<T> : IStoredProcParamDefinitionProvider<T>
    {
        private readonly CommandCreateOptions commandCreateOptions;
        private readonly ISqlezeParameter<T> sqlezeParameter;
        private readonly IStoredProcMetadataCache storedProcMetadataCache;
        private readonly ICollation collation;

        public StoredProcParamDefinitionProvider(
            ISqlezeParameter<T> sqlezeParameter,
            IStoredProcMetadataCache storedProcMetadataCache,
            ICollation collation,
            CommandCreateOptions commandCreateOptions)
        {
            this.sqlezeParameter = sqlezeParameter;
            this.storedProcMetadataCache = storedProcMetadataCache;
            this.collation = collation;

            this.commandCreateOptions = commandCreateOptions;
        }

        public StoredProcParamDefinition? GetDefinition()
        {
            if(!commandCreateOptions.IsStoredProc)
                return null;

            var paramDefs = storedProcMetadataCache.GetStoredProcParams(commandCreateOptions.Sql);
            return findParameterByName(paramDefs);
        }

        public async Task<StoredProcParamDefinition?> GetDefinitionAsync(CancellationToken cancellationToken = default)
        {
            if(!commandCreateOptions.IsStoredProc)
                return null;

            var paramDefs = await storedProcMetadataCache
                .GetStoredProcParamsAsync(commandCreateOptions.Sql, cancellationToken)
                .ConfigureAwait(false);

            return findParameterByName(paramDefs);
        }

        private StoredProcParamDefinition? findParameterByName(IReadOnlyList<StoredProcParamDefinition> paramDefs)
        {
            // Both parameterName and adoName should normally begin with @
            // except for the return parameter which is named with an empty string.
            return paramDefs.SingleOrDefault(
                x => collation.Comparer.Compare(
                    x.ParameterName, sqlezeParameter.AdoName) == 0);

            // TODO - throw exception if parameter not found???
        }
    }
}
