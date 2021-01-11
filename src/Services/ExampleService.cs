using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NLog;

namespace Module
{
    [ServiceBinding(typeof(ExampleService))]
    public class ExampleService
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public ExampleService(NativeEventService nativeEventService) => nativeEventService.Subscribe<NwModule, ModuleEvents.OnModuleLoad>(NwModule.Instance, OnModuleLoad);

        private void OnModuleLoad(ModuleEvents.OnModuleLoad eventInfo)
        {
            Log.Warn("Module Boot");
        }
    }
}