using Sqleze;

namespace Sqleze
{
    public class SqlezeParameterProvider<T> : ISqlezeParameterProvider<T>
    {
        public SqlezeParameterProvider(ISqlezeParameter<T> sqlezeParameter,
            IAdoParameterFactory<T> adoParameterFactory)
        {
            SqlezeParameter = sqlezeParameter;
            AdoParameterFactory = adoParameterFactory;
        }

        public ISqlezeParameter<T> SqlezeParameter { get; }
        public IAdoParameterFactory<T> AdoParameterFactory { get; }

        ISqlezeParameter ISqlezeParameterProvider.SqlezeParameter => this.SqlezeParameter;
        IAdoParameterFactory ISqlezeParameterProvider.AdoParameterFactory => this.AdoParameterFactory;
    }



}
