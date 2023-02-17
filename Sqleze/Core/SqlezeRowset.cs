using Sqleze.Converters.Metadata;
using Sqleze.DryIoc;
using Sqleze;
using Sqleze.RowsetMetadata;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public class SqlezeRowset<T> : ISqlezeRowset<T>
    {
        private readonly IReader<T> reader;
        private readonly IAdo ado;
        private readonly IGenericResolver<IRowsetMetadataProvider> rowsetMetadataProviderResolver;
        private readonly int rowsetCounter;

        public SqlezeRowset(IReader<T> reader, IAdo ado,
            IGenericResolver<IRowsetMetadataProvider> rowsetMetadataProviderResolver)
        {
            this.reader = reader;
            this.ado = ado;
            this.rowsetMetadataProviderResolver = rowsetMetadataProviderResolver;

            // Capture which rowset number we are on at time of construction.
            this.rowsetCounter = ado.RowsetCounter;
        }

        public IEnumerable<T> Enumerate()
        {
            verifyRowset();

            foreach(var x in reader.Enumerate())
            {
                yield return x;
            }

            ado.NextResult();
        }

        public async IAsyncEnumerable<T> EnumerateAsync(
            [EnumeratorCancellation]
            CancellationToken cancellationToken = default)
        {
            verifyRowset();

            await foreach(var x in reader
                .EnumerateAsync(cancellationToken)
                .ConfigureAwait(false))
            {
                yield return x;
            }

            await ado.NextResultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        private void verifyRowset()
        {
            if(ado.RowsetCounter != this.rowsetCounter)
                throw new InvalidOperationException(
                    "Cannot use rowset after NextResult() has been called. Did you call Enumerate() twice?");
        }

        public TMetadata GetMetadata<TMetadata>()
        {
            verifyRowset();

            var rowsetMetadataProvider = rowsetMetadataProviderResolver
                .Resolve<IRowsetMetadataProvider, IRowsetMetadataProvider<TMetadata>>(typeof(TMetadata));

            return rowsetMetadataProvider.GetMetadata();
        }
    }
}
