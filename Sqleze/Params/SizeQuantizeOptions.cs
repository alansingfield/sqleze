using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params;

public interface ISizeQuantizeOptions
{
    ImmutableArray<int> Sizes { get; }
}

public record NVarCharSizeQuantizeOptions(
    ImmutableArray<int> Sizes
) : ISizeQuantizeOptions;

public record VarCharSizeQuantizeOptions(
    ImmutableArray<int> Sizes
) : ISizeQuantizeOptions;

public record VarBinarySizeQuantizeOptions(
    ImmutableArray<int> Sizes
) : ISizeQuantizeOptions;


public class NVarCharSizeQuantizeRoot { }
public class VarCharSizeQuantizeRoot { }
public class VarBinarySizeQuantizeRoot { }
