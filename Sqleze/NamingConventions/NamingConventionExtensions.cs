using Sqleze.Composition;
using Sqleze;
using Sqleze.NamingConventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public static class NamingConventionExtensions
    {
        public static ISqlezeBuilder WithCamelUnderscoreNaming(
            this ISqlezeBuilder sqlezeConnectionBuilder)
        {
            // Scope will be: IScopedSqlezeConnectionBuilder<CamelUnderscoreRowsetRoot>
            return sqlezeConnectionBuilder.With<CamelUnderscoreNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeCommandBuilder WithCamelUnderscoreNaming(
            this ISqlezeConnection sqlezeConnection)
        {
            // Scope will be: IScopedSqlezeCommandBuilder<CamelUnderscoreNamingConventionRoot>
            return sqlezeConnection.With<CamelUnderscoreNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeReaderBuilder WithCamelUnderscoreNaming(
            this ISqlezeCommand sqlezeCommand)
        {
            return sqlezeCommand.With<CamelUnderscoreNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeReaderBuilder WithCamelUnderscoreNaming(
            this ISqlezeParameter sqlezeParameter)
        {
            return sqlezeParameter.Command.With<CamelUnderscoreNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeParameterBuilder WithCamelUnderscoreNaming(
            this ISqlezeParameterCollection sqlezeParameterCollection)
        {
            return sqlezeParameterCollection.With<CamelUnderscoreNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeRowsetBuilder WithCamelUnderscoreNaming(
            this ISqlezeReader sqlezeReader)
        {
            return sqlezeReader.With<CamelUnderscoreNamingConventionRoot>((_, _) => { });
        }

        
        public static ISqlezeReaderBuilder WithCamelUnderscoreNaming(
            this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory)
        {
            return scopedSqlezeParameterFactory.Command.With<CamelUnderscoreNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeBuilder WithNeutralNaming(
            this ISqlezeBuilder sqlezeConnectionBuilder)
        {
            // Scope will be: IScopedSqlezeConnectionBuilder<NeutralRowsetRoot>
            return sqlezeConnectionBuilder.With<NeutralNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeCommandBuilder WithNeutralNaming(
            this ISqlezeConnection sqlezeConnection)
        {
            // Scope will be: IScopedSqlezeCommandBuilder<NeutralNamingConventionRoot>
            return sqlezeConnection.With<NeutralNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeReaderBuilder WithNeutralNaming(
            this ISqlezeCommand sqlezeCommand)
        {
            return sqlezeCommand.With<NeutralNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeReaderBuilder WithNeutralNaming(
            this ISqlezeParameter sqlezeParameter)
        {
            return sqlezeParameter.Command.With<NeutralNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeParameterBuilder WithNeutralNaming(
            this ISqlezeParameterCollection sqlezeParameterCollection)
        {
            return sqlezeParameterCollection.With<NeutralNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeRowsetBuilder WithNeutralNaming(
            this ISqlezeReader sqlezeReader)
        {
            return sqlezeReader.With<NeutralNamingConventionRoot>((_, _) => { });
        }

        public static ISqlezeReaderBuilder WithNeutralNaming(
            this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory)
        {
            return scopedSqlezeParameterFactory.Command.With<NeutralNamingConventionRoot>((_, _) => { });
        }
    }
}
