using System.Collections.Generic;
//using NLog;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(ItemHandlerService))]
    public class ItemHandlerService
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<string, IItemHandler> itemHandlers = new();

        public ItemHandlerService(IEnumerable<IItemHandler> handlers)
        {
            foreach (IItemHandler itemHandler in handlers)
                itemHandlers[itemHandler.Tag] = itemHandler;

            NwModule.Instance.OnActivateItem += OnActivateItem;
        }
        private void OnActivateItem(ModuleEvents.OnActivateItem activateItem)
        {
            //logger.Info(activateItem.ActivatedItem.Name);

            if (itemHandlers.TryGetValue(activateItem.ActivatedItem.Tag, out var handler))
                handler.HandleActivateItem(activateItem);
        }
    }
}