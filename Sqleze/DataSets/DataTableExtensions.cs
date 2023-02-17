using Sqleze;
using Sqleze.DataSets;
using Sqleze.Util;
using System.Data;

namespace Sqleze;

public static class DataTableExtensions
{
    public static ISqlezeReader FillDataTable(this ISqlezeReader sqlezeReader, DataTable dataTable)
    {
        var builder = sqlezeReader.With<FillDataTableRoot>((root, scope) =>
        {
            // Drop the supplied DataTableProvider into the scope so DataTableRowset can pick it up.
            scope.Use(new DataTableProvider(dataTable));
        });

        // The action of enumerating populates the DataTable.
        var _ = builder.OpenRowsetNullable<DataTable>().Enumerate().Single();

        return sqlezeReader;
    }
    public static async Task<ISqlezeReader> FillDataTableAsync(
        this ISqlezeReader sqlezeReader, 
        DataTable dataTable, 
        CancellationToken cancellationToken = default)
    {
        var builder = sqlezeReader.With<FillDataTableRoot>((root, scope) =>
        {
            // Drop the supplied DataTableProvider into the scope so DataTableRowset can pick it up.
            scope.Use(new DataTableProvider(dataTable));
        });

        // The action of enumerating populates the DataTable.
        var _ = await builder
            .OpenRowsetNullable<DataTable>()
            .EnumerateAsync(cancellationToken)
            .SingleAsync(cancellationToken)
            .ConfigureAwait(false);

        return sqlezeReader;
    }


    public static ISqlezeReader FillDataTable(this ISqlezeCommand sqlezeCommand, DataTable dataTable)
    {
        return sqlezeCommand.ExecuteReader().FillDataTable(dataTable);
    }

    public static ISqlezeReader FillDataTable(this ISqlezeReaderBuilder sqlezeReaderBuilder, DataTable dataTable)
    {
        return sqlezeReaderBuilder.ExecuteReader().FillDataTable(dataTable);
    }

    public static ISqlezeReader FillDataTable(this ISqlezeParameter sqlezeParameter, DataTable dataTable)
    {
        return sqlezeParameter.ExecuteReader().FillDataTable(dataTable);
    }

    public static ISqlezeReader FillDataTable(this ISqlezeParameterCollection sqlezeParameterCollection, DataTable dataTable)
    {
        return sqlezeParameterCollection.ExecuteReader().FillDataTable(dataTable);
    }

    public static async Task<ISqlezeReader> FillDataTableAsync(this ISqlezeCommand sqlezeCommand, DataTable dataTable,
        CancellationToken cancellationToken = default)
    {
        var sqlezeReader = await sqlezeCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        await sqlezeReader.FillDataTableAsync(dataTable, cancellationToken).ConfigureAwait(false);

        return sqlezeReader;
    }

    public static async Task<ISqlezeReader> FillDataTableAsync(this ISqlezeReaderBuilder sqlezeReaderBuilder, DataTable dataTable,
        CancellationToken cancellationToken = default)
    {
        var sqlezeReader = await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        
        await sqlezeReader.FillDataTableAsync(dataTable, cancellationToken).ConfigureAwait(false);

        return sqlezeReader;
    }


    public static async Task<ISqlezeReader> FillDataTableAsync(this ISqlezeParameter sqlezeParameter, 
        DataTable dataTable, 
        CancellationToken cancellationToken = default)
    {
        var sqlezeReader = await sqlezeParameter.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        await sqlezeReader.FillDataTableAsync(dataTable, cancellationToken).ConfigureAwait(false);

        return sqlezeReader;
    }

    public static async Task<ISqlezeReader> FillDataTableAsync(this ISqlezeParameterCollection sqlezeParameterCollection, 
        DataTable dataTable, 
        CancellationToken cancellationToken = default)
    {
        var sqlezeReader = await sqlezeParameterCollection.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        await sqlezeReader.FillDataTableAsync(dataTable, cancellationToken).ConfigureAwait(false);

        return sqlezeReader;
    }

}

