using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(IItemHandler))]
    public class Sequencer : IItemHandler
    {
        public string Tag => "itm_sequencer";

        public void HandleActivateItem(ModuleEvents.OnActivateItem activateItem)
        {

        }
    }
}