using System.Collections.Generic;
using System.Linq;

//using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Module
{
    [ServiceBinding(typeof(ModuleItemAcquire))]

    public class ModuleItemAcquire
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ModuleItemAcquire(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnAcquireItem>(NwModule.Instance, OnAcquireItem);

        private static void OnAcquireItem(ModuleEvents.OnAcquireItem acquireItem)
        {
            PrintGPValueOnItem(acquireItem);
            RemoveAllItemProperties(acquireItem);

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

            SendMessageToAllPartyInArea(acquireItem, acquireItem.Item.Name, 40);
        }

        private static string PrintGPValueOnItem(ModuleEvents.OnAcquireItem acquireItem) => !acquireItem.Item.PlotFlag
                ? (acquireItem.Item.Description = $"{"Gold Piece Value:".ColorString(new Color(255, 255, 0))}{acquireItem.Item.GoldValue.ToString().ColorString(new Color(255, 165, 0))}\n\n{acquireItem.Item.OriginalDescription}")
                : acquireItem.Item.OriginalDescription;

        private static void SendMessageToAllPartyInArea(ModuleEvents.OnAcquireItem acquireItem, string message, float distance)
        {
            if (acquireItem.AcquiredBy is NwPlayer player)
            {
                player.SendServerMessage(message);
                
                foreach (NwPlayer member in player.PartyMembers.Where(member => member.Distance(player) == distance))
                {
                    member.SendServerMessage(message);
                }
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

        private static void RemoveAllItemProperties(ModuleEvents.OnAcquireItem acquireItem)
        {
            if (acquireItem.Item.ItemProperties.Any(x => x.DurationType == EffectDuration.Temporary))
            {
                IEnumerable<ItemProperty> tempIP = acquireItem.Item.ItemProperties.Where(x => x.DurationType == EffectDuration.Temporary);

                foreach (ItemProperty property in tempIP)
                {
                    acquireItem.Item.RemoveItemProperty(property);
                }
            }
        }
    }
}