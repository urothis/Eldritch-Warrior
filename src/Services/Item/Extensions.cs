using System.Linq;
using NWN.API;

namespace Services.Item
{
    public static class Extensions
    {
        public static bool HasTemporaryItemProperty(this NwItem nwItem) => nwItem.ItemProperties.Any(x => x.DurationType == EffectDuration.Temporary);

        public static string PrintGPValueOnItem(this NwItem nwItem)
            => !nwItem.PlotFlag
            ? (nwItem.Description = $"{"Gold Piece Value:".ColorString(Color.YELLOW)}{nwItem.GoldValue.ToString().ColorString(Color.ORANGE)}\n\n{nwItem.OriginalDescription}")
            : nwItem.OriginalDescription;

        public static void RemoveAllTemporaryItemProperties(this NwItem nwItem)
        {
            foreach (NWN.API.ItemProperty property in nwItem.ItemProperties.Where(x => x.DurationType == EffectDuration.Temporary))
            {
                nwItem.RemoveItemProperty(property);
            }
        }

        public static void SendMessageToAllPartyWithinDistance(this NwPlayer nwPlayer, string message, float distance)
        {
            foreach (NwPlayer member in nwPlayer.PartyMembers.Where(member => member.Distance(nwPlayer) == distance))
            {
                member.SendServerMessage(message);
            }
        }
    }
}