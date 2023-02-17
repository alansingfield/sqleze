using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Sizing
{
    public interface ISizeMeasurer
    {
        int GetSize();
    }

    public interface ISizeMeasurer<T> : ISizeMeasurer
    {
    }


}
