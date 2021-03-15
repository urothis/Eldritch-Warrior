using System;
using NLog;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;

using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(ClientEnter))]
    public class ClientEnter
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ClientEnter(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, OnClientEnter);

        private void OnClientEnter(ModuleEvents.OnClientEnter enter)
        {
            /* Check player name and boot if its inappropriate */
            if (ClientCheckName(enter, enter.Player.Name))
            {
                //enter.Player.Delete($"{enter.Player.BicFileName}.bic has been deleted.");
                return;
            }

            WelcomeMessage(enter);

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

        private static void WelcomeMessage(ModuleEvents.OnClientEnter enter)
        {
            enter.Player.SendServerMessage("Welcome to the server!".ColorString(SelectRandomColor(new(0, 0, 0), (Random)(new()))));

            string colorString = $"\n{"NAME".ColorString(Color.GREEN)}:{enter.Player.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{enter.Player.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{enter.Player.BicFileName.ColorString(Color.WHITE)}";
            string clientDM = $"NAME:{enter.Player.Name} ID:{enter.Player.CDKey}";

            if (enter.Player.IsDM && Extensions.DMList.ContainsKey(enter.Player.CDKey))
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
                NwModule.Instance.SpeakString($"\n{"LOGIN".ColorString(Color.LIME)}:{colorString}", TalkVolume.Shout);
                Log.Info($"LOGIN:{$"NAME:{enter.Player.Name} ID:{enter.Player.CDKey} BIC:{enter.Player.BicFileName}"}.");
            }
        }

        private static Color SelectRandomColor(Color color, Random random)
        {
            switch (random.Next(0, 16))
            {
                case 0: color = Color.BLUE; break;
                case 1: color = Color.BROWN; break;
                case 2: color = Color.CYAN; break;
                case 3: color = Color.GRAY; break;
                case 4: color = Color.GREEN; break;
                case 5: color = Color.LIME; break;
                case 6: color = Color.MAGENTA; break;
                case 7: color = Color.MAROON; break;
                case 8: color = Color.NAVY; break;
                case 9: color = Color.OLIVE; break;
                case 10: color = Color.ORANGE; break;
                case 11: color = Color.PINK; break;
                case 12: color = Color.PURPLE; break;
                case 13: color = Color.RED; break;
                case 14: color = Color.ROSE; break;
                case 15: color = Color.SILVER; break;
                case 16: color = Color.TEAL; break;
            }
            return color;
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
                if (Extensions.WordFilter.Contains(censoredWord.ToLower()))
                {
                    enter.Player.BootPlayer($"BOOTED - Inappropriate character name {censoredWord} in {enter.Player.Name}");
                    Log.Info($"BOOTED - Inappropriate character name {censoredWord} in {enter.Player.Name}");
                    return true;
                }
            }
            return false;
        }
    }
}
