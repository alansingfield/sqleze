using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Util
{
    public static class DeconstructExtensions
    {
        public static void Deconstruct<TKey, TElement> (
            this IGrouping<TKey, TElement> arg,
            out TKey key,
            out IEnumerable<TElement> elements)
        {
            key = arg.Key;
            elements = arg.AsEnumerable();
        }
    }
}
