using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Config;

namespace TestCommon.TestUtil
{
    public static class SettingsFileExtensions
    {
        #if DRYIOC_DLL
        public
        #else
        internal
        #endif
        static void RegisterTestSettings(this IRegistrator container)
        {
            container.RegisterSettingsFiles(
                new[] { "serverSettings.json" });
        }
    }
}
