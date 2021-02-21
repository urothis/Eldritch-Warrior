using System;
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
        private char wildcard = '!';
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
            var pc = obj.Player.SubRace;

            if (String.IsNullOrEmpty(pc))
            {
                //We are not a playable race, exit.
            }
            else if (pc[0].Equals(wildcard))
            {
                //Go to validate subrace an init
            }
            else
            {
                obj.Player.SendServerMessage($"\n{"ERROR".ColorString(Color.RED)}: The subrace name you have entered doesn't exist.\n");
            }
        }
    }
}