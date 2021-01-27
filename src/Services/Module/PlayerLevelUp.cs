//using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.API.Constants;
using NWN.Services;
using System;
using System.Threading.Tasks;

namespace Services.Module
{
    [ServiceBinding(typeof(PlayerLevelUp))]

    public class PlayerLevelUp
    {
        public PlayerLevelUp(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerLevelUp>(NwModule.Instance, OnPlayerLevelUp);

        private static async void OnPlayerLevelUp(ModuleEvents.OnPlayerLevelUp levelUp)
        {
            levelUp.Player.SaveCharacter();

            await LevelUpVfx(levelUp);
        }

        private static async Task LevelUpVfx(ModuleEvents.OnPlayerLevelUp levelUp)
        {
            if (levelUp.Player.Level % 10 == 0)
            {
                await NwTask.Delay(TimeSpan.FromSeconds(0.1));
                levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfWailOBanshees));

            }

            if (levelUp.Player.Level % 5 == 0)
            {
                await NwTask.Delay(TimeSpan.FromSeconds(0.8));
                levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.ImpLightningS));
            }

            switch (levelUp.Player.GoodEvilAlignment)
            {
                case Alignment.Evil:
                    await NwTask.Delay(TimeSpan.FromSeconds(0.2));
                    levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfLosEvil10));
                    await NwTask.Delay(TimeSpan.FromSeconds(0.4));
                    levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfLosEvil20));
                    await NwTask.Delay(TimeSpan.FromSeconds(0.6));
                    levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfLosEvil30));
                    break;
                case Alignment.Good:
                    await NwTask.Delay(TimeSpan.FromSeconds(0.2));
                    levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfLosHoly10));
                    await NwTask.Delay(TimeSpan.FromSeconds(0.5));
                    levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfLosHoly20));
                    await NwTask.Delay(TimeSpan.FromSeconds(0.6));
                    levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfLosHoly30));
                    break;
                default:
                    await NwTask.Delay(TimeSpan.FromSeconds(0.2));
                    levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfLosNormal10));
                    await NwTask.Delay(TimeSpan.FromSeconds(0.4));
                    levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfLosNormal20));
                    await NwTask.Delay(TimeSpan.FromSeconds(0.6));
                    levelUp.Player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfLosNormal30));
                    break;
            }
        }
    }
}