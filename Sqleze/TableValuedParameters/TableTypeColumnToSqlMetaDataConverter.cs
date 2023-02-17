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
                return sqlMetaDataFactory.New(
                    columnDef.ColumnName,
                    sqlDbType,
                    (byte)columnDef.Precision,
                    (byte)columnDef.Scale);

            // No size, precision or scale.
            return sqlMetaDataFactory.New(
                columnDef.ColumnName,
                sqlDbType);
        }
    }
}
