using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze
{
    public interface IAdoParameterFactory
    {
        IAdoParameter Create();
        Task<IAdoParameter> CreateAsync(CancellationToken cancellationToken = default);
    }
    public interface IAdoParameterFactory<T> : IAdoParameterFactory { }
    public interface IAdoParameterFactory<T, TValue> : IAdoParameterFactory<T>
        where T: IEnumerable<TValue>
        { }
}
