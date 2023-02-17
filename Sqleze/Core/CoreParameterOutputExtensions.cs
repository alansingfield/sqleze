using Sqleze.Composition;
using Sqleze.Dynamics;
using Sqleze;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze;

public static class CoreParameterOutputExtensions
{
    public static ISqlezeParameter<T> OutputTo<T>(this ISqlezeParameter<T> sqlezeParameter,
        Action<T?> outputAction)
    {
        sqlezeParameter.OutputAction = outputAction;
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;

        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> OutputTo<T>(this ISqlezeParameter<T> sqlezeParameter,
        Expression<Func<T?>> member)
    {
        var expr = ExpressionSetter.Prepare<T?>(member);

        sqlezeParameter.OutputAction = expr.Setter;
        sqlezeParameter.Mode = SqlezeParameterMode.Scalar;

        return sqlezeParameter;
    }


    public static ISqlezeParameter<T> OutputTo<T>(
    this ISqlezeParameterCollection sqlezeParameterCollection, string parameterName, Action<T?> outputAction)
    {
        return outputToInternal(sqlezeParameterCollection, parameterName, outputAction);
    }

    public static ISqlezeParameter<T> OutputTo<T>(
        this ISqlezeParameter sqlezeParameter, string parameterName, Action<T?> outputAction)
    {
        // To allow chaining of OutputTo() calls, link up to owner collection.
        return outputToInternal(sqlezeParameter.Command.Parameters, parameterName, outputAction);
    }

    public static ISqlezeParameter<T> OutputTo<T>(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T?>> member)
    {
        return outputToInternalByFunc(sqlezeParameterCollection, member);
    }

    public static ISqlezeParameter<T> OutputTo<T>(
        this ISqlezeParameter sqlezeParameter, Expression<Func<T?>> member)
    {
        // To allow chaining of OutputTo() calls, link up to owner collection.
        return outputToInternalByFunc(sqlezeParameter.Command.Parameters, member);
    }

    public static ISqlezeParameter<T> OutputTo<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, string parameterName, Action<T?> outputAction, bool exitContext)
    {
        // This parameter is really only here to allow us to override OutputTo<> to give a different result type.
        if(exitContext != true)
            throw new Exception($"Parameter {nameof(exitContext)} must be true if supplied");

        return outputToInternal(
            scopedSqlezeParameterFactory.Command.Parameters,
            parameterName,
            outputAction, scopedSqlezeParameterFactory);
    }

    public static ISqlezeParameter<T> OutputTo<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Expression<Func<T?>> member, bool exitContext)
    {
        if(exitContext != true)
            throw new Exception($"Parameter {nameof(exitContext)} must be true if supplied");

        return outputToInternalByFunc(
            scopedSqlezeParameterFactory.Command.Parameters,
            member, scopedSqlezeParameterFactory);
    }


    public static IScopedSqlezeParameterFactory OutputTo<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, string parameterName, Action<T?> outputAction)
    {
        outputToInternal(
            scopedSqlezeParameterFactory.Command.Parameters,
            parameterName,
            outputAction, scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    public static IScopedSqlezeParameterFactory OutputTo<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Expression<Func<T?>> member)
    {
        outputToInternalByFunc(
            scopedSqlezeParameterFactory.Command.Parameters,
            member, scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    public static IScopedSqlezeParameterFactory OutputTo<T>(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        string parameterName,
        Action<T?> outputAction)
    {
        var scopedSqlezeParameterFactory = sqlezeParameterBuilder.Build();

        outputToInternal(
            scopedSqlezeParameterFactory.Command.Parameters,
            parameterName,
            outputAction,
            scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    public static IScopedSqlezeParameterFactory OutputTo<T>(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        Expression<Func<T?>> member)
    {
        var scopedSqlezeParameterFactory = sqlezeParameterBuilder.Build();

        outputToInternalByFunc(
            scopedSqlezeParameterFactory.Command.Parameters,
            member,
            scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    private static ISqlezeParameter<T> outputToInternal<T>(
        ISqlezeParameterCollection sqlezeParameterCollection,
        string parameterName,
        Action<T?> outputAction,
        IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null)
    {
        var sqlezeParameter = sqlezeParameterCollection.AddOrReplace<T>(parameterName, scopedSqlezeParameterFactory);

        return sqlezeParameter.OutputTo(outputAction);
    }

    private static ISqlezeParameter<T> outputToInternalByFunc<T>(
        ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T?>> member,
        IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null)
    {
        var expr = ExpressionSetter.Prepare<T?>(member);

        var sqlezeParameter = sqlezeParameterCollection.AddOrReplace<T>(expr.MemberName, scopedSqlezeParameterFactory);

        return sqlezeParameter.OutputTo(expr.Setter);
    }

}
