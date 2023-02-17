using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public interface ICollation
    {
        StringComparer Comparer { get; }
    }
}
