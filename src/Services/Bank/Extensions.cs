using System.Linq;
using NWN.API;
using NWN.API.Events;

namespace Services.Bank
{
    public static class Extensions
    {
        public static readonly int maxItem = 30;
        public static readonly string bankItem = "ITEM_BANK";
        public static bool ValidateChestLimit(this PlaceableEvents.OnClose obj) => obj.Placeable.Inventory.Items.Count<NwItem>() <= maxItem;
        public static bool ItemHasInventory(this PlaceableEvents.OnClose obj) => obj.Placeable.Inventory.Items.Any<NwItem>(item => item.HasInventory);
    }
}