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
            //Log.Debug(unacquireItem);
            /*
                if statement is here to stop
                System.NullReferenceException: Object reference not set to an instance of an object.
            */
            if (unacquireItem.Item is NwItem)
            {
                unacquireItem.Item.PrintGPValueOnItem();
                unacquireItem.Item.RemoveAllTemporaryItemProperties();
            }

            /* This is to short circuit the rest of this code if we are DM */
            if (unacquireItem.LostBy is NwPlayer nwPlayer)
            {
                nwPlayer.SaveCharacter();
            }
        }
    }
}