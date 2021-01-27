using System.Linq;

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
            acquireItem.Item.PrintGPValueOnItem();
            acquireItem.Item.CheckAndRemoveTemporaryItemProperties();

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
                playerA.ExportCharacter();
                playerB.ExportCharacter();
                playerA.SendServerMessage("Server-vault character saved.");
                playerB.SendServerMessage("Server-vault character saved.");
            }
        }
    }
}