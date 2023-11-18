using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.MockSqlClient;
public interface SqlConnectionStringBuilder
{
    string ConnectionString { get; set; }
    string Password { get; set; }
}
