using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

namespace Module
{
    [ServiceBinding(typeof(ModuleClientEnter))]
    public class ModuleClientEnter
    {
        // Gets the server log. By default, this reports to "nwm.log"
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        // As this class has the ServiceBinding attribute, the constructor of this class will be called during server startup.
        // The EventService is a core service from NWN.Managed. As it is defined as a constructor parameter, it will be injected during startup.
        public ModuleClientEnter(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, OnClientEnter);

        // This function will be called as if the same script was called by a toolset event, or by another script.
        // This function must always return void, or a bool in the case of a conditional.
        private async void OnClientEnter(ModuleEvents.OnClientEnter enter)
        {
            await ClientPrintLogin(enter);
            ClientEnterJournal(enter);

            if (enter.Player.IsDM)
            {
                return;
            }
        }

        /* List of DM Public Keys */
        private static readonly Dictionary<string, string> dmID = new()
        {
            { "QR4JFL9A", "milliorn" },
            { "QRMXQ6GM", "milliorn" },
        };

        /* Add default journal entries */
        private static void ClientEnterJournal(ModuleEvents.OnClientEnter enter)
        {
            enter.Player.AddJournalQuestEntry("test", 1, false);
        }

        /*
            https://gist.github.com/Jorteck/f7049ca1995ccea4dd5d4886f8c4254e

            Print to shout of client logging in if we are PC.
            Print to dm channel if dm logs in.
        */
        private static async Task ClientPrintLogin(ModuleEvents.OnClientEnter enter)
        {
            string colorString = $"\n{"NAME".ColorString(Color.GREEN)}:{enter.Player.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{enter.Player.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{Player.GetBicFileName(enter.Player).ColorString(Color.WHITE)}";
            string client = $"NAME:{enter.Player.Name} ID:{enter.Player.CDKey} BIC:{Player.GetBicFileName(enter.Player)}";

            if (enter.Player.IsDM && dmID.ContainsKey(enter.Player.CDKey))
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Entering DM ID VERIFIED".ColorString(Color.GREEN)}:{colorString}");
                Log.Info($"DM VERIFIED:{client}.");
                Console.WriteLine($"DM VERIFIED:{client}.");

            }
            else if (enter.Player.IsDM)
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Entering DM ID DENIED".ColorString(Color.RED)}:{colorString}");
                Log.Info($"DM DENIED:{client}.");
                Console.WriteLine($"DM DENIED:{client}.");
            }
            else
            {
                await NwModule.Instance.SpeakString($"\n{"LOGIN".ColorString(Color.GREEN)}:{colorString}", TalkVolume.Shout);
                Log.Info($"LOGIN:{client}.");
                Console.WriteLine($"LOGIN:{client}.");
            }
        }
    }
}
