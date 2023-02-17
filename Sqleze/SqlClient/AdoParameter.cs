using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Sqleze.SqlClient
{
    public interface IAdoParameter
    {
        MS.SqlParameter SqlParameter { get; }
        Action? CaptureOutput { get; }
    }

    public class AdoParameter : IAdoParameter
    {
        public MS.SqlParameter SqlParameter { get; }
        public Action? CaptureOutput { get; }

        public AdoParameter(
            MS.SqlParameter sqlParameter,
            Action? captureOutput = null)
        {
            this.SqlParameter = sqlParameter;
            this.CaptureOutput = captureOutput;
        }
    }
}
