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
            Console.WriteLine($"SERVER LOADED:{DateTime.Now.ToString(@"yyyy/MM/dd hh:mm:ss tt", new CultureInfo("en-US"))}");

            /* NWNX */
            Administration.GameOptions.RestoreSpellUses = true;

            /* Set Fog Color an Amount in all outdoor areas */
            SetAreaEnviroment();
        }

        private static void SetAreaEnviroment()
        {
            /* Iterate all areas in module */
            // Instantiate random number generator using system-supplied value as seed.
            Random random = new();

            foreach (NwArea area in NwModule.Instance.Areas.Where(area => !area.IsInterior))
            {
                var values = Enum.GetValues(typeof(FogColor));
                FogColor fogColor = (FogColor)values.GetValue(random.Next(values.Length));

                area.SetFogColor(FogType.All, fogColor);
                area.SetFogAmount(FogType.All, random.Next(1, 12));

                Skybox skybox = (Skybox)random.Next(Enum.GetNames(typeof(Skybox)).Length);
                area.SkyBox = skybox;

                WeatherType weather = (WeatherType)random.Next(Enum.GetNames(typeof(Skybox)).Length);
                area.Weather = weather;

                if (area.SkyBox == Skybox.GrassStorm)
                {
                    area.Weather = WeatherType.Rain;
                }

                if (area.SkyBox == Skybox.Icy)
                {
                    area.Weather = WeatherType.Snow;
                }
            }
        }
    }
}
