using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Dynamics
{
    public interface IConstructorLambdaBuilder<T>
    {
        Func<object?[], int[], T> GetConstructorFunc(ConstructorInfo constructorInfo);
    }

    public class ConstructorLambdaBuilder<T> : IConstructorLambdaBuilder<T>
    {
        private readonly IDefaultFallbackExpressionBuilder defaultFallbackExpressionBuilder;

        public ConstructorLambdaBuilder(IDefaultFallbackExpressionBuilder defaultFallbackExpressionBuilder)
        {
            this.defaultFallbackExpressionBuilder = defaultFallbackExpressionBuilder;
        }

        /// <summary>
        /// Builds a lambda function for object creation, aimed at populating C# records from their
        /// constructor.
        /// </summary>
        /// <param name="constructorInfo">Constructor of type T to use</param>
        /// <returns>Lambda function taking two arguments; an array of values, and an array of positional
        /// mappings, where mapping[0] sets the index within the values array to read the 1st constructor
        /// arg from, mapping[1] sets the index for the 2nd constructor arg etc.
        /// If mapping[n] returns -1, or the mapping array is shorter than the number of constructor args,
        /// a null value is assumed. However, since we can't send a null value to a non-nullable parameter,
        /// we provide a fallback value.
        /// For value types, this is the default. For strings, an empty string. For arrays, an empty array.</returns>
        /// <exception cref="Exception"></exception>
        public Func<object?[], int[], T> GetConstructorFunc(ConstructorInfo constructorInfo)
        {
            var consParams = constructorInfo.GetParameters();
            var nullabilityInfoCtx = new NullabilityInfoContext();

            if(consParams.Any(x => x.Name == null))
                throw new Exception($"Unable to use constructor for type {typeof(T).Name} because it has nameless parameters");

            // Input parameter to the required Action is object?[]
            var values = Expression.Parameter(typeof(object?[]), "values");

            // Also need a mapping of position in object?[] array to constructor param index
            // If null then we supply the default value instead
            var indices = Expression.Parameter(typeof(int[]), "indices");

            var pos = Expression.Parameter(typeof(int), "pos");
            var val = Expression.Parameter(typeof(object), "val");

            Expression[] consArgExprs = consParams
                .Select((p, idx) =>
                {
                    var defaultFallback = defaultFallbackExpressionBuilder.Build(
                        p.ParameterType,
                        nullabilityInfoCtx.Create(p).WriteState);

                    return Expression.Block(

                        // Return type
                        p.ParameterType,

                        // Local vars
                        new[] { pos, val },

                        // pos = -1; val = null;
                        Expression.Assign(pos, Expression.Constant(-1)),
                        Expression.Assign(val, Expression.Constant(null)),

                        // if(idx < indices.Length) pos = indices[idx]
                        Expression.IfThen(
                            Expression.LessThan(
                                Expression.Constant(idx),
                                Expression.ArrayLength(indices)
                            ),
                            Expression.Assign(
                                pos,
                                Expression.ArrayAccess(indices, Expression.Constant(idx)))),

                        // if(pos >= 0 && pos < values.Length) val = values[pos]
                        Expression.IfThen(
                            Expression.And(
                                Expression.GreaterThanOrEqual(pos, Expression.Constant(0)),
                                Expression.LessThan(pos, Expression.ArrayLength(values))
                            ),
                            Expression.Assign(
                                val,
                                Expression.ArrayAccess(
                                    values,
                                    pos
                                )
                            )
                        ),

                        // return (val == null) ? defaultFallback : Convert(val, p.ParameterType)
                        Expression.Condition(
                            Expression.Equal(
                                val,
                                Expression.Constant(null, typeof(object))
                            ),
                            defaultFallback,
                            Expression.Convert(val, p.ParameterType))
                    );
                })
                .ToArray();

            var newExpression = Expression.New(constructorInfo, consArgExprs);

            var lambdaFunc = (Func<object?[], int[], T>)
                Expression.Lambda(newExpression,
                new ParameterExpression[] { values, indices })
                .Compile();

            return lambdaFunc;
        }
    }
}
