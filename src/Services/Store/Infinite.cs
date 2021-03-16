using NWN.API.Events;

using NWN.Services;
namespace Services.Store
{
    [ServiceBinding(typeof(Infinite))]

    public class Infinite
    {
        public Infinite(ScriptEventService script) => script.SetHandler<StoreEvents.OnOpen>("store_infinite", Open);

        private void Open(StoreEvents.OnOpen obj)
        {
            foreach (var item in obj.Player.Inventory.Items)
            {
                item.Infinite = true;
                item.Stolen = true;
            }
        }
    }
}