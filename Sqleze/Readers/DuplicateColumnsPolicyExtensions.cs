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

public static class DuplicateColumnsPolicyExtensions
{
    public static ISqlezeBuilder WithDuplicateColumnsPolicy(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        bool throwIfDuplicate
    )
    {
        return sqlezeConnectionBuilder.With<DuplicateColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new DuplicateColumnsPolicyOptions(throwIfDuplicate));
            });
    }

    public static ISqlezeCommandBuilder WithDuplicateColumnsPolicy(
        this ISqlezeCommandBuilder sqlezeCommandBuilder,
        bool throwIfDuplicate
    )
    {
        return sqlezeCommandBuilder.With<DuplicateColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new DuplicateColumnsPolicyOptions(throwIfDuplicate));
            });
    }

    public static ISqlezeReaderBuilder WithDuplicateColumnsPolicy(
        this ISqlezeCommand sqlezeCommand,
        bool throwIfDuplicate)
    {
        return sqlezeCommand.With<DuplicateColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new DuplicateColumnsPolicyOptions(throwIfDuplicate));
            });
    }

    public static ISqlezeRowsetBuilder WithDuplicateColumnsPolicy(
        this ISqlezeRowsetBuilder sqlezeRowsetBuilder,
        bool throwIfDuplicate
    )
    {
        return sqlezeRowsetBuilder.With<DuplicateColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new DuplicateColumnsPolicyOptions(throwIfDuplicate));
            });
    }
    public static ISqlezeReaderBuilder WithDuplicateColumnsPolicy(
        this ISqlezeReaderBuilder sqlezeReaderBuilder,
        bool throwIfDuplicate
    )
    {
        return sqlezeReaderBuilder.With<DuplicateColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new DuplicateColumnsPolicyOptions(throwIfDuplicate));
            });
    }
}
