using System;
using NWN.API;
using NWN.API.Constants;

namespace Services.PlayerCharacter
{
    public static class Extensions
    {
        
        public static void PlayerHasStabilized(this NwPlayer pc)
        {
            pc.PlayVoiceChat(VoiceChatType.GuardMe);
            pc.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.ImpHealingS));
            pc.ApplyEffect(EffectDuration.Instant, Effect.Resurrection());
            pc.SendServerMessage($"{pc.Name} has stabilized.");
        }

        public static void PlayerHasDied(this NwPlayer pc)
        {
            pc.PlayVoiceChat(VoiceChatType.Death);
            pc.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.ImpDeath));
            pc.ApplyEffect(EffectDuration.Instant, Effect.Death());
        }

        public static void ScreamOnDying(this NwPlayer pc)
        {
            pc.ApplyEffect(EffectDuration.Instant, Effect.Damage(1));
            Random random = new();
            switch (random.Next(1, 6))
            {
                case 1: pc.PlayVoiceChat(VoiceChatType.Cuss); break;
                case 2: pc.PlayVoiceChat(VoiceChatType.NearDeath); break;
                case 3: pc.PlayVoiceChat(VoiceChatType.Pain1); break;
                case 4: pc.PlayVoiceChat(VoiceChatType.Pain2); break;
                case 5: pc.PlayVoiceChat(VoiceChatType.Pain3); break;
            }
        }
    }
}