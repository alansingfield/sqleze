using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.MockSqlClient;

public interface SqlParameterCollection : IList
{
    SqlParameter Add(SqlParameter value);
    void Remove(SqlParameter value);
}
