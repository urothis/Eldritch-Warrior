using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Module
{
    [ServiceBinding(typeof(ModulePlayerDying))]
    public class ModulePlayerDying
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModulePlayerDying(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerDying>(NwModule.Instance, OnPlayerDying);

        private static void OnPlayerDying(ModuleEvents.OnPlayerDying dying)
        {
        }
    }
}