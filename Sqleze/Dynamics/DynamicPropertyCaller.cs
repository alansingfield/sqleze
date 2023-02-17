using Sqleze.Collections;
using Sqleze.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Dynamics;

public interface IDynamicPropertyCaller<T>
{
    Func<T, object?[]> CompilePropertyGetFunc(string[] propertyNames);
    Func<T, object?> CompilePropertyGetFunc(string propertyName);
    Action<T, object?[]> CompilePropertySetAction(string?[] propertyNames);
}

/// <summary>
/// This class provides a faster alternative to Reflection where object property access is
/// required at runtime but the names of the properties to be accessed are unknown. It creates
/// a compiled lambda expression for the property access which is then cached.
/// </summary>
/// <typeparam name="T">Type of object to work with</typeparam>
public class DynamicPropertyCaller<T> : IDynamicPropertyCaller<T>
{
    private readonly ConcurrentCache<string[], Func<T, object?[]>> s_getterFuncs =
        new ConcurrentCache<string[], Func<T, object?[]>>(new StringArrayComparer());

    private readonly ConcurrentCache<string, Func<T, object?>> s_sgetterFuncs =
        new ConcurrentCache<string, Func<T, object?>>();

    private readonly ConcurrentCache<string?[], Action<T, object?[]>> s_setterActions =
        new ConcurrentCache<string?[], Action<T, object?[]>>(new StringArrayComparer());

    private IDefaultFallbackExpressionBuilder defaultFallbackExpressionBuilder;

    public DynamicPropertyCaller(IDefaultFallbackExpressionBuilder defaultFallbackExpressionBuilder)
    {
        this.defaultFallbackExpressionBuilder = defaultFallbackExpressionBuilder;
    }

    /// <summary>
    /// Returns a lambda expression that will take an object and return an array of
    /// objects, one element for the value of each property named in the propertyNames array.
    /// Note that the compilation is only performed once, the compiled lambda function is
    /// cached.
    /// </summary>
    /// <param name="propertyNames">Names of properties</param>
    /// <returns>Function that will read these properties from type T and return them in object[]</returns>
    public Func<T, object?[]> CompilePropertyGetFunc(string[] propertyNames)
    {
        // Use the ConcurrentCache to only perform this work once and return a cached result
        // from then on.
        return s_getterFuncs.Get(propertyNames, (key) =>
        {
            // One parameter to the Func<>, the object we are going to read from.
            var source = Expression.Parameter(typeof(T), "source");

            // Loop through each property, creating a type conversion expression to read
            // the property and convert to object.
            var convertExpressions = new List<Expression>();
            foreach(var propertyName in propertyNames)
            {
                // Create an expression along the lines of:
                // Convert(source.~propertyName~, typeof(object))
                var convertExpression = Expression.Convert(
                    Expression.PropertyOrField(source, propertyName),
                    typeof(object));

                convertExpressions.Add(convertExpression);
            }

            // Create an array initialiser using all the Convert expressions e.g.
            // = new object[] { conv1, conv2, conv3 }
            // - this converts all the property-accessed values to objects in the array.
            var arrayInitialiser = Expression.NewArrayInit(typeof(object), convertExpressions);

            // Compile this into a lambda expression that takes the source object, reads
            // the fields and returns an object array.
            return (Func<T, object[]>)
                Expression.Lambda(arrayInitialiser, source)
                .Compile();
        });
    }


    /// <summary>
    /// Returns a lambda expression that will take an object and return an array of
    /// objects, one element for the value of each property named in the propertyNames array.
    /// Note that the compilation is only performed once, the compiled lambda function is
    /// cached.
    /// </summary>
    /// <param name="propertyNames">Names of properties</param>
    /// <returns>Function that will read these properties from type T and return them in object[]</returns>
    public Func<T, object?> CompilePropertyGetFunc(string propertyName)
    {
        // Use the ConcurrentCache to only perform this work once and return a cached result
        // from then on.
        return s_sgetterFuncs.Get(propertyName, (key) =>
        {
            // One parameter to the Func<>, the object we are going to read from.
            var source = Expression.Parameter(typeof(T), "source");

            // Create an expression along the lines of:
            // Convert(source.~propertyName~, typeof(object))
            var convertExpression = Expression.Convert(
                Expression.PropertyOrField(source, propertyName),
                typeof(object));

            // Compile this into a lambda expression that takes the source object, reads
            // the field and returns an object.
            return (Func<T, object?>)
                Expression.Lambda(convertExpression, source)
                .Compile();
        });
    }

    /// <summary>
    /// Returns an Action that will take an entity object, and an array of values
    /// in an object[] array, and populate the entity with the values in the array.
    /// Note that the compilation is only performed once, the compiled lambda function is
    /// cached.
    /// </summary>
    /// <param name="propertyNames">Names of properties</param>
    /// <returns>
    /// Action that takes an object of type T and an object array, populates the first
    /// object's properties with the values of the array.
    /// </returns>
    public Action<T, object?[]> CompilePropertySetAction(string?[] propertyNames)
    {
        // Use the ConcurrentCache to only perform this work once and return a cached result
        // from then on.
        return s_setterActions.Get(propertyNames, (key) =>
        {
            // Two parameters to the Action; the target object itself, and the values to
            // write into it.
            var target = Expression.Parameter(typeof(T), "target");
            var values = Expression.Parameter(typeof(object[]), "values");

            var exceptionCons = getExceptionConstructor();
            var nullabilityInfoCtx = new NullabilityInfoContext();

            var assignments = new List<Expression>();

            // Get the properties of our target object.
            var props = typeof(T).GetProperties();

            // Loop through each property we want to set in order. 
            foreach(var (propertyName, idx) in propertyNames.SelectIndexed())
            {
                // Skip over null property names.
                // We use that feature to deal with "init" properties which are set at the constructor.
                if(propertyName == null)
                    continue;

                // Find the PropertyInfo by property name
                var prop = props.Single(p => p.Name == propertyName);

                // Calculate a default fallback expression for non-nullable values e.g.
                // "" for a string, byte[0] for byte array.
                var defaultFallback = defaultFallbackExpressionBuilder.Build(
                    prop.PropertyType,
                    nullabilityInfoCtx.Create(prop).WriteState);

                var val = Expression.Parameter(typeof(object), "val");
                var exceptionParameter = Expression.Parameter(typeof(Exception), "exception");

                var assignment = Expression.Block(

                    // Return type
                    typeof(object),

                    // Local vars
                    new[] { val, exceptionParameter },

                    // val = values[index]
                    Expression.Assign(
                        val,
                        Expression.ArrayAccess(values, Expression.Constant(idx))
                    ),

                    Expression.TryCatch(
                        Expression.Block(
                            typeof(object),

                            // target.~propertyName~ = (val == null)
                            //      ? defaultFallback
                            //      : Convert(val, p.ParameterType) 
                            Expression.Assign(
                                Expression.Property(target, prop),
                                Expression.Condition(
                                    Expression.Equal(
                                        val,
                                        Expression.Constant(null, typeof(object))
                                    ),
                                    defaultFallback,
                                    Expression.Convert(val, prop.PropertyType)
                                )
                            ),
                            Expression.Constant(null)
                        ),
                        Expression.Catch(exceptionParameter,
                            Expression.Block(
                                typeof(object),

                                Expression.Throw(
                                    Expression.New(
                                        exceptionCons,
                                        Expression.Constant("Unable to write to property '" + propertyName + "'"),
                                        exceptionParameter
                                    )
                                ),

                                Expression.Constant(null)
                            )
                        )
                    ),

                    // return null
                    Expression.Constant(null)
                );

                assignments.Add(assignment);

            }

            // Add a "return null;" to the end to the assignments list, so the lambda
            // expression always has a valid return type. If the last property was a value
            // type (bool, int etc) Expression.Block() would try to coerce this into type
            // object and fail.
            assignments.Add(Expression.Constant(null));

            // Join these assignment statements together into a block.
            var block = Expression.Block(typeof(object), assignments);

            // Compile the block of assignment statements into a Lambda Func<> expression
            var lambdaFunc = (Func<T, object?[], object>)
                Expression.Lambda(
                    block,
                    new ParameterExpression[] { target, values }
                ).Compile();

            // Convert from Func<> to Action<> by discarding unwanted result.
            return (a, b) => lambdaFunc(a, b);
        });
    }

    private static ConstructorInfo getExceptionConstructor()
    {
        var exceptionCons = typeof(Exception)
            .GetConstructor(new[] { typeof(string), typeof(Exception) });

        if(exceptionCons == null)
            throw new Exception("Unable to find constructor for Exception(string, Exception)");

        return exceptionCons;
    }

    /// <summary>
    /// A comparer which does a case-sensitive comparison on string arrays.
    /// </summary>
    private class StringArrayComparer : IEqualityComparer<string?[]>
    {
        public bool Equals(string?[]? x, string?[]? y)
        {
            if(x is null && y is null)
                return true;

            if(x is null || y is null)
                return false;

            return x.SequenceEqual(y);
        }

        public int GetHashCode(string?[] input)
        {
            var hash = new HashCode();

            foreach(var s in input)
            {
                hash.Add(s);
            }

            return hash.ToHashCode();
        }
    }
}