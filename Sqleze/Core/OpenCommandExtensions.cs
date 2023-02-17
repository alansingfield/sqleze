using Sqleze;
using Sqleze.Options;

namespace Sqleze;

public static class OpenCommandExtensions
{
    public static ISqlezeCommand OpenCommand(this ISqlezeConnection sqlezeConnection, string sql, bool isStoredProc)
    {
        var builder = sqlezeConnection.WithCommandText(sql, isStoredProc);

        return builder.Build().OpenCommand();
    }

    public static ISqlezeCommand OpenCommand(this ISqlezeCommandBuilder sqlezeCommandBuilder, string sql, bool isStoredProc)
    {
        var builder = sqlezeCommandBuilder.WithCommandText(sql, isStoredProc);

        return builder.Build().OpenCommand();
    }

    public static ISqlezeCommandBuilder WithCommandText(this ISqlezeCommandBuilder sqlezeCommandBuilder, string sql, bool isStoredProc = false)
    {
        return sqlezeCommandBuilder.With<CommandTextRoot>((root, scope) =>
        {
            scope.Use(new CommandCreateOptions(sql, isStoredProc));
        });
    }
    public static ISqlezeCommandBuilder WithCommandText(this ISqlezeConnection sqlezeConnection, string sql, bool isStoredProc = false)
    {
        return sqlezeConnection.With<CommandTextRoot>((root, scope) =>
        {
            scope.Use(new CommandCreateOptions(sql, isStoredProc));
        });
    }

    public static ISqlezeCommandBuilder WithSql(this ISqlezeCommandBuilder sqlezeCommandBuilder, string sql)
        => sqlezeCommandBuilder.WithCommandText(sql, false);

    public static ISqlezeCommandBuilder WithSql(this ISqlezeConnection sqlezeConnection, string sql)
        => sqlezeConnection.WithCommandText(sql, false);

    public static ISqlezeCommandBuilder WithStoredProc(this ISqlezeCommandBuilder sqlezeCommandBuilder, string storedProcName)
        => sqlezeCommandBuilder.WithCommandText(storedProcName, true);

    public static ISqlezeCommandBuilder WithStoredProc(this ISqlezeConnection sqlezeConnection, string storedProcName)
        => sqlezeConnection.WithCommandText(storedProcName, true);
}

public class CommandTextRoot { }
