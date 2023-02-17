using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public class ScopedSqlezeConnectionFactory : IScopedSqlezeConnectionFactory
{
    private readonly Func<ISqlezeConnectionProvider> newSqlezeConnectionProvider;

    public ScopedSqlezeConnectionFactory(
        Func<ISqlezeConnectionProvider> newSqlezeConnectionProvider)
    {
        // Each call to this func creates a new provider with a new scope
        this.newSqlezeConnectionProvider = newSqlezeConnectionProvider;
    }

    public ISqlezeConnection Connect() => newSqlezeConnectionProvider().SqlezeConnection;
}