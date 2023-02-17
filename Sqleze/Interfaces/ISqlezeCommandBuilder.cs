using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public interface ISqlezeCommandBuilder
{
    ISqlezeCommandBuilder With<T>(Action<T, ISqlezeScope> configure);
    ISqlezeCommandFactory Build();
}
