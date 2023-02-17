using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.ValueGetters
{
    public interface IKnownSqlDbTypeFinder
    {
        Type FindKnownSqlDbType(string sqlDataTypeName);
    }

    public class KnownSqlDbTypeFinder : IKnownSqlDbTypeFinder
    {
        private readonly IKnownSqlDbTypeResolver[] knownSqlDbTypeResolvers;

        public KnownSqlDbTypeFinder(IKnownSqlDbTypeResolver[] knownSqlDbTypeResolvers)
        {
            this.knownSqlDbTypeResolvers = knownSqlDbTypeResolvers;
        }

        public Type FindKnownSqlDbType(string sqlDataTypeName)
        {
            // To support additional types like geometry and hierarchyid, we can have
            // more than one IKnownSqlDbTypeResolver in the container.
            // This will convert text "nvarchar" to typeof(IKnownSqlDbTypeNVarChar)
            return knownSqlDbTypeResolvers
                .Select(x => x.ResolveKnownSqlDbType(sqlDataTypeName))
                .Where(x => x != null)
                .FirstOrDefault()
                ?? typeof(IKnownSqlDbType);     // Fallback if not known.
        }
    }
}
