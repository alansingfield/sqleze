namespace Sqleze;

public interface ISqlezeParameterProvider
{
    ISqlezeParameter SqlezeParameter { get; }
    IAdoParameterFactory AdoParameterFactory { get; }
}

public interface ISqlezeParameterProvider<T> : ISqlezeParameterProvider
{
    new ISqlezeParameter<T> SqlezeParameter { get; }
    new IAdoParameterFactory<T> AdoParameterFactory { get; }
}
