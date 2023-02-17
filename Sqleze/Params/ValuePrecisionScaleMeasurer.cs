using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params;

public interface IValuePrecisionScaleMeasurer<TValue>
{
    (byte precision, byte scale)? GetPrecisionScale(TValue? val);
}

public class ValuePrecisionScaleMeasurerDefault<TValue> : IValuePrecisionScaleMeasurer<TValue>
{
    public (byte precision, byte scale)? GetPrecisionScale(TValue? val) => null;
}


public class ValuePrecisionScaleMeasurerSqlDecimalNullable : IValuePrecisionScaleMeasurer<SqlDecimal?>
{
    public (byte precision, byte scale)? GetPrecisionScale(SqlDecimal? val)
    {
        if(val == null)
            return null;

        if(val.Value.IsNull)
            return null;

        return (precision: val.Value.Precision, scale: val.Value.Scale);
    }
}
public class ValuePrecisionScaleMeasurerSqlDecimal : IValuePrecisionScaleMeasurer<SqlDecimal>
{
    public (byte precision, byte scale)? GetPrecisionScale(SqlDecimal val)
    {
        if(val.IsNull)
            return null;

        return (precision: val.Precision, scale: val.Scale);
    }
}

public class ValuePrecisionScaleMeasurerDecimalNullable : IValuePrecisionScaleMeasurer<decimal?>
{
    public (byte precision, byte scale)? GetPrecisionScale(decimal? val)
    {
        if(val == null)
            return null;

        return (precision: val.Value.Precision(), scale: val.Value.Scale());
    }
}

public class ValuePrecisionScaleMeasurerDecimal : IValuePrecisionScaleMeasurer<decimal>
{
    public (byte precision, byte scale)? GetPrecisionScale(Decimal val)
    {
        return (precision: val.Precision(), scale: val.Scale());
    }
}