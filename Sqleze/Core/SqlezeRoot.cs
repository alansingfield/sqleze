using Microsoft.Extensions.Configuration;
using Sqleze;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public interface ISqlezeRoot
{
    ISqlezeBuilder Builder { get; }

    ISqleze Factory(string connectionString);
}

public class SqlezeRoot : ISqlezeRoot
{
    private readonly ISqlezeBuilder sqlezeBuilder;
    private readonly ConcurrentDictionary<string, ISqleze> sqlezeFactories;

    public SqlezeRoot(
        ISqlezeBuilder sqlezeBuilder
    )
    {
        this.sqlezeBuilder = sqlezeBuilder;

        sqlezeFactories = new();
    }

    public ISqlezeBuilder Builder => sqlezeBuilder;

    public ISqleze Factory(string connectionString)
    {
        return sqlezeFactories.GetOrAdd(connectionString,
            x => sqlezeBuilder.WithConnectionString(x).Build());
    }
}
