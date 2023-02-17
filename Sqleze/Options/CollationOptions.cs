using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Options
{
    // Default is for SQL_Latin1_General_CP1_CI_AS / Latin1_General_CI_AS
    public class CollationOptions
    {
        public int Lcid { get; set; } = 1033;
        public int ComparisonStyle { get; set; } = 196609;
    }
}
