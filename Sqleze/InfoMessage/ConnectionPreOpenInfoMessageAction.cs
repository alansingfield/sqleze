using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.InfoMessage;
public class ConnectionPreOpenInfoMessageAction : IConnectionPreOpen
{
    private readonly InfoMessageActionOptions options;

    public ConnectionPreOpenInfoMessageAction(
        InfoMessageActionOptions options)
    {
        this.options = options;
    }

    public void PreOpen(MS.SqlConnection sqlConnection)
    {
        if(options.InfoMessageAction != null)
        {
            sqlConnection.InfoMessage += (sender, e) => 
            {
                options.InfoMessageAction(e);
            };
        }
    }
}


public record InfoMessageActionOptions
(
    Action<MS.SqlInfoMessageEventArgs>? InfoMessageAction
);