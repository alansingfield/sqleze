using System.Data.SqlTypes;

namespace Sqleze.OutputParamReaders
{
    public class OutputParamReaderSqlTypeNullable<T> : IOutputParamReader<T>
    {
        public Action PrepareOutputAction(MS.SqlParameter mssqlParameter, Action<T?> writeAction)
        {
            return () =>
            {
                object? val;

                val = mssqlParameter.SqlValue;

                if((val as INullable)?.IsNull ?? false)
                    val = null;

                writeAction((T?)val);
            };
        }
    }
}
