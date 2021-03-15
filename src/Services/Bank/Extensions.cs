using System.Linq;
using NWN.API;
using NWN.API.Events;
using NWNX.API;

namespace Services.Bank
{
    public static class Extensions
    {
        public static readonly int maxItem = 30;
        public static readonly string bankItem = "ITEM_BANK";
        public static bool ValidateChestLimit(this PlaceableEvents.OnClose obj) => obj.Placeable.Inventory.Items.Count<NwItem>() <= maxItem;
        public static bool ValidateChestLimit(this PlaceableEvents.OnDisturbed obj) => obj.Placeable.Inventory.Items.Count<NwItem>() <= maxItem;
        public static bool ItemHasInventory(this PlaceableEvents.OnClose obj) => obj.Placeable.Inventory.Items.Any<NwItem>(item => item.HasInventory);

        public static void CopyDestroyItem(this NwItem item, NwPlayer pc)
        {
            item.Destroy();
            item.Clone(pc);
        }

        public static void CheckDisturbedItems(this NwItem item, PlaceableEvents.OnDisturbed obj, NwPlayer pc)
        {
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
        }

        public static void CheckClosedItems(this PlaceableEvents.OnClose obj)
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
                var chest = NwPlaceable.Create(obj.Placeable.ResRef, obj.Placeable.Location, false, obj.Placeable.Tag);
                chest.Name = obj.Placeable.Name;
            }
        }

        public static NwPlaceable CheckUsed(this PlaceableEvents.OnUsed obj)
        {
            NwPlayer pc = (NwPlayer)obj.UsedBy;
            string store = pc.GetCampaignVariable<string>($"{Extensions.bankItem}_{pc.Area.Name}", pc.UUID.ToUUIDString()).Value;
            return store != string.Empty ? NwObject.Deserialize<NwPlaceable>(store) : CreateNewBankObject(pc, obj.Placeable.Tag);
        }

        private static NwPlaceable CreateNewBankObject(NwPlayer player, string objectTag)
        {
            NwPlaceable bank = NwPlaceable.Create("_bank_", player.Location);
            bank.GetLocalVariable<string>("CHEST_TAG").Value = objectTag;
            player.GetCampaignVariable<string>(Extensions.bankItem, player.UUID.ToUUIDString()).Value = bank.Serialize();
            return bank;
        }
    }
}