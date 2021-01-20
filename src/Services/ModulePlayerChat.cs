using System.Text;
//using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

namespace Module
{
    [ServiceBinding(typeof(ModulePlayerChat))]
    public class ModulePlayerChat
    {
        private static readonly char wildcard = '!';
        private static readonly string notReady = "Feature not implemented.";

        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModulePlayerChat(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerChat>(NwModule.Instance, PlayerChat);

        // handle chat commands
        private void PlayerChat(ModuleEvents.OnPlayerChat playerChat) => ChatTools(playerChat);

        private static void ChatTools(ModuleEvents.OnPlayerChat chat)
        {
            chat.Message = chat.Message.ToLower();

            if (chat.Message.StartsWith(wildcard))
            {
                chat.Message = chat.Message[1..];
                string[] chatArray = chat.Message.Split(' ');

                if (chatArray[0].Equals("portrait", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetPortrait(chat, chatArray);
                    return;
                }
                
                if (chatArray[0].Equals("voice", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetVoice(chat, chatArray);
                    return;
                }
            }
        }

        private static void SetPortrait(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.PortraitId = n;
            }
        }

        private static void SetVoice(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out _))
            {
                chat.Message = notReady;
            }
        }
    }
}