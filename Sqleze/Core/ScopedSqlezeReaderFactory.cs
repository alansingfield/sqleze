using Sqleze;
using Sqleze.Options;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public class ScopedSqlezeReaderFactory : IScopedSqlezeReaderFactory
{
    private readonly Func<ISqlezeReaderProvider> newSqlezeReaderProvider;
    private readonly IEnumerable<ICommandPreExecute> commandPreExecutes;

    public ScopedSqlezeReaderFactory(
        Func<ISqlezeReaderProvider> newSqlezeReaderProvider,
        IEnumerable<ICommandPreExecute> commandPreExecutes)
    {
        this.newSqlezeReaderProvider = newSqlezeReaderProvider;
        this.commandPreExecutes = commandPreExecutes;
    }

    public IEnumerable<ICommandPreExecute> GetCommandPreExecutes()
    {
        return commandPreExecutes;
    }

    public ISqlezeReader OpenReader()
    {
        // The new() func creates a new scope, returns the SqlezeReader from within.
        return newSqlezeReaderProvider().SqlezeReader;
    }
}


