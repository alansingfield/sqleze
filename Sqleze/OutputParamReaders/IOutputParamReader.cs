using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.OutputParamReaders
{
    public interface IOutputParamReader<T>
    {
        /// <summary>
        /// This prepares a lamdba for later use once the query has completed.
        /// The lambda reads the output parameter value and writes back to the
        /// writeAction (which should be a capture of the target variable)
        /// </summary>
        /// <param name="sqlParameter"></param>
        /// <param name="writeAction"></param>
        /// <returns></returns>
        Action PrepareOutputAction(MS.SqlParameter sqlParameter, Action<T?> writeAction);
    }
}
