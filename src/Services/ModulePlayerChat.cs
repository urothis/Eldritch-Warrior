using System;
using System.Diagnostics;

using NLog;

using NWN.API;
using NWN.API.Events;

using NWN.Services;


namespace Module
{
    [ServiceBinding(typeof(ModulePlayerChat))]
    public class ModulePlayerChat
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        
        public ModulePlayerChat(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerChat>(NwModule.Instance, OnChatMessage);

        // handle chat commands
        private void OnChatMessage(ModuleEvents.OnPlayerChat eventInfo)
        {
        }
    }
}