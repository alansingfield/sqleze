using Sqleze.Converters.Metadata;
using Sqleze.Readers;
using Sqleze.SqlClient;
using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Converters.SqlTypes;


namespace Sqleze.RowsetMetadata
{
    public record FieldSchema
    (
        string ColumnName,
        int ColumnOrdinal,
        Type? ProviderSpecificDataType,
        string DataTypeName,
        SqlDbType? SqlDbType,
        bool IsAnsi,
        int? Size,
        byte? Precision,
        byte? Scale,
        bool Nullable
    );

    public class RowsetMetadataProviderFieldSchemas : IRowsetMetadataProvider<IEnumerable<FieldSchema>>
    {
        private readonly ISchemaDataRowToSqlValueMetadataConverter converter;
        private readonly IAdoDataReader adoDataReader;

        public RowsetMetadataProviderFieldSchemas(ISchemaDataRowToSqlValueMetadataConverter converter,
            IAdoDataReader adoDataReader)
        {
            this.converter = converter;
            this.adoDataReader = adoDataReader;
        }

        public IEnumerable<FieldSchema> GetMetadata()
        {
            var schemaTable = adoDataReader.SqlDataReader.GetSchemaTable();

            return schemaTable.Rows.OfType<DataRow>()
                .Select((row, ordinal) => 
                {
                    var q = converter.ToSqlValueMetadata(row, ordinal);

                    return new FieldSchema(
                        q.ColumnName,
                        q.ColumnOrdinal,
                        q.ProviderSpecificDataType,
                        q.DataTypeName,
                        q.SqlDbType,
                        q.IsAnsi,
                        q.Size,
                        q.Precision,
                        q.Scale,
                        q.Nullable
                    );
                })
                .ToImmutableArray();
        }
    }
}

namespace Sqleze
{
    using Sqleze.RowsetMetadata;

    public static class FieldSchemaExtensions
    {
        /// <summary>
        /// Full sql type name like varchar(max) or numeric(18,2)
        /// </summary>
        /// <param name="fieldSchema"></param>
        /// <returns></returns>
        public static string SqlDataType(this FieldSchema fieldSchema)
        {
            if(fieldSchema.SqlDbType?.HasSize() ?? false)
            {
                return $"{fieldSchema.DataTypeName}({max(fieldSchema.Size)})";
            }

            // This will be decimal(p,s)
            if(fieldSchema.SqlDbType?.HasPrecision() ?? false)
            {
                return $"{fieldSchema.DataTypeName}({fieldSchema.Precision},{fieldSchema.Scale})";
            }

            // This will be datetime2(s), time(s), datetimeoffset(s)
            if(fieldSchema.SqlDbType?.HasScale() ?? false)
            {
                return $"{fieldSchema.DataTypeName}({fieldSchema.Scale})";
            }

            return fieldSchema.DataTypeName;
        }

        private static string max(int? size)
        {
            return size == -1 ? "max" : size?.ToString() ?? "";
        }
    }
}
