using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Module
{
    [ServiceBinding(typeof(ModuleClientLeave))]
    public class ModuleClientLeave
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModuleClientLeave(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientLeave>(NwModule.Instance, OnClientLeave);

        private static void OnClientLeave(ModuleEvents.OnClientLeave leave)
        {
            /* This is to short circuit the rest of this code if we are DM */
            if (leave.Player.IsDM)
            {
                return;
            }

            Log.Info(leave.Player.Name);
        }
    }
}