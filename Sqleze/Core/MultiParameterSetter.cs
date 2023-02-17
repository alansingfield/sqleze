using Sqleze;
using Sqleze.DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Registration
{
    public static class MultiParameterSetterRegistrationExtensions
    {
        public static void RegisterMultiParameterSetter(this IRegistrator registrator)
        {
            registrator.Register(typeof(IParameterSetter<,>), typeof(ParameterSetter<,>));
            registrator.RegisterGenericPromotion(typeof(IParameterSetter<>), typeof(IParameterSetter<,>));

            registrator.Register(typeof(MultiParameterRoot<>), Reuse.ScopedOrSingleton);
            registrator.Register(typeof(MultiParameterPropertyOptions<>), Reuse.ScopedOrSingleton);
        }
    }
}

namespace Sqleze
{
    public static class MultiParameterSetterExtensions
    {
        /// <summary>
        /// Read each public property on type T and add parameter and value to the parameters collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlezeParameterCollection"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ISqlezeParameterCollection Set<T>(
            this ISqlezeParameterCollection sqlezeParameterCollection,
            T entity)
        {
            setMultipleParameters(sqlezeParameterCollection, entity);

            return sqlezeParameterCollection;
        }

        /// <summary>
        /// Read each public property on type T and add parameter and value to the parameters collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scopedSqlezeParameterFactory"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IScopedSqlezeParameterFactory Set<T>(
            this IScopedSqlezeParameterFactory scopedSqlezeParameterFactory,
            T entity)
        {
            setMultipleParameters(
                scopedSqlezeParameterFactory.Command.Parameters,
                entity,
                scopedSqlezeParameterFactory);

            return scopedSqlezeParameterFactory;
        }


        /// <summary>
        /// Read each public property on type T and add parameter and value to the parameters collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlezeParameterCollection"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IScopedSqlezeParameterFactory Set<T>(
            this ISqlezeParameterBuilder sqlezeParameterBuilder,
            T entity)
        {
            var factory = sqlezeParameterBuilder.Build();
            setMultipleParameters(factory.Command.Parameters, entity, factory);

            return factory;
        }


        private static void setMultipleParameters<T>(
            ISqlezeParameterCollection sqlezeParameterCollection,
            T entity,
            IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null)
        {
            sqlezeParameterCollection.With<MultiParameterRoot<T>>((root, _) =>
            {
                foreach(var parameterSetter in root.ParameterSetter.ResolvePerProperty(
                    prop => (true, new object?[] { new MultiParameterPropertyOptions<T>(entity, prop) })
                    ))
                {
                    parameterSetter.WriteToParameter(scopedSqlezeParameterFactory);
                }
            });
        }

    }

}

namespace Sqleze
{
    public interface IParameterSetter<TEntity>
    {
        void WriteToParameter(IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null);
    }

    public interface IParameterSetter<TEntity, TValue> : IParameterSetter<TEntity>
    {

    }

    public class ParameterSetter<TEntity, TValue> : IParameterSetter<TEntity, TValue>
    {
        private readonly ISqlezeParameterCollection sqlezeParameterCollection;
        private readonly MultiParameterPropertyOptions<TEntity> options;

        public ParameterSetter(
            ISqlezeParameterCollection sqlezeParameterCollection,
            MultiParameterPropertyOptions<TEntity> multiParameterPropertyOptions)
        {
            this.sqlezeParameterCollection = sqlezeParameterCollection;
            this.options = multiParameterPropertyOptions;
        }

        public void WriteToParameter(IScopedSqlezeParameterFactory? scopedSqlezeParameterFactory = null)
        {
            // Create the parameter using the property name.
            var sqlezeParameter = sqlezeParameterCollection
                .AddOrReplace<TValue>(options.PropertyInfo.Name,
                scopedSqlezeParameterFactory);

            // Using reflection, read the value of the property
            // TODO - swap to compiled lambda expression
            var value = (TValue?)(options.PropertyInfo.GetValue(
                options.Entity,
                BindingFlags.GetProperty,
                null, null, null));

            // Write the property value to the parameter.
            sqlezeParameter.Value = value;
        }
    }


    public class MultiParameterRoot<TEntity>
    {
        public MultiParameterRoot(
            IMultiResolver<IParameterSetter<TEntity>, TEntity> parameterSetter)
        {
            ParameterSetter = parameterSetter;
        }

        public IMultiResolver<IParameterSetter<TEntity>, TEntity> ParameterSetter { get; }
    }

    public record MultiParameterPropertyOptions<TEntity>(
        TEntity Entity, 
        PropertyInfo PropertyInfo);

}
