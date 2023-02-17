using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Sqleze.Dynamics;

public class ExpressionSetter
{
    public static ExpressionSetterResult<T> Prepare<T>(Expression<Func<T>> expression)
    {
        if(expression == null)
            throw new ArgumentNullException(nameof(expression));

        var memberInfo = resolveMemberInfo(expression.Body);

        string memberName = memberInfo.Name;
        PropertyInfo? propertyInfo = memberInfo as PropertyInfo;

        var value = Expression.Parameter(typeof(T), "value");

        Expression assignment;
        try
        {
            assignment = Expression.Assign(
                expression.Body,
                value);
        }
        catch(ArgumentException e) when(e.ParamName == "left")
        {
            // Override the property name but still show "Expression is not writeable"
            throw new ArgumentException("Expression must be writeable", memberName, e);
        }
        catch(Exception)
        {
            throw;
        }

        var block = Expression.Block(typeof(object), new Expression[]
        {
            assignment,
            Expression.Constant(null)
        });

        // Compile the block of assignment statements into a Lambda Func<> expression
        var lambdaFunc = (Func<T, object>)
            Expression.Lambda(
                block,
                new ParameterExpression[] { value }
            ).Compile();

        // Convert from Func<> to Action<> by discarding unwanted result.
        Action<T> setter = (x) => lambdaFunc(x);

        return new ExpressionSetterResult<T>(memberName, propertyInfo, setter);

    }

    private static MemberInfo resolveMemberInfo(Expression expression)
    {
        switch(expression)
        {
            case MemberExpression memberExpression:
                return memberExpression.Member;
        }

        throw new ArgumentException(nameof(expression));
    }

}

public record ExpressionSetterResult<T>
(
    string MemberName, 
    PropertyInfo? PropertyInfo, 
    Action<T> Setter
);
