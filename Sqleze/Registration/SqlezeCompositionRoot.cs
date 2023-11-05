using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration;
public class SqlezeCompositionRoot
{
    public SqlezeCompositionRoot(IRegistrator registrator)
    {
        registrator.RegisterSqleze();
    }
}
