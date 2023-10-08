using Sqleze.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sqleze.Util;
using Sqleze.Readers;

namespace Sqleze.Readers
{
    public record DuplicateColumnsPolicyOptions
    (
        bool ThrowIfDuplicate
    );

    public class DuplicateColumnsPolicyRoot { }

    public interface IDuplicateColumnsPolicy
    {
        void Handle(IEnumerable<(DataReaderFieldInfo DataReaderFieldInfo, PropertyInfo PropertyInfo)> values);
    }

    public class DuplicateColumnsPolicy : IDuplicateColumnsPolicy
    {
        private readonly DuplicateColumnsPolicyOptions options;

        public DuplicateColumnsPolicy(DuplicateColumnsPolicyOptions options)
        {
            this.options = options;
        }

        public void Handle(IEnumerable<(DataReaderFieldInfo DataReaderFieldInfo, PropertyInfo PropertyInfo)> values)
        {
            if(!options.ThrowIfDuplicate)
                return;

            if(!values.Any())
                return;

            string message = String.Join("\r\n",
                values.ToLookup(x => x.PropertyInfo, x => x.DataReaderFieldInfo.ColumnName)
                    .Select(x => $"Multiple columns cannot map to the same property [{x.Key.Name}] -> {String.Join(", ", x.AsEnumerable().ToList())}."));

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
    static class DuplicateColumnsPolicyRegistrationExtensions
    {
        public static void RegisterDuplicateColumnsPolicy(this IRegistrator registrator)
        {
            // Default DuplicateColumns property is to throw if more than one column maps to the
            // same property.
            registrator.RegisterInstance<DuplicateColumnsPolicyOptions>(
                new DuplicateColumnsPolicyOptions(ThrowIfDuplicate: true));

            registrator.Register<IDuplicateColumnsPolicy, DuplicateColumnsPolicy>(Reuse.ScopedOrSingleton);
            registrator.Register<DuplicateColumnsPolicyRoot>();
        }
    }
}