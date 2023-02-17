using Sqleze.ConnectionStrings;
using Sqleze.DryIoc;
using Sqleze;
using Sqleze.NamingConventions;
using Sqleze.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public static class CheckedAdoDataReaderExtensions
{
    public static ISqlezeBuilder WithForceSynchronousRead(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        bool force
    )
    {
        return sqlezeConnectionBuilder.With<CheckedAdoDataReaderRoot>(
            (root, scope) =>
            {
                scope.Use(new CheckedAdoDataReaderOptions(UseSynchronousReadWhenAsync: force));
            });
    }

    public static ISqlezeCommandBuilder WithForceSynchronousRead(
        this ISqlezeConnection sqlezeConnection,
        bool force
    )
    {
        return sqlezeConnection.With<CheckedAdoDataReaderRoot>(
            (root, scope) =>
            {
                scope.Use(new CheckedAdoDataReaderOptions(UseSynchronousReadWhenAsync: force));
            });
    }
    public static ISqlezeCommandBuilder WithForceSynchronousRead(
        this ISqlezeCommandBuilder sqlezeCommandBuilder,
        bool force
    )
    {
        return sqlezeCommandBuilder.With<CheckedAdoDataReaderRoot>(
            (root, scope) =>
            {
                scope.Use(new CheckedAdoDataReaderOptions(UseSynchronousReadWhenAsync: force));
            });
    }

    public static ISqlezeReaderBuilder WithForceSynchronousRead(
        this ISqlezeCommand sqlezeCommand,
        bool force)
    {
        return sqlezeCommand.With<CheckedAdoDataReaderRoot>(
            (root, scope) =>
            {
                scope.Use(new CheckedAdoDataReaderOptions(UseSynchronousReadWhenAsync: force));
            });
    }

    public static ISqlezeRowsetBuilder WithForceSynchronousRead(
        this ISqlezeRowsetBuilder sqlezeRowsetBuilder,
        bool force
    )
    {
        return sqlezeRowsetBuilder.With<CheckedAdoDataReaderRoot>(
            (root, scope) =>
            {
                scope.Use(new CheckedAdoDataReaderOptions(UseSynchronousReadWhenAsync: force));
            });
    }
    public static ISqlezeReaderBuilder WithForceSynchronousRead(
        this ISqlezeReaderBuilder sqlezeReaderBuilder,
        bool force
    )
    {
        return sqlezeReaderBuilder.With<CheckedAdoDataReaderRoot>(
            (root, scope) =>
            {
                scope.Use(new CheckedAdoDataReaderOptions(UseSynchronousReadWhenAsync: force));
            });
    }
}
