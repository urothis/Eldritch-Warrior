using System;
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
        private static readonly string itemBankName = "ITEM_BANK";
        public ItemBank(ScriptEventService scriptEventService)
        {
            scriptEventService.SetHandler<PlaceableEvents.OnUsed>("bank_used_player", OnBankUsed);
            scriptEventService.SetHandler<PlaceableEvents.OnUsed>("bank_used_static", OnBankUsed);
            scriptEventService.SetHandler<PlaceableEvents.OnDisturbed>("bank_disturb", OnBankDisturbed);
            scriptEventService.SetHandler<PlaceableEvents.OnClose>("bank_onclose", OnBankClose);
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

        public static NwPlaceable GetPlayerBankObject(NwPlayer player, string objectTag)
        {
            var store = player.GetCampaignVariable<string>(itemBankName, player.UUID.ToUUIDString()).Value;
            return store != string.Empty ? NwObject.Deserialize<NwPlaceable>(store) : CreateNewBankObject(player, objectTag);
        }

        public static NwPlaceable CreateNewBankObject(NwPlayer player, string objectTag)
        {
            var bank = NwPlaceable.Create("_bank_", player.Location);
            // set variable on object
            bank.GetLocalVariable<string>("CHEST_TAG").Value = objectTag;
            // set value on player
            player.GetCampaignVariable<string>(itemBankName, player.UUID.ToUUIDString()).Value = bank.Serialize();
            return bank;
        }

        private static void BankRefuse(NwPlayer player, NwItem item)
        {
            var serializedItem = item.Serialize();
            item.Destroy();
            player.AcquireItem(NwItem.Deserialize<NwItem>(serializedItem));
        }

        private void OnBankUsed(PlaceableEvents.OnUsed onBankUsed)
        {
            if (onBankUsed.UsedBy is NwPlayer player)
            {
                player.ForceOpenInventory(GetPlayerBankObject(player, onBankUsed.Placeable.Tag));
            }
        }

        private void OnBankDisturbed(PlaceableEvents.OnDisturbed onBankDisturbed)
        {
            if (onBankDisturbed.Disturber is NwPlayer player)
            {
                var item = onBankDisturbed.DisturbedItem;
                var chest = onBankDisturbed.Placeable;
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

                        if (chest.Items.Count() > 10)
                        {
                            player.FloatingTextString("You can only store 10 items.");
                            BankRefuse(player, item);
                            return;
                        }

                        player.GetCampaignVariable<string>("banking_" + onBankDisturbed.Placeable.Tag, player.UUID.ToUUIDString()).Value = onBankDisturbed.Placeable.Serialize();
                        break;
                    case InventoryDisturbType.Removed:
                        player.GetCampaignVariable<string>("banking_" + onBankDisturbed.Placeable.Tag, player.UUID.ToUUIDString()).Value = onBankDisturbed.Placeable.Serialize();
                        break;
                    case InventoryDisturbType.Stolen:
                        break;
                }
            }
        }

        private void OnBankClose(PlaceableEvents.OnClose onBankDisturbed)
        {
            if (onBankDisturbed.LastClosedBy is not NwPlayer player) return;
            // save chest
            player.GetCampaignVariable<string>("banking_" + onBankDisturbed.Placeable.Tag, player.UUID.ToUUIDString()).Value = onBankDisturbed.Placeable.Serialize();

            // destroy all items inside the chest
            foreach (var itemInChest in onBankDisturbed.Placeable.Items)
            {
                itemInChest.Destroy();
            }

            // destroy
            onBankDisturbed.Placeable.Destroy();
        }
    }
}