using Sqleze.Collections;
using Sqleze;
using Sqleze.Metadata;
using Sqleze.OutputParamReaders;
using Sqleze.SqlClient;
using Sqleze.TableValuedParameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params
{
    public class TableTypeAdoParameterFactory<T, TValue> : IAdoParameterFactory<T, TValue>
        where T : IEnumerable<TValue>
    {
        private readonly ISqlezeParameter<T> sqlezeParameter;
        private readonly Func<MS.SqlParameter> newMsSqlParameter;
        private readonly ITableTypeMetadataCache tableTypeMetadataCache;
        private readonly ISqlDataRecordMapper<TValue> sqlDataRecordPropertyMapper;
        private readonly ITableTypeColumnToSqlMetaDataConverter tableTypeColumnToSqlMetaDataConverter;
        private readonly Func<SqlDataRecordAdapterCreateOptions<TValue>, ISqlDataRecordAdapter<TValue>> newSqlDataRecordAdapter;
        private readonly ITableTypeNameResolver<T> tableTypeNameResolver;

        public TableTypeAdoParameterFactory(
            ISqlezeParameter<T> sqlezeParameter,
            Func<MS.SqlParameter> newMsSqlParameter,
            ITableTypeMetadataCache tableTypeMetadataCache,
            ISqlDataRecordMapper<TValue> sqlDataRecordPropertyMapper,
            ITableTypeColumnToSqlMetaDataConverter tableTypeColumnToSqlMetaDataConverter,
            Func<SqlDataRecordAdapterCreateOptions<TValue>, ISqlDataRecordAdapter<TValue>> newSqlDataRecordAdapter,
            ITableTypeNameResolver<T> tableTypeNameResolver)
        {
            this.sqlezeParameter = sqlezeParameter;
            this.newMsSqlParameter = newMsSqlParameter;
            this.tableTypeMetadataCache = tableTypeMetadataCache;
            this.sqlDataRecordPropertyMapper = sqlDataRecordPropertyMapper;
            this.tableTypeColumnToSqlMetaDataConverter = tableTypeColumnToSqlMetaDataConverter;
            this.newSqlDataRecordAdapter = newSqlDataRecordAdapter;
            this.tableTypeNameResolver = tableTypeNameResolver;
        }

        public IAdoParameter Create()
        {
            verifyParameterMode();

            string tableTypeName = tableTypeNameResolver.ResolveTableTypeName();
            var tableTypeColumns = tableTypeMetadataCache.GetTableTypeColumns(tableTypeName);

            return constructAdoParameter(tableTypeName, tableTypeColumns);
        }

        public async Task<IAdoParameter> CreateAsync(CancellationToken cancellationToken = default)
        {
            verifyParameterMode();

            string tableTypeName = await tableTypeNameResolver
                .ResolveTableTypeNameAsync(cancellationToken)
                .ConfigureAwait(false);

            var tableTypeColumns = await tableTypeMetadataCache
                .GetTableTypeColumnsAsync(tableTypeName, cancellationToken)
                .ConfigureAwait(false);

            return constructAdoParameter(tableTypeName, tableTypeColumns);
        }

        private IAdoParameter constructAdoParameter(string tableTypeName, IReadOnlyList<TableTypeColumnDefinition> tableTypeColumns)
        {
            if(tableTypeColumns.Count == 0)
                throw new Exception($"Unable to determine table type definition for parameter {sqlezeParameter.AdoName}");

            var mssqlParameter = newMsSqlParameter();

            mssqlParameter.ParameterName = sqlezeParameter.AdoName;
            mssqlParameter.SqlDbType = SqlDbType.Structured;

            mssqlParameter.TypeName = tableTypeName;
            mssqlParameter.Value = prepareMsSqlParameterValue(tableTypeColumns);

            return new AdoParameter(mssqlParameter);
        }

        private object? prepareMsSqlParameterValue(IReadOnlyList<TableTypeColumnDefinition> tableTypeColumns)
        {
            var value = sqlezeParameter.Value;

            // If null parameter value supplied, pass through to ADO parameter.
            if(value == null)
                return null;

            // This is a wrapper around an enumerable which calls MoveNext() early to determine if there
            // are items in the collection.
            var peeker = new EnumerablePeek<TValue>(value);

            // If no records found by the peeker, pass C# null as the value to the MS-Parameter.
            if(peeker.IsEmpty())
                return null;

            // Convert the table type column definitions to SqlMetaData array
            var sqlMetaDatas = tableTypeColumns
                .Select(x => tableTypeColumnToSqlMetaDataConverter.Convert(x))
                .ToArray();

            // Create a mapper which will look at the table type columns and build a lambda
            // that will write from an object's properties into SqlDataRecord.
            var writeAction = sqlDataRecordPropertyMapper.Map(tableTypeColumns);

            // Build the SqlDataRecordAdapter and return to be placed into the MS-Parameter value.
            return newSqlDataRecordAdapter(new SqlDataRecordAdapterCreateOptions<TValue>
            (
                Items: peeker,
                SqlMetaDatas: sqlMetaDatas,
                WriteAction: writeAction
            ));
        }

        private void verifyParameterMode()
        {
            if(sqlezeParameter.Mode is not SqlezeParameterMode.TableType)
                throw new Exception($"Incorrect {nameof(IAdoParameterFactory)} for parameter with mode {sqlezeParameter.Mode}");
        }
    }

}
