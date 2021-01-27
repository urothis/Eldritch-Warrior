using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(PlayerRest))]
    public class PlayerRest
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        // constructor
        public PlayerRest(NativeEventService nativeEventService) => nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerRest>(NwModule.Instance, OnPlayerRest);

        private static void OnPlayerRest(ModuleEvents.OnPlayerRest playerRest)
        {
            
        }
    }
}