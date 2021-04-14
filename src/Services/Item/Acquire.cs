//using NLog;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(Acquire))]

    public class Acquire
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Acquire()
        {
            NwModule.Instance.OnAcquireItem += acquireItem =>
            {
                /*
                    if statement is here to stop
                    System.NullReferenceException: Object reference not set to an instance of an object.
                */
                if (acquireItem.Item is NwItem)
                {
                    acquireItem.Item.PrintGPValueOnItem();
                    acquireItem.Item.RemoveAllTemporaryItemProperties();
                    NotifyLoot(acquireItem);
                }

                /* This is to short circuit the rest of this code if we are DM */
                if (acquireItem.AcquiredBy is NwPlayer { IsDM: true })
                {
                    return;
                }

                if (acquireItem.AcquiredBy is NwPlayer && acquireItem.AcquiredFrom is NwPlayer)
                {
                    FixBarterExploit(acquireItem);
                }
            };
        }

        private static void NotifyLoot(ModuleEvents.OnAcquireItem acquireItem) => SendLootMessageToParty(acquireItem, $"{acquireItem.AcquiredBy.Name.ColorString(Color.PINK)} obtained {acquireItem.Item.BaseItemType.ToString().ColorString(Color.WHITE)}.", 40);

        private static void SendLootMessageToParty(ModuleEvents.OnAcquireItem acquireItem, string message, float distance)
        {
            if (acquireItem.AcquiredBy is NwPlayer player)
            {
                if (!player.IsDM)
                {
                    player.SendMessageToAllPartyWithinDistance(message, distance);
                }

                player.SendServerMessage(message);
            }
        }

        /*
            If you are bartering items with another player and you have items in the temporary inventories
            that someone dropped to offer you and you logout and login you will dupe those items. This is part of the
            code to stop bartering exploits that are similar to the one above but it doesn't stop that exploit. Basically
            you just create an invalid state or force one to put an item in to dupe it.
        */
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