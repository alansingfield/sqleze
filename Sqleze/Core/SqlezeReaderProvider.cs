using Sqleze;

namespace Sqleze
{
    public class SqlezeReaderProvider : ISqlezeReaderProvider
    {
        public SqlezeReaderProvider(ISqlezeReader sqlezeReader)
        {
            SqlezeReader = sqlezeReader;
        }

        public ISqlezeReader SqlezeReader { get; init; }
    }



}
