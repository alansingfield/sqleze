using Sqleze.Dynamics;
using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Readers
{
    public interface IBestMatchConstructor<T>
    {
        Func<object?[], T> Find(IReadOnlyList<IColumnProperty<T>> columnPropertyMappings);
    }

    public class BestMatchConstructor<T> : IBestMatchConstructor<T>
    {
        private readonly IConstructorLambdaCache<T> constructorLambdaCache;

        public BestMatchConstructor(IConstructorLambdaCache<T> constructorLambdaCache)
        {
            this.constructorLambdaCache = constructorLambdaCache;
        }

        public Func<object?[], T> Find(IReadOnlyList<IColumnProperty<T>> columnPropertyMappings)
        {
            // Pick the most appropriate constructor along with a mapping array which tells us
            // which position in the values array should provide each constructor param.
            var consWithMap = chooseBestConstructor(columnPropertyMappings);

            // Get a func for constructing the object with supplied parameters
            var consFunc = constructorLambdaCache.GetConstructorFunc(consWithMap.ConstructorInfo);

            // Capture consParamMap in a closure and return
            return arg => consFunc(arg, consWithMap.Map);
        }

        private ConsWithMap chooseBestConstructor(
            IReadOnlyList<IColumnProperty<T>> columnPropertyMappings)
        {
            // Indices into CPM array of properties without setters (constructor only)
            // So if the 1st, 3rd and 4th columns returned by SQL correspond to properties without setters,
            // this array will be [0, 2, 3] (0-based)

            var consOnlyIndices = columnPropertyMappings
                .Select((x, idx) => x.PropertyConsOnly ? idx : (int?)null)
                .Where(x => x != null)
                .Select(x => x!.Value)
                .ToArray();

            // If no cons-onlies and there is a parameterless constructor, use that.
            if(!consOnlyIndices.Any())
            {
                var parameterlessCons = typeof(T).GetConstructor(Type.EmptyTypes);
                if(parameterlessCons != null)
                    return new ConsWithMap(parameterlessCons, Array.Empty<int>());
            }

            var choices = typeof(T).GetConstructors().Select(x => new
            {
                ConstructorInfo = x,
                // Name is not going to be null; this only occurs on return parameters.
                ConsParamNames = x.GetParameters().Select(x => x.Name ?? "").ToArray()
            })
            .Select(x => new
            {
                x.ConstructorInfo,
                Map = constructorParamMap(x.ConsParamNames, columnPropertyMappings)
            })
            .Select(x => new
            {
                ConsWithMap = new ConsWithMap(x.ConstructorInfo, x.Map),
                ConsParamIndices = x.Map.Where(y => y != -1).ToArray(),
            })
            .Select(x => new
            {
                x.ConsWithMap,
                Intersection = consOnlyIndices.Intersect(x.ConsParamIndices).ToArray(),
                ExtraConsOnly = consOnlyIndices.Except(x.ConsParamIndices).ToArray(),
                ExtraConsParam = x.ConsParamIndices.Except(consOnlyIndices).ToArray(),
            })
            .ToList()
            ;

            // Find a constructor with EXACT match for our list of cons-only properties
            // (regardless of ordering)
            var exact = choices.Where(x =>
                x.Intersection.Length == consOnlyIndices.Length
                && x.ExtraConsOnly.Length == 0
                && x.ExtraConsParam.Length == 0)
                .ToList();

            if(exact.Count == 1)
                return exact[0].ConsWithMap;

            if(exact.Count > 0)
                throw new Exception($"Unable to choose a constructor for type {typeof(T).Name} because more than one constructor exactly matches the columns received from SQL.");

            // Find a constructor which covers all our cons-only properties but maybe has a few
            // more constructor parameters which we will have to set to default values.

            var withExtraConsParams = choices.Where(x =>
                   x.Intersection.Length == consOnlyIndices.Length
                && x.ExtraConsOnly.Length == 0)
                .ToList();

            if(withExtraConsParams.Count == 1)
                return withExtraConsParams[0].ConsWithMap;

            if(withExtraConsParams.Count > 1)
                throw new Exception($"Unable to choose a constructor for type {typeof(T).Name} because more than one constructor matches columns received from SQL.");

            throw new Exception($"Unable to choose a constructor for type {typeof(T).Name} because no constructor found which populates every SQL-mapped init property.");
        }

        private int[] constructorParamMap(
            string[] consParams,
            IReadOnlyList<IColumnProperty<T>> columnPropertyMappings)
        {
            // Result is one element for each constructor parameter. Start with -1 meaning no matching
            // column found.
            var map = consParams.Select(x => new MapEntry(x)
            {
                Result = -1
            })
            .ToArray();

            // Search each column received and attempt to match to a constructor parameter
            foreach(var (cpm, cpmIndex) in columnPropertyMappings.SelectIndexed())
            {
                string propName = cpm.PropertyName;

                // Find a match (case-insensitive) and exclude ones we've already matched.
                var itm = map.FirstOrDefault(x =>
                    String.Equals(x.ConsParamName, propName, StringComparison.OrdinalIgnoreCase)
                    && x.Result == -1);

                if(itm != null)
                    itm.Result = cpmIndex;
            }

            return map.Select(x => x.Result).ToArray();
        }

        private record MapEntry(string ConsParamName)
        {
            public int Result { get; set; }
        }

        private record ConsWithMap(ConstructorInfo ConstructorInfo, int[] Map);

    }
}
