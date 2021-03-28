using NLog;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(ClientLeave))]
    public class ClientLeave
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public ClientLeave()
        {
            NwModule.Instance.OnClientLeave += leave =>
            {
                ClientPrintLogout(leave);

                if (!leave.Player.IsDM)
                {
                    ClientLeaveDeathLog(leave);
                }

                leave.Player.ClientStoreHitPoints();
            };
        }

        /* Auto-Kill if we logout while in combat state */
        private static int ClientLeaveDeathLog(ModuleEvents.OnClientLeave leave) => leave.Player.IsInCombat ? leave.Player.HP = -1 : leave.Player.HP;

        private static async void ClientPrintLogout(ModuleEvents.OnClientLeave leave)
        {
            string colorString = $"\n{"NAME".ColorString(Color.GREEN)}:{leave.Player.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{leave.Player.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{leave.Player.BicFileName.ColorString(Color.WHITE)}";
            string client = $"NAME:{leave.Player.Name} ID:{leave.Player.CDKey} BIC:{leave.Player.BicFileName}";
            string clientDM = $"NAME:{leave.Player.Name} ID:{leave.Player.CDKey}";

            if (leave.Player.IsDM)
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Exiting DM".ColorString(Color.GREEN)}:{colorString}");
                Log.Info($"DM Exiting:{clientDM}.");
            }
            else
            {
                await NwModule.Instance.SpeakString($"\n{"LOGOUT".ColorString(Color.LIME)}:{colorString}", TalkVolume.Shout);
                Log.Info($"LOGOUT:{client}.");
            }
        }
    }
}