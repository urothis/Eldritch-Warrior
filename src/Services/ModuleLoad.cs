using System;
using System.Globalization;

using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NWNX.API;

namespace Module
{
    [ServiceBinding(typeof(ModuleLoad))]
    public class ModuleLoad
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public ModuleLoad(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnModuleLoad>(NwModule.Instance, OnModuleLoad);

        private void OnModuleLoad(ModuleEvents.OnModuleLoad eventInfo)
        {
            /* Print to console when we boot*/
            Console.WriteLine($"MODULE LOADED:{DateTime.Now.ToString(@"yyyy/MM/dd hh:mm:ss tt", new CultureInfo("en-US"))}");

            /* NWNX */
            Administration.GameOptions.RestoreSpellUses = true;

        }
    }
}
