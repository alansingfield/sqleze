using Sqleze.Options;
using Sqleze.TableValuedParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.TableValuedParameters
{
    public record TableTypeUnmappedPropertiesPolicyOptions
    (
        bool ThrowOnUnmapped
    );

    public class TableTypeUnmappedPropertiesPolicyRoot { }

    public interface ITableTypeUnmappedPropertiesPolicy
    {
        void Handle(IReadOnlyCollection<(PropertyInfo PropertyInfo, string ColumnName)> values);
    }

    public class TableTypeUnmappedPropertiesPolicy : ITableTypeUnmappedPropertiesPolicy
    {
        private readonly TableTypeUnmappedPropertiesPolicyOptions options;

        public TableTypeUnmappedPropertiesPolicy(TableTypeUnmappedPropertiesPolicyOptions options)
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
                $"No table-type parameter columns were found to map to property {x.PropertyInfo.Name}, was expecting {x.ColumnName}"));

            throw new Exception(message);
        }
    }
}

namespace Sqleze.Registration
{
    public static class TableTypeUnmappedPropertiesPolicyRegistrationExtensions
    {
        public static void RegisterTableTypeUnmappedPropertiesPolicy(this IRegistrator registrator)
        {
            // Default TableTypeUnmappedPropertiesPolicy is to throw if a property doesn't get a matching
            // column.
            registrator.RegisterInstance<TableTypeUnmappedPropertiesPolicyOptions>(
                new TableTypeUnmappedPropertiesPolicyOptions(ThrowOnUnmapped: true));

            registrator.Register<ITableTypeUnmappedPropertiesPolicy, TableTypeUnmappedPropertiesPolicy>(Reuse.ScopedOrSingleton);
            registrator.Register<TableTypeUnmappedPropertiesPolicyRoot>();
        }
    }
}