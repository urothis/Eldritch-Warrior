using System.Linq;
using System.Numerics;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

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
                CheckIsInBattle(player, 6, location);
            }
        }

        private void CheckIsInBattle(NwPlayer player, int v, Vector3 location)
        {
            throw new System.NotImplementedException();
        }
    }
}