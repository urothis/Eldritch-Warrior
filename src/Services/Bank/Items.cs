using System;
using System.Linq;
using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NWN.API.Constants;
using NWNX.API;

namespace Services.Bank
{
    [ServiceBinding(typeof(Items))]
    public class Items
    {
        public Items(ScriptEventService script)
        {
            script.SetHandler<PlaceableEvents.OnClose>("bank_close", Close);
            script.SetHandler<PlaceableEvents.OnDisturbed>("bank_disturb", Disturbed);
        }

        private void Disturbed(PlaceableEvents.OnDisturbed obj)
        {
            NwPlayer pc = (NwPlayer)obj.Disturber;
            NwItem item = obj.DisturbedItem;

            switch (obj.DisturbType)
            {
                case InventoryDisturbType.Added:
                case InventoryDisturbType.Removed:
                    if (item.HasInventory)
                    {
                        pc.FloatingTextString($"{item.Name} has an inventory. Cannot bank this item.");
                        item.CopyDestroyItem(pc);
                    }
                    else if (item.PlotFlag)
                    {
                        pc.FloatingTextString($"{item.Name} is marked plot. Cannot bank this item.");
                        item.CopyDestroyItem(pc);
                    }
                    else if (item.Stolen)
                    {
                        pc.FloatingTextString($"{item.Name} is marked stolen. Cannot bank this item.");
                        item.CopyDestroyItem(pc);
                    }
                    else if (!obj.ValidateChestLimit())
                    {
                        pc.FloatingTextString($"Maximum item count of {Extensions.maxItem} exceeded! Current item count is {obj.Placeable.Inventory.Items.Count<NwItem>()}.");
                        item.CopyDestroyItem(pc);
                    }
                    else
                    {
                        pc.GetCampaignVariable<string>($"{Extensions.bankItem}_{pc.Area.Name}", pc.UUID.ToUUIDString()).Value = obj.Placeable.Serialize();
                    }
                    break;
            }
        }

        private void Close(PlaceableEvents.OnClose obj)
        {
            NwPlayer pc = (NwPlayer)obj.LastClosedBy;

            if (!obj.ValidateChestLimit())
            {
                pc.FloatingTextString($"Maximum item count of {Extensions.maxItem} exceeded! Current item count is {obj.Placeable.Inventory.Items.Count<NwItem>()}.");
            }
            else if (obj.ItemHasInventory())
            {
                pc.FloatingTextString("Item with inventories cannot be stored in a bank chest.");
            }
            else if (obj.Placeable.Inventory.Items.Any<NwItem>(i => i.PlotFlag))
            {
                pc.FloatingTextString("Items marked plot are not allowed.");
            }
            else if (obj.Placeable.Inventory.Items.Any<NwItem>(i => i.Stolen))
            {
                pc.FloatingTextString("Stolen items are not allowed.");
            }
            else
            {
                pc.GetCampaignVariable<string>($"{Extensions.bankItem}_{pc.Area.Name}", pc.UUID.ToUUIDString()).Value = obj.Placeable.Serialize();
                obj.Placeable.Destroy();
            }
        }
    }
}