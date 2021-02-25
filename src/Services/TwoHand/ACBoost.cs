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
            if (creature.StopScript())
            {
                return;
            }
            else if (creature.CheckCreaturekSizeAndWeapon())
            {
                creature.AddBuff();
                return;
            }
            else
            {
                creature.RemoveBuff();
            }
        }
    }
}
