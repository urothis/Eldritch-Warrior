using System;
using System.Threading.Tasks;
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
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModulePlayerDying(NativeEventService native, SchedulerService scheduler)
        {
            native.Subscribe<NwModule, ModuleEvents.OnPlayerDying>(NwModule.Instance, OnPlayerDying);
            scheduler.ScheduleRepeating(CheckState, TimeSpan.FromSeconds(1));
        }

        private static async void OnPlayerDying(ModuleEvents.OnPlayerDying dying)
        {
        }

        private static async Task CheckState()
        {
            Random random = new();

            while (dying.Player.HP < -10)
            {
                int dice = random.Next(1, 10);
                /* 10% chance to stablize */
                if (dice == 10)
                {
                    dying.Player.PlayVoiceChat(VoiceChatType.GuardMe);
                    dying.Player.HP = 1;
                    return;
                }
                else
                {
                    dice = random.Next(1, 5);
                    ScreamOnDying(dying, dice);
                    dying.Player.HP--;
                }
            }
            PlayerHasDied(dying);
        }

        private static bool PlayerIsDead(ModuleEvents.OnPlayerDying dying) => dying.Player.HP <= -10;

        private static void ScreamOnDying(ModuleEvents.OnPlayerDying dying, int dice)
        {
            switch (dice)
            {
                case 1: dying.Player.PlayVoiceChat(VoiceChatType.Cuss); break;
                case 2: dying.Player.PlayVoiceChat(VoiceChatType.HealMe); break;
                case 3: dying.Player.PlayVoiceChat(VoiceChatType.NearDeath); break;
                case 4: dying.Player.PlayVoiceChat(VoiceChatType.Pain1); break;
                case 5: dying.Player.PlayVoiceChat(VoiceChatType.Pain2); break;
                case 6: dying.Player.PlayVoiceChat(VoiceChatType.Pain3); break;
                default: break;
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