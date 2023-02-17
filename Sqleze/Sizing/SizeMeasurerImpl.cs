using Sqleze;
using Sqleze.Params;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Sizing
{
    public class SizeMeasurerString : ISizeMeasurer<string?>
    {
        private readonly ISqlezeParameter<string?> sqlezeParameter;

        public SizeMeasurerString(
            ISqlezeParameter<string?> sqlezeParameter
            )
        {
            this.sqlezeParameter = sqlezeParameter;
        }

        public int GetSize()
        {
            return sqlezeParameter.Value?.Length ?? 0;
        }
    }

    public class SizeMeasurerSqlString : ISizeMeasurer<SqlString?>
    {
        private readonly ISqlezeParameter<SqlString?> sqlezeParameter;

        public SizeMeasurerSqlString(
            ISqlezeParameter<SqlString?> sqlezeParameter
            )
        {
            this.sqlezeParameter = sqlezeParameter;
        }

        public int GetSize()
        {
            return sqlezeParameter.Value?.Value?.Length ?? 0;
        }

    }

    public class SizeMeasurerSqlChars : ISizeMeasurer<SqlChars?>
    {
        private readonly ISqlezeParameter<SqlChars?> sqlezeParameter;

        public SizeMeasurerSqlChars(
            ISqlezeParameter<SqlChars?> sqlezeParameter
            )
        {
            this.sqlezeParameter = sqlezeParameter;
        }

        public int GetSize()
        {
            return sqlezeParameter.Value?.Value?.Length ?? 0;
        }

    }

    public class SizeMeasurerSqlBinary : ISizeMeasurer<SqlBinary>
    {
        private readonly ISqlezeParameter<SqlBinary> sqlezeParameter;

        public SizeMeasurerSqlBinary(
            ISqlezeParameter<SqlBinary> sqlezeParameter
            )
        {
            this.sqlezeParameter = sqlezeParameter;
        }

        public int GetSize()
        {
            return sqlezeParameter.Value.Value.Length;
        }

    }

    public class SizeMeasurerSqlBinaryNullable : ISizeMeasurer<SqlBinary?>
    {
        private readonly ISqlezeParameter<SqlBinary?> sqlezeParameter;

        public SizeMeasurerSqlBinaryNullable(
            ISqlezeParameter<SqlBinary?> sqlezeParameter
            )
        {
            this.sqlezeParameter = sqlezeParameter;
        }

        public int GetSize()
        {
            return sqlezeParameter.Value?.Value.Length ?? 0;
        }

    }


    public class SizeMeasurerSqlBytes : ISizeMeasurer<SqlBytes?>
    {
        private readonly ISqlezeParameter<SqlBytes?> sqlezeParameter;

        public SizeMeasurerSqlBytes(
            ISqlezeParameter<SqlBytes?> sqlezeParameter
            )
        {
            this.sqlezeParameter = sqlezeParameter;
        }

        public int GetSize()
        {
            return sqlezeParameter.Value?.Value.Length ?? 0;
        }
    }


    public class SizeMeasurerByteArray : ISizeMeasurer<byte[]?>
    {
        private readonly ISqlezeParameter<byte[]?> sqlezeParameter;

        public SizeMeasurerByteArray(
            ISqlezeParameter<byte[]?> sqlezeParameter
            )
        {
            this.sqlezeParameter = sqlezeParameter;
        }

        public int GetSize()
        {
            return sqlezeParameter.Value?.Length ?? 0;
        }

    }
}
