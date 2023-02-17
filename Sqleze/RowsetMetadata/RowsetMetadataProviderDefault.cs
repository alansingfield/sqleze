using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.RowsetMetadata;
public class RowsetMetadataProviderDefault<T> : IRowsetMetadataProvider<T> where T : notnull
{
    public T GetMetadata()
    {
        throw new NotImplementedException("The requested metadata type has not been registered");
    }
}
