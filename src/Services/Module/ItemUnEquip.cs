using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(ItemUnEquip))]
    public class ItemUnEquip
    {
        public ItemUnEquip(NativeEventService nativeEventService) => nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerUnequipItem>(NwModule.Instance, OnPlayerUnequipItem);

        private static void OnPlayerUnequipItem(ModuleEvents.OnPlayerUnequipItem unequipItem)
        {
            unequipItem.Item.PrintGPValueOnItem();

            if (unequipItem.UnequippedBy is NwPlayer nwPlayer)
            {
                nwPlayer.SaveCharacter();
            }
        }
    }
}