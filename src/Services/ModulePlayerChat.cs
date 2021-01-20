//using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NWN.API.Constants;

using NWNX.API;

namespace Module
{
    [ServiceBinding(typeof(ModulePlayerChat))]
    public class ModulePlayerChat
    {
        private static readonly char playerWildcard = '!';
        private static readonly string notReady = "Feature not implemented.";

        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModulePlayerChat(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerChat>(NwModule.Instance, PlayerChat);

        // handle chat commands
        private void PlayerChat(ModuleEvents.OnPlayerChat playerChat) => ChatTools(playerChat);

        private static void ChatTools(ModuleEvents.OnPlayerChat chat)
        {
            chat.Message = chat.Message.ToLower();

            if (chat.Message.StartsWith(playerWildcard))
            {
                chat.Message = chat.Message[1..];
                string[] chatArray = chat.Message.Split(' ');

                if (chatArray[0].Equals("portrait", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetPortrait(chat, chatArray);
                }

                if (chatArray[0].Equals("voice", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetVoice(chat, chatArray);
                }

                if (chatArray[0].Equals("skin", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetSkin(chat, chatArray);
                }

                if (chatArray[0].Equals("hair", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetHair(chat, chatArray);
                }

                if (chatArray[0].Equals("tattoocolor2", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetTattooColor2(chat, chatArray);
                }
            }
        }

        private static int SetPortrait(ModuleEvents.OnPlayerChat chat, string[] chatArray) => int.TryParse(chatArray[1], out int n) ? (chat.Sender.PortraitId = n) : 0;

        private static string SetVoice(ModuleEvents.OnPlayerChat chat, string[] chatArray) => int.TryParse(chatArray[1], out _) ? (chat.Message = notReady) : chat.Message;

        private static void SetSkin(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.SetColor(ColorChannel.Skin, n);
            }
        }

        private static void SetHair(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.SetColor(ColorChannel.Hair, n);
            }
        }

        private static void SetTattooColor1(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.SetColor(ColorChannel.Tattoo1, n);
            }
        }

        private static void SetTattooColor2(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.SetColor(ColorChannel.Tattoo2, n);
            }
        }
    }
}