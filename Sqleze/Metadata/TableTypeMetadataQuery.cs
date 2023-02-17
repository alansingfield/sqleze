using Sqleze;
using Sqleze.NamingConventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sqleze.Metadata.TableTypeMetadataQuery;

namespace Sqleze.Metadata
{
    public interface ITableTypeMetadataQuery
    {
        IReadOnlyList<TableTypeColumnDefinition> Query(string sqlTypeName);
        Task<IReadOnlyList<TableTypeColumnDefinition>> QueryAsync(string sqlTypeName, CancellationToken cancellationToken = default);
    }

    public class TableTypeMetadataQuery : ITableTypeMetadataQuery
    {
        private readonly IConnectionStringProvider connectionStringProvider;
        private readonly ISqlezeBuilder sqlezeConnectionBuilder;

        public TableTypeMetadataQuery(
            IConnectionStringProvider connectionStringProvider,
            ISqlezeBuilder sqlezeConnectionBuilder)
        {
            this.connectionStringProvider = connectionStringProvider;
            this.sqlezeConnectionBuilder = sqlezeConnectionBuilder;
        }


        public async Task<IReadOnlyList<TableTypeColumnDefinition>> QueryAsync(string sqlTypeName,
            CancellationToken cancellationToken = default)
        {
            var factory = setupConnectionFactory();

            using var conn = factory.Connect();
            var command = buildCommand(conn, sqlTypeName);

            return (await command.ReadListAsync<TableTypeColumnDefinition>(cancellationToken).ConfigureAwait(false))
                .AsReadOnly();
        }

        public IReadOnlyList<TableTypeColumnDefinition> Query(string sqlTypeName)
        {
            var factory = setupConnectionFactory();

            using var conn = factory.Connect();
            var command = buildCommand(conn, sqlTypeName);

            return command.ReadList<TableTypeColumnDefinition>()
                .AsReadOnly();
        }

        private ISqleze setupConnectionFactory()
        {
            // Pull out the connection string verbatim.
            var connStr = connectionStringProvider.GetConnectionString();

            // Go back to the root container and reconfigure in a known state.
            // Only special thing we want is the connection string.
            var connectionFactory = sqlezeConnectionBuilder
                .WithConnectionString(connStr)
                .WithCamelUnderscoreNaming()
                .Build();

            return connectionFactory;
        }

        private static ISqlezeCommand buildCommand(ISqlezeConnection conn, string sqlTypeName)
        {
            var command = conn.Sql(@"

SELECT	column_name = sycol.name,
        column_ordinal = ROW_NUMBER() OVER (ORDER BY sycol.column_id) - 1,
        datatype = CASE
            WHEN ISNULL(ustyp.name, sytyp.name) IN('varchar', 'char')
             AND COLLATIONPROPERTY(sycol.collation_name, 'CodePage') = 65001
                THEN 'n' ELSE '' END
                +
            ISNULL(ustyp.name, sytyp.name),
        length = CASE 
            WHEN sycol.max_length <= 0 THEN sycol.max_length
            WHEN ISNULL(ustyp.name, sytyp.name) IN ('nvarchar', 'nchar') THEN sycol.max_length / 2
            ELSE ISNULL(sycol.max_length, 0)
        END,
        sycol.precision,
        sycol.scale,
        sycol.is_nullable
FROM	sys.columns sycol
    INNER JOIN
        sys.table_types stype
    ON	sycol.object_id = stype.type_table_object_id
    INNER JOIN
        sys.schemas schma
    ON	stype.schema_id = schma.schema_id
    INNER JOIN
        sys.types sytyp
    ON	sycol.user_type_id = sytyp.user_type_id
    LEFT OUTER JOIN
        sys.types ustyp
    ON  sytyp.system_type_id = ustyp.user_type_id
WHERE schma.name = ISNULL(PARSENAME(@typename, 2), SCHEMA_NAME())
AND   stype.name = PARSENAME(@typename, 1)
ORDER BY sycol.column_id

            ");

            command.Parameters.Set("@typename", sqlTypeName).AsNVarChar(280);

            return command;
        }


    }
}
