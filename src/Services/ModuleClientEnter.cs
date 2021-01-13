using System.Threading.Tasks;
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
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        // As this class has the ServiceBinding attribute, the constructor of this class will be called during server startup.
        // The EventService is a core service from NWN.Managed. As it is defined as a constructor parameter, it will be injected during startup.
        public ModuleClientEnter(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, OnClientEnter);

        // This function will be called as if the same script was called by a toolset event, or by another script.
        // This function must always return void, or a bool in the case of a conditional.
        private async void OnClientEnter(ModuleEvents.OnClientEnter enter)
        {
            await ClientPrintLogin(enter);
        }

        private static async Task ClientPrintLogin(ModuleEvents.OnClientEnter enter)
        {
            /*
                https://gist.github.com/Jorteck/f7049ca1995ccea4dd5d4886f8c4254e
            */
            Log.Info($"Client enter event called by {enter.Player.Name}");

            await NwModule.Instance.SpeakString($"{"LOGIN".ColorString(Color.GREEN)}:\n{"NAME".ColorString(Color.GREEN)}:{enter.Player.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{enter.Player.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{Player.GetBicFileName(enter.Player).ColorString(Color.WHITE)}", TalkVolume.Shout);
        }
    }
}