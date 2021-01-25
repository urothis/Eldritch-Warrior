using System;
using System.Threading.Tasks;
using NLog;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Module
{
    [ServiceBinding(typeof(PlayerDying))]
    public class PlayerDying
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        // constructor
        public PlayerDying(NativeEventService nativeEventService, SchedulerService scheduler)
        {
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerDying>(NwModule.Instance, OnPlayerDying);
            scheduler.ScheduleRepeating(Bleed, TimeSpan.FromSeconds(1));

        }

        private static void OnPlayerDying(ModuleEvents.OnPlayerDying dying)
        {

        }

        private static void Bleed()
        {
            NwModule.Instance.SpeakString(DateTime.Now.ToString(), TalkVolume.Shout);
        }

        private static bool PlayerIsDead(NwPlayer player) => player.HP <= -10;

        private static void ScreamOnDying(NwPlayer player, int dice)
        {
            switch (dice)
            {
                case 1: player.PlayVoiceChat(VoiceChatType.Cuss); break;
                case 2: player.PlayVoiceChat(VoiceChatType.HealMe); break;
                case 3: player.PlayVoiceChat(VoiceChatType.NearDeath); break;
                case 4: player.PlayVoiceChat(VoiceChatType.Pain1); break;
                case 5: player.PlayVoiceChat(VoiceChatType.Pain2); break;
                case 6: player.PlayVoiceChat(VoiceChatType.Pain3); break;
            }
        }

        private static void PlayerHasDied(NwPlayer player)
        {
            player.PlayVoiceChat(VoiceChatType.Death);
            player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.ImpDeath));
            player.ApplyEffect(EffectDuration.Instant, Effect.Death());
        }
    }
}