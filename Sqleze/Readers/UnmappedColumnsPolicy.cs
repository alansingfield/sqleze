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
    public record UnmappedColumnsPolicyOptions
    (
        bool ThrowOnNamed,
        bool ThrowOnUnnamed
    );

    public class UnmappedColumnsPolicyRoot { }

    public interface IUnmappedColumnsPolicy
    {
        void Handle(IEnumerable<DataReaderFieldInfo> dataReaderFieldInfos);
    }

    public class UnmappedColumnsPolicy : IUnmappedColumnsPolicy
    {
        private readonly UnmappedColumnsPolicyOptions options;

        public UnmappedColumnsPolicy(UnmappedColumnsPolicyOptions options)
        {
            this.options = options;
        }

        public void Handle(IEnumerable<DataReaderFieldInfo> dataReaderFieldInfos)
        {
            if(!options.ThrowOnUnnamed
                && !options.ThrowOnNamed)
                return;

            if(dataReaderFieldInfos.Count() == 0)
                return;

            var selection = dataReaderFieldInfos.Where(x =>
                (x.ColumnName == "" && options.ThrowOnUnnamed)
                || (x.ColumnName != "" && options.ThrowOnNamed))
                .ToList();

            if(selection.Count == 0)
                return;

            string message = String.Join("\r\n", selection.Select(x =>
                $"Column '{x.ColumnName}' at position {x.ColumnOrdinal + 1} was returned but no property maps to it."));

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
    static class UnmappedColumnsPolicyRegistrationExtensions
    {
        public static void RegisterUnmappedColumnsPolicy(this IRegistrator registrator)
        {
            // Default UnmappedColumnsPolicy is to throw if an unmapped column with a name is returned,
            // but if an unnamed one (No Column Name) is then ignore those.
            registrator.RegisterInstance<UnmappedColumnsPolicyOptions>(
                new UnmappedColumnsPolicyOptions(ThrowOnNamed: true, ThrowOnUnnamed: false));

            registrator.Register<IUnmappedColumnsPolicy, UnmappedColumnsPolicy>(Reuse.Scoped);
            registrator.Register<UnmappedColumnsPolicyRoot>();

            registrator.Register<UnmappedColumnsPolicyOptions>(
                Reuse.Scoped,
                setup: Setup.With(asResolutionCall: true, condition: Condition.ScopedToGenericArg<UnmappedColumnsPolicyRoot>()
                ));
        }
    }
}