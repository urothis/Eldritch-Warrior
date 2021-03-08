using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NWN.API.Constants;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(AmmoDuplicator))]
    public class AmmoDuplicator
    {
        public AmmoDuplicator(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnActivateItem>(NwModule.Instance, ActivateItem);

        private void ActivateItem(ModuleEvents.OnActivateItem item)
        {
            NwCreature pc = item.ItemActivator;
            NwGameObject target = item.TargetObject;
            switch ()
            {
                default:
                break;
            }
        }
    }
}