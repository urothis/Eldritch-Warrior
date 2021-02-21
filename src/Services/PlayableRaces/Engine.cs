using NWN.API;
using NWN.API.Events;

using NWN.Services;

namespace Services.PlayableRaces
{
    [ServiceBinding(typeof(OnEnter))]
    public class OnEnter
    {
        public OnEnter(NativeEventService nativeEventService) => nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, OnClientEnter);

        private static void OnClientEnter(ModuleEvents.OnClientEnter onClient)
        {

        }
    }
}