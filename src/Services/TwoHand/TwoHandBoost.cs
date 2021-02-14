using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;
using System;

namespace Services.TwoHand
{
    [ServiceBinding(typeof(ACBoost))]
    public class ACBoost
    {
        public ACBoost(NativeEventService nativeEventService)
        {
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerEquipItem>(NwModule.Instance, OnPlayerEquipItem);
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerUnequipItem>(NwModule.Instance, OnPlayerUnequipItem);
        }

        private void OnPlayerUnequipItem(ModuleEvents.OnPlayerUnequipItem unequipItem) => TwoHandBoost(unequipItem.UnequippedBy);
        private void OnPlayerEquipItem(ModuleEvents.OnPlayerEquipItem equipItem) => TwoHandBoost(equipItem.Player);

        private static void TwoHandBoost(NwCreature creature)
        {
            if (StopScript(creature))
            {
                return;
            }
            else if (CheckCreaturekSizeAndWeapon(creature))
            {
                
            }
        }

        private static bool CheckCreaturekSizeAndWeapon(NwCreature creature)
        {
            if (creature.Size.Equals(CreatureSize.Medium) && HasTwoHandLargeWeapon(creature))
            {
                return true;
            }
            else if (creature.Size.Equals(CreatureSize.Small) && HasTwoHandBoostMediumWeapon(creature))
            {
                return true;
            }
            else if (creature.Size.Equals(CreatureSize.Tiny) && HasTwoHandBoostSmallmWeapon(creature))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool HasTwoHandBoostSmallmWeapon(NwCreature creature)
        {
            return creature.GetItemInSlot(InventorySlot.RightHand).BaseItemType switch
            {
                BaseItemType.Handaxe or
                BaseItemType.Kama => true,
                BaseItemType.LightCrossbow or
                BaseItemType.LightHammer or
                BaseItemType.LightMace or
                BaseItemType.Shortsword or
                BaseItemType.Sickle or
                BaseItemType.Sling or
                BaseItemType.ThrowingAxe or
                _ => false,
            };
        }

        private static bool HasTwoHandBoostMediumWeapon(NwCreature creature)
        {
            return creature.GetItemInSlot(InventorySlot.RightHand).BaseItemType switch
            {
                BaseItemType.Battleaxe or
                BaseItemType.Bastardsword or
                BaseItemType.Club or
                BaseItemType.DwarvenWaraxe or
                BaseItemType.HeavyCrossbow or
                BaseItemType.Katana => true,
                BaseItemType.Morningstar or
                BaseItemType.LightFlail or
                BaseItemType.Longsword or
                BaseItemType.MagicStaff or
                BaseItemType.Rapier or
                BaseItemType.Scimitar or
                BaseItemType.Shortbow or
                BaseItemType.Warhammer or
                _ => false,
            };
        }

        private static bool HasTwoHandLargeWeapon(NwCreature creature)
        {
            return creature.GetItemInSlot(InventorySlot.RightHand).BaseItemType switch
            {
                BaseItemType.DireMace or 
                BaseItemType.Doubleaxe or 
                BaseItemType.Greataxe or 
                BaseItemType.Greatsword or 
                BaseItemType.Halberd or 
                BaseItemType.HeavyFlail or 
                BaseItemType.Longbow or
                BaseItemType.Quarterstaff or 
                BaseItemType.Scythe or 
                BaseItemType.ShortSpear or 
                BaseItemType.Trident or 
                BaseItemType.TwoBladedSword => true,
                _ => false,
            };
        }

        private static bool StopScript(NwCreature creature) => CheckCreatureSize(creature) || HasShieldEquipped(creature);

        private static bool HasShieldEquipped(NwCreature creature) => creature.GetItemInSlot(InventorySlot.LeftHand).BaseItemType switch
        {
            BaseItemType.LargeShield or BaseItemType.SmallShield or BaseItemType.TowerShield => true,
            _ => false,
        };

        private static bool CheckCreatureSize(NwCreature creature)
        {
            return creature.Size switch
            {
                CreatureSize.Huge or CreatureSize.Invalid or CreatureSize.Large => true,
                _ => false,
            };
        }
    }
}
