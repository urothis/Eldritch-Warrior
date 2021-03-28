using System.Linq;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.LootSplitter
{
    [ServiceBinding(typeof(Placeable))]
    public class Placeable
    {
        [ScriptHandler("plc_sell_loot")]
        private void Disturbed(PlaceableEvents.OnDisturbed obj)
        {
            //Process only objects added
            switch (obj.DisturbType)
            {
                case InventoryDisturbType.Added:
                    DestroyItemForGold(obj);
                    break;
                case InventoryDisturbType.Removed:
                case InventoryDisturbType.Stolen:
                    break;
            }
        }

        private static void DestroyItemForGold(PlaceableEvents.OnDisturbed obj)
        {
            NwPlayer pc = (NwPlayer)obj.Disturber;

            if (obj.DisturbedItem.BaseItemType == BaseItemType.Gold)
            {
                GiveGoldEqually(pc, obj.DisturbedItem.StackSize);
                CloneDestroy(obj, pc);
            }
            else if (obj.DisturbedItem.HasInventory)
            {
                pc.FloatingTextString($"{pc.Name} cannot sell inventory items {pc.Name.ColorString(Color.WHITE)}!".ColorString(Color.ORANGE));
                CloneDestroy(obj, pc);

            }
            else if (obj.DisturbedItem.PlotFlag)
            {
                pc.FloatingTextString($"{pc.Name} cannot sell plot items {pc.Name.ColorString(Color.WHITE)}!".ColorString(Color.ORANGE));
                CloneDestroy(obj, pc);
            }
            else
            {
                int itemValue = obj.DisturbedItem.GoldValue / 10 > 0 ? obj.DisturbedItem.GoldValue / 10 : 1;

                pc.FloatingTextString($"{pc.Name} sold {pc.Name.ColorString(Color.WHITE)} for {itemValue}!".ColorString(Color.GREEN));
                GiveGoldEqually(pc, itemValue);
            }
        }

        private static void GiveGoldEqually(NwPlayer pc, int itemValue)
        {
            int goldDivided = itemValue / pc.PartyMembers.Count<NwPlayer>();
            pc.FloatingTextString($"{goldDivided.ToString().ColorString(Color.WHITE)} given to each player after splitting {itemValue.ToString().ColorString(Color.WHITE)} from {pc.Name.ColorString(Color.WHITE)}.");

            foreach (NwPlayer player in pc.PartyMembers)
            {
                player.GiveGold(goldDivided, true);
            }
        }

        private static void CloneDestroy(PlaceableEvents.OnDisturbed obj, NwPlayer pc) => obj.DisturbedItem.Clone(pc).Destroy();
    }
}