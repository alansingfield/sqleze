using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Sqleze.Mock.MockSqlClient
{
    public interface SqlParameter
    {
        string ParameterName { get; set; }
        ParameterDirection Direction { get; set; }
        object? Value { get; set; }
        object? SqlValue { get; set; }
        SqlDbType SqlDbType { get; set; }
        string UdtTypeName { get; set; }
        string TypeName { get; set; }
        int Size { get; set; }
        byte Scale { get; set; }
        byte Precision { get; set; }
    }
}
