using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.Bank
{
    [ServiceBinding(typeof(Items))]
    public class Items
    {
        [ScriptHandler("bank_close")]
        private static void Close(PlaceableEvents.OnClose obj) => obj.CheckClosedItems();

        [ScriptHandler("bank_onused")]
        private static void Used(PlaceableEvents.OnUsed obj) => ((NwPlayer)obj.UsedBy).ForceOpenInventory(obj.CheckUsed());

        [ScriptHandler("bank_disturb")]
        private static void Disturbed(PlaceableEvents.OnDisturbed obj)
        {
            switch (obj.DisturbType)
            {
                case InventoryDisturbType.Added:
                case InventoryDisturbType.Removed:
                    obj.DisturbedItem.CheckDisturbedItems(obj, (NwPlayer)obj.Disturber);
                    break;
            }
        }
    }
}