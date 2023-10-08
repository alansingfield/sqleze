using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Util;
using Sqleze.Metadata;
using Sqleze.TableValuedParameters;

namespace Sqleze.TableValuedParameters
{
    public class TableTypeDuplicateColumnsPolicyRoot { }

    public interface ITableTypeDuplicateColumnsPolicy
    {
        void Handle(IEnumerable<(TableTypeColumnDefinition TableTypeColumnDefinition, PropertyInfo PropertyInfo)> values);
    }

    public class TableTypeDuplicateColumnsPolicy : ITableTypeDuplicateColumnsPolicy
    {
        public void Handle(IEnumerable<(TableTypeColumnDefinition TableTypeColumnDefinition, PropertyInfo PropertyInfo)> values)
        {
            if(!values.Any())
                return;

            string message = String.Join("\r\n",
                values.ToLookup(x => x.PropertyInfo, x => x.TableTypeColumnDefinition.ColumnName)
                    .Select(x => $"Multiple table type columns cannot map to the same property [{x.Key.Name}] -> {String.Join(", ", x.AsEnumerable().ToList())}."));

            throw new Exception(message);
        }
    }
}

namespace Sqleze.Registration
{
    #if DRYIOC_DLL
    public
    #else
    internal
    #endif
    static class TableTypeDuplicateColumnsPolicyRegistrationExtensions
    {
        public static void RegisterTableTypeDuplicateColumnsPolicy(this IRegistrator registrator)
        {
            registrator.Register<ITableTypeDuplicateColumnsPolicy, TableTypeDuplicateColumnsPolicy>(Reuse.ScopedOrSingleton);
            registrator.Register<TableTypeDuplicateColumnsPolicyRoot>();
        }
    }
}