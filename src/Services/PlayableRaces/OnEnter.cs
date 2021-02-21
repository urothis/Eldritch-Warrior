using NWN.API;
using NWN.API.Events;

using NWN.Services;

namespace Services.PlayableRaces
{
    [ServiceBinding(typeof(OnEnter))]
    public class OnEnter
    {
        public OnEnter(NativeEventService nativeEventService) => nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, OnClientEnter);

        private static void OnClientEnter(ModuleEvents.OnClientEnter onClient)
        {
            //  The player didn't input a subrace so stop here
            if (onClient.Player.SubRace.ToString().Length == 0)
            {
                return;
            }

            //  If valid Subrace proceed else tell player subrace is not valid
            if (onClient.Player.SubraceValid())
            {
                if (onClient.Player.GetCampaignVariable<string>("SUBRACE", onClient.Player.UUID.ToUUIDString()).Value == string.Empty)
                {
                    //New Character apply everything otherwise reapply subrace
                    onClient.Player.GetCampaignVariable<string>("SUBRACE", onClient.Player.UUID.ToUUIDString()).Value = onClient.Player.UUID.ToUUIDString();
                    // apply new stuff here
                }
                else
                {
                    //Reapply subrace
                }
            }
            else
            {
                onClient.Player.SendServerMessage($"\n{"ERROR".ColorString(Color.RED)}: The subrace name you have entered doesn't exist.\n");
            }
        }
    }
}