using System;

using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.PlayableRaces
{
    [ServiceBinding(typeof(Engine))]
    public class Engine
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Engine(NativeEventService nativeEventService, NWNXEventService nWNX)
        {
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, ClientEnter);
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerRespawn>(NwModule.Instance, PlayerRespawn);
            nWNX.Subscribe<LevelEvents.OnLevelUpBefore>(LevelUpBefore);
        }

        private void LevelUpBefore(LevelEvents.OnLevelUpBefore obj)
        {
            throw new NotImplementedException();
        }

        private void PlayerRespawn(ModuleEvents.OnPlayerRespawn obj)
        {
            throw new NotImplementedException();
        }

        private void ClientEnter(ModuleEvents.OnClientEnter obj)
        {
            if (obj.Player.SubraceValid())
            {
                if(String.IsNullOrEmpty(NwModule.Instance.GetCampaignVariable<string>("SUBRACE", obj.Player.UUID.ToUUIDString() ) ) )
                {
                    Log.Info("HELLO TEST");
                }
            }
        }
    }
}
