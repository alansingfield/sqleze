using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SqlClient;
public interface ICommandPreExecute
{
    void PreExecute(MS.SqlCommand sqlCommand);
}
