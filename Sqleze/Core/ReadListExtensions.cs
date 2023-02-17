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
public static class ReadListExtensions
{
    public static List<T> ReadList<T>(this ISqlezeReader sqlezeReader)
    where T : notnull
    {
        return sqlezeReader
            .WithScalarReaderFallbackPolicy(useDefaultInsteadOfNull: true)
            .OpenRowset<T>()
            .Enumerate()
            .ToList();
    }

    public static List<T?> ReadListNullable<T>(this ISqlezeReader sqlezeReader)
    {
        return sqlezeReader
            .OpenRowsetNullable<T?>()
            .Enumerate()
            .ToList();
    }

    public static ISqlezeReader ReadList<T>(this ISqlezeReader sqlezeReader, out List<T> result)
        where T : notnull
    {
        result = sqlezeReader.ReadList<T>();
        return sqlezeReader;
    }

    public static ISqlezeReader ReadListNullable<T>(this ISqlezeReader sqlezeReader, out List<T?> result)
    {
        result = sqlezeReader.ReadListNullable<T?>();
        return sqlezeReader;
    }

    public static ISqlezeReader ReadList<T>(this ISqlezeReader sqlezeReader, Expression<Func<List<T>>> action)
        where T : notnull
    {
        var info = ExpressionSetter.Prepare(action);
        info.Setter(sqlezeReader.ReadList<T>());

        return sqlezeReader;
    }

    public static ISqlezeReader ReadListNullable<T>(this ISqlezeReader sqlezeReader, Expression<Func<List<T?>>> action)
    {
        var info = ExpressionSetter.Prepare(action);
        info.Setter(sqlezeReader.ReadListNullable<T?>());

        return sqlezeReader;
    }

    public static List<T> ReadList<T>(this ISqlezeCommand sqlezeCommand)
        where T : notnull
        => sqlezeCommand
            .ExecuteReader()
            .ReadList<T>();

    public static List<T?> ReadListNullable<T>(this ISqlezeCommand sqlezeCommand)
        => sqlezeCommand
            .ExecuteReader()
            .ReadListNullable<T?>();


    public static ISqlezeReader ReadList<T>(this ISqlezeCommand sqlezeCommand, Expression<Func<List<T>>> action)
        where T : notnull
    {
        var sqlezeReader = sqlezeCommand.ExecuteReader();
        sqlezeReader.ReadList<T>(action);

        return sqlezeReader;
    }

    public static ISqlezeReader ReadListNullable<T>(this ISqlezeCommand sqlezeCommand, Expression<Func<List<T?>>> action)
    {
        var sqlezeReader = sqlezeCommand.ExecuteReader();
        sqlezeReader.ReadListNullable<T>(action);

        return sqlezeReader;
    }


    // async versions. Note we can't have the OUT parameter ones due to how async works.

    public static async Task<List<T>> ReadListAsync<T>(this ISqlezeReader sqlezeReader, CancellationToken cancellationToken = default)
        where T : notnull
    {
        return await sqlezeReader
            .WithScalarReaderFallbackPolicy(useDefaultInsteadOfNull: true)
            .OpenRowset<T>()
            .EnumerateAsync(cancellationToken)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<List<T?>> ReadListNullableAsync<T>(this ISqlezeReader sqlezeReader, CancellationToken cancellationToken = default)
    {
        return await sqlezeReader
            .OpenRowsetNullable<T?>()
            .EnumerateAsync(cancellationToken)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }



    public static async Task<ISqlezeReader> ReadListAsync<T>(this ISqlezeReader sqlezeReader, Expression<Func<List<T>>> action, CancellationToken cancellationToken = default)
    where T : notnull
    {
        var result = await sqlezeReader
            .ReadListAsync<T>(cancellationToken)
            .ConfigureAwait(false);

        var info = ExpressionSetter.Prepare(action);
        info.Setter(result);

        return sqlezeReader;
    }

    public static async Task<ISqlezeReader> ReadListNullableAsync<T>(this ISqlezeReader sqlezeReader, Expression<Func<List<T?>>> action, CancellationToken cancellationToken = default)
    {
        var result = await sqlezeReader
            .ReadListNullableAsync<T?>(cancellationToken)
            .ConfigureAwait(false);

        var info = ExpressionSetter.Prepare(action);
        info.Setter(result);

        return sqlezeReader;
    }

    public static async Task<List<T>> ReadListAsync<T>(this ISqlezeCommand sqlezeCommand, CancellationToken cancellationToken = default)
        where T : notnull
    {
        return await
            (
                await sqlezeCommand
                    .ExecuteReaderAsync(null, cancellationToken)
                    .ConfigureAwait(false)
            )
            .ReadListAsync<T>(cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<List<T?>> ReadListNullableAsync<T>(this ISqlezeCommand sqlezeCommand, CancellationToken cancellationToken = default)
    {
        return await
            (
                await sqlezeCommand
                    .ExecuteReaderAsync(null, cancellationToken)
                    .ConfigureAwait(false)
            )
            .ReadListNullableAsync<T>(cancellationToken)
            .ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadListAsync<T>(
        this ISqlezeCommand sqlezeCommand,
        Expression<Func<List<T>>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        var reader = await sqlezeCommand
            .ExecuteReaderAsync(null, cancellationToken)
            .ConfigureAwait(false);

        return await reader.ReadListAsync<T>(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadListNullableAsync<T>(
        this ISqlezeCommand sqlezeCommand,
        Expression<Func<List<T?>>> action,
        CancellationToken cancellationToken = default)
    {
        var reader = await sqlezeCommand
            .ExecuteReaderAsync(null, cancellationToken)
            .ConfigureAwait(false);

        return await reader.ReadListNullableAsync<T?>(action, cancellationToken).ConfigureAwait(false);
    }


    public static async Task<ISqlezeReader> ReadListAsync<T>(
        this Task<ISqlezeReader> sqlezeReaderTask,
        Expression<Func<List<T>>> action,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        var reader = await sqlezeReaderTask.ConfigureAwait(false);
        
        return await reader.ReadListAsync<T>(action, cancellationToken).ConfigureAwait(false);
    }

    public static async Task<ISqlezeReader> ReadListNullableAsync<T>(
        this Task<ISqlezeReader> sqlezeReaderTask,
        Expression<Func<List<T?>>> action,
        CancellationToken cancellationToken = default)
    {
        var reader = await sqlezeReaderTask.ConfigureAwait(false);

        return await reader.ReadListNullableAsync<T?>(action, cancellationToken).ConfigureAwait(false);
    }

}
