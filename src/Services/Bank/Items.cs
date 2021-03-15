using System;
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
        }

        private void Close(PlaceableEvents.OnClose obj)
        {
            var pc = obj.LastClosedBy;
            Location loc = obj.LastClosedBy.Location;
            string name = pc.Name;
            string chestTag = obj.Placeable.GetLocalVariable<string>("CHEST_TAG");
        }
    }
}