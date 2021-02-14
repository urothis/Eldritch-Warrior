using System;
using System.Collections.Generic;
using System.Linq;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

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
        }

        private static bool StopScript(NwCreature creature) => TwoHandBoostCheckCreatureSize(creature) || TwoHandBoostCheckShield(creature);

        private static bool TwoHandBoostCheckShield(NwCreature creature) => creature.GetItemInSlot(InventorySlot.LeftHand).BaseItemType switch
        {
            BaseItemType.LargeShield or BaseItemType.SmallShield or BaseItemType.TowerShield => true,
            _ => false,
        };

        private static bool TwoHandBoostCheckCreatureSize(NwCreature creature)
        {
            return creature.Size switch
            {
                CreatureSize.Huge or CreatureSize.Invalid or CreatureSize.Large => true,
                _ => false,
            };
        }
    }
}
