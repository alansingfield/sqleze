using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Converters.SqlTypes;
using Sqleze.Metadata;

namespace Sqleze.TableValuedParameters
{
    public interface ITableTypeColumnToSqlMetaDataConverter
    {
        MSS.SqlMetaData Convert(TableTypeColumnDefinition columnDef);
    }

    public class TableTypeColumnToSqlMetaDataConverter : ITableTypeColumnToSqlMetaDataConverter
    {
        private readonly ISqlMetaDataFactory sqlMetaDataFactory;

        public TableTypeColumnToSqlMetaDataConverter(
            ISqlMetaDataFactory sqlMetaDataFactory)
        {
            this.sqlMetaDataFactory = sqlMetaDataFactory;
        }


        public MSS.SqlMetaData Convert(TableTypeColumnDefinition columnDef)
        {
            var sqlDbType = SqlDbTypeConverter.ToSqlDbTypeKnown(columnDef.Datatype);

            // Sized like nvarchar(20) etc
            if (sqlDbType.HasSize())
                return sqlMetaDataFactory.New(
                    columnDef.ColumnName,
                    sqlDbType,
                    columnDef.Length);

            // Has a precision OR scale, pass them both
            if (sqlDbType.HasPrecision() || sqlDbType.HasScale())
            {
                byte precision =
                    sqlDbType.HasPrecision()
                    ? (byte)columnDef.Precision
                    : (byte)0;

                byte scale =
                    sqlDbType.HasScale()
                    ? (byte)columnDef.Scale
                    : (byte)0;

                return sqlMetaDataFactory.New(
                    columnDef.ColumnName,
                    sqlDbType,
                    precision,
                    scale);
            }

            // No size, precision or scale.
            return sqlMetaDataFactory.New(
                columnDef.ColumnName,
                sqlDbType);
        }
    }
}
