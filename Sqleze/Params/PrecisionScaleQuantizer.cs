using Sqleze;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params;

public interface IPrecisionScaleQuantizer<TValue>
{
    (byte precision, byte scale)? QuantizePrecisionScale();
}

public class PrecisionScaleQuantizer<TValue> 
    : IPrecisionScaleQuantizer<TValue> 
{
    private readonly IValuePrecisionScaleMeasurer<TValue> valuePrecisionScaleMeasurer;
    private readonly ISqlezeParameter<TValue> sqlezeParameter;
    private readonly PrecisionScaleQuantizeOptions precisionScaleQuantizeOptions;

    public PrecisionScaleQuantizer(
        IValuePrecisionScaleMeasurer<TValue> valuePrecisionScaleMeasurer,
        ISqlezeParameter<TValue> sqlezeParameter,
        PrecisionScaleQuantizeOptions precisionScaleQuantizeOptions
        )
    {
        this.valuePrecisionScaleMeasurer = valuePrecisionScaleMeasurer;
        this.sqlezeParameter = sqlezeParameter;
        this.precisionScaleQuantizeOptions = precisionScaleQuantizeOptions;
    }

    public (byte precision, byte scale)? QuantizePrecisionScale()
    {
        TValue? value = sqlezeParameter.Value;

        var measured = valuePrecisionScaleMeasurer.GetPrecisionScale(value);

        if(measured == null)
            return null;

        // If specified, use a preferred decimal precision and scale, if it fits.
        // e.g. 1.23 will fit in a numeric(10,3)
        foreach(var preferred in this.precisionScaleQuantizeOptions.PrecisionScales)
        {
            if(measured.Value.precision <= preferred.precision 
                && measured.Value.scale <= preferred.scale)
                return preferred;
        }

        // Fallback on what the ADO provider will do.
        return null;
    }
}
