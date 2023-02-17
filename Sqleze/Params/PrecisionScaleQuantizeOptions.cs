using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params;

public record PrecisionScaleQuantizeOptions
(
    ImmutableArray<(byte precision, byte scale)> PrecisionScales
);


public class PrecisionScaleQuantizeRoot { }
