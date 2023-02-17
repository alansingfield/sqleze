using Sqleze.DryIoc;
using Sqleze.Options;
using Sqleze.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Readers
{
    public record UnmappedPropertiesPolicyOptions
    (
        bool ThrowOnUnmapped
    );

    public class UnmappedPropertiesPolicyRoot { }

    public interface IUnmappedPropertiesPolicy
    {
        void Handle(IReadOnlyCollection<(PropertyInfo PropertyInfo, string ColumnName)> values);
    }

    public class UnmappedPropertiesPolicy : IUnmappedPropertiesPolicy
    {
        private readonly UnmappedPropertiesPolicyOptions options;

        public UnmappedPropertiesPolicy(UnmappedPropertiesPolicyOptions options)
        {
            this.options = options;
        }

        public void Handle(IReadOnlyCollection<(PropertyInfo PropertyInfo, string ColumnName)> values)
        {
            if(!options.ThrowOnUnmapped)
                return;

            if(values.Count == 0)
                return;

            string message = String.Join(", ", values.Select(x =>
                $"No columns were found to map to property {x.PropertyInfo.Name}, was expecting {x.ColumnName}"));

            throw new Exception(message);
        }
    }
}

namespace Sqleze.Registration
{
    public static class UnmappedPropertiesPolicyRegistrationExtensions
    {
        public static void RegisterUnmappedPropertiesPolicy(this IRegistrator registrator)
        {
            // Default UnmappedPropertiesPolicy is to throw if a property doesn't get a matching
            // column.
            registrator.RegisterInstance<UnmappedPropertiesPolicyOptions>(
                new UnmappedPropertiesPolicyOptions(ThrowOnUnmapped: true));

            registrator.Register<IUnmappedPropertiesPolicy, UnmappedPropertiesPolicy>(Reuse.Scoped);
            registrator.Register<UnmappedPropertiesPolicyRoot>();

            registrator.Register<UnmappedPropertiesPolicyOptions>(
                Reuse.Scoped,
                setup: Setup.With(asResolutionCall: true, condition: Condition.ScopedToGenericArg<UnmappedPropertiesPolicyRoot>()
                ));
        }
    }
}