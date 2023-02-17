using Sqleze.Converters.SqlTypes;
using Sqleze.DryIoc;
using Sqleze;
using Sqleze.Metadata;
using Sqleze.Options;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params
{
    public record ScalarParameterSpec
    (
        SqlDbType SqlDbType,
        int? Size,
        byte? Precision,
        byte? Scale);

    public interface IParameterSpecResolver<T>
    {
        ScalarParameterSpec Resolve();
        Task<ScalarParameterSpec> ResolveAsync(CancellationToken cancellationToken = default);
    }

    public class ParameterSpecResolver<T> : IParameterSpecResolver<T>
    {
        private readonly ISqlezeParameter<T> sqlezeParameter;
        private readonly CommandCreateOptions commandCreateOptions;
        private readonly IStoredProcParamDefinitionProvider<T> storedProcParamDefinitionProvider;
        private readonly IKnownSqlDbTypeFinder knownSqlDbTypeFinder;
        private readonly ISizeQuantizer<T> sizeQuantizer;
        private readonly IPrecisionScaleQuantizer<T> precisionScaleQuantizer;

        public ParameterSpecResolver(
            ISqlezeParameter<T> sqlezeParameter,
            CommandCreateOptions commandCreateOptions,
            IStoredProcParamDefinitionProvider<T> storedProcParamDefinitionProvider,
            IKnownSqlDbTypeFinder knownSqlDbTypeFinder,
            ISizeQuantizer<T> sizeQuantizer,
            IPrecisionScaleQuantizer<T> precisionScaleQuantizer)
        {
            this.sqlezeParameter = sqlezeParameter;
            this.commandCreateOptions = commandCreateOptions;
            this.storedProcParamDefinitionProvider = storedProcParamDefinitionProvider;
            this.knownSqlDbTypeFinder = knownSqlDbTypeFinder;
            this.sizeQuantizer = sizeQuantizer;
            this.precisionScaleQuantizer = precisionScaleQuantizer;
        }

        public ScalarParameterSpec Resolve()
        {
            if(commandCreateOptions.IsStoredProc)
            {
                var paramDef = storedProcParamDefinitionProvider.GetDefinition();

                return resolveInternal(paramDef);
            }
            else
            {
                return resolveAdhoc();
            }
        }

        public async Task<ScalarParameterSpec> ResolveAsync(CancellationToken cancellationToken = default)
        {
            if(commandCreateOptions.IsStoredProc)
            {
                var paramDef = await storedProcParamDefinitionProvider
                    .GetDefinitionAsync(cancellationToken)
                    .ConfigureAwait(false);

                return resolveInternal(paramDef);
            }
            else
            {
                return resolveAdhoc();
            }
        }

        private ScalarParameterSpec resolveInternal(StoredProcParamDefinition? paramDef)
        {
            if(paramDef != null && paramDef.IsTableType)
                throw new Exception("Scalar value cannot be passed to a table-valued parameter");

            // If parameter definition not found, fallback to calculating size from value passed.
            if(paramDef == null)
                return resolveAdhoc();

            // Populate length / scale / precision from stored proc parameter definition.

            string sqlTypeName = paramDef.ParameterType ?? sqlezeParameter.SqlTypeName;

            // Given a SqlTypeName like "nvarchar", set to the correct Microsoft enum.
            var sqlDbType = SqlDbTypeConverter.ToSqlDbTypeKnown(sqlTypeName);

            // Size could be -1 meaning MAX, 0 meaning unknown, or any other value.
            if(sqlDbType.HasSize() && paramDef.Length != 0)
                return new ScalarParameterSpec(sqlDbType, paramDef.Length, null, null);
            
            if(sqlDbType.HasPrecision() && paramDef.Precision > 0)
                return new ScalarParameterSpec(sqlDbType, null, paramDef.Precision, paramDef.Scale);
            
            if(sqlDbType.HasScale() && paramDef.Scale > 0)
                return new ScalarParameterSpec(sqlDbType, null, null, paramDef.Scale);

            return new ScalarParameterSpec(sqlDbType, null, null, null);
        }

        private ScalarParameterSpec resolveAdhoc()
        {
            var sqlDbType = SqlDbTypeConverter.ToSqlDbTypeKnown(sqlezeParameter.SqlTypeName);

            if(sqlDbType.HasSize())
            {
                // Was the size specified by the caller? Use that.
                if(sqlezeParameter.Length != 0)
                    return new ScalarParameterSpec(sqlDbType, sqlezeParameter.Length, null, null);

                // Is there an output but we don't know what size to use? Need to use the max
                // size possible.
                if(sqlezeParameter.OutputAction != null)
                {
                    // Can we use varchar(max), nvarchar(max), varbinary(max) ?
                    int? size = sqlDbType.HasVarMaxSize() ? -1 : sqlDbType.MaxFixedSize();

                    return new ScalarParameterSpec(sqlDbType, size, null, null);
                }

                int? quantizedSize = sizeQuantizer.QuantizeSize(sqlDbType);

                // Create a parameter spec like nvarchar(200).
                return new ScalarParameterSpec(sqlDbType, quantizedSize, null, null);
            }
            
            // For decimal(p,s) we can specify the precision and scale.
            if(sqlDbType.HasPrecision() && sqlDbType.HasScale())
            {
                // Was precision/scale specified by the caller? Use that.
                if(sqlezeParameter.Precision > 0)
                    return new ScalarParameterSpec(sqlDbType, null, sqlezeParameter.Precision, sqlezeParameter.Scale);

                if(sqlezeParameter.OutputAction != null)
                {
                    throw new Exception($"Decimal output parameter {sqlezeParameter.AdoName} must have precision and scale specified. Use .AsDecimal(p,s)");
                }

                // Expand the precision/scale to one of the preferred values.
                // e.g. numeric(10,1) becomes numeric(18,2)
                var quantizedPrecisionScale = precisionScaleQuantizer.QuantizePrecisionScale();

                return new ScalarParameterSpec(sqlDbType, 
                    null, 
                    quantizedPrecisionScale?.precision, 
                    quantizedPrecisionScale?.scale);
            }
            else if(sqlDbType.HasScale())
            {
                // For datetime2(n), time(n)
                return new ScalarParameterSpec(sqlDbType, null, null, sqlezeParameter.Scale);
            }

            // Fallback position is that we just know the sqlDbType
            return new ScalarParameterSpec(sqlDbType, null, null, null);
        }
    }
}