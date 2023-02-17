using Sqleze.SqlClient;
using Sqleze.ValueGetters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Readers
{
    public interface IReaderGetValueResolver<TValue>
    {
        IReaderGetValue<TValue> GetReaderGetValue();
    }

    public interface IReaderGetValueResolver<TValue, TSqlDbType>
        : IReaderGetValueResolver<TValue>
        where TSqlDbType : IKnownSqlDbType
    {
    }

    public class ReaderGetValueResolver<TValue, TSqlDbType> 
        : IReaderGetValueResolver<TValue, TSqlDbType> where TSqlDbType : IKnownSqlDbType
    {
        private readonly IReaderGetValue<TValue> readerGetValue;
        private readonly IReaderGetValue<TValue, TSqlDbType>[] readerGetValueForSqlDbTypes;

        public ReaderGetValueResolver(
            IReaderGetValue<TValue> readerGetValue,
            IReaderGetValue<TValue, TSqlDbType>[] readerGetValueForSqlDbTypes)
        {
            this.readerGetValue = readerGetValue;
            this.readerGetValueForSqlDbTypes = readerGetValueForSqlDbTypes;
        }

        public IReaderGetValue<TValue> GetReaderGetValue()
        {
            // We may have a specific reader registered for the SqlDbType(source); if not
            // fall back to the one registered for the destination type
            return readerGetValueForSqlDbTypes.FirstOrDefault() ?? readerGetValue;
        }
    }
}
