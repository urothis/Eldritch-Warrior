using NWN.API;
using NWN.API.Events;

using NWN.Services;

namespace Services.TwoHand
{
    [ServiceBinding(typeof(ACBoost))]
    public class ACBoost
    {
        public ACBoost(NativeEventService nativeEventService)
        {
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerEquipItem>(NwModule.Instance, PlayerEquipItem);
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerUnequipItem>(NwModule.Instance, PlayerUnequipItem);
        }

        private void PlayerUnequipItem(ModuleEvents.OnPlayerUnequipItem unequipItem) => (unequipItem.UnequippedBy as NwCreature).RemoveBuff();
        private void PlayerEquipItem(ModuleEvents.OnPlayerEquipItem equipItem)
        {
            var pc = equipItem.Player as NwCreature;

            if (!pc.CheckCreatureSize() || !pc.HasShieldEquipped() && pc.CheckCreaturekSizeAndWeapon())
            {
                pc.AddBuff();
            }
        }
    }
}
