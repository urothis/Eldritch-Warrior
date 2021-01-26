using System;

//using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;

using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(PlayerDying))]
    public class PlayerDying
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        // constructor
        public PlayerDying(NativeEventService nativeEventService) => nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerDying>(NwModule.Instance, OnPlayerDying);

        private static void OnPlayerDying(ModuleEvents.OnPlayerDying dying) => Bleed(dying);

        private async static void Bleed(ModuleEvents.OnPlayerDying dying)
        {
            await NwTask.WhenAny(NwTask.Run(async () =>
            {
                await NwTask.Delay(TimeSpan.FromSeconds(1));
            }));

            ScreamOnDying(dying);

            Random random = new();
            int stabilize = random.Next(1, 10);
            dying.Player.SendServerMessage($"Stabilize roll:{stabilize.ToString().ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));

            if (dying.Player.HP <= -127)
            {
                PlayerHasDied(dying);
                return;
            }
            else if (stabilize == 1)
            {
                PlayerHasStabilized(dying);
                return;
            }
            else
            {
                Bleed(dying);
            }
        }

        private static void PlayerHasStabilized(ModuleEvents.OnPlayerDying dying)
        {
            dying.Player.PlayVoiceChat(VoiceChatType.GuardMe);
            dying.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.ImpHealingS));
            dying.Player.ApplyEffect(EffectDuration.Instant, Effect.Resurrection());
            dying.Player.SendServerMessage($"{dying.Player.Name} has stabilized.");
        }

        private static void PlayerHasDied(ModuleEvents.OnPlayerDying dying)
        {
            dying.Player.PlayVoiceChat(VoiceChatType.Death);
            dying.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.ImpDeath));
            dying.Player.ApplyEffect(EffectDuration.Instant, Effect.Death());
        }

        private static void ScreamOnDying(ModuleEvents.OnPlayerDying dying)
        {
            dying.Player.ApplyEffect(EffectDuration.Instant, Effect.Damage(1));
            Random random = new();
            switch (random.Next(1, 6))
            {
                case 1: dying.Player.PlayVoiceChat(VoiceChatType.Cuss); break;
                case 2: dying.Player.PlayVoiceChat(VoiceChatType.NearDeath); break;
                case 3: dying.Player.PlayVoiceChat(VoiceChatType.Pain1); break;
                case 4: dying.Player.PlayVoiceChat(VoiceChatType.Pain2); break;
                case 5: dying.Player.PlayVoiceChat(VoiceChatType.Pain3); break;
            }
        }
    }
}