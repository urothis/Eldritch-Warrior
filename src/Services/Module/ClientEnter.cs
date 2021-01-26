using System.Collections.Generic;
using System.Threading.Tasks;

using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

namespace Services.Module
{
    [ServiceBinding(typeof(ClientEnter))]
    public class ClientEnter
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ClientEnter(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, OnClientEnter);

        private async void OnClientEnter(ModuleEvents.OnClientEnter enter)
        {
            /* Check player name and boot if its inappropriate */
            if (ClientCheckName(enter, enter.Player.Name))
            {
                Administration.DeletePlayerCharacter(enter.Player, false);
                return;
            }

            await ClientPrintLogin(enter);

            /* Add default journal entries */
            ClientEnterJournal(enter);

            /* This is to short circuit the rest of this code if we are DM */
            if (enter.Player.IsDM)
            {
                return;
            }

            /* Check if we are brand new player. */
            ClientFirstLogin(enter);

            /* Restore hitpoints */
            enter.Player.ClientRestoreHitPoints();
        }

        private static void ClientEnterJournal(ModuleEvents.OnClientEnter enter) => enter.Player.AddJournalQuestEntry("test", 1, false);

        private static void ClientFirstLogin(ModuleEvents.OnClientEnter enter)
        {
            if (!enter.Player.HasItemByResRef("itm_recall"))
            {
                enter.Player.DestroyAllItems();
            }
        }

        private static bool ClientCheckName(ModuleEvents.OnClientEnter enter, string text)
        {
            string[] censoredText = text.Split(' ');

            foreach (string censoredWord in censoredText)
            {
                if (ModuleExtensions.WordFilter.Contains(censoredWord.ToLower()))
                {
                    enter.Player.BootPlayer($"BOOTED - Inappropriate character name {censoredWord} in {enter.Player.Name}");
                    Log.Info($"BOOTED - Inappropriate character name {censoredWord} in {enter.Player.Name}");
                    return true;
                }
            }
            return false;
        }

        /*
            https://gist.github.com/Jorteck/f7049ca1995ccea4dd5d4886f8c4254e

            Print to shout of client logging in if we are PC.
            Print to dm channel if dm logs in.
        */
        private static async Task ClientPrintLogin(ModuleEvents.OnClientEnter enter)
        {
            string colorString = $"\n{"NAME".ColorString(Color.GREEN)}:{enter.Player.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{enter.Player.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{Player.GetBicFileName(enter.Player).ColorString(Color.WHITE)}";
            string clientDM = $"NAME:{enter.Player.Name} ID:{enter.Player.CDKey}";

            if (enter.Player.IsDM && ModuleExtensions.DMList.ContainsKey(enter.Player.CDKey))
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Entering DM ID VERIFIED".ColorString(Color.GREEN)}:{colorString}");
                Log.Info($"DM VERIFIED:{clientDM}.");

            }
            else if (enter.Player.IsDM)
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Entering DM ID DENIED".ColorString(Color.RED)}:{colorString}");
                Log.Info($"DM DENIED:{clientDM}.");
                enter.Player.BootPlayer("DENIED DM Access.");
            }
            else
            {
                await NwModule.Instance.SpeakString($"\n{"LOGIN".ColorString(Color.LIME)}:{colorString}", TalkVolume.Shout);
                Log.Info($"LOGIN:{$"NAME:{enter.Player.Name} ID:{enter.Player.CDKey} BIC:{Player.GetBicFileName(enter.Player)}"}.");
            }
        }
    }
}
