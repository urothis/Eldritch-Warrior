using System.Linq;

using NWN.API;
using NWN.API.Events;

namespace Services.Module
{
    public static class ModuleExtensions
    {
        public static string PrintGPValueOnItem(this NwItem nwItem) => !nwItem.PlotFlag
            ? (nwItem.Description = $"{"Gold Piece Value:".ColorString(new Color(255, 255, 0))}{nwItem.GoldValue.ToString().ColorString(new Color(255, 165, 0))}\n\n{nwItem.OriginalDescription}")
            : nwItem.OriginalDescription;

        /* Store hitpoints */
        public static void ClientStoreHitPoints(this NwPlayer player) => player.GetCampaignVariable<int>("Hit_Points", player.Name).Value = player.HP;
        public static void ClientRestoreHitPoints(this NwPlayer player) => player.HP = player.GetCampaignVariable<int>("Hit_Points", player.Name).Value;
        public static bool HasItemByResRef(this NwPlayer player, string nwItem) => player.Items.Any(x => x.ResRef == nwItem);
    }
}