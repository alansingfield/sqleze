using Sqleze;

namespace Sqleze
{
    public class SqlezeRowsetProvider<T> : ISqlezeRowsetProvider<T>
    {
        public SqlezeRowsetProvider(ISqlezeRowset<T> sqlezeRowset)
        {
            SqlezeRowset = sqlezeRowset;
        }

        public ISqlezeRowset<T> SqlezeRowset { get; init; }

    }



}
