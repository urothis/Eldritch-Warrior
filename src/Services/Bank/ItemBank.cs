using System.Collections.Generic;
using System.Linq;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

namespace Services.Bank
{
    [ServiceBinding(typeof(ItemBank))]
    public class ItemBank
    {
        private static readonly string itemBankCampaign = "ITEM_BANK_";

        public ItemBank(ScriptEventService scriptEventService)
        {
            scriptEventService.SetHandler<PlaceableEvents.OnClose>("bank_onclose", OnBankClose);
            scriptEventService.SetHandler<PlaceableEvents.OnDisturbed>("bank_disturb", OnBankDisturbed);
            scriptEventService.SetHandler<PlaceableEvents.OnUsed>("bank_used_player", OnBankUsed);
            scriptEventService.SetHandler<PlaceableEvents.OnUsed>("bank_used_static", OnBankUsed);
        }

        private static readonly List<BaseItemType> BlockedItemTypes = new()
        {
            BaseItemType.CreatureBludgeoningWeapon,
            BaseItemType.CreaturePiercingWeapon,
            BaseItemType.CreaturePiercingWeapon,
            BaseItemType.CreatureItem,
            BaseItemType.CreatureSlashingWeapon,
            BaseItemType.CreatureSlashingAndPiercingWeapon,
            BaseItemType.Gold,
            BaseItemType.Invalid,
            BaseItemType.LargeBox,
        };

        private static readonly List<string> BlockedTag = new()
        {
        };


        private void OnBankClose(PlaceableEvents.OnClose onBankDisturbed)
        {
            if (onBankDisturbed.LastClosedBy is not NwPlayer player) return;
            // save chest
            player.GetCampaignVariable<string>(itemBankCampaign + onBankDisturbed.Placeable.Area.Tag, player.UUID.ToUUIDString()).Value = onBankDisturbed.Placeable.Serialize();

            // destroy all items inside the chest
            //foreach (NwItem itemInChest in onBankDisturbed.Placeable.HasInventory.Items)
            foreach (NwItem itemInChest in onBankDisturbed.Placeable.HasInventory)
            {
                itemInChest.Destroy();
            }

            // destroy
            onBankDisturbed.Placeable.Destroy();
        }

        private void OnBankUsed(PlaceableEvents.OnUsed onUsed)
        {
            if (onUsed.UsedBy is NwPlayer player)
            {
                player.ForceOpenInventory(GetPlayerBankObject(player, onUsed.Placeable.Tag));
            }
        }

        private void OnBankDisturbed(PlaceableEvents.OnDisturbed onBankDisturbed)
        {
            if (onBankDisturbed.Disturber is NwPlayer player)
            {
                NwItem item = onBankDisturbed.DisturbedItem;
                switch (onBankDisturbed.DisturbType)
                {
                    case InventoryDisturbType.Added:
                        if (item.StackSize < 1)
                        {
                            player.FloatingTextString("Stacking items not allowed.");
                            BankRefuse(player, item);
                            return;
                        }

                        if (item.HasInventory)
                        {
                            player.FloatingTextString("Items with inventory not allowed.");
                            BankRefuse(player, item);
                            return;
                        }

                        if (BlockedItemTypes.Any(x => x == item.BaseItemType))
                        {
                            player.FloatingTextString("Illegal Item type, not allowed.");
                            BankRefuse(player, item);
                            return;
                        }

                        if (BlockedTag.Any(x => x == item.Tag))
                        {
                            player.FloatingTextString("Illegal item.");
                            BankRefuse(player, item);
                            return;
                        }

                        int itemLimit = 75;
                        if (onBankDisturbed.Placeable.Inventory.Items.Count() > itemLimit)
                        {
                            player.FloatingTextString($"You can only store {itemLimit} items.");
                            BankRefuse(player, item);
                            return;
                        }

                        player.GetCampaignVariable<string>(itemBankCampaign + onBankDisturbed.Placeable.Tag, player.UUID.ToUUIDString()).Value = onBankDisturbed.Placeable.Serialize();
                        break;
                    case InventoryDisturbType.Removed:
                        player.GetCampaignVariable<string>(itemBankCampaign + onBankDisturbed.Placeable.Tag, player.UUID.ToUUIDString()).Value = onBankDisturbed.Placeable.Serialize();
                        break;
                    case InventoryDisturbType.Stolen:
                        break;
                }
            }
        }

        private static void BankRefuse(NwPlayer player, NwItem item)
        {
            string serializedItem = item.Serialize();
            item.Destroy();
            player.AcquireItem(NwObject.Deserialize<NwItem>(serializedItem));
        }

        private static NwPlaceable GetPlayerBankObject(NwPlayer player, string objectTag)
        {
            string store = player.GetCampaignVariable<string>(itemBankCampaign, player.UUID.ToUUIDString()).Value;
            return store != string.Empty ? NwObject.Deserialize<NwPlaceable>(store) : CreateNewBankObject(player, objectTag);
        }

        private static NwPlaceable CreateNewBankObject(NwPlayer player, string objectTag)
        {
            NwPlaceable bank = NwPlaceable.Create("_bank_", player.Location);
            // set variable on object
            bank.GetLocalVariable<string>("CHEST_TAG").Value = objectTag;
            // set value on player
            player.GetCampaignVariable<string>(itemBankCampaign, player.UUID.ToUUIDString()).Value = bank.Serialize();
            return bank;
        }
    }
}