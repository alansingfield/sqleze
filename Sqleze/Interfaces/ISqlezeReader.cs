using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public interface ISqlezeReader
    {
        ISqlezeRowset<T> OpenRowsetNullable<T>();
        ISqlezeRowsetBuilder With<T>(Action<T, ISqlezeScope> configure);
    }
}
