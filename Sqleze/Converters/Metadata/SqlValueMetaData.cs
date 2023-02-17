using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Converters.Metadata
{
    public class SqlValueMetaData
    {
        public string ColumnName { get; set; } = "";
        public int ColumnOrdinal { get; set; }
        public Type? ProviderSpecificDataType { get; set; }
        public string DataTypeName { get; set; } = "";
        public SqlDbType? SqlDbType { get; set; }
        public bool IsAnsi { get; set; }
        public int? Size { get; set; }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }
        public bool Nullable { get; set; }
    }
}
