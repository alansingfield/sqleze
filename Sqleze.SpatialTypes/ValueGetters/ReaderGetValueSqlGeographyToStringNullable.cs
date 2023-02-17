using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using Sqleze.ValueGetters;

namespace Sqleze.SpatialTypes.ValueGetters
{
    //public class ReaderGetValueSqlGeographyToStringNullable : IReaderGetValue<string?, IKnownSqlDbTypeGeography>
    //{
    //    public string? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    //    {
    //        if(sqlDataReader.IsDBNull(columnOrdinal))
    //            return null;

    //        return new string(
    //            SqlGeography.Deserialize(
    //                sqlDataReader.GetSqlBytes(columnOrdinal))
    //            .STAsText()
    //            .Value
    //        );
    //    }
    //}

}
