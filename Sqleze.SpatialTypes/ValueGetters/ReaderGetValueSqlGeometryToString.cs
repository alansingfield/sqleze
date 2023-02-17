using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using Sqleze.ValueGetters;

namespace Sqleze.SpatialTypes.ValueGetters
{
    //public class ReaderGetValueSqlGeometryToString : IReaderGetValue<string, IKnownSqlDbTypeGeometry>
    //{
    //    public string GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    //    {
    //        if(sqlDataReader.IsDBNull(columnOrdinal))
    //            return "";

    //        return new string(
    //            SqlGeometry.Deserialize(
    //                sqlDataReader.GetSqlBytes(columnOrdinal))
    //            .STAsText()
    //            .Value
    //        );
    //    }
    //}

}
