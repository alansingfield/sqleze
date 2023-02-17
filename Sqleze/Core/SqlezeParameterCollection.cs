using Sqleze.Composition;
using Sqleze;
using Sqleze.Options;
using Sqleze.Params;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public class SqlezeParameterCollection : ISqlezeParameterCollection
{
    private readonly IResolverContext commandScope;
    private readonly Lazy<ISqlezeCommand> lazySqlezeCommand;
    private readonly ISqlezeParameterFactory sqlezeParameterFactory;
    private readonly IParameterPreparation parameterPreparation;

    protected IDictionary<string, ISqlezeParameterProvider> DictByAdoName { get; init; }
    protected IDictionary<string, ISqlezeParameterProvider> DictByName { get; init; }

    public SqlezeParameterCollection(
        IResolverContext commandScope,
        Lazy<ISqlezeCommand> lazySqlezeCommand,
        ISqlezeParameterFactory sqlezeParameterFactory,
        ICollation collation,
        IParameterPreparation parameterPreparation)
    {
        this.commandScope = commandScope;
        this.lazySqlezeCommand = lazySqlezeCommand;
        this.sqlezeParameterFactory = sqlezeParameterFactory;
        this.parameterPreparation = parameterPreparation;

        // Internal Dictionary is keyed by the AdoName (the actual parameter name being sent
        // to SQL) - normally case insensitive (depending on DB collation)
        this.DictByAdoName = new Dictionary<string, ISqlezeParameterProvider>(collation.Comparer);

        // Also keep track of the parameter names (names within C#, case SENSITIVE).
        this.DictByName = new Dictionary<string, ISqlezeParameterProvider>();
    }

    public ISqlezeParameter<T> AddOrReplace<T>(
        string parameterName,
        IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null)
    {
        // Choose either the factory we were given, or open a new one if not supplied.
        IScopedSqlezeParameterFactory factory;

        if(scopedSqlezeParameterFactory != null)
            factory = scopedSqlezeParameterFactory;
        else
            factory = sqlezeParameterFactory.OpenScope();

        // Initialise the parameter and its corresponding AdoParameterFactory
        var sqlezeParameterProvider = factory.Create<T>(parameterName);

        // Push the created provider into the preparation
        parameterPreparation.AddOrReplace(sqlezeParameterProvider);

        var param = sqlezeParameterProvider.SqlezeParameter;

        // Indexed by the AdoName - remove any existing item then add the new one.
        if(this.DictByAdoName.Remove(param.AdoName, out var removedItem))
        {
            this.DictByName.Remove(removedItem.SqlezeParameter.Name);
        }

        this.DictByAdoName.Add(param.AdoName, sqlezeParameterProvider);
        this.DictByName.Add(param.Name, sqlezeParameterProvider);        // TODO: This will crash if ADOName / Name aren't consistent
        

        return param;
    }


    public bool Remove(string parameterName)
    {
        // Are we indexed by AdoName or Name ???
        if(parameterName.StartsWith("@"))
        {
            if(this.DictByAdoName.Remove(parameterName, out var prov))
            {
                parameterPreparation.Remove(prov);
                this.DictByName.Remove(prov.SqlezeParameter.Name);
                return true;
            }
        }
        else
        {
            if(this.DictByName.Remove(parameterName, out var prov))
            {
                parameterPreparation.Remove(prov);
                this.DictByAdoName.Remove(prov.SqlezeParameter.AdoName);
                return true;
            }
        }

        return false;
    }

    public bool TryGet(string parameterName,
        [NotNullWhen(true)]
        out ISqlezeParameter? sqlezeParameter)
    {
        // Are we indexed by AdoName or Name ???
        if(parameterName.StartsWith("@"))
        {
            if(this.DictByAdoName.TryGetValue(parameterName, out var prov))
            {
                sqlezeParameter = prov.SqlezeParameter;
                return true;
            }
        }
        else
        {
            if(this.DictByName.TryGetValue(parameterName, out var prov))
            {
                sqlezeParameter = prov.SqlezeParameter;
                return true;
            }
        }

        sqlezeParameter = null;
        return false;
    }

    public ISqlezeParameterBuilder With<T>(Action<T, ISqlezeScope> configure)
    {
        var scopedFactoryFunc = commandScope.Resolve<Func<IScopedSqlezeParameterBuilder<T>>>();

        // Create a new factory which is configured to open a new scope
        var scopedFactory = scopedFactoryFunc();

        // Configure factory scope and create the SqlezeRowsetFactory
        return scopedFactory.Create(configure);
    }

    public int Count => this.DictByAdoName.Count;

    public ISqlezeCommand Command => lazySqlezeCommand.Value;

    public IEnumerator<ISqlezeParameter> GetEnumerator()
    {
        return this.DictByAdoName.Values
            .Select(x => x.SqlezeParameter)
            .ToList()
            .AsReadOnly()
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)this.GetEnumerator();

    public void Clear()
    {
        this.DictByAdoName.Clear();
        this.DictByName.Clear();
        parameterPreparation.Clear();
    }
}
