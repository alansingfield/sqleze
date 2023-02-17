using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Dynamics;

public interface IDefaultFallbackExpressionBuilder
{
    Expression Build(Type type, NullabilityState nullabilityState);
}

public class DefaultFallbackExpressionBuilder : IDefaultFallbackExpressionBuilder
{
    public Expression Build(Type type, NullabilityState nullabilityState)
    {
        // Is it a non-nullable reference type?
        var nonNullable = !type.IsValueType
            && nullabilityState is NullabilityState.NotNull;

        // For value types and nullable reference types, default is good enough.
        if(!nonNullable)
            return Expression.Default(type);

        // If there is a parameterless constructor, use this instead of null.
        var parameterlessConstructor = type.GetConstructor(Type.EmptyTypes);
        if(parameterlessConstructor != null)
        {
            // null becomes new T()
            return Expression.New(parameterlessConstructor);
        }

        // Strings don't have a parameterless constructor but an empty string is
        // ok for a non-nullable string.
        if(type == typeof(string))
        {
            return Expression.Constant(String.Empty, typeof(string));
        }

        // For arrays with 1 rank, we create a zero-length array.
        // This caters for byte[] mainly.
        if(type.IsArray && type.GetArrayRank() == 1)
        {
            var elementType = type.GetElementType();

            // Not even sure this could ever happen since we just called GetArrayRank()
            if(elementType == null)
                throw new Exception($"Unable to determine element type of non-nullable array type {type}");

            // null becomes new T[0]
            return Expression.NewArrayInit(elementType);
        }

        throw new Exception($"Unable to determine fallback value for non-nullable reference type {type} with no parameterless constructor found");
    }
}
