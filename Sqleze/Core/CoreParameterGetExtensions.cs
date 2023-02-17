using Sqleze.Composition;
using Sqleze.Dynamics;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public static partial class CoreParameterGetExtensions
{

    /// <summary>
    /// Retrieve a parameter by its name and type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sqlezeParameterCollection"></param>
    /// <param name="parameterName"></param>
    /// <param name="sqlezeParameter"></param>
    /// <returns>Strongly-typed parameter</returns>
    public static bool TryGet<T>(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        string parameterName,
        [NotNullWhen(true)]
        out ISqlezeParameter<T>? sqlezeParameter)
    {
        if(!sqlezeParameterCollection.TryGet(parameterName, out var result))
        {
            sqlezeParameter = null;
            return false;
        }

        // If you get the type wrong, same as not found.
        if(!(result is ISqlezeParameter<T>))
        {
            sqlezeParameter = null;
            return false;
        }

        sqlezeParameter = (ISqlezeParameter<T>)result;
        return true;
    }

    public static ISqlezeParameter<T> Get<T>(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        string parameterName)
    {
        if(!sqlezeParameterCollection.TryGet<T>(parameterName, out var sqlezeParameter))
            throw new ArgumentException($"Parameter {parameterName} not found, or is not of type {typeof(T).Name}");

        return sqlezeParameter;
    }
    public static ISqlezeParameter Get(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        string parameterName)
    {
        if(!sqlezeParameterCollection.TryGet(parameterName, out var sqlezeParameter))
            throw new ArgumentException($"Parameter {parameterName} not found");

        return sqlezeParameter;
    }


    public static bool TryGet<T>(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T>> member,
        [NotNullWhen(true)]
        out ISqlezeParameter<T>? sqlezeParameter)
    {
        string parameterName = ExpressionGetter.Get(member).MemberName;

        return sqlezeParameterCollection.TryGet(parameterName, out sqlezeParameter);
    }

    public static ISqlezeParameter<T> Get<T>(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T>> member)
    {
        string parameterName = ExpressionGetter.Get(member).MemberName;

        return sqlezeParameterCollection.Get<T>(parameterName);
    }


    public static ISqlezeParameter<T> Get<T>(
        this ISqlezeParameter sqlezeParameter,
        string parameterName)
    {
        return sqlezeParameter.Command.Parameters.Get<T>(parameterName);
    }
    public static ISqlezeParameter Get(
        this ISqlezeParameter sqlezeParameter,
        string parameterName)
    {
        return sqlezeParameter.Command.Parameters.Get(parameterName);
    }


    public static bool TryGet<T>(
        this ISqlezeParameter sqlezeParameterIn,
        Expression<Func<T>> member,
        [NotNullWhen(true)]
        out ISqlezeParameter<T>? sqlezeParameter)
    {
        return sqlezeParameterIn.Command.Parameters.TryGet<T>(member, out sqlezeParameter);
    }

    public static ISqlezeParameter<T> Get<T>(
        this ISqlezeParameter sqlezeParameter,
        Expression<Func<T>> member)
    {
        return sqlezeParameter.Command.Parameters.Get<T>(member);
    }

}
