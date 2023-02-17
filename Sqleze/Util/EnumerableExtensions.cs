using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Util;

public static class EnumerableExtensions
{
    public static IEnumerable<(TOuter, TInner)> Join<TOuter, TInner, TKey>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector)
    {
        return outer.Join(
            inner,
            outerKeySelector,
            innerKeySelector,
            (Outer, Inner) => (Outer, Inner));
    }

    /// <summary>
    /// Convenience method to add an index number to a foreach iteration.
    /// foreach(var (item, index) in enumerable.SelectIndexed())
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public static IEnumerable<(T Item, int Index)> SelectIndexed<T>(this IEnumerable<T> items)
    {
        return items.Select((Item, Index) => (Item, Index));
    }


    //public static bool SequenceEqualUnordered<TSource>(
    //    this IEnumerable<TSource> first,
    //    IEnumerable<TSource> second, IEqualityComparer<TSource>? comparer)
    //{
    //    if(second == null)
    //        return false;

    //    if(first.Except(second, comparer).Any())
    //        return false;

    //    if(second.Except(first, comparer).Any())
    //        return false;




    //}
}
