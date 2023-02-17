using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.InfoMessage;
public class InfoMessageRoot
{
    public InfoMessageRoot(InfoMessageActionOptions parentOptions)
    {
        ParentOptions = parentOptions;
    }

    public InfoMessageActionOptions ParentOptions { get; }
}
