using Sqleze.ConnectionStrings;
using Sqleze.DryIoc;
using Sqleze;
using Sqleze.NamingConventions;
using Sqleze.TableValuedParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public static class TableTypeUnmappedPropertiesPolicyExtensions
{
    public static ISqlezeBuilder WithTableTypeUnmappedPropertiesPolicy(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        bool throwOnUnmapped
    )
    {
        return sqlezeConnectionBuilder.With<TableTypeUnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new TableTypeUnmappedPropertiesPolicyOptions(throwOnUnmapped));
            });
    }

    public static ISqlezeCommandBuilder WithTableTypeUnmappedPropertiesPolicy(
        this ISqlezeCommandBuilder sqlezeCommandBuilder,
        bool throwOnUnmapped
    )
    {
        return sqlezeCommandBuilder.With<TableTypeUnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new TableTypeUnmappedPropertiesPolicyOptions(throwOnUnmapped));
            });
    }

    public static ISqlezeReaderBuilder WithTableTypeUnmappedPropertiesPolicy(
        this ISqlezeCommand sqlezeCommand,
        bool throwOnUnmapped)
    {
        return sqlezeCommand.With<TableTypeUnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new TableTypeUnmappedPropertiesPolicyOptions(throwOnUnmapped));
            });
    }

    public static ISqlezeRowsetBuilder WithTableTypeUnmappedPropertiesPolicy(
        this ISqlezeRowsetBuilder sqlezeRowsetBuilder,
        bool throwOnUnmapped
    )
    {
        return sqlezeRowsetBuilder.With<TableTypeUnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new TableTypeUnmappedPropertiesPolicyOptions(throwOnUnmapped));
            });
    }

    public static ISqlezeReaderBuilder WithTableTypeUnmappedPropertiesPolicy(
        this ISqlezeReaderBuilder sqlezeReaderBuilder,
        bool throwOnUnmapped
    )
    {
        return sqlezeReaderBuilder.With<TableTypeUnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new TableTypeUnmappedPropertiesPolicyOptions(throwOnUnmapped));
            });
    }
}
