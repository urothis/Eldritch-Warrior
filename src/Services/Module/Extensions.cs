using NWN.API;

namespace Services.Module
{
    public static class Extensions
    {
        public static void SaveCharacter(this NwPlayer player)
        {
            player.ExportCharacter();
            player.SendServerMessage($"{player.BicFileName.ColorString(Color.GREEN)}.bic file has been saved.".ColorString(Color.WHITE));
        }
    }
}