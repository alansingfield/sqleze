using Sqleze;

namespace Sqleze
{
    public class SqlezeCommandProvider : ISqlezeCommandProvider
    {
        public SqlezeCommandProvider(ISqlezeCommand sqlezeCommand)
        {
            SqlezeCommand = sqlezeCommand;
        }

        public ISqlezeCommand SqlezeCommand { get; init; }
    }



}
