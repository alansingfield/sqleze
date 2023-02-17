using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sqleze.Metadata.StoredProcParamDefinition;

namespace Sqleze.Metadata
{
    public record StoredProcParamDefinition
    (
        int ParameterId,
        string ParameterName,
        string ParameterType,
        bool IsTableType,
        bool IsAssemblyType,
        int Length,
        byte Precision,
        byte Scale,
        bool IsOutput,
        bool IsReturn
    );
}
