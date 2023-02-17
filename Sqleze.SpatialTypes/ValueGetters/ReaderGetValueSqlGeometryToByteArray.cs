using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using Sqleze.Interfaces;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SpatialTypes.ValueGetters
{

    //public class ReaderGetValueSqlGeometryToByteArray : IReaderGetValue<byte[], IKnownSqlDbTypeGeometry>
    //{
    //    private static readonly byte[] emptyBytes = new byte[0];

    //    public byte[] GetValue(SqlDataReader sqlDataReader, int columnOrdinal)
    //    {
    //        if(sqlDataReader.IsDBNull(columnOrdinal))
    //            return emptyBytes;

    //        // To get the Geometry as a byte array, we have to use GetSqlBytes() rather than
    //        // sqlDataReader.Value - because the former tries to construct the wrong type
    //        // of SqlGeometry and fails.
    //        return sqlDataReader.GetSqlBytes(columnOrdinal).Value;
    //    }
    //}
}
