using Sqleze.Timeout;
using Sqleze;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class TimeoutExtensions
{
    public static ISqlezeBuilder WithCommandTimeout(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        int timeoutSeconds)
    {
        return sqlezeConnectionBuilder.With<CommandTimeoutRoot>(
        (root, scope) =>
        {
            scope.Use(new CommandTimeoutOptions(timeoutSeconds));
        });
    }

    public static ISqlezeCommandBuilder WithCommandTimeout(
        this ISqlezeConnection sqlezeConnection,
        int timeoutSeconds
    )
    {
        return sqlezeConnection.With<CommandTimeoutRoot>(
            (root, scope) =>
            {
                scope.Use(new CommandTimeoutOptions(timeoutSeconds));
            });
    }

    public static ISqlezeCommandBuilder WithCommandTimeout(
        this ISqlezeCommandBuilder sqlezeCommandBuilder,
        int timeoutSeconds
    )
    {
        return sqlezeCommandBuilder.With<CommandTimeoutRoot>(
            (root, scope) =>
            {
                scope.Use(new CommandTimeoutOptions(timeoutSeconds));
            });
    }

    public static ISqlezeReaderBuilder WithCommandTimeout(
        this ISqlezeCommand sqlezeCommand,
        int timeoutSeconds
    )
    {
        return sqlezeCommand.With<CommandTimeoutRoot>(
            (root, scope) =>
            {
                scope.Use(new CommandTimeoutOptions(timeoutSeconds));
            });
    }

    public static ISqlezeReaderBuilder WithCommandTimeout(
        this ISqlezeReaderBuilder sqlezeReaderBuilder,
        int timeoutSeconds
    )
    {
        return sqlezeReaderBuilder.With<CommandTimeoutRoot>(
            (root, scope) =>
            {
                scope.Use(new CommandTimeoutOptions(timeoutSeconds));
            });
    }
}
