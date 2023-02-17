using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Readers
{
    public interface IDataReaderFieldNames
    {
        DataReaderFieldInfo[] GetFieldInfos();
    }

    public class DataReaderFieldNames : IDataReaderFieldNames
    {
        private readonly IAdoDataReader adoDataReader;
        private readonly IAdoConnection adoConnection;

        public DataReaderFieldNames(
            IAdoDataReader adoDataReader, 
            IAdoConnection adoConnection)
        {
            this.adoDataReader = adoDataReader;
            this.adoConnection = adoConnection;
        }

        public DataReaderFieldInfo[] GetFieldInfos()
        {
            return Enumerable.Range(
                0, adoDataReader.SqlDataReader.VisibleFieldCount)
                .Select(i => new DataReaderFieldInfo(
                
                    ColumnOrdinal: i,
                    ColumnName:  adoDataReader.SqlDataReader.GetName(i),
                    SqlDataTypeName:  getDataTypeName(i)
                ))
                .ToArray();
        }

        private string getDataTypeName(int columnOrdinal)
        {
            var name = adoDataReader.SqlDataReader.GetDataTypeName(columnOrdinal);

            // Shortcut - normally we get a one-part name like "varchar".
            if(!name.Contains('.'))
                return name;

            string databaseName = adoConnection.SqlConnection.Database;

            // UDTs are returned in three-part format, DatabaseName.schema.column
            if(!name.StartsWith(databaseName + ".", StringComparison.InvariantCultureIgnoreCase))
                return name;

            // If we get "sys.geometry" we just want "geometry" as sys is the default
            // schema for types.
            string fullname = name.Substring(databaseName.Length + 1);
            if(fullname.StartsWith("sys.", StringComparison.InvariantCultureIgnoreCase))
                return fullname[4..];

            return fullname;
        }
    }

    public record DataReaderFieldInfo
    (
        int ColumnOrdinal,
        string ColumnName,
        string SqlDataTypeName
    );
}
