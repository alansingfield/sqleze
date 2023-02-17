using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Timeout;
public class CommandPreExecuteTimeout : ICommandPreExecute
{
    private readonly CommandTimeoutOptions options;

    public CommandPreExecuteTimeout(
        CommandTimeoutOptions options)
    {
        this.options = options;
    }

    public void PreExecute(MS.SqlCommand sqlCommand)
    {
        sqlCommand.CommandTimeout = options.CommandTimeout;
    }
}


public record CommandTimeoutOptions
(
    int CommandTimeout = 30
);