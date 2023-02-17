using Sqleze;
using Sqleze.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Params
{
    public interface IParameterPreparation
    {
        IEnumerable<IAdoParameter> Prepare();
        Task<IReadOnlyList<IAdoParameter>> PrepareAsync(CancellationToken cancellationToken = default);
        void AddOrReplace(ISqlezeParameterProvider sqlezeParameterProvider);
        void Remove(ISqlezeParameterProvider sqlezeParameterProvider);
        void Clear();
    }

    public class ParameterPreparation : IParameterPreparation
    {
        private readonly Func<MS.SqlParameter> newSqlParameter;
        private readonly ICollation collation;

        protected IDictionary<string, ISqlezeParameterProvider> DictByAdoName { get; init; }

        public ParameterPreparation(
            Func<MS.SqlParameter> newSqlParameter,
            ICollation collation)
        {
            this.newSqlParameter = newSqlParameter;
            this.collation = collation;

            this.DictByAdoName = new Dictionary<string, ISqlezeParameterProvider>(collation.Comparer);
        }

        public void AddOrReplace(ISqlezeParameterProvider sqlezeParameterProvider)
        {
            string adoName = sqlezeParameterProvider.SqlezeParameter.AdoName;

            this.DictByAdoName[adoName] = sqlezeParameterProvider;
        }
        public void Remove(ISqlezeParameterProvider sqlezeParameterProvider)
        {
            string adoName = sqlezeParameterProvider.SqlezeParameter.AdoName;

            this.DictByAdoName.Remove(adoName);
        }

        public void Clear()
        {
            this.DictByAdoName.Clear();
        }

        public IEnumerable<IAdoParameter> Prepare()
        {
            foreach(var provider in this.DictByAdoName.Values)
            {
                // The configuration methods may have customised the AdoParameterBuilder,
                // so we only pull it out at this point.
                yield return provider.AdoParameterFactory.Create();
            }
        }

        public async Task<IReadOnlyList<IAdoParameter>> PrepareAsync(
            CancellationToken cancellationToken = default)
        {
            // Although parameter resolution may involve an async call, we don't need to use
            // IAsyncEnumerable to stream them through, a normal load into list is OK.
            var result = new List<IAdoParameter>();

            foreach(var provider in this.DictByAdoName.Values)
            {
                // The configuration methods may have customised the AdoParameterBuilder,
                // so we only pull it out at this point.
                result.Add(await provider.AdoParameterFactory
                    .CreateAsync(cancellationToken)
                    .ConfigureAwait(false));
            }

            return result.AsReadOnly();
        }


    }
}
