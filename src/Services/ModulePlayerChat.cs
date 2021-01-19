using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Module
{
    [ServiceBinding(typeof(ModulePlayerChat))]
    public class ModulePlayerChat
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModulePlayerChat(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerChat>(NwModule.Instance, PlayerChat);

        // handle chat commands
        private void PlayerChat(ModuleEvents.OnPlayerChat playerChat)
        {
            if (playerChat.Message.Equals("picture"))
            {
                Log.Warn("if (playerChat.Message.Equals(picture))");
                playerChat.Sender.PortraitId = 100;
            }
        }
    }
}