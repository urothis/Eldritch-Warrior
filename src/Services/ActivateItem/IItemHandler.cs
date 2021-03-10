//using NLog;
using NWN.API.Events;

namespace Services.ActivateItem
{
    public interface IItemHandler
    {
        public string Tag { get; }

        public void HandleActivateItem(ModuleEvents.OnActivateItem activateItem);
    }
}