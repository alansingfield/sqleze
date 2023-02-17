using Sqleze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SqlClient;
public class ConnectionPreOpenConnectionString : IConnectionPreOpen
{
    private readonly IConnectionStringProvider connectionStringProvider;

    public ConnectionPreOpenConnectionString(
        IConnectionStringProvider connectionStringProvider)
    {
        this.connectionStringProvider = connectionStringProvider;
    }

    public void PreOpen(MS.SqlConnection sqlConnection)
    {
        string connectionString = connectionStringProvider.GetConnectionString();

        if(String.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Connection string is not configured.");

        sqlConnection.ConnectionString = connectionString;
    }
}
