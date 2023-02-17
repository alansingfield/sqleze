//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sqleze.SqlClient;
//public class ConnectionPreOpenInfoMessageOnUserError : IConnectionPreOpen
//{
//    private readonly InfoMessageOnUserErrorOptions options;

//    public ConnectionPreOpenInfoMessageOnUserError(
//        InfoMessageOnUserErrorOptions options)
//    {
//        this.options = options;
//    }

//    public void PreOpen(MS.SqlConnection sqlConnection)
//    {
//        sqlConnection.FireInfoMessageEventOnUserErrors = options.FireInfoMessageEventOnUserErrors;
//    }
//}


//public record InfoMessageOnUserErrorOptions
//(
//    bool FireInfoMessageEventOnUserErrors
//);