using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Security;
public class ConnectionPreOpenAccessToken : IConnectionPreOpen
{
    private readonly AccessTokenOptions options;

    public ConnectionPreOpenAccessToken(
        AccessTokenOptions options)
    {
        this.options = options;
    }

    public void PreOpen(MS.SqlConnection sqlConnection)
    {
        if(options.AccessToken != null)
            sqlConnection.AccessToken = options.AccessToken;
    }
}


public record AccessTokenOptions
(
    string? AccessToken
);