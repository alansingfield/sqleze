using Sqleze.Converters;
using Sqleze.DryIoc;
using Sqleze.Dynamics;
using Sqleze;
using Sqleze.SqlClient;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sqleze.Readers
{
    public interface IScalarReader<T> : IReader<T?> { }

    /// <summary>
    /// Enumerates through the returned ResultSet, returning the first column as a
    /// simple scalar type (or string).
    /// </summary>
    public class ScalarReader<T> : IScalarReader<T?>
    {
        private readonly IDataReaderFieldNames dataReaderFieldNames;
        private readonly IAdoDataReader adoDataReader;
        private readonly ICheckedAdoDataReader checkedAdoDataReader;
        private readonly IGenericResolver<IReaderGetValueResolver<T>> genericResolver;
        private readonly IKnownSqlDbTypeFinder knownSqlDbTypeFinder;
        private readonly ScalarReaderFallbackPolicyOptions scalarReaderFallbackPolicy;
        private readonly IDefaultValueCache<T> defaultValueCache;

        public ScalarReader(
            IDataReaderFieldNames dataReaderFieldNames,
            IAdoDataReader adoDataReader,
            ICheckedAdoDataReader checkedAdoDataReader,
            IGenericResolver<IReaderGetValueResolver<T>> genericResolver,
            IKnownSqlDbTypeFinder knownSqlDbTypeFinder,
            ScalarReaderFallbackPolicyOptions scalarReaderFallbackPolicy,
            IDefaultValueCache<T> defaultValueCache,
            IAdo ado)
        {
            this.dataReaderFieldNames = dataReaderFieldNames;
            this.adoDataReader = adoDataReader;
            this.checkedAdoDataReader = checkedAdoDataReader;
            this.genericResolver = genericResolver;
            this.knownSqlDbTypeFinder = knownSqlDbTypeFinder;
            this.scalarReaderFallbackPolicy = scalarReaderFallbackPolicy;
            this.defaultValueCache = defaultValueCache;
        }

        public IEnumerable<T?> Enumerate()
        {
            var readerGetValue = resolveReaderGetValue();

            // Read from the first column
            while(checkedAdoDataReader.Read())
            {
                yield return getValue(readerGetValue);
            }
        }


        public async IAsyncEnumerable<T?> EnumerateAsync(
            [EnumeratorCancellation] 
            CancellationToken cancellationToken)
        {
            var readerGetValue = resolveReaderGetValue();

            // Read from the first column
            while(await checkedAdoDataReader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                yield return getValue(readerGetValue);
            }
        }

        private T? getValue(IReaderGetValue<T> readerGetValue)
        {
            T? result = readerGetValue.GetValue(adoDataReader.SqlDataReader, 0);

            if (result == null && scalarReaderFallbackPolicy.UseDefaultInsteadOfNull)
            {
                return defaultValueCache.DefaultValue();
            }

            return result;
        }


        private IReaderGetValue<T> resolveReaderGetValue()
        {
            // Type like nvarchar, datetime2 from SQL reader
            string sqlDbTypeName = dataReaderFieldNames.GetFieldInfos()[0].SqlDataTypeName;

            // Convert to typeof(IKnownSqlDbTypeNVarChar) or similar
            var sqlDbType = knownSqlDbTypeFinder.FindKnownSqlDbType(sqlDbTypeName);

            // Resolve a suitable IReaderGetValue to read the field with.
            return genericResolver.Resolve(sqlDbType).GetReaderGetValue();
        }
    }
}
