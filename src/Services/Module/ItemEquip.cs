//using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(ItemEquip))]
    public class ItemEquip
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        // constructor
        public ItemEquip(NativeEventService nativeEventService) => nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerEquipItem>(NwModule.Instance, OnPlayerEquipItem);

        private static void OnPlayerEquipItem(ModuleEvents.OnPlayerEquipItem equipItem) => equipItem.Item.PrintGPValueOnItem();
    }
}