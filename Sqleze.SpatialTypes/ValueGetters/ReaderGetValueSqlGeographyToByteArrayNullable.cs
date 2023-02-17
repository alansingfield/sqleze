using Microsoft.Data.SqlClient;
using Sqleze.ValueGetters;

namespace Sqleze.SpatialTypes.ValueGetters
{
    public class ReaderGetValueSqlGeographyToByteArrayNullable : IReaderGetValue<byte[]?, IKnownSqlDbTypeGeography>
    {
        public byte[]? GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
        {
            if(sqlDataReader.IsDBNull(columnOrdinal))
                return null;

            // To get the Geography as a byte array, we have to use GetSqlBytes() rather than
            // sqlDataReader.Value - because the former tries to construct the wrong type
            // of SqlGeography and fails.
            return sqlDataReader.GetSqlBytes(columnOrdinal).Value;
        }
    }
}
