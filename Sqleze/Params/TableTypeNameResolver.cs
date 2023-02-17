using Sqleze;
using Sqleze.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params
{
    public interface ITableTypeNameResolver<T>
    {
        string ResolveTableTypeName();
        Task<string> ResolveTableTypeNameAsync(CancellationToken cancellationToken = default);
    }

    public class TableTypeNameResolver<T> : ITableTypeNameResolver<T>
    {
        private readonly ISqlezeParameter<T> sqlezeParameter;
        private readonly IStoredProcParamDefinitionProvider<T> storedProcParamDefinitionProvider;

        public TableTypeNameResolver(ISqlezeParameter<T> sqlezeParameter,
            IStoredProcParamDefinitionProvider<T> storedProcParamDefinitionProvider)
        {
            this.sqlezeParameter = sqlezeParameter;
            this.storedProcParamDefinitionProvider = storedProcParamDefinitionProvider;
        }

        public string ResolveTableTypeName()
        {
            var paramDef = storedProcParamDefinitionProvider.GetDefinition();

            return resolve(paramDef);
        }

        public async Task<string> ResolveTableTypeNameAsync(CancellationToken cancellationToken = default)
        {
            var paramDef = await storedProcParamDefinitionProvider
                .GetDefinitionAsync(cancellationToken)
                .ConfigureAwait(false);

            return resolve(paramDef);
        }

        private string resolve(StoredProcParamDefinition? paramDef)
        {
            // If we are using a stored procedure then we can get the table-valued parameter type name
            // from the stored proc parameter list.
            if(paramDef != null && !String.IsNullOrEmpty(paramDef.ParameterType))
            {
                if(!paramDef.IsTableType)
                    throw new Exception($"Table-valued parameter {paramDef.ParameterName} cannot accept a value of type {sqlezeParameter.ValueType}");

                return paramDef.ParameterType;
            }

            // Otherwise we fall back to whatever was specified by the caller. This may have been
            // defaulted by RegisterKnownTableType()
            return sqlezeParameter.SqlTypeName;
        }
    }
}
