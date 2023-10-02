using Sqleze.Dynamics;
using Sqleze;
using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class ReadArrayExtensions
{
    public static T[] ReadArray<T>(this ISqlezeReader sqlezeReader)
    where T : notnull
    {
        return sqlezeReader
            .WithScalarReaderFallbackPolicy(useDefaultInsteadOfNull: true)
            .OpenRowset<T>()
            .Enumerate()
            .ToArray();
    }

    public static T[] ReadArrayNullable<T>(this ISqlezeReader sqlezeReader)
    {
        return sqlezeReader
            .OpenRowsetNullable<T>()
            .Enumerate()
            .ToArray();
    }

    public static ISqlezeReader ReadArray<T>(this ISqlezeReader sqlezeReader, out T[] result)
    where T : notnull
    {
        result = sqlezeReader.ReadArray<T>();
        return sqlezeReader;
    }

    public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeReader sqlezeReader, out T?[] result)
    {
        result = sqlezeReader.ReadArrayNullable<T?>();
        return sqlezeReader;
    }

    public static ISqlezeReader ReadArray<T>(this ISqlezeReader sqlezeReader, Expression<Func<T[]>> action)
    where T : notnull
    {
        var info = ExpressionSetter.Prepare(action);
        info.Setter(sqlezeReader.ReadArray<T>());

        return sqlezeReader;
    }

    public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeReader sqlezeReader, Expression<Func<T?[]>> action)
    {
        var info = ExpressionSetter.Prepare(action);
        info.Setter(sqlezeReader.ReadArrayNullable<T?>());

        return sqlezeReader;
    }

    public static T[] ReadArray<T>(this ISqlezeCommand sqlezeCommand)
        where T : notnull
        => sqlezeCommand
            .ExecuteReader()
            .ReadArray<T>();

    public static T?[] ReadArrayNullable<T>(this ISqlezeCommand sqlezeCommand)
        => sqlezeCommand
            .ExecuteReader()
            .ReadArrayNullable<T?>();

    public static ISqlezeReader ReadArray<T>(this ISqlezeCommand sqlezeCommand, Expression<Func<T[]>> action)
    where T : notnull
    {
        var sqlezeReader = sqlezeCommand.ExecuteReader();
        sqlezeReader.ReadArray<T>(action);

        return sqlezeReader;
    }

    public static ISqlezeReader ReadArrayNullable<T>(this ISqlezeCommand sqlezeCommand, Expression<Func<T?[]>> action)
    {
        var sqlezeReader = sqlezeCommand.ExecuteReader();
        sqlezeReader.ReadArrayNullable<T>(action);

        return sqlezeReader;
    }


    public static T[] ReadArray<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory)
        where T : notnull
        => scopedSqlezeParameterFactory.Command.ReadArray<T>();

    public static T?[] ReadArrayNullable<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory)
        => scopedSqlezeParameterFactory.Command.ReadArrayNullable<T?>();

    public static ISqlezeReader ReadArray<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        Expression<Func<T[]>> action)
    where T : notnull 
        => scopedSqlezeParameterFactory.Command.ExecuteReader().ReadArray<T>(action);

    public static ISqlezeReader ReadArrayNullable<T>(this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        Expression<Func<T?[]>> action)
        => scopedSqlezeParameterFactory.Command.ReadArrayNullable<T>(action);


    // async versions. Note we can't have the OUT parameter ones due to how async works.

    public static async Task<T[]> ReadArrayAsync<T>(this ISqlezeReader sqlezeReader, CancellationToken cancellationToken = default)
        where T : notnull
    {
        return await sqlezeReader
            .WithScalarReaderFallbackPolicy(useDefaultInsteadOfNull: true)
            .OpenRowset<T>()
            .EnumerateAsync(cancellationToken)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<T?[]> ReadArrayNullableAsync<T>(this ISqlezeReader sqlezeReader, CancellationToken cancellationToken = default)
    {
        return await sqlezeReader
            .OpenRowsetNullable<T?>()
            .EnumerateAsync(cancellationToken)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
    }



    public static async Task<ISqlezeReader> ReadArrayAsync<T>(this ISqlezeReader sqlezeReader, Expression<Func<T[]>> action, CancellationToken cancellationToken = default)
    where T : notnull
    {
        var result = await sqlezeReader
            .ReadArrayAsync<T>(cancellationToken)
            .ConfigureAwait(false);

        var info = ExpressionSetter.Prepare(action);
        info.Setter(result);

        return sqlezeReader;
    }

    public static async Task<ISqlezeReader> ReadArrayNullableAsync<T>(this ISqlezeReader sqlezeReader, Expression<Func<T?[]>> action, CancellationToken cancellationToken = default)
    {
        var result = await sqlezeReader
            .ReadArrayNullableAsync<T?>(cancellationToken)
            .ConfigureAwait(false);

        var info = ExpressionSetter.Prepare(action);
        info.Setter(result);

        return sqlezeReader;
    }

    public static async Task<T[]> ReadArrayAsync<T>(this ISqlezeCommand sqlezeCommand, CancellationToken cancellationToken = default)
        where T : notnull
    {
        return await
            (
                await sqlezeCommand
                    .ExecuteReaderAsync(null, cancellationToken)
                    .ConfigureAwait(false)
            )
            .ReadArrayAsync<T>(cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<T?[]> ReadArrayNullableAsync<T>(this ISqlezeCommand sqlezeCommand, CancellationToken cancellationToken = default)
    {
        return await
            (
                await sqlezeCommand
                    .ExecuteReaderAsync(null, cancellationToken)
                    .ConfigureAwait(false)
            )
            .ReadArrayNullableAsync<T>(cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadArrayAsync<T>(
        this ISqlezeCommand sqlezeCommand,
        Expression<Func<T[]>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        var reader = await sqlezeCommand
            .ExecuteReaderAsync(null, cancellationToken)
            .ConfigureAwait(false);

        return await reader.ReadArrayAsync<T>(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadArrayNullableAsync<T>(
        this ISqlezeCommand sqlezeCommand,
        Expression<Func<T?[]>> action,
        CancellationToken cancellationToken = default)
    {
        var reader = await sqlezeCommand
            .ExecuteReaderAsync(null, cancellationToken)
            .ConfigureAwait(false);

        return await reader.ReadArrayNullableAsync<T?>(action, cancellationToken).ConfigureAwait(false);
    }


    public static async Task<T[]> ReadArrayAsync<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        CancellationToken cancellationToken = default)
        where T : notnull 
        => await scopedSqlezeParameterFactory
                .Command
                .ReadArrayAsync<T>(cancellationToken)
                .ConfigureAwait(false);

    public static async Task<T?[]> ReadArrayNullableAsync<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        CancellationToken cancellationToken = default)
        => await scopedSqlezeParameterFactory
                .Command
                .ReadArrayNullableAsync<T>(cancellationToken)
                .ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadArrayAsync<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        Expression<Func<T[]>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
        => await scopedSqlezeParameterFactory
                .Command
                .ReadArrayAsync<T>(action, cancellationToken)
                .ConfigureAwait(false);

    public static async Task<ISqlezeReader> ReadArrayNullableAsync<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
        Expression<Func<T?[]>> action,
        CancellationToken cancellationToken = default)
        => await scopedSqlezeParameterFactory
                .Command
                .ReadArrayNullableAsync<T?>(action, cancellationToken).ConfigureAwait(false);


    public static async Task<ISqlezeReader> ReadArrayAsync<T>(
        this Task<ISqlezeReader> sqlezeReaderTask,
        Expression<Func<T[]>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        var reader = await sqlezeReaderTask.ConfigureAwait(false);

        return await reader.ReadArrayAsync<T>(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadArrayNullableAsync<T>(
        this Task<ISqlezeReader> sqlezeReaderTask,
        Expression<Func<T?[]>> action,
        CancellationToken cancellationToken = default)
    {
        var reader = await sqlezeReaderTask.ConfigureAwait(false);

        return await reader.ReadArrayNullableAsync<T?>(action, cancellationToken).ConfigureAwait(false);
    }

}
