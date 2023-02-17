using Sqleze.Options;

namespace Sqleze;

public interface ISqlezeCommandProvider
{
    ISqlezeCommand SqlezeCommand { get; }
}

