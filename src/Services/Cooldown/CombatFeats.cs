using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Cooldown
{
    [ServiceBinding(typeof(CombatFeats))]

    public class CombatFeats
    {
        public CombatFeats(NWNXEventService nWNX)
        {
            nWNX.Subscribe<FeatUseEvents.OnUseFeatBefore>(OnUseFeatBefore);
        }

        private void OnUseFeatBefore(FeatUseEvents.OnUseFeatBefore obj)
        {
            //throw new NotImplementedException();
        }
    }
}