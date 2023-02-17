using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Sqleze.Dynamics;

public class ExpressionGetter
{
    public static ExpressionGetterResult<T> Get<T>(Expression<Func<T>> expression)
    {
        if(expression == null)
            throw new ArgumentNullException(nameof(expression));

        var memberInfo = resolveMemberInfo(expression.Body);

        string memberName = memberInfo.Name;

        T value = expression.Compile()();

        return new ExpressionGetterResult<T>(memberName, value);
        
    }

    private static MemberInfo resolveMemberInfo(Expression expression)
    {
        switch(expression)
        {
            case MemberExpression memberExpression:
                return memberExpression.Member;

            case UnaryExpression unaryExpression:
                return resolveMemberInfo(unaryExpression.Operand);
        }

        throw new ArgumentException(nameof(expression));
    }

}

public class ExpressionGetterResult<T>
{
    public ExpressionGetterResult(string memberName, T value)
    {
        this.MemberName = memberName;
        this.Value = value;
    }

    public string MemberName { get; }
    public T Value { get; set; }
}
