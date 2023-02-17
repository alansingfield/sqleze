using Sqleze.Converters.Metadata;
using Sqleze.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.RowsetMetadata;

public interface IRowsetMetadataProvider
{
}

public interface IRowsetMetadataProvider<TMetatadata> : IRowsetMetadataProvider
{
    TMetatadata GetMetadata();
}
