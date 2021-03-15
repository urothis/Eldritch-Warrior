using NWN.API.Events;

namespace Services.Bank
{
    public static class Extensions
    {
        public static bool ValidateItemBankPlaceable(this PlaceableEvents.OnClose obj)
        {
            return true;
        }
    }
}