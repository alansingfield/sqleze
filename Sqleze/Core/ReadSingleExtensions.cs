using Sqleze.Dynamics;
using Sqleze;
using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Sqleze;
public static class ReadSingleExtensions
{

    public static T ReadSingle<T>(this ISqlezeReader sqlezeReader)
        where T : notnull
    {
        var rowset = sqlezeReader
            .OpenRowset<T>();

        return rowset.Enumerate().Single();
    }

    public static T? ReadSingleNullable<T>(this ISqlezeReader sqlezeReader)
    {
        var rowset = sqlezeReader
            .OpenRowsetNullable<T>();

        return rowset.Enumerate().Single();
    }

    public static T? ReadSingleOrDefault<T>(this ISqlezeReader sqlezeReader)
    {
        var rowset = sqlezeReader.OpenRowsetNullable<T>();

        return rowset.Enumerate().SingleOrDefault();
    }

    public static ISqlezeReader ReadSingle<T>(this ISqlezeReader sqlezeReader, Expression<Func<T>> action)
    where T : notnull
    {
        var setter = ExpressionSetter.Prepare<T>(action).Setter;

        var rowset = sqlezeReader
            .OpenRowset<T>();

        T result = rowset.Enumerate().Single();

        setter(result);

        return sqlezeReader;
    }

    public static ISqlezeReader ReadSingleNullable<T>(this ISqlezeReader sqlezeReader, Expression<Func<T?>> action)
    {
        var setter = ExpressionSetter.Prepare<T?>(action).Setter;

        var rowset = sqlezeReader
            .OpenRowsetNullable<T>();

        T? result = rowset.Enumerate().Single();

        setter(result);

        return sqlezeReader;
    }

    public static ISqlezeReader ReadSingleOrDefault<T>(this ISqlezeReader sqlezeReader, Expression<Func<T?>> action)
    {
        var setter = ExpressionSetter.Prepare<T?>(action).Setter;

        var rowset = sqlezeReader
            .OpenRowsetNullable<T>();

        T? result = rowset.Enumerate().SingleOrDefault();

        setter(result);

        return sqlezeReader;
    }

    public static T ReadSingle<T>(this ISqlezeCommand sqlezeCommand)
        where T : notnull
        => sqlezeCommand
            .ExecuteReader()
            .ReadSingle<T>();

    public static T? ReadSingleNullable<T>(this ISqlezeCommand sqlezeCommand)
        => sqlezeCommand
            .ExecuteReader()
            .ReadSingleNullable<T?>();

    public static T? ReadSingleOrDefault<T>(this ISqlezeCommand sqlezeCommand)
        => sqlezeCommand
            .ExecuteReader()
            .ReadSingleOrDefault<T?>();



    public static ISqlezeReader ReadSingle<T>(this ISqlezeCommand sqlezeCommand, Expression<Func<T>> action)
        where T : notnull
    {
        var reader = sqlezeCommand.ExecuteReader();
        return reader.ReadSingle<T>(action);
    }

    public static ISqlezeReader ReadSingleNullable<T>(this ISqlezeCommand sqlezeCommand, Expression<Func<T?>> action)
    {
        var reader = sqlezeCommand.ExecuteReader();
        return reader.ReadSingleNullable<T?>(action);
    }

    public static ISqlezeReader ReadSingleOrDefault<T>(this ISqlezeCommand sqlezeCommand, Expression<Func<T?>> action)
    {
        var reader = sqlezeCommand.ExecuteReader();
        return reader.ReadSingleOrDefault<T?>(action);
    }

    public static T ReadSingle<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory)
        where T : notnull
        => scopedSqlezeParameterFactory.Command.ReadSingle<T>();

    public static T? ReadSingleNullable<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory)
        => scopedSqlezeParameterFactory.Command.ReadSingleNullable<T?>();

    public static T? ReadSingleOrDefault<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory)
        => scopedSqlezeParameterFactory.Command.ReadSingleOrDefault<T?>();

    public static ISqlezeReader ReadSingle<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Expression<Func<T>> action)
        where T : notnull
        => scopedSqlezeParameterFactory.Command.ReadSingle<T>(action);

    public static ISqlezeReader ReadSingleNullable<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Expression<Func<T?>> action)
        => scopedSqlezeParameterFactory.Command.ReadSingleNullable<T?>(action);

    public static ISqlezeReader ReadSingleOrDefault<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Expression<Func<T?>> action)
        => scopedSqlezeParameterFactory.Command.ReadSingleOrDefault<T?>(action);

    public static async Task<T> ReadSingleAsync<T>(this ISqlezeReader sqlezeReader,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        return await sqlezeReader
            .OpenRowset<T>()
            .EnumerateAsync(cancellationToken)
            .SingleAsync(cancellationToken)
            .ConfigureAwait(false);
    }
    public static async Task<T?> ReadSingleNullableAsync<T>(this ISqlezeReader sqlezeReader,
        CancellationToken cancellationToken = default)
    {
        return await sqlezeReader
            .OpenRowsetNullable<T>()
            .EnumerateAsync(cancellationToken)
            .SingleAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<T?> ReadSingleOrDefaultAsync<T>(this ISqlezeReader sqlezeReader,
        CancellationToken cancellationToken = default)
    {
        return await sqlezeReader
            .OpenRowsetNullable<T>()
            .EnumerateAsync(cancellationToken)
            .SingleOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }


    public static async Task<ISqlezeReader> ReadSingleAsync<T>(this ISqlezeReader sqlezeReader,
        Expression<Func<T>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        var setter = ExpressionSetter.Prepare<T>(action).Setter;

        T result = await sqlezeReader
            .ReadSingleAsync<T>(cancellationToken)
            .ConfigureAwait(false);

        setter(result);

        return sqlezeReader;
    }

    public static async Task<ISqlezeReader> ReadSingleNullableAsync<T>(this ISqlezeReader sqlezeReader,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
    {
        var setter = ExpressionSetter.Prepare<T?>(action).Setter;

        T? result = await sqlezeReader
            .ReadSingleNullableAsync<T>(cancellationToken)
            .ConfigureAwait(false);

        setter(result);

        return sqlezeReader;
    }

    public static async Task<ISqlezeReader> ReadSingleOrDefaultAsync<T>(this ISqlezeReader sqlezeReader,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
    {
        var setter = ExpressionSetter.Prepare<T?>(action).Setter;

        T? result = await sqlezeReader
            .ReadSingleOrDefaultAsync<T>(cancellationToken)
            .ConfigureAwait(false);

        setter(result);

        return sqlezeReader;
    }


    public static async Task<ISqlezeReader> ReadSingleAsync<T>(this Task<ISqlezeReader> sqlezeReaderTask,
        Expression<Func<T>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        var r = await sqlezeReaderTask.ConfigureAwait(false);
        await r.ReadSingleAsync<T>(action, cancellationToken).ConfigureAwait(false);
        return r;
    }

    public static async Task<ISqlezeReader> ReadSingleNullableAsync<T>(this Task<ISqlezeReader> sqlezeReaderTask,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
    {
        var r = await sqlezeReaderTask.ConfigureAwait(false);
        await r.ReadSingleNullableAsync<T>(action, cancellationToken).ConfigureAwait(false);
        return r;
    }

    public static async Task<ISqlezeReader> ReadSingleOrDefaultAsync<T>(this Task<ISqlezeReader> sqlezeReaderTask,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
    {
        var r = await sqlezeReaderTask.ConfigureAwait(false);
        await r.ReadSingleOrDefaultAsync<T>(action, cancellationToken).ConfigureAwait(false);
        return r;
    }






    public static async Task<T> ReadSingleAsync<T>(this ISqlezeCommand sqlezeCommand,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        var reader = await sqlezeCommand 
            .ExecuteReaderAsync(cancellationToken)
            .ConfigureAwait(false);

        return await reader
            .ReadSingleAsync<T>(cancellationToken)
            .ConfigureAwait(false);
    }
    public static async Task<T?> ReadSingleNullableAsync<T>(this ISqlezeCommand sqlezeCommand,
        CancellationToken cancellationToken = default)
    {
        var reader = await sqlezeCommand
            .ExecuteReaderAsync(cancellationToken)
            .ConfigureAwait(false);

        return await reader
            .ReadSingleNullableAsync<T>(cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<T?> ReadSingleOrDefaultAsync<T>(this ISqlezeCommand sqlezeCommand,
        CancellationToken cancellationToken = default)
    {
        var reader = await sqlezeCommand
            .ExecuteReaderAsync(cancellationToken)
            .ConfigureAwait(false);

        return await reader
            .ReadSingleOrDefaultAsync<T>(cancellationToken)
            .ConfigureAwait(false);
    }





    public static async Task<ISqlezeReader> ReadSingleAsync<T>(this ISqlezeCommand sqlezeCommand,
        Expression<Func<T>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        var reader = await sqlezeCommand
            .ExecuteReaderAsync(cancellationToken)
            .ConfigureAwait(false);

        return await reader
            .ReadSingleAsync<T>(action, cancellationToken)
            .ConfigureAwait(false);
    }
    public static async Task<ISqlezeReader> ReadSingleNullableAsync<T>(this ISqlezeCommand sqlezeCommand,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
    {
        var reader = await sqlezeCommand
            .ExecuteReaderAsync(cancellationToken)
            .ConfigureAwait(false);

        return await reader
            .ReadSingleNullableAsync<T>(action, cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadSingleOrDefaultAsync<T>(this ISqlezeCommand sqlezeCommand,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
    {
        var reader = await sqlezeCommand
            .ExecuteReaderAsync(cancellationToken)
            .ConfigureAwait(false);

        return await reader
            .ReadSingleOrDefaultAsync<T>(action, cancellationToken)
            .ConfigureAwait(false);
    }
        
    public static async Task<T> ReadSingleAsync<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await scopedSqlezeParameterFactory
            .Command
            .ReadSingleAsync<T>(cancellationToken)
            .ConfigureAwait(false);
    
    public static async Task<T?> ReadSingleNullableAsync<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        CancellationToken cancellationToken = default)
        => await scopedSqlezeParameterFactory
            .Command
            .ReadSingleNullableAsync<T>(cancellationToken)
            .ConfigureAwait(false);

    public static async Task<T?> ReadSingleOrDefaultAsync<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        CancellationToken cancellationToken = default)
        => await scopedSqlezeParameterFactory
            .Command
            .ReadSingleOrDefaultAsync<T>(cancellationToken)
            .ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadSingleAsync<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        Expression<Func<T>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await scopedSqlezeParameterFactory
            .Command
            .ReadSingleAsync<T>(action, cancellationToken)
            .ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadSingleNullableAsync<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
        => await scopedSqlezeParameterFactory
            .Command
            .ReadSingleNullableAsync<T>(action, cancellationToken)
            .ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadSingleOrDefaultAsync<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        Expression<Func<T?>> action,
        CancellationToken cancellationToken = default)
        => await scopedSqlezeParameterFactory
            .Command
            .ReadSingleOrDefaultAsync<T>(action, cancellationToken)
            .ConfigureAwait(false);
}
