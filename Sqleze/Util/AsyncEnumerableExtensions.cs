using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Util
{
    public static class AsyncEnumerableExtensions
    {
        public static async Task<List<T>> ToListAsync<T>(
            this IAsyncEnumerable<T> source,
            CancellationToken cancellationToken = default)
        {
            var result = new List<T>();
            await foreach(var itm in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                result.Add(itm);
            }
            return result;
        }

        public static async Task<T[]> ToArrayAsync<T>(
            this IAsyncEnumerable<T> source,
            CancellationToken cancellationToken = default)
        {
            return (await source.ToListAsync().ConfigureAwait(false)).ToArray();
        }

        public async static Task<TSource> SingleAsync<TSource>(
            this IAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            var enu = source.WithCancellation(cancellationToken)
                .ConfigureAwait(false)
                .GetAsyncEnumerator();

            await using(enu)
            {
                if(!await enu.MoveNextAsync())
                    throw new InvalidOperationException("No elements found in sequence");

                TSource result = enu.Current;

                if(await enu.MoveNextAsync())
                    throw new InvalidOperationException("More than one element matched");

                return result;
            }
        }

        public async static Task<TSource?> SingleOrDefaultAsync<TSource>(
            this IAsyncEnumerable<TSource> source,
            CancellationToken cancellationToken = default)
        {
            var enu = source.WithCancellation(cancellationToken)
                .ConfigureAwait(false)
                .GetAsyncEnumerator();

            await using(enu)
            {
                if(!await enu.MoveNextAsync())
                    return default;

                TSource result = enu.Current;

                if(await enu.MoveNextAsync())
                    throw new InvalidOperationException("More than one element matched");

                return result;
            }
        }

    }
}
