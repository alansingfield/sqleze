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
public static class ScalarReaderFallbackPolicyExtensions
{
    public static ISqlezeBuilder WithScalarReaderFallbackPolicy(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        bool useDefaultInsteadOfNull
    )
    {
        return sqlezeConnectionBuilder.With<ScalarReaderFallbackPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new ScalarReaderFallbackPolicyOptions(useDefaultInsteadOfNull));
            });
    }

    public static ISqlezeCommandBuilder WithScalarReaderFallbackPolicy(
        this ISqlezeCommandBuilder sqlezeCommandBuilder,
        bool useDefaultInsteadOfNull
    )
    {
        return sqlezeCommandBuilder.With<ScalarReaderFallbackPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new ScalarReaderFallbackPolicyOptions(useDefaultInsteadOfNull));
            });
    }

    public static ISqlezeReaderBuilder WithScalarReaderFallbackPolicy(
        this ISqlezeCommand sqlezeCommand,
        bool useDefaultInsteadOfNull)
    {
        return sqlezeCommand.With<ScalarReaderFallbackPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new ScalarReaderFallbackPolicyOptions(useDefaultInsteadOfNull));
            });
    }

    public static ISqlezeRowsetBuilder WithScalarReaderFallbackPolicy(
        this ISqlezeRowsetBuilder sqlezeRowsetBuilder,
        bool useDefaultInsteadOfNull
    )
    {
        return sqlezeRowsetBuilder.With<ScalarReaderFallbackPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new ScalarReaderFallbackPolicyOptions(useDefaultInsteadOfNull));
            });
    }

    public static ISqlezeRowsetBuilder WithScalarReaderFallbackPolicy(
        this ISqlezeReader sqlezeReader,
        bool useDefaultInsteadOfNull
    )
    {
        return sqlezeReader.With<ScalarReaderFallbackPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new ScalarReaderFallbackPolicyOptions(useDefaultInsteadOfNull));
            });
    }

    public static ISqlezeReaderBuilder WithScalarReaderFallbackPolicy(
        this ISqlezeReaderBuilder sqlezeReaderBuilder,
        bool useDefaultInsteadOfNull
    )
    {
        return sqlezeReaderBuilder.With<ScalarReaderFallbackPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new ScalarReaderFallbackPolicyOptions(useDefaultInsteadOfNull));
            });
    }
}
