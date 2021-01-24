//using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Module
{
    [ServiceBinding(typeof(ModulePlayerDying))]
    public class ModulePlayerDying
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModulePlayerDying(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerDying>(NwModule.Instance, OnPlayerDying);

        private static void OnPlayerDying(ModuleEvents.OnPlayerDying dying) => BleedOut(dying, 1);

        private static bool PlayerIsDead(ModuleEvents.OnPlayerDying dying) => dying.Player.HP <= 10;

        private static void BleedOut(ModuleEvents.OnPlayerDying dying, int bleedAmount)
        {
            dying.Player.HP = bleedAmount > 0 ? dying.Player.HP-- : dying.Player.HP++;

            if (PlayerIsDead(dying))
            {
                PlayerHasDied(dying);
                return;
            }
        }

        private static void PlayerHasDied(ModuleEvents.OnPlayerDying dying)
        {
            dying.Player.PlayVoiceChat(VoiceChatType.Death);
            dying.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.ImpDeath));
            dying.Player.ApplyEffect(EffectDuration.Instant, Effect.Death());
        }
    }
}