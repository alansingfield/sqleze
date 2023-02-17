namespace Sqleze.OutputParamReaders
{
    public class OutputParamReader<T> : IOutputParamReader<T>
    {
        public Action PrepareOutputAction(MS.SqlParameter mssqlParameter, Action<T?> writeAction)
        {
            return () =>
            {
                object? val;

                val = mssqlParameter.Value;
                if(val is DBNull)
                    val = null;

                //T? convertedValue = (T?)Convert.ChangeType(val, typeof(T?));

                writeAction((T?)val);
            };
        }
    }
}
