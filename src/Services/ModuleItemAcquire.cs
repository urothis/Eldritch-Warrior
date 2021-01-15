using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Module
{
    [ServiceBinding(typeof(ModuleItemAcquire))]

    public class ModuleItemAcquire
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ModuleItemAcquire(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnAcquireItem>(NwModule.Instance, OnAcquireItem);

        private static void OnAcquireItem(ModuleEvents.OnAcquireItem acquireItem)
        {

        }
    }
}