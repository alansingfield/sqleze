using Sqleze;

namespace Sqleze
{
    public class SqlezeConnectionProvider : ISqlezeConnectionProvider
    {
        public SqlezeConnectionProvider(ISqlezeConnection sqlezeConnection)
        {
            SqlezeConnection = sqlezeConnection;
        }

        public ISqlezeConnection SqlezeConnection { get; init; }
    }



}
