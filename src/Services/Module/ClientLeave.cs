using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(ClientLeave))]
    public class ClientLeave
    {
        public ClientLeave(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnClientLeave>(NwModule.Instance, OnClientLeave);

        private static void OnClientLeave(ModuleEvents.OnClientLeave leave)
        {
            leave.ClientPrintLogout();

            if (!leave.Player.IsDM)
            {
                leave.ClientLeaveDeathLog();
            }

            leave.Player.ClientStoreHitPoints();
        }
    }
}