using Sqleze.DataSets;
using Sqleze.DryIoc;
using Sqleze;
using Sqleze.RowsetMetadata;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.DataSets
{
    public class FillDataTableRoot { }

    public record DataTableProvider
    (
        DataTable DataTable
    );

    public class DataTableRowset : ISqlezeRowset<DataTable>
    {
        private readonly IGenericResolver<IRowsetMetadataProvider> rowsetMetadataProviderResolver;
        private readonly IAdo ado;
        private readonly DataTableProvider dataTableProvider;
        private readonly IAdoDataReader adoDataReader;
        private readonly int rowsetCounter;

        public DataTableRowset(IGenericResolver<IRowsetMetadataProvider> rowsetMetadataProviderResolver,
            IAdo ado,
            DataTableProvider dataTableProvider,
            IAdoDataReader adoDataReader)
        {
            this.rowsetMetadataProviderResolver = rowsetMetadataProviderResolver;
            this.ado = ado;
            this.dataTableProvider = dataTableProvider;
            this.adoDataReader = adoDataReader;

            // Capture which rowset number we are on at time of construction.
            this.rowsetCounter = ado.RowsetCounter;
        }

        public IEnumerable<DataTable> Enumerate()
        {
            // Return a single DataTable even though we've enumerated through all the rows.
            yield return fillDataTable();

            // Either go to next rowset or close the reader if all rowsets received.
            ado.NextResult();
        }

        public async IAsyncEnumerable<DataTable> EnumerateAsync(
            [EnumeratorCancellation]
            CancellationToken cancellationToken = default)
        {
            yield return fillDataTable();

            await ado.NextResultAsync(cancellationToken).ConfigureAwait(false);
        }

        private DataTable fillDataTable()
        {
            verifyRowset();

            var dataTable = dataTableProvider.DataTable;
            var reader = adoDataReader.SqlDataReader;

            var adapter = new SqlezeDataAdapter()
            {
                FillLoadOption = LoadOption.OverwriteChanges,
                MissingSchemaAction = MissingSchemaAction.AddWithKey
            };

            try
            {
                adapter.Fill(new[] { dataTable }, reader, 0, 0);
            }
            catch(ConstraintException constraintException) when(dataTable.HasErrors)
            {
                constraintException.Data["TableName"] = dataTable.TableName;
                constraintException.Data["RowError"] = dataTable.GetErrors().FirstOrDefault()?.RowError;

                throw;
            }

            return dataTable;
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

namespace Sqleze.Registration
{
    public static class DataSetReaderRegistrationExtensions
    {
        public static void RegisterFillDataTable(this IRegistrator registrator)
        {
            // This overrides the normal ISqlezeRowset with a special implementation for DataTable.
            registrator.Register<ISqlezeRowset<DataTable>, DataTableRowset>(
                reuse: Reuse.ScopedToService<ISqlezeRowsetProvider>(),
                setup: Setup.With(asResolutionCall: true));

            registrator.Register<FillDataTableRoot>();
            registrator.Register<DataTableProvider>(
                reuse: Reuse.Scoped,
                setup: Setup.With(condition: Condition.ScopedToGenericArg<FillDataTableRoot>()));
        }
    }
}
