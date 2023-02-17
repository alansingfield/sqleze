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

public static class UnmappedPropertiesPolicyExtensions
{
    public static ISqlezeBuilder WithIgnoreUnmappedProperties(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        bool ignore = true
    )
    {
        return sqlezeConnectionBuilder.With<UnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedPropertiesPolicyOptions(!ignore));
            });
    }

    public static ISqlezeCommandBuilder WithIgnoreUnmappedProperties(
        this ISqlezeCommandBuilder sqlezeCommandBuilder,
        bool ignore = true
    )
    {
        return sqlezeCommandBuilder.With<UnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedPropertiesPolicyOptions(!ignore));
            });
    }

    public static ISqlezeReaderBuilder WithIgnoreUnmappedProperties(
        this ISqlezeCommand sqlezeCommand,
        bool ignore = true)
    {
        return sqlezeCommand.With<UnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedPropertiesPolicyOptions(!ignore));
            });
    }

    public static ISqlezeRowsetBuilder WithIgnoreUnmappedProperties(
        this ISqlezeRowsetBuilder sqlezeRowsetBuilder,
        bool ignore = true
    )
    {
        return sqlezeRowsetBuilder.With<UnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedPropertiesPolicyOptions(!ignore));
            });
    }

    public static ISqlezeReaderBuilder WithIgnoreUnmappedProperties(
        this ISqlezeReaderBuilder sqlezeReaderBuilder,
        bool ignore = true
    )
    {
        return sqlezeReaderBuilder.With<UnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedPropertiesPolicyOptions(!ignore));
            });
    }

    public static ISqlezeReaderBuilder WithIgnoreUnmappedProperties(
        this ISqlezeParameter sqlezeParameter,
        bool ignore = true
    )
    {
        return sqlezeParameter.Command.With<UnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedPropertiesPolicyOptions(!ignore));
            });
    }

    public static ISqlezeReaderBuilder WithIgnoreUnmappedProperties(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        bool ignore = true
    )
    {
        return sqlezeParameterCollection.Command.With<UnmappedPropertiesPolicyRoot>(
            (root, scope) =>
            {
                scope.Use(new UnmappedPropertiesPolicyOptions(!ignore));
            });
    }
}
