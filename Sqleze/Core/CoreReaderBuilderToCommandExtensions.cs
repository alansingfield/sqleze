using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

// Expose all the Read* functions on ISqlezeReaderBuilder
public static partial class CoreReaderBuilderToCommandExtensions
{

    public static T ReadSingle<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder)
        where T : notnull
        => sqlezeReaderBuilder.ExecuteReader().ReadSingle<T>();

    public static T? ReadSingleNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder)
        => sqlezeReaderBuilder.ExecuteReader().ReadSingleNullable<T>();

    public static T? ReadSingleOrDefault<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder)
        where T : notnull
        => sqlezeReaderBuilder.ExecuteReader().ReadSingleOrDefault<T>();


    public static ISqlezeReader ReadSingle<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, Expression<Func<T>> action)
        where T : notnull
    {
        return sqlezeReaderBuilder.ExecuteReader().ReadSingle<T>(action);
    }
    public static ISqlezeReader ReadSingleNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, Expression<Func<T?>> action)
    {
        return sqlezeReaderBuilder.ExecuteReader().ReadSingleNullable(action);
    }
    public static ISqlezeReader ReadSingleOrDefault<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, Expression<Func<T?>> action)
    {
        return sqlezeReaderBuilder.ExecuteReader().ReadSingleOrDefault(action);
    }
    public static async Task<T?> ReadSingleNullableAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        CancellationToken cancellationToken = default)
    {
        return await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadSingleNullableAsync<T>(cancellationToken).ConfigureAwait(false);
    }

    public static async Task<T> ReadSingleAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
            .ReadSingleAsync<T>(cancellationToken).ConfigureAwait(false);

    public static async Task<T?> ReadSingleOrDefaultAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
            .ReadSingleOrDefaultAsync<T>(cancellationToken).ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadSingleAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        Expression<Func<T>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
            .ReadSingleAsync<T>(action, cancellationToken).ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadSingleNullableAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
        => await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
            .ReadSingleNullableAsync<T?>(action, cancellationToken).ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadSingleOrDefaultAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
        => await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
            .ReadSingleOrDefaultAsync<T?>(action, cancellationToken).ConfigureAwait(false);



    public static ISqlezeReader ReadList<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, out List<T> result)
        where T : notnull
        => sqlezeReaderBuilder
                .ExecuteReader()
                .ReadList<T>(out result);

    public static List<T> ReadList<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder)
        where T : notnull
        => sqlezeReaderBuilder
                .ExecuteReader()
                .ReadList<T>();

    public static ISqlezeReader ReadListNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, out List<T?> result)
        => sqlezeReaderBuilder
                .ExecuteReader()
                .ReadListNullable<T>(out result);

    public static List<T?> ReadListNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder)
        => sqlezeReaderBuilder
                .ExecuteReader()
                .ReadListNullable<T?>();

    public static ISqlezeReader ReadArray<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, out T[] result)
        where T : notnull
        => sqlezeReaderBuilder
                .ExecuteReader()
                .ReadArray<T>(out result);

    public static T[] ReadArray<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder)
        where T : notnull
        => sqlezeReaderBuilder
                .ExecuteReader()
                .ReadArray<T>();

    public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, out T?[] result)
        => sqlezeReaderBuilder
                .ExecuteReader()
                .ReadArrayNullable<T?>(out result);

    public static T?[] ReadArrayNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder)
        => sqlezeReaderBuilder.ExecuteReader().ReadArrayNullable<T?>();

    public static async Task<List<T>> ReadListAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadListAsync<T>(cancellationToken).ConfigureAwait(false);

    public static async Task<T[]> ReadArrayAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadArrayAsync<T>(cancellationToken).ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadArrayAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        Expression<Func<T[]>> action, CancellationToken cancellationToken = default)
        where T : notnull
    {
        return await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadArrayAsync(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadArrayNullableAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        Expression<Func<T?[]>> action, CancellationToken cancellationToken = default)
    {
        return await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadArrayNullableAsync(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<T?[]> ReadArrayNullableAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, CancellationToken cancellationToken = default)
    {
        return await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadArrayNullableAsync<T?>(cancellationToken).ConfigureAwait(false);
    }

    public static ISqlezeReader ReadArray<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, Expression<Func<T[]>> action)
        where T : notnull
    {
        return sqlezeReaderBuilder.ExecuteReader().ReadArray<T>(action);
    }
    public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, Expression<Func<T?[]>> action)
    {
        return sqlezeReaderBuilder.ExecuteReader().ReadArrayNullable<T>(action);
    }
    public static async Task<ISqlezeReader> ReadListAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        Expression<Func<List<T>>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        return await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadListAsync<T>(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadListNullableAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        Expression<Func<List<T?>>> action,
        CancellationToken cancellationToken = default)
    {
        return await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadListNullableAsync<T>(action, cancellationToken).ConfigureAwait(false);
    }
    public static async Task<List<T?>> ReadListNullableAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
        CancellationToken cancellationToken = default)
    {
        return await (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadListNullableAsync<T>(cancellationToken).ConfigureAwait(false);
    }
    public static ISqlezeReader ReadList<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, Expression<Func<List<T>>> action)
        where T : notnull
    {
        return sqlezeReaderBuilder.ExecuteReader().ReadList<T>(action);
    }

    public static ISqlezeReader ReadListNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, Expression<Func<List<T?>>> action)
    {
        return sqlezeReaderBuilder.ExecuteReader().ReadListNullable<T>(action);
    }




    //public static ISqlezeReader ReadArray<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, out T[] result) { throw new NotImplementedException(); }
    //public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, out T?[] result) { throw new NotImplementedException(); }
    //public static T[] ReadArray<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder) { throw new NotImplementedException(); }
    //public static T[] ReadArrayNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder) { throw new NotImplementedException(); }
    //public static async Task<List<T>> ReadListAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, CancellationToken cancellationToken = default) { throw new NotImplementedException(); }
    //public static async Task<ISqlezeReader> ReadSingleAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder, CancellationToken cancellationToken = default)
    //    where T : notnull
    //{ throw new NotImplementedException(); }
    //public static async Task<T?> ReadSingleOrDefaultAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
    //    CancellationToken cancellationToken = default)
    //{ 
    //    await return (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadSingleOrDefaultAsync<T>(cancellationToken).ConfigureAwait(false);
    //}

    //public static async Task<T> ReadSingleAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
    //    CancellationToken cancellationToken = default)
    //    where T : notnull
    //{
    //    await return (await sqlezeReaderBuilder.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false)).ReadSingleAsync<T>(cancellationToken).ConfigureAwait(false);
    //}
    //public static async Task<ISqlezeReader> ReadSingleOrDefaultAsync<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder,
    //    CancellationToken cancellationToken = default)
    //{ throw new NotImplementedException(); }
    //public static T ReadSingle<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder) { throw new NotImplementedException(); }
    //public static T? ReadSingleNullable<T>(this ISqlezeReaderBuilder sqlezeReaderBuilder) { throw new NotImplementedException(); }


}




