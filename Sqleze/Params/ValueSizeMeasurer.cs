using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params
{
    public interface IValueSizeMeasurer<TValue>
    {
        int? GetSize(TValue? val);
    }

    public class ValueSizeMeasurerDefault<TValue> : IValueSizeMeasurer<TValue>
    {
        public int? GetSize(TValue? val) => null;
    }

    public class ValueSizeMeasurerSqlStringNullable : IValueSizeMeasurer<SqlString?>
    {
        public int? GetSize(SqlString? val) =>
            val == null ? null
                        : val.Value.IsNull ? null
                        : val.Value.Value.Length;
    }
    public class ValueSizeMeasurerSqlString : IValueSizeMeasurer<SqlString>
    {
        public int? GetSize(SqlString val) => val.IsNull ? null : val.Value.Length;
    }

    public class ValueSizeMeasurerSqlBinaryNullable : IValueSizeMeasurer<SqlBinary?>
    {
        public int? GetSize(SqlBinary? val) =>
            val == null ? null
                        : val.Value.IsNull ? null
                        : val.Value.Value.Length;
    }

    public class ValueSizeMeasurerSqlBinary : IValueSizeMeasurer<SqlBinary>
    {
        public int? GetSize(SqlBinary val) => val.IsNull ? null : val.Value.Length;
    }


    public class ValueSizeMeasurerSqlBytes : IValueSizeMeasurer<SqlBytes?>
    {
        public int? GetSize(SqlBytes? val) => val == null ? null
            : val.Value.Length;
    }

    public class ValueSizeMeasurerSqlChars : IValueSizeMeasurer<SqlChars?>
    {

        public int? GetSize(SqlChars? val) => val == null ? null
            : val.Value.Length;
    }

    public class ValueSizeMeasurerStringNullable : IValueSizeMeasurer<string?>
    {
        public int? GetSize(string? val) => val?.Length;
    }

    public class ValueSizeMeasurerByteArrayNullable : IValueSizeMeasurer<byte[]?>
    {
        public int? GetSize(byte[]? val) => val?.Length;
    }
}


