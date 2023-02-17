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

public static partial class CoreParameterSetExtensions
{
    public static ISqlezeParameter<T> Set<T>(
        this ISqlezeParameterCollection sqlezeParameterCollection, string parameterName, T value)
    {
        return setInternal(sqlezeParameterCollection, parameterName, value);
    }

    public static ISqlezeParameter<T> Set<T>(
        this ISqlezeParameter sqlezeParameter, string parameterName, T value)
    {
        // To allow chaining of Set() calls, link up to owner collection.
        return setInternal(sqlezeParameter.Command.Parameters, parameterName, value);
    }

    public static ISqlezeParameter<T> Set<T>(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T>> value)
    {
        return setInternalByFunc(sqlezeParameterCollection, value);
    }

    public static ISqlezeParameter<T> Set<T>(
        this ISqlezeParameter sqlezeParameter, Expression<Func<T>> value)
    {
        // To allow chaining of Set() calls, link up to owner collection.
        return setInternalByFunc(sqlezeParameter.Command.Parameters, value);
    }

    public static ISqlezeParameter<T> Set<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, string parameterName, T value, bool exitContext)
    {
        // This parameter is really only here to allow us to override Set<> to give a different result type.
        if(exitContext != true)
            throw new Exception($"Parameter {nameof(exitContext)} must be true if supplied");

        return setInternal(
            scopedSqlezeParameterFactory.Command.Parameters,
            parameterName,
            value, scopedSqlezeParameterFactory);
    }

    public static ISqlezeParameter<T> Set<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Expression<Func<T>> value, bool exitContext)
    {
        if(exitContext != true)
            throw new Exception($"Parameter {nameof(exitContext)} must be true if supplied");

        return setInternalByFunc(
            scopedSqlezeParameterFactory.Command.Parameters,
            value, scopedSqlezeParameterFactory);
    }


    public static IScopedSqlezeParameterFactory Set<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, string parameterName, T value)
    {
        setInternal(
            scopedSqlezeParameterFactory.Command.Parameters,
            parameterName,
            value, scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    public static IScopedSqlezeParameterFactory Set<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Expression<Func<T>> value)
    {
        setInternalByFunc(
            scopedSqlezeParameterFactory.Command.Parameters,
            value, scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    public static IScopedSqlezeParameterFactory Set<T>(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        string parameterName,
        T value)
    {
        var scopedSqlezeParameterFactory = sqlezeParameterBuilder.Build();

        setInternal(
            scopedSqlezeParameterFactory.Command.Parameters,
            parameterName, 
            value, 
            scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    public static IScopedSqlezeParameterFactory Set<T>(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        Expression<Func<T>> value)
    {
        var scopedSqlezeParameterFactory = sqlezeParameterBuilder.Build();

        setInternalByFunc(
            scopedSqlezeParameterFactory.Command.Parameters, 
            value, 
            scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }


    private static ISqlezeParameter<T> setInternal<T>(
        ISqlezeParameterCollection sqlezeParameterCollection,
        string parameterName,
        T value,
        IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null)
    {
        var sqlezeParameter = sqlezeParameterCollection.AddOrReplace<T>(parameterName, scopedSqlezeParameterFactory);
        sqlezeParameter.Value = value;
        return sqlezeParameter;
    }

    private static ISqlezeParameter<T> setInternalByFunc<T>(
        ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T>> value,
        IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null)
    {
        var x = ExpressionGetter.Get(value);

        var param = sqlezeParameterCollection.AddOrReplace<T>(x.MemberName, scopedSqlezeParameterFactory);
        param.Value = x.Value;

        return param;
    }

}
