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

public static partial class CoreParameterReturnExtensions
{
   public static ISqlezeParameter<T> ReturnTo<T>(this ISqlezeParameter<T> sqlezeParameter,
        Action<T?> outputAction)
    {
        sqlezeParameter.OutputAction = outputAction;
        sqlezeParameter.Mode = SqlezeParameterMode.ReturnValue;

        return sqlezeParameter;
    }

    public static ISqlezeParameter<T> ReturnTo<T>(this ISqlezeParameter<T> sqlezeParameter,
        Expression<Func<T?>> member)
    {
        var expr = ExpressionSetter.Prepare<T?>(member);

        sqlezeParameter.OutputAction = expr.Setter;
        sqlezeParameter.Mode = SqlezeParameterMode.ReturnValue;

        return sqlezeParameter;
    }


    public static ISqlezeParameter<T> ReturnTo<T>(
        this ISqlezeParameterCollection sqlezeParameterCollection, Action<T?> outputAction)
    {
        return returnToInternal(sqlezeParameterCollection, outputAction);
    }

    public static ISqlezeParameter<T> ReturnTo<T>(
        this ISqlezeParameter sqlezeParameter, Action<T?> outputAction)
    {
        // To allow chaining of ReturnTo() calls, link up to owner collection.
        return returnToInternal(sqlezeParameter.Command.Parameters, outputAction);
    }

    public static ISqlezeParameter<T> ReturnTo<T>(
        this ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T?>> member)
    {
        return returnToInternalByFunc(sqlezeParameterCollection, member);
    }

    public static ISqlezeParameter<T> ReturnTo<T>(
        this ISqlezeParameter sqlezeParameter, Expression<Func<T?>> member)
    {
        // To allow chaining of ReturnTo() calls, link up to owner collection.
        return returnToInternalByFunc(sqlezeParameter.Command.Parameters, member);
    }

    public static ISqlezeParameter<T> ReturnTo<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Action<T?> outputAction, bool exitContext)
    {
        // This parameter is really only here to allow us to override ReturnTo<> to give a different result type.
        if(exitContext != true)
            throw new Exception($"Parameter {nameof(exitContext)} must be true if supplied");

        return returnToInternal(
            scopedSqlezeParameterFactory.Command.Parameters,
            outputAction, scopedSqlezeParameterFactory);
    }

    public static ISqlezeParameter<T> ReturnTo<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Expression<Func<T?>> member, bool exitContext)
    {
        if(exitContext != true)
            throw new Exception($"Parameter {nameof(exitContext)} must be true if supplied");

        return returnToInternalByFunc(
            scopedSqlezeParameterFactory.Command.Parameters,
            member, scopedSqlezeParameterFactory);
    }


    public static IScopedSqlezeParameterFactory ReturnTo<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Action<T?> outputAction)
    {
        returnToInternal(
            scopedSqlezeParameterFactory.Command.Parameters,
            outputAction, scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    public static IScopedSqlezeParameterFactory ReturnTo<T>(
        this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory, Expression<Func<T?>> member)
    {
        returnToInternalByFunc(
            scopedSqlezeParameterFactory.Command.Parameters,
            member, scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    public static IScopedSqlezeParameterFactory ReturnTo<T>(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        Action<T?> outputAction)
    {
        var scopedSqlezeParameterFactory = sqlezeParameterBuilder.Build();

        returnToInternal(
            scopedSqlezeParameterFactory.Command.Parameters,
            outputAction,
            scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    public static IScopedSqlezeParameterFactory ReturnTo<T>(
        this ISqlezeParameterBuilder sqlezeParameterBuilder,
        Expression<Func<T?>> member)
    {
        var scopedSqlezeParameterFactory = sqlezeParameterBuilder.Build();

        returnToInternalByFunc(
            scopedSqlezeParameterFactory.Command.Parameters,
            member,
            scopedSqlezeParameterFactory);

        return scopedSqlezeParameterFactory;
    }

    private static ISqlezeParameter<T> returnToInternal<T>(
        ISqlezeParameterCollection sqlezeParameterCollection,
        Action<T?> outputAction,
        IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null)
    {
        var sqlezeParameter = sqlezeParameterCollection.AddOrReplace<T>("", scopedSqlezeParameterFactory);

        return sqlezeParameter.ReturnTo(outputAction);
    }

    private static ISqlezeParameter<T> returnToInternalByFunc<T>(
        ISqlezeParameterCollection sqlezeParameterCollection,
        Expression<Func<T?>> member,
        IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null)
    {
        var expr = ExpressionSetter.Prepare<T?>(member);

        var sqlezeParameter = sqlezeParameterCollection.AddOrReplace<T>("", scopedSqlezeParameterFactory);

        return sqlezeParameter.ReturnTo(expr.Setter);
    }

}
