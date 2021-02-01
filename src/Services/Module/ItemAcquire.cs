//using NLog;

using NWN.API;
using NWN.API.Events;

using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(ItemAcquire))]

    public class ItemAcquire
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ItemAcquire(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnAcquireItem>(NwModule.Instance, OnAcquireItem);

        private static void OnAcquireItem(ModuleEvents.OnAcquireItem acquireItem)
        {
            /*
                if statement is here to stop
                System.NullReferenceException: Object reference not set to an instance of an object.
            */
            if (acquireItem.Item is NwItem)
            {
                acquireItem.Item.PrintGPValueOnItem();
                acquireItem.Item.RemoveAllTemporaryItemProperties();
            }

            /* This is to short circuit the rest of this code if we are DM */
            if (acquireItem.AcquiredBy is NwPlayer { IsDM: true })
            {
                return;
            }

            if (acquireItem.AcquiredBy is NwPlayer && acquireItem.AcquiredFrom is NwPlayer)
            {
                FixBarterExploit(acquireItem);
                return;
            }

            NotifyLoot(acquireItem);
        }

        private static void NotifyLoot(ModuleEvents.OnAcquireItem acquireItem) => SendLootMessageToParty(acquireItem, $"{acquireItem.AcquiredBy.Name.ColorString(Color.PINK)} obtained {acquireItem.Item.BaseItemType.ToString().ColorString(Color.WHITE)}.", 40);

        private static void SendLootMessageToParty(ModuleEvents.OnAcquireItem acquireItem, string message, float distance)
        {
            if (acquireItem.AcquiredBy is NwPlayer player)
            {
                player.SendServerMessage(message);
                player.SendMessageToAllPartyWithinDistance(message, distance);
            }
        }

        private static void FixBarterExploit(ModuleEvents.OnAcquireItem acquireItem)
        {
            if (acquireItem.AcquiredBy is NwPlayer playerA && acquireItem.AcquiredFrom is NwPlayer playerB)
            {
                playerA.SaveCharacter();
                playerB.SaveCharacter();
            }
        }
    }
}