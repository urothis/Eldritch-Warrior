using System.Linq;
using System.Numerics;

using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NWN.API.Constants;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(RecallStone))]
    public class RecallStone
    {
        public RecallStone(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnActivateItem>(NwModule.Instance, ActivateItem);

        private void ActivateItem(ModuleEvents.OnActivateItem activeItemEvent)
        {
            NwPlayer player = (NwPlayer)activeItemEvent.ItemActivator;
            Vector3 location = player.Position;
            string[] noTeleport = { "test" };
            var areaName = player.Area.Name;

            if (noTeleport.Contains(areaName))
            {
                player.SendServerMessage($"You cannot teleport out of {areaName.ToString().ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));
            }
            else if (player.IsInCombat)
            {
                player.SendServerMessage($"You cannot teleport while in combat.".ColorString(Color.ORANGE));
            }
            else
            {
                player.FloatingTextString("Teleporting commencing in...");
                CheckIsInBattle(player, 6, location);
            }
        }

        private static void CheckIsInBattle(NwPlayer player, int timeLeft, Vector3 location)
        {
            player.FloatingTextString($"{timeLeft}", false);

            player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.DurParalyzeHold));
            player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfPwkill));
            player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.ImpLightningS));

            //  Cancel teleport if conditions are met
            if (player.IsInCombat)
            {
                player.SendServerMessage("Cannot teleport while in combat.".ColorString(Color.ORANGE));
                return;
            }

            if (player.Position != location)
            {
                player.SendServerMessage("Cannot teleport while in moving.".ColorString(Color.ORANGE));
                return;
            }

            if (timeLeft == 0)
            {
                player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfElectricExplosion));
                //Teleport
            }
            else
            {
                timeLeft--;
                //Recursive
            }
        }
    }
}