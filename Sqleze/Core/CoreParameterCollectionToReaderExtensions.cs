using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

// Expose all the Read* functions on ISqlezeParameterCollection
public static partial class CoreParameterCollectionExtensions
{
    public static T ReadSingle<T>(this ISqlezeParameterCollection sqlezeParameterCollection)
        where T : notnull
        => sqlezeParameterCollection.Command
            .ReadSingle<T>();

    public static T? ReadSingleNullable<T>(this ISqlezeParameterCollection sqlezeParameterCollection)
        => sqlezeParameterCollection.Command
            .ReadSingleNullable<T>();

    public static T? ReadSingleOrDefault<T>(this ISqlezeParameterCollection sqlezeParameterCollection)
        => sqlezeParameterCollection.Command
            .ReadSingleOrDefault<T>();


    public static ISqlezeReader ReadSingle<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T>> action)
        where T : notnull
        => sqlezeParameterCollection.Command
            .ReadSingle<T>(action);

    public static ISqlezeReader ReadSingleNullable<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T?>> action)
        => sqlezeParameterCollection.Command
            .ReadSingleNullable<T>(action);

    public static ISqlezeReader ReadSingleOrDefault<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T?>> action)
        => sqlezeParameterCollection.Command
            .ReadSingleOrDefault<T>(action);


    public static async Task<T> ReadSingleAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await sqlezeParameterCollection.Command
            .ReadSingleAsync<T>(cancellationToken)
            .ConfigureAwait(false);

    public static async Task<T?> ReadSingleNullableAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        CancellationToken cancellationToken = default)
        => await sqlezeParameterCollection.Command
            .ReadSingleNullableAsync<T>(cancellationToken)
            .ConfigureAwait(false);

    public static async Task<T?> ReadSingleOrDefaultAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        CancellationToken cancellationToken = default)
        => await sqlezeParameterCollection.Command
            .ReadSingleOrDefaultAsync<T>(cancellationToken)
            .ConfigureAwait(false);


    public static async Task<ISqlezeReader> ReadSingleAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await sqlezeParameterCollection.Command
            .ReadSingleAsync<T>(action, cancellationToken).ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadSingleNullableAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
        => await sqlezeParameterCollection.Command
            .ReadSingleNullableAsync<T?>(action, cancellationToken).ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadSingleOrDefaultAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
        => await sqlezeParameterCollection.Command
            .ReadSingleOrDefaultAsync<T?>(action, cancellationToken).ConfigureAwait(false);



    public static ISqlezeReader ReadList<T>(this ISqlezeParameterCollection sqlezeParameterCollection, out List<T> result)
        where T : notnull
        => sqlezeParameterCollection.Command
            .ExecuteReader()
            .ReadList<T>(out result);

    public static List<T> ReadList<T>(this ISqlezeParameterCollection sqlezeParameterCollection)
        where T : notnull
        => sqlezeParameterCollection.Command
            .ReadList<T>();

    public static List<T?> ReadListNullable<T>(this ISqlezeParameterCollection sqlezeParameterCollection)
        => sqlezeParameterCollection.Command
            .ReadListNullable<T?>();

    public static ISqlezeReader ReadArray<T>(this ISqlezeParameterCollection sqlezeParameterCollection, out T[] result)
        where T : notnull
        => sqlezeParameterCollection.Command
            .ExecuteReader()
            .ReadArray<T>(out result);

    public static T[] ReadArray<T>(this ISqlezeParameterCollection sqlezeParameterCollection)
        where T : notnull
        => sqlezeParameterCollection.Command
            .ReadArray<T>();

    public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeParameterCollection sqlezeParameterCollection, out T?[] result)
        => sqlezeParameterCollection.Command
            .ExecuteReader()
            .ReadArrayNullable<T?>(out result);

    public static T?[] ReadArrayNullable<T>(this ISqlezeParameterCollection sqlezeParameterCollection)
        => sqlezeParameterCollection.Command
            .ReadArrayNullable<T?>();

    public static async Task<List<T>> ReadListAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await sqlezeParameterCollection.Command
            .ReadListAsync<T>(cancellationToken)
            .ConfigureAwait(false);

    public static async Task<List<T?>> ReadListNullableAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        CancellationToken cancellationToken = default)
        => await sqlezeParameterCollection.Command
            .ReadListNullableAsync<T?>(cancellationToken)
            .ConfigureAwait(false);

    public static async Task<T[]> ReadArrayAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await sqlezeParameterCollection.Command
            .ReadArrayAsync<T>(cancellationToken)
            .ConfigureAwait(false);

    public static async Task<T?[]> ReadArrayNullableAsync<T>(this ISqlezeParameterCollection sqlezeParameterCollection,
        CancellationToken cancellationToken = default)
        => await sqlezeParameterCollection.Command
            .ReadArrayNullableAsync<T?>(cancellationToken)
            .ConfigureAwait(false);



    public static ISqlezeReader ExecuteReader(this ISqlezeParameterCollection sqlezeParameterCollection)
        => sqlezeParameterCollection.Command
            .ExecuteReader();

    public static async Task<ISqlezeReader> ExecuteReaderAsync(this ISqlezeParameterCollection sqlezeParameterCollection,
        CancellationToken cancellationToken = default)
        => await sqlezeParameterCollection.Command
            .ExecuteReaderAsync(cancellationToken)
            .ConfigureAwait(false);

    public static int ExecuteNonQuery(this ISqlezeParameterCollection sqlezeParameterCollection)
        => sqlezeParameterCollection.Command
            .ExecuteNonQuery();

    public static async Task<int> ExecuteNonQueryAsync(this ISqlezeParameterCollection sqlezeParameterCollection,
        CancellationToken cancellationToken = default)
        => await sqlezeParameterCollection.Command
            .ExecuteNonQueryAsync(cancellationToken)
            .ConfigureAwait(false);
}
