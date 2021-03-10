using System;
using System.Linq;
using System.Numerics;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(IItemHandler))]
    public class RecallStone : IItemHandler
    {
        public string Tag => "itm_recall";
        private string[] noTeleport = { "test123" };

        public void HandleActivateItem(ModuleEvents.OnActivateItem activateItem)
        {
            NwPlayer player = (NwPlayer)activateItem.ItemActivator;
            Vector3 location = player.Position;
            string areaName = player.Area.Name;

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
                player.FloatingTextString("Teleporting commencing in...".ColorString(Color.GREEN));
                CheckIsInBattle(player, 6, location);
            }
        }

        private static async void CheckIsInBattle(NwPlayer player, int timeLeft, Vector3 location)
        {
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
                player.SendServerMessage("Cannot teleport while moving.".ColorString(Color.ORANGE));
                return;
            }

            if (timeLeft == 0)
            {
                player.SendServerMessage("Teleporting...".ColorString(Color.GREEN));
                player.PlayVoiceChat(VoiceChatType.Goodbye);
                player.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.FnfElectricExplosion));
                player.Location = NwModule.Instance.StartingLocation;
            }
            else
            {
                player.FloatingTextString($"{timeLeft}", false);
                timeLeft--;
                await NwTask.Delay(TimeSpan.FromSeconds(1));
                CheckIsInBattle(player, timeLeft, location);
            }
        }
    }
}