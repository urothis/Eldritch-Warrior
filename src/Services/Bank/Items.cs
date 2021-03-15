using System.Linq;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Bank
{
    [ServiceBinding(typeof(Items))]
    public class Items
    {
        public Items(ScriptEventService script)
        {
            script.SetHandler<PlaceableEvents.OnClose>("bank_close", Close);
        }

        private void Close(PlaceableEvents.OnClose obj)
        {
            NwPlayer pc = (NwPlayer)obj.LastClosedBy;
            Location loc = obj.LastClosedBy.Location;
            string name = pc.Name;
            string chestTag = obj.Placeable.GetLocalVariable<string>("CHEST_TAG");

            if (!obj.ValidateChestLimit())
            {
                pc.FloatingTextString($"Maximum item count of {Extensions.maxItem} exceeded! Current item count is {obj.Placeable.Inventory.Items.Count<NwItem>()}.");
                return;
            }
            else if (obj.ItemHasInventory())
            {
                pc.FloatingTextString("Item with inventories cannot be stored in a bank chest.");
                return;
            }
        }
    }
}