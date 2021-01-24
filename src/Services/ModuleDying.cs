using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Module
{
    [ServiceBinding(typeof(ModulePlayerDying))]
    public class ModulePlayerDying
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModulePlayerDying(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerDying>(NwModule.Instance, OnPlayerDying);

        private static void OnPlayerDying(ModuleEvents.OnPlayerDying dying)
        {
            BleedOut(dying, 1);
        }

        private static void BleedOut(ModuleEvents.OnPlayerDying dying, int bleedAmount)
        {
            dying.Player.HP = bleedAmount > 0 ? dying.Player.HP-- : dying.Player.HP++;

            ScreamOnDeath(dying);
        }

        private static void ScreamOnDeath(ModuleEvents.OnPlayerDying dying)
        {
            if (dying.Player.HP <= 10)
            {
                dying.Player.PlayVoiceChat(VoiceChatType.Death);
            }
        }
    }
}