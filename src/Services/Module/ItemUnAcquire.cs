//using NLog;

using NWN.API;
using NWN.API.Events;

using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(ItemUnAcquire))]

    public class ItemUnAcquire
    {
        public ItemUnAcquire(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnUnacquireItem>(NwModule.Instance, OnUnacquireItem);

        private static void OnUnacquireItem(ModuleEvents.OnUnacquireItem unacquireItem)
        {
            unacquireItem.Item.PrintGPValueOnItem();

            /* This is to short circuit the rest of this code if we are DM */
            if (unacquireItem.LostBy is NwPlayer { IsDM: true })
            {
                return;
            }
        }
    }
}