using Sqleze;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.ValueGetters;
using Sqleze.Dynamics;
using System.Runtime.CompilerServices;
using Sqleze.DryIoc;

namespace Sqleze.Readers
{
    public interface IDictionaryReader<T> : IReader<T>
    { } 

    public class DictionaryReader<T> : IDictionaryReader<T> where T : IDictionary<string, object?>
    {
        private readonly IAdoDataReader adoDataReader;
        private readonly ICheckedAdoDataReader checkedAdoDataReader;
        private readonly INamingConvention namingConvention;
        private readonly IDataReaderFieldNames dataReaderFieldNames;
        private readonly IGenericResolver<IReaderGetValueResolver<object?>> genericResolver;
        private readonly IKnownSqlDbTypeFinder knownSqlDbTypeFinder;
        private readonly IConstructorCache<T> constructorCache;

        public DictionaryReader(
            IAdoDataReader adoDataReader,
            ICheckedAdoDataReader checkedAdoDataReader,
            INamingConvention namingConvention,
            IDataReaderFieldNames dataReaderFieldNames,
            IGenericResolver<IReaderGetValueResolver<object?>> genericResolver,
            IKnownSqlDbTypeFinder knownSqlDbTypeFinder,
            IConstructorCache<T> constructorCache)
        {
            this.adoDataReader = adoDataReader;
            this.checkedAdoDataReader = checkedAdoDataReader;
            this.namingConvention = namingConvention;
            this.dataReaderFieldNames = dataReaderFieldNames;
            this.genericResolver = genericResolver;
            this.knownSqlDbTypeFinder = knownSqlDbTypeFinder;
            this.constructorCache = constructorCache;
        }

        public IEnumerable<T> Enumerate()
        {
            var mappings = getFieldMappings();

            while(checkedAdoDataReader.Read())
            {
                var item = constructorCache.Create();
                var dict = (IDictionary<string, object?>)item;

                foreach(var col in mappings)
                {
                    dict.Add(col.DictionaryKey,
                        col.ReaderGetValue.GetValue(adoDataReader.SqlDataReader, col.ColumnOrdinal));
                }

                yield return item;
            }

        }

        public async IAsyncEnumerable<T> EnumerateAsync(
            [EnumeratorCancellation]
            CancellationToken cancellationToken)
        {
            var mappings = getFieldMappings();

            while(await checkedAdoDataReader
                .ReadAsync(cancellationToken)
                .ConfigureAwait(false))
            {
                var item = constructorCache.Create();
                var dict = (IDictionary<string, object?>)item;

                foreach(var col in mappings)
                {
                    dict.Add(col.DictionaryKey,
                        col.ReaderGetValue.GetValue(adoDataReader.SqlDataReader, col.ColumnOrdinal));
                }

                yield return item;
            }
        }

        private Mapping[] getFieldMappings()
        {
            return dataReaderFieldNames.GetFieldInfos()
                .Where(x => x.ColumnName != "")
                .Select(x => new Mapping(
                    x.ColumnOrdinal,
                    namingConvention.SqlToDotNet(x.ColumnName),
                    resolveReaderGetValue(x.SqlDataTypeName)
                ))
                .ToArray();
        }

        private IReaderGetValue<object?> resolveReaderGetValue(string sqlDbTypeName)
        {
            // Convert to typeof(IKnownSqlDbTypeNVarChar) or similar
            var sqlDbType = knownSqlDbTypeFinder.FindKnownSqlDbType(sqlDbTypeName);

            // Resolve a suitable IReaderGetValue to read the field with.
            return genericResolver.Resolve(sqlDbType).GetReaderGetValue();
        }

        private record Mapping(
            int ColumnOrdinal,
            string DictionaryKey,
            IReaderGetValue<object?> ReaderGetValue
        );
    }

}
