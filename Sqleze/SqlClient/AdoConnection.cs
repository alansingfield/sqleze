using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SqlClient
{
    public interface IAdoConnection
    {
        MS.SqlConnection SqlConnection { get; }
    }

    public class AdoConnection : IAdoConnection
    {
        public AdoConnection(IAdo ado)
        {
            if(ado.SqlConnection == null)
                throw new Exception("IAdo.SqlConnection not set");

            this.SqlConnection = ado.SqlConnection;
        }

        public MS.SqlConnection SqlConnection { get; set; }
    }
}
