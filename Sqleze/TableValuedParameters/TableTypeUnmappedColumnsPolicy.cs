using Sqleze.Metadata;
using Sqleze.Options;
using Sqleze.Readers;
using Sqleze.TableValuedParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.TableValuedParameters
{
    public class TableTypeUnmappedColumnsPolicyRoot { }

    public interface ITableTypeUnmappedColumnsPolicy
    {
        void Handle(IEnumerable<TableTypeColumnDefinition> dataReaderFieldInfos);
    }

    public class TableTypeUnmappedColumnsPolicy : ITableTypeUnmappedColumnsPolicy
    {
        public void Handle(IEnumerable<TableTypeColumnDefinition> tableTypeColumnDefinitions)
        {
            if(tableTypeColumnDefinitions.Count() == 0)
                return;

            string message = String.Join("\r\n", tableTypeColumnDefinitions.Select(x =>
                $"Table-type parameter column '{x.ColumnName}' at position {x.ColumnOrdinal + 1} does not have a corresponding property."));

            throw new Exception(message);
        }

    }
}

namespace Sqleze.Registration
{
    public static class TableTypeUnmappedColumnsPolicyRegistrationExtensions
    {
        public static void RegisterTableTypeUnmappedColumnsPolicy(this IRegistrator registrator)
        {
            registrator.Register<ITableTypeUnmappedColumnsPolicy, TableTypeUnmappedColumnsPolicy>(Reuse.ScopedOrSingleton);
            registrator.Register<TableTypeUnmappedColumnsPolicyRoot>();
        }
    }
}