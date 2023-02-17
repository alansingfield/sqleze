using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Options;

public record class CommandCreateOptions
(
    string Sql,
    bool IsStoredProc
)
{
    public static CommandCreateOptions Default = new CommandCreateOptions("", false);
}; 
