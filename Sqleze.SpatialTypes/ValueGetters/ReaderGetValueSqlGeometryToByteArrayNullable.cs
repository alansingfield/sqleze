using Microsoft.Data.SqlClient;
using Sqleze.ValueGetters;

namespace Sqleze.SpatialTypes.ValueGetters
{
    //public class ReaderGetValueSqlGeometryToByteArrayNullable : IReaderGetValue<byte[]?, IKnownSqlDbTypeGeometry>
    //{
    //    public byte[]? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    //    {
    //        if(sqlDataReader.IsDBNull(columnOrdinal))
    //            return null;

    //        // To get the Geometry as a byte array, we have to use GetSqlBytes() rather than
    //        // sqlDataReader.Value - because the former tries to construct the wrong type
    //        // of SqlGeometry and fails.
    //        return sqlDataReader.GetSqlBytes(columnOrdinal).Value;
    //    }
    //}
}
