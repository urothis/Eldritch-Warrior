using System;
using NWN.API;
using NWN.API.Events;

using NWN.Services;
namespace Services.Store
{
    [ServiceBinding(typeof(Infinite))]

    public class Infinite
    {
        public Infinite(ScriptEventService script) => script.SetHandler<StoreEvents.OnOpen>("plc_sell_loot", Open);


        private void Open(StoreEvents.OnOpen obj)
        {

        }
    }
}