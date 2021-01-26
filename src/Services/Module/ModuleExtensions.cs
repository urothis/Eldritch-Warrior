using NWN.API;

namespace Services.Module
{
    public static class ModuleExtensions
    {
        public static string PrintGPValueOnItem(this NwItem nwItem) => !nwItem.PlotFlag
            ? (nwItem.Description = $"{"Gold Piece Value:".ColorString(new Color(255, 255, 0))}{nwItem.GoldValue.ToString().ColorString(new Color(255, 165, 0))}\n\n{nwItem.OriginalDescription}")
            : nwItem.OriginalDescription;
    }
}