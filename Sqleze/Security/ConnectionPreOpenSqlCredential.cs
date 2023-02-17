using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Security;
public class ConnectionPreOpenSqlCredential : IConnectionPreOpen
{
    private readonly SqlCredentialOptions options;

    public ConnectionPreOpenSqlCredential(
        SqlCredentialOptions options)
    {
        this.options = options;
    }

    public void PreOpen(MS.SqlConnection sqlConnection)
    {
        if(options.Credential != null)
            sqlConnection.Credential = options.Credential;
    }
}


public record SqlCredentialOptions
(
    MS.SqlCredential? Credential
);