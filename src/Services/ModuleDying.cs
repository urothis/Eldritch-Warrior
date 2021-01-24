//using NLog;
using System;

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

        private static void OnPlayerDying(ModuleEvents.OnPlayerDying dying)
        {
            _ = BleedOutAsync(dying, 1);
        }

        private static bool PlayerIsDead(ModuleEvents.OnPlayerDying dying) => dying.Player.HP <= 10 + dying.Player.Level;

        private static async System.Threading.Tasks.Task BleedOutAsync(ModuleEvents.OnPlayerDying dying, int bleedAmount)
        {
            /* keep executing recursively until character is dead or at +1 hit points */
            if (dying.Player.HP <= 0)
            {
                HealOrDamage(dying, bleedAmount);

                if (PlayerIsDead(dying))
                {
                    PlayerHasDied(dying);
                    return;
                }

                if (bleedAmount > 0)
                {
                    Stabilize(dying, ref bleedAmount);
                }
            }

            await NwTask.Delay(TimeSpan.FromSeconds(1));
            await BleedOutAsync(dying, bleedAmount);
        }

        private static void Stabilize(ModuleEvents.OnPlayerDying dying, ref int bleedAmount)
        {
            /* only check if character has not stablized */
            Random random = new();
            int dice = random.Next(1, 10);
            /* 10% chance to stablize */
            if (dice == 1)
            {
                /* reverse the bleeding process */
                bleedAmount--;
                dying.Player.PlayVoiceChat(VoiceChatType.GuardMe);
            }
            else
            {
                dice = random.Next(1, 5);
                ScreamOnDying(dying, dice);
            }
        }

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

        private static void HealOrDamage(ModuleEvents.OnPlayerDying dying, int bleedAmount)
        {
            /* a positive bleeding amount means damage, otherwise heal the character */
            switch (bleedAmount)
            {
                case > 0:
                    dying.Player.ApplyEffect(EffectDuration.Instant, Effect.Damage(bleedAmount));
                    break;
                default:
                    dying.Player.ApplyEffect(EffectDuration.Instant, Effect.Heal(-bleedAmount));
                    break;
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