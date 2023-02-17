using Sqleze.Converters.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public interface ISqlezeRowset 
    {
        TMetadata GetMetadata<TMetadata>();
    }

    public interface ISqlezeRowset<T> : ISqlezeRowset
    {
        IEnumerable<T> Enumerate();
        IAsyncEnumerable<T> EnumerateAsync(CancellationToken cancellationToken = default);
    }
}
