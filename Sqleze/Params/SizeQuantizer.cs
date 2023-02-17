using Sqleze.Converters.SqlTypes;
using Sqleze;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params;

public interface ISizeQuantizer<TValue>
{
    int? QuantizeSize(SqlDbType sqlDbType);
}

public class SizeQuantizer<TValue> : ISizeQuantizer<TValue> 
{
    private readonly IValueSizeMeasurer<TValue> valueSizeMeasurer;
    private readonly ISqlezeParameter<TValue> sqlezeParameter;
    private readonly Lazy<NVarCharSizeQuantizeOptions> nVarCharSizeQuantizeOptionsLazy;
    private readonly Lazy<VarCharSizeQuantizeOptions> varCharSizeQuantizeOptionsLazy;
    private readonly Lazy<VarBinarySizeQuantizeOptions> varBinarySizeQuantizeOptionsLazy;

    public SizeQuantizer(
        IValueSizeMeasurer<TValue> valueSizeMeasurer,
        ISqlezeParameter<TValue> sqlezeParameter,
        Lazy<NVarCharSizeQuantizeOptions> nVarCharSizeQuantizeOptionsLazy,
        Lazy<VarCharSizeQuantizeOptions> varCharSizeQuantizeOptionsLazy,
        Lazy<VarBinarySizeQuantizeOptions> varBinarySizeQuantizeOptionsLazy
        )
    {
        this.valueSizeMeasurer = valueSizeMeasurer;
        this.sqlezeParameter = sqlezeParameter;
        this.nVarCharSizeQuantizeOptionsLazy = nVarCharSizeQuantizeOptionsLazy;
        this.varCharSizeQuantizeOptionsLazy = varCharSizeQuantizeOptionsLazy;
        this.varBinarySizeQuantizeOptionsLazy = varBinarySizeQuantizeOptionsLazy;
    }

    public int? QuantizeSize(SqlDbType sqlDbType)
    {
        int? measuredSize = valueSizeMeasurer.GetSize(sqlezeParameter.Value);

        if(measuredSize == null)
            return null;

        // Resolve the correct size options based on nvarchar, varchar or varbinary.
        ISizeQuantizeOptions? sizeOptions = sqlDbType switch
        {
            SqlDbType.NVarChar  => nVarCharSizeQuantizeOptionsLazy.Value,
            SqlDbType.VarChar   => varCharSizeQuantizeOptionsLazy.Value,
            SqlDbType.VarBinary => varBinarySizeQuantizeOptionsLazy.Value,
            _ => null
        };

        if(sizeOptions?.Sizes == null) return null;

        // If asked to produce parameter value bigger than the max fixed size
        // nvarchar(4000) or varchar(8000) then use nvarchar(max) or varchar(max).
        if(sqlDbType.HasVarMaxSize() && measuredSize > sqlDbType.MaxFixedSize())
            return -1;

        // If specified, we can use a preferred size e.g. prefer varchar(10), varchar(50) or
        // whichever fits the data supplied. The sizes are expected to be in
        // ascending order.
        foreach(var preferredSize in sizeOptions.Sizes)
        {
            // Will the measured length of data fit?
            if(measuredSize <= preferredSize) 
                return preferredSize;
        }

        // Last attempt - if we can fit within the maximum length for the type use that.
        if(measuredSize <= sqlDbType.MaxFixedSize())
            return sqlDbType.MaxFixedSize();

        // Here, this must be a type like char(s) or nchar(s) where we don't have
        // a (max) option but the text is longer than the maximum allowed size.
        return null;
    }
}
