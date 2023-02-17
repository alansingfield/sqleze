using Sqleze.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqleze.Readers
{
    public record ScalarReaderFallbackPolicyOptions
    (
        bool UseDefaultInsteadOfNull
    );


    public class ScalarReaderFallbackPolicyRoot { }

}

namespace Sqleze.Registration
{
    public static class ScalarReaderFallbackPolicyRegistrationExtensions
    {
        public static void RegisterScalarReaderFallbackPolicy(this IRegistrator registrator)
        {
            // Default fallback policy is to leave nulls alone.
            registrator.RegisterInstance<ScalarReaderFallbackPolicyOptions>(
                new ScalarReaderFallbackPolicyOptions(UseDefaultInsteadOfNull: false));

            registrator.Register<ScalarReaderFallbackPolicyRoot>();
        }
    }
}