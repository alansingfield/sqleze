using Sqleze.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Sqleze.Readers
{
    public record CheckedAdoDataReaderOptions
    (
        /// Workaround for issue: https://github.com/dotnet/SqlClient/issues/593
        bool UseSynchronousReadWhenAsync
    );

    public class CheckedAdoDataReaderRoot { }
}

namespace Sqleze.Registration
{
    public static class CheckedAdoDataReaderRegistrationExtensions
    {
        public static void RegisterCheckedAdoDataReader(this IRegistrator registrator)
        {
            // Use Synchronous read by default to avoid performance issue with large binary types.
            registrator.RegisterInstance<CheckedAdoDataReaderOptions>(
                new CheckedAdoDataReaderOptions(UseSynchronousReadWhenAsync: true));

            registrator.Register<ICheckedAdoDataReader, CheckedAdoDataReader>(Reuse.ScopedOrSingleton);
            registrator.Register<CheckedAdoDataReaderRoot>();
        }
    }
}