using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon.TestUtil
{
    public static class DryIocExtensions
    {
        #if !DRYIOC_ABSENT

        #if DRYIOC_DLL
        public
        #else
        internal
        #endif
        static void ValidateOrThrow(this IContainer container)
        {
            var validateResult = String.Join("\r\n", container.Validate(x => !x.ServiceType.IsOpenGeneric())
                .Select(x => new { x.Key, x.Value })
                .Distinct()
                .Select(x => $"{x.Key} {x.Value}"));

            if(!String.IsNullOrEmpty(validateResult))
                throw new Exception(validateResult);
        }

        #endif
    }
}
