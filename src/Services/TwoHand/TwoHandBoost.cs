using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

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
            else if (TwoHandBoostCheckSizeAndWeapon(creature))
            {
                
            }
        }

        private static bool TwoHandBoostCheckSizeAndWeapon(NwCreature creature)
        {
            if (creature.Size.Equals(CreatureSize.Medium))
            {
                return true;
            }
            if (creature.Size.Equals(CreatureSize.Small))
            {
                return true;
            }
            if (creature.Size.Equals(CreatureSize.Tiny))
            {
                return true;
            }
            else
            {
                return false;
            }
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
