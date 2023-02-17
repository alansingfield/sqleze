using Sqleze;
using Sqleze.Security;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;
public static class SqlCredentialExtensions
{
    public static ISqlezeBuilder WithSqlCredential(
        this ISqlezeBuilder sqlezeConnectionBuilder,
        MS.SqlCredential? credential)
    {
        return sqlezeConnectionBuilder.With<SqlCredentialRoot>(
        (root, scope) =>
        {
            scope.Use(new SqlCredentialOptions(credential));
        });
    }
}
