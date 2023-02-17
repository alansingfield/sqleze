using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.MockSqlClient
{
    public delegate void SqlInfoMessageEventHandler(object sender, SqlInfoMessageEventArgs e);

    public class SqlInfoMessageEventArgs : EventArgs
    {
        public List<SqlError> Errors { get; set; } = new List<SqlError>();

        public string? Message { get; set; }
        public string? Source { get; set; }
    }

    public sealed class SqlError
    {
        public byte Class { get; set; }
        public int LineNumber { get; set; }
        public string? Message { get; set; }
        public int Number { get; set; }
        public string? Procedure { get; set; }
        public string? Server { get; set; }
        public string? Source { get; set; }
        public byte State { get; set; }
    }
}
