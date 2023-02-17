using Sqleze;
using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public class ScopedSqlezeCommandFactory : IScopedSqlezeCommandFactory
{
    private readonly Func<ISqlezeCommandProvider> newSqlezeCommandProvider;

    public ScopedSqlezeCommandFactory(
        Func<ISqlezeCommandProvider> newSqlezeCommandProvider)
    {
        this.newSqlezeCommandProvider = newSqlezeCommandProvider;
    }

    public ISqlezeCommand OpenCommand()
    {
        return newSqlezeCommandProvider()
            .SqlezeCommand;
    }
}

