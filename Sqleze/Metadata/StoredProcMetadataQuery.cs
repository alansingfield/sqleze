using Sqleze;
using Sqleze.NamingConventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Metadata
{
    public interface IStoredProcMetadataQuery
    {
        IReadOnlyList<StoredProcParamDefinition> Query(string procName);
        Task<IReadOnlyList<StoredProcParamDefinition>> QueryAsync(string procName, CancellationToken cancellationToken = default);
    }

    public class StoredProcMetadataQuery : IStoredProcMetadataQuery
    {
        private readonly ISqleze sqleze;

        public StoredProcMetadataQuery(
            IConnectionStringProvider connectionStringProvider,
            ISqlezeBuilder sqlezeConnectionBuilder,
            ISqleze sqleze)
        {
            this.sqleze = sqleze;
        }


        public async Task<IReadOnlyList<StoredProcParamDefinition>> QueryAsync(string procName,
            CancellationToken cancellationToken = default)
        {
            using var conn = sqleze.Connect();
            var command = buildCommand(conn, procName);

            return (await command.ReadListAsync<StoredProcParamDefinition>(cancellationToken).ConfigureAwait(false))
                .AsReadOnly();
        }

        public IReadOnlyList<StoredProcParamDefinition> Query(string procName)
        {
            using var conn = sqleze.Connect();
            var command = buildCommand(conn, procName);

            return command.ReadList<StoredProcParamDefinition>()
                .AsReadOnly();
        }

        private ISqlezeCommand buildCommand(ISqlezeConnection conn, string procName)
        {
            var command = conn.WithCamelUnderscoreNaming().Sql(@"

SELECT	syprm.parameter_id,
        parameter_name = syprm.name,
		parameter_type = CASE 
			WHEN sytyp.is_table_type = 1 THEN 
				pschm.name + '.' + sytyp.name 
			ELSE ISNULL(ustyp.name, sytyp.name) END,
		sytyp.is_table_type,
        sytyp.is_assembly_type,
		length = CASE 
			WHEN syprm.max_length <= 0 THEN syprm.max_length
			WHEN ISNULL(ustyp.name, sytyp.name) IN ('nvarchar', 'nchar') THEN syprm.max_length / 2
			ELSE ISNULL(syprm.max_length, 0)
		END,
        syprm.precision,
        syprm.scale,
        syprm.is_output,
        is_return = CONVERT(bit, CASE WHEN syprm.parameter_id = 0 THEN 1 ELSE 0 END)
FROM	sys.schemas sysch
	INNER JOIN
		sys.objects syobj
	ON	sysch.schema_id = syobj.schema_id
	INNER JOIN
		sys.parameters syprm
	ON	syobj.object_id = syprm.object_id
	INNER JOIN
		sys.types sytyp
	ON	syprm.user_type_id = sytyp.user_type_id
    LEFT OUTER JOIN
        sys.types ustyp
    ON  sytyp.system_type_id = ustyp.user_type_id
    AND sytyp.is_table_type = 0
	INNER JOIN
		sys.schemas pschm
	ON	sytyp.schema_id = pschm.schema_id
WHERE	syobj.type IN ('P', 'FN', 'IF')
AND		sysch.name = ISNULL(PARSENAME(@procname, 2), SCHEMA_NAME())
AND		syobj.name = PARSENAME(@procname, 1)
UNION ALL
SELECT	parameter_id = 0,
        parameter_name = '',
		parameter_type = sytyp.name,
		sytyp.is_table_type,
        sytyp.is_assembly_type,
		length = sytyp.max_length,
        sytyp.precision,
        sytyp.scale,
        is_output = CONVERT(bit, 1),
        is_return = CONVERT(bit, 1)
FROM	sys.schemas sysch
	INNER JOIN
		sys.objects syobj
	ON	sysch.schema_id = syobj.schema_id
	INNER JOIN
		sys.types sytyp
	ON	sytyp.user_type_id = 56
WHERE	syobj.type = 'P'
AND		sysch.name = ISNULL(PARSENAME(@procname, 2), SCHEMA_NAME())
AND		syobj.name = PARSENAME(@procname, 1)
ORDER BY 1

            ");

            command.Parameters.Set("@procname", procName).AsNVarChar(280);

            return command;
        }


    }
}
