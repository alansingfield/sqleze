using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ConnectionStrings
{
    public interface IVerbatimConnectionStringProvider : IConnectionStringProvider { }

    public class VerbatimConnectionStringProvider : IVerbatimConnectionStringProvider
    {
        private readonly VerbatimConnectionOptions options;

        public VerbatimConnectionStringProvider(VerbatimConnectionOptions options)
        {
            this.options = options;
        }

        public string GetConnectionString()
        {
            return this.options.ConnectionString;
        }
    }
}
