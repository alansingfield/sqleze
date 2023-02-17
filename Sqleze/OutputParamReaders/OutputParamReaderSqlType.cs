namespace Sqleze.OutputParamReaders
{
    public class OutputParamReaderSqlType<T> : IOutputParamReader<T>
    {
        public Action PrepareOutputAction(MS.SqlParameter mssqlParameter, Action<T?> writeAction)
        {
            return () =>
            {
                object? val;

                val = mssqlParameter.SqlValue;
                writeAction((T?)val);
            };
        }
    }
}
