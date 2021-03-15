using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.Bank
{
    [ServiceBinding(typeof(Items))]
    public class Items
    {
        public Items(ScriptEventService script)
        {
            script.SetHandler<PlaceableEvents.OnClose>("bank_close", Close);
            script.SetHandler<PlaceableEvents.OnDisturbed>("bank_disturb", Disturbed);
            script.SetHandler<PlaceableEvents.OnUsed>("bank_onused", Used);
        }
        private static void Close(PlaceableEvents.OnClose obj) => obj.CheckClosedItems();
        private static void Used(PlaceableEvents.OnUsed obj) => ((NwPlayer)obj.UsedBy).ForceOpenInventory(obj.CheckUsed());

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