using System.Collections.Generic;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.ActivateItem
{
    public interface IItemHandler
    {
        public string Tag { get; }

        public void HandleActivateItem(ModuleEvents.OnActivateItem activateItem);
    }

    [ServiceBinding(typeof(ItemHandlerService))]
    public class ItemHandlerService
    {
        private readonly Dictionary<string, IItemHandler> itemHandlers = new();

        public ItemHandlerService(IEnumerable<IItemHandler> handlers)
        {
            foreach (IItemHandler itemHandler in handlers)
                itemHandlers[itemHandler.Tag] = itemHandler;

            NwModule.Instance.OnActivateItem += OnActivateItem;
        }
        private void OnActivateItem(ModuleEvents.OnActivateItem activateItem)
        {
            if (itemHandlers.TryGetValue(activateItem.ActivatedItem.Tag, out var handler))
                handler.HandleActivateItem(activateItem);
        }
    }
}