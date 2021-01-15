using System.Threading.Tasks;

using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

namespace Module
{
    [ServiceBinding(typeof(ModuleClientLeave))]
    public class ModuleClientLeave
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModuleClientLeave(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientLeave>(NwModule.Instance, OnClientLeaveAsync);

        private async void OnClientLeaveAsync(ModuleEvents.OnClientLeave leave)
        {
            await ClientPrintLogout(leave);

            /* This is to short circuit the rest of this code if we are DM */
            if (leave.Player.IsDM)
            {
                return;
            }
            
            ClientLeaveDeathLog(leave);
        }

        /* Auto-Kill if we logout while in combat state */
        private static int ClientLeaveDeathLog(ModuleEvents.OnClientLeave leave)
        {
            if (leave.Player.IsInCombat)
            {
                return leave.Player.HP = -1;
            }
            return 0;
        }

        private static async Task ClientPrintLogout(ModuleEvents.OnClientLeave leave)
        {
            string colorString = $"\n{"NAME".ColorString(Color.GREEN)}:{leave.Player.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{leave.Player.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{Player.GetBicFileName(leave.Player).ColorString(Color.WHITE)}";
            string client = $"NAME:{leave.Player.Name} ID:{leave.Player.CDKey} BIC:{Player.GetBicFileName(leave.Player)}";
            string clientDM = $"NAME:{leave.Player.Name} ID:{leave.Player.CDKey}";

            if (leave.Player.IsDM)
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Exiting DM".ColorString(Color.GREEN)}:{colorString}");
                Log.Info($"DM Exiting:{clientDM}.");

            }
            else
            {
                await NwModule.Instance.SpeakString($"\n{"LOGOUT".ColorString(Color.GREEN)}:{colorString}", TalkVolume.Shout);
                Log.Info($"LOGOUT:{client}.");
            }
        }
    }
}