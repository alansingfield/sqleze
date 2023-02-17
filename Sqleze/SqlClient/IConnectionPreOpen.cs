using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SqlClient;
public interface IConnectionPreOpen
{
    void PreOpen(MS.SqlConnection sqlConnection);
}
