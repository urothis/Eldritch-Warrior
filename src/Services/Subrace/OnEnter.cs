//using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Subrace
{
    [ServiceBinding(typeof(OnEnter))]
    public class OnEnter
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public OnEnter(NativeEventService nativeEventService) => nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, OnClientEnter);

        private static void OnClientEnter(ModuleEvents.OnClientEnter onClient) => onClient.SubRaceEnter();
    }
}