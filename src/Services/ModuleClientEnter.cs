using System;
using System.Globalization;
using System.Linq;

using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

namespace Module
{
    [ServiceBinding(typeof(ModuleClientEnter))]
    public class ModuleClientEnter
    {
        // Gets the server log. By default, this reports to "nwm.log"
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        // As this class has the ServiceBinding attribute, the constructor of this class will be called during server startup.
        // The EventService is a core service from NWN.Managed. As it is defined as a constructor parameter, it will be injected during startup.
        public ModuleClientEnter(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, OnClientEnter);

        // This function will be called as if the same script was called by a toolset event, or by another script.
        // This function must always return void, or a bool in the case of a conditional.
        private void OnClientEnter(ModuleEvents.OnClientEnter enter)
        {
            enter.Player.SendServerMessage($"Welcome to the server, {enter.Player.Name}!", Color.PINK);
        }
    }
}