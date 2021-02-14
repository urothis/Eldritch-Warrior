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

        private void OnPlayerUnequipItem(ModuleEvents.OnPlayerUnequipItem unequipItem) => TwoHandBoost();

        private void OnPlayerEquipItem(ModuleEvents.OnPlayerEquipItem equipItem) => TwoHandBoost();

        private void TwoHandBoost()
        {
            if (TwoHandBoostCheckCreatureSize(oPC) || TwoHandBoostCheckShield(oPC))
            {
                return;
            }

            object oItem = GetItemInSlot(INVENTORY_SLOT_RIGHTHAND, oPC);

            if (TwoHandBoostCheckSizeAndWeapon(oPC))
            {
                TwoHandBoostBuffAdd(oPC);
                return;
            }

            else
            {
                TwoHandBoostBuffRemoved(oPC);
            }
        }
    }
}
