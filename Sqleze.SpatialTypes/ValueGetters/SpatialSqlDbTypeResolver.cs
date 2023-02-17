using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SpatialTypes.ValueGetters
{
    public class SpatialSqlDbTypeResolver : IKnownSqlDbTypeResolver
    {
        public Type? ResolveKnownSqlDbType(string sqlTypeName)
        {
            return (sqlTypeName.ToLowerInvariant()) switch
            {
                "geometry" => typeof(IKnownSqlDbTypeGeometry),
                "geography" => typeof(IKnownSqlDbTypeGeography),
                "hierarchyid" => typeof(IKnownSqlDbTypeHierarchyId),
                _ => null
            };
        }
    }

    public interface IKnownSqlDbTypeGeometry : IKnownSqlDbType { }
    public interface IKnownSqlDbTypeGeography : IKnownSqlDbType { }
    public interface IKnownSqlDbTypeHierarchyId : IKnownSqlDbType { }
}
