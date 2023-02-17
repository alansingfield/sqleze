using Sqleze;
using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public class SqlezeCommandFactory : ISqlezeCommandFactory
{
    private readonly Func<IScopedSqlezeCommandFactory> newScopedSqlezeCommandFactory;

    public SqlezeCommandFactory(Func<IScopedSqlezeCommandFactory> newScopedSqlezeCommandFactory)
    {
        this.newScopedSqlezeCommandFactory = newScopedSqlezeCommandFactory;
    }

    public ISqlezeCommand OpenCommand()
    {
        return newScopedSqlezeCommandFactory().OpenCommand();
    }
}
