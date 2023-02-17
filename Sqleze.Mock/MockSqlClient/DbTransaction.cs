using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Mock.MockSqlClient;
public interface DbTransaction : IDisposable, IAsyncDisposable { }
