using Sqleze.ConnectionStrings;
using Sqleze.DryIoc;
using Sqleze;
using Sqleze.NamingConventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Readers;

namespace Sqleze;
public static class UnmappedColumnsPolicyExtensions
{
    public static ISqlezeBuilder WithUnmappedColumnsPolicy(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        bool throwOnNamed = false,
        bool throwOnUnnamed = false
    )
    {
        return sqlezeConnectionBuilder.With<UnmappedColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedColumnsPolicyOptions(throwOnNamed, throwOnUnnamed));
            });
    }

    public static ISqlezeCommandBuilder WithUnmappedColumnsPolicy(
        this ISqlezeCommandBuilder sqlezeCommandBuilder,
        bool throwOnNamed = false,
        bool throwOnUnnamed = false
    )
    {
        return sqlezeCommandBuilder.With<UnmappedColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedColumnsPolicyOptions(throwOnNamed, throwOnUnnamed));
            });
    }

    public static ISqlezeReaderBuilder WithUnmappedColumnsPolicy(
        this ISqlezeCommand sqlezeCommand,
        bool throwOnNamed = false,
        bool throwOnUnnamed = false)
    {
        return sqlezeCommand.With<UnmappedColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedColumnsPolicyOptions(throwOnNamed, throwOnUnnamed));
            });
    }

    public static ISqlezeRowsetBuilder WithUnmappedColumnsPolicy(
        this ISqlezeRowsetBuilder sqlezeRowsetBuilder,
        bool throwOnNamed = false,
        bool throwOnUnnamed = false
    )
    {
        return sqlezeRowsetBuilder.With<UnmappedColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedColumnsPolicyOptions(throwOnNamed, throwOnUnnamed));
            });
    }
    public static ISqlezeReaderBuilder WithUnmappedColumnsPolicy(
        this ISqlezeReaderBuilder sqlezeReaderBuilder,
        bool throwOnNamed = false,
        bool throwOnUnnamed = false
    )
    {
        return sqlezeReaderBuilder.With<UnmappedColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedColumnsPolicyOptions(throwOnNamed, throwOnUnnamed));
            });
    }


    public static ISqlezeReaderBuilder WithUnmappedColumnsPolicy(
        this ISqlezeParameter sqlezeParameter,
        bool throwOnNamed = false,
        bool throwOnUnnamed = false
    )
    {
        return sqlezeParameter.Command.With<UnmappedColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedColumnsPolicyOptions(throwOnNamed, throwOnUnnamed));
            });
    }

    public static ISqlezeReaderBuilder WithUnmappedColumnsPolicy(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        bool throwOnNamed = false,
        bool throwOnUnnamed = false
    )
    {
        return sqlezeParameterCollection.Command.With<UnmappedColumnsPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedColumnsPolicyOptions(throwOnNamed, throwOnUnnamed));
            });
    }
}
