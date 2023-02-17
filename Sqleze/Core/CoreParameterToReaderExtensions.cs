using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

// Expose all the Read* functions on ISqlezeParameter
public static partial class CoreParameterExtensions
{

    public static ISqlezeReader ExecuteReader(this ISqlezeParameter sqlezeParameter)
        => sqlezeParameter.Command
                .ExecuteReader();

    public static async Task<ISqlezeReader> ExecuteReaderAsync(this ISqlezeParameter sqlezeParameter, CancellationToken cancellationToken = default)
        => await sqlezeParameter.Command
                .ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

    public static int ExecuteNonQuery(this ISqlezeParameter sqlezeParameter)
        => sqlezeParameter.Command
                .ExecuteNonQuery();

    public static async Task<ISqlezeReader> ExecuteNonQueryAsync(this ISqlezeParameter sqlezeParameter, CancellationToken cancellationToken = default)
        => await sqlezeParameter.Command
                .ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);


    public static async Task<T?> ReadSingleNullableAsync<T>(this ISqlezeParameter sqlezeParameter,
        CancellationToken cancellationToken = default)
    {
        return await sqlezeParameter.Command.ReadSingleNullableAsync<T>(cancellationToken).ConfigureAwait(false);
    }
    public static ISqlezeReader ReadSingle<T>(this ISqlezeParameter sqlezeParameter, Expression<Func<T>> action)
        where T : notnull
    {
        return sqlezeParameter.Command.ReadSingle<T>(action);
    }
    public static ISqlezeReader ReadSingleNullable<T>(this ISqlezeParameter sqlezeParameter, Expression<Func<T?>> action)
    {
        return sqlezeParameter.Command.ReadSingleNullable(action);
    }
    public static ISqlezeReader ReadSingleOrDefault<T>(this ISqlezeParameter sqlezeParameter, Expression<Func<T?>> action)
    {
        return sqlezeParameter.Command.ReadSingleOrDefault(action);
    }

    public static ISqlezeReader ReadList<T>(this ISqlezeParameter sqlezeParameter, out List<T> result)
        where T : notnull
        => sqlezeParameter.Command
                .ExecuteReader()
                .ReadList<T>(out result);

    public static List<T> ReadList<T>(this ISqlezeParameter sqlezeParameter)
        where T : notnull
        => sqlezeParameter.Command
                .ExecuteReader()
                .ReadList<T>();

    public static ISqlezeReader ReadListNullable<T>(this ISqlezeParameter sqlezeParameter, out List<T?> result)
        => sqlezeParameter.Command
                .ExecuteReader()
                .ReadListNullable<T>(out result);

    public static List<T?> ReadListNullable<T>(this ISqlezeParameter sqlezeParameter)
        => sqlezeParameter.Command
                .ExecuteReader()
                .ReadListNullable<T?>();

    public static ISqlezeReader ReadArray<T>(this ISqlezeParameter sqlezeParameter, out T[] result)
        where T : notnull
        => sqlezeParameter.Command
                .ExecuteReader()
                .ReadArray<T>(out result);

    public static T[] ReadArray<T>(this ISqlezeParameter sqlezeParameter)
        where T : notnull
        => sqlezeParameter.Command
                .ExecuteReader()
                .ReadArray<T>();

    public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeParameter sqlezeParameter, out T?[] result)
        => sqlezeParameter.Command
                .ExecuteReader()
                .ReadArrayNullable<T?>(out result);

    public static T?[] ReadArrayNullable<T>(this ISqlezeParameter sqlezeParameter)
        => sqlezeParameter.Command.ReadArrayNullable<T?>();

    public static T ReadSingle<T>(this ISqlezeParameter sqlezeParameter)
        where T : notnull
        => sqlezeParameter.Command.ReadSingle<T>();

    public static T? ReadSingleNullable<T>(this ISqlezeParameter sqlezeParameter)
        => sqlezeParameter.Command.ReadSingleNullable<T>();

    public static T? ReadSingleOrDefault<T>(this ISqlezeParameter sqlezeParameter)
        where T : notnull
        => sqlezeParameter.Command.ReadSingleOrDefault<T>();

    public static async Task<List<T>> ReadListAsync<T>(this ISqlezeParameter sqlezeParameter,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await sqlezeParameter.Command.ReadListAsync<T>(cancellationToken).ConfigureAwait(false);

    public static async Task<T[]> ReadArrayAsync<T>(this ISqlezeParameter sqlezeParameter,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await sqlezeParameter.Command.ReadArrayAsync<T>(cancellationToken).ConfigureAwait(false);

    public static async Task<T> ReadSingleAsync<T>(this ISqlezeParameter sqlezeParameter,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await sqlezeParameter.Command.ReadSingleAsync<T>(cancellationToken).ConfigureAwait(false);

    public static async Task<T?> ReadSingleOrDefaultAsync<T>(this ISqlezeParameter sqlezeParameter,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await sqlezeParameter.Command.ReadSingleOrDefaultAsync<T>(cancellationToken).ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadArrayAsync<T>(this ISqlezeParameter sqlezeParameter,
        Expression<Func<T[]>> action, CancellationToken cancellationToken = default)
        where T: notnull
    {
        return await sqlezeParameter.Command.ReadArrayAsync(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadArrayNullableAsync<T>(this ISqlezeParameter sqlezeParameter,
        Expression<Func<T?[]>> action, CancellationToken cancellationToken = default)
    { 
        return await sqlezeParameter.Command.ReadArrayNullableAsync(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<T?[]> ReadArrayNullableAsync<T>(this ISqlezeParameter sqlezeParameter, CancellationToken cancellationToken = default) 
    {
        return await sqlezeParameter.Command.ReadArrayNullableAsync<T?>(cancellationToken).ConfigureAwait(false);
    }

    public static ISqlezeReader ReadArray<T>(this ISqlezeParameter sqlezeParameter, Expression<Func<T[]>> action)
        where T : notnull
    {
        return sqlezeParameter.Command.ReadArray<T>(action);
    }
    public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeParameter sqlezeParameter, Expression<Func<T?[]>> action) 
    {
        return sqlezeParameter.Command.ReadArrayNullable<T>(action);
    }
    public static async Task<ISqlezeReader> ReadListAsync<T>(this ISqlezeParameter sqlezeParameter, 
        Expression<Func<List<T>>> action, 
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        return await sqlezeParameter.Command.ReadListAsync<T>(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadListNullableAsync<T>(this ISqlezeParameter sqlezeParameter, 
        Expression<Func<List<T?>>> action, 
        CancellationToken cancellationToken = default)
    {
        return await sqlezeParameter.Command.ReadListNullableAsync<T>(action, cancellationToken).ConfigureAwait(false);
    }
    public static async Task<List<T?>> ReadListNullableAsync<T>(this ISqlezeParameter sqlezeParameter,
        CancellationToken cancellationToken = default)
    {
        return await sqlezeParameter.Command.ReadListNullableAsync<T>(cancellationToken).ConfigureAwait(false);
    }
    public static ISqlezeReader ReadList<T>(this ISqlezeParameter sqlezeParameter, Expression<Func<List<T>>> action)
        where T : notnull
    {
        return sqlezeParameter.Command.ReadList<T>(action);
    }

    public static ISqlezeReader ReadListNullable<T>(this ISqlezeParameter sqlezeParameter, Expression<Func<List<T?>>> action)
    {
        return sqlezeParameter.Command.ReadListNullable<T>(action);
    }
    public static async Task<ISqlezeReader> ReadSingleNullableAsync<T>(this ISqlezeParameter sqlezeParameter,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
    {
        return await sqlezeParameter.Command.ReadSingleNullableAsync<T>(action, cancellationToken).ConfigureAwait(false);
    }




    //public static ISqlezeReader ReadArray<T>(this ISqlezeParameter sqlezeParameter, out T[] result) { throw new NotImplementedException(); }
    //public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeParameter sqlezeParameter, out T?[] result) { throw new NotImplementedException(); }
    //public static T[] ReadArray<T>(this ISqlezeParameter sqlezeParameter) { throw new NotImplementedException(); }
    //public static T[] ReadArrayNullable<T>(this ISqlezeParameter sqlezeParameter) { throw new NotImplementedException(); }
    //public static async Task<List<T>> ReadListAsync<T>(this ISqlezeParameter sqlezeParameter, CancellationToken cancellationToken = default) { throw new NotImplementedException(); }
    //public static async Task<ISqlezeReader> ReadSingleAsync<T>(this ISqlezeParameter sqlezeParameter, CancellationToken cancellationToken = default)
    //    where T : notnull
    //{ throw new NotImplementedException(); }
    //public static async Task<T?> ReadSingleOrDefaultAsync<T>(this ISqlezeParameter sqlezeParameter,
    //    CancellationToken cancellationToken = default)
    //{ 
    //    return await sqlezeParameter.Command.ReadSingleOrDefaultAsync<T>(cancellationToken).ConfigureAwait(false);
    //}

    //public static async Task<T> ReadSingleAsync<T>(this ISqlezeParameter sqlezeParameter,
    //    CancellationToken cancellationToken = default)
    //    where T : notnull
    //{
    //    return await sqlezeParameter.Command.ReadSingleAsync<T>(cancellationToken).ConfigureAwait(false);
    //}
    //public static async Task<ISqlezeReader> ReadSingleOrDefaultAsync<T>(this ISqlezeParameter sqlezeParameter,
    //    CancellationToken cancellationToken = default)
    //{ throw new NotImplementedException(); }
    //public static T ReadSingle<T>(this ISqlezeParameter sqlezeParameter) { throw new NotImplementedException(); }
    //public static T? ReadSingleNullable<T>(this ISqlezeParameter sqlezeParameter) { throw new NotImplementedException(); }


}
