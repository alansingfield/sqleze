using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public interface ISqlezeRoot
{
    ISqlezeBuilder OpenBuilder();
}

public class SqlezeRoot : ISqlezeRoot
{
    private readonly ISqlezeBuilder builder;
    public SqlezeRoot(ISqlezeBuilder builder)
    {
        this.builder = builder;
    }

    public ISqlezeBuilder OpenBuilder() => builder;
}


// ABSOLUTE root is called Core - this is static with a global container.
// Only in non-DLL version?
// Could have methods to do the registration using reflection perhaps?
// SqlezeCore.Root gives you ISqlezeRoot (based on a default static)
// SqlezeCore.SetConfiguration(IConfiguration)
// SqlezeCore.Builder
// SqlezeCore.Factory
// SqlezeCore.Connect(x) - extension method
// SqlezeCore.NewRoot() makes another container for you
// SqlezeCore.Root.Register(xxx) - for the config methods.

