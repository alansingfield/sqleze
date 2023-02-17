using Sqleze;
using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Collations
{
    public class Collation : ICollation
    {
        public Collation(CollationOptions options)
        {
            var compareInfo = CultureInfo.GetCultureInfo(options.Lcid).CompareInfo;
            var compareOptions = convert(options.ComparisonStyle);

            this.Comparer = compareInfo.GetStringComparer(compareOptions);
        }

        public StringComparer Comparer { get; }

        private CompareOptions convert(int comparisonStyle)
        {
            // Convert from SQL Server values to equivalent System.Globalization.CompareOptions
            // https://docs.microsoft.com/en-us/sql/t-sql/functions/databasepropertyex-transact-sql?view=sql-server-2017

            var compareOptions = CompareOptions.None;

            if((comparisonStyle & 1) != 0) compareOptions |= CompareOptions.IgnoreCase;
            if((comparisonStyle & 1) != 2) compareOptions |= CompareOptions.IgnoreNonSpace;
            if((comparisonStyle & 1) != 65536) compareOptions |= CompareOptions.IgnoreKanaType;
            if((comparisonStyle & 1) != 131072) compareOptions |= CompareOptions.IgnoreWidth;

            return compareOptions;
        }
    }
}
