using System;
using System.Globalization;

using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NWNX.API;
using NWN.API.Constants;
using System.Linq;

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

            /* Iterate all areas in module */
            // Instantiate random number generator using system-supplied value as seed.
            Random rand = new();
            foreach (NwArea area in NwModule.Instance.Areas.Where(area => !area.IsInterior))
            {
                area.SetFogAmount(FogType.All, rand.Next(1, 12));

                Array values = FogColor.GetValues(typeof(FogColor));
                Random random = new();
                FogColor randomBar = (FogColor)values.GetValue(random.Next(values.Length));

                area.SetFogColor(FogType.All, randomBar);
            }
        }
    }
}
