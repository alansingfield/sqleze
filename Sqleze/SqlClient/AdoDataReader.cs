using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.SqlClient
{
    public interface IAdoDataReader
    {
        MS.SqlDataReader SqlDataReader { get; }
    }

    public class AdoDataReader : IAdoDataReader
    {
        public AdoDataReader(IAdo ado)
        {
            if(ado.SqlDataReader == null)
                throw new Exception("There is no active rowset to read (the query has already completed)");

            this.SqlDataReader = ado.SqlDataReader;
        }

        public MS.SqlDataReader SqlDataReader { get; set; }
    }
}
