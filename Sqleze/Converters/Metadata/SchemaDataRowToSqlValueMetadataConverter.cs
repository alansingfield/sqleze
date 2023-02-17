using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Converters.SqlTypes;

namespace Sqleze.Converters.Metadata
{
    public interface ISchemaDataRowToSqlValueMetadataConverter
    {
        SqlValueMetaData ToSqlValueMetadata(DataRow row, int columnOrdinal);
    }

    public class SchemaDataRowToSqlValueMetadataConverter : ISchemaDataRowToSqlValueMetadataConverter
    {
        /// <summary>
        /// Converts loosely-typed DataRow of schema information from SqlDataReader to a proper
        /// object.
        /// </summary>
        /// <param name="row">From sqlDataReader.GetSchemaTable().Rows[columnOrdinal]</param>
        /// <param name="columnOrdinal">Column number</param>
        /// <returns></returns>
        public SqlValueMetaData ToSqlValueMetadata(DataRow row, int columnOrdinal)
        {
            // For some reason there isn't a SchemaTableColumn constant for DataTypeName.
            string dataTypeName = (string)row["DataTypeName"];

            // Convert from text "nvarchar" to SqlDbType.NVarChar
            var sqlDbType = SqlDbTypeConverter.ToSqlDbType(dataTypeName);

            var result = new SqlValueMetaData()
            {
                ColumnOrdinal = columnOrdinal,
                ColumnName = (string)row[SchemaTableColumn.ColumnName],
                ProviderSpecificDataType = (Type)row[SchemaTableOptionalColumn.ProviderSpecificDataType],
                Nullable = (bool)row[SchemaTableColumn.AllowDBNull],
                DataTypeName = dataTypeName,
                SqlDbType = sqlDbType
            };

            result.IsAnsi = sqlDbType?.IsAnsiType() ?? false;

            if(sqlDbType?.HasPrecision() ?? false)
            {
                result.Precision = Convert.ToByte(row[SchemaTableColumn.NumericPrecision]);
            }

            if(sqlDbType?.HasScale() ?? false)
            {
                result.Scale = Convert.ToByte(row[SchemaTableColumn.NumericScale]);
            }

            if(sqlDbType?.HasSize() ?? false)
            {
                int size = Convert.ToInt32(row[SchemaTableColumn.ColumnSize]);

                // MAX columns are given a size of maxint - change to -1 to be consistent.
                if(size == int.MaxValue)
                    size = -1;

                result.Size = size;
            }

            return result;
        }
    }
}