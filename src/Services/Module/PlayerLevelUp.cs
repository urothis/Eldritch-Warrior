//using NLog;

using NWN.API;
using NWN.API.Events;

using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(PlayerLevelUp))]

    public class PlayerLevelUp
    {
        public PlayerLevelUp(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerLevelUp>(NwModule.Instance, OnPlayerLevelUp);

        private static void OnPlayerLevelUp(ModuleEvents.OnPlayerLevelUp levelUp)
        {
            levelUp.Player.SaveCharacter();
        }
    }
}