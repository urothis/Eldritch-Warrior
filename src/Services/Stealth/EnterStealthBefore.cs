using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Stealth
{
    [ServiceBinding(typeof(EnterStealthBefore))]
    public class EnterStealthBefore
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EnterStealthBefore(NWNXEventService nWNX) => nWNX.Subscribe<StealthEvents.OnEnterStealthBefore>(OnEnterStealthBefore);

        private void OnEnterStealthBefore(StealthEvents.OnEnterStealthBefore enterStealthBefore)
        {
            var pc = (NwCreature)enterStealthBefore.Player;

            if (!enterStealthBefore.Player.StealthModeActive &&
            !pc.HasFeatPrepared(Feat.HideInPlainSight))
            {
                //NWNX_Events_SkipEvent();
            }
        }
    }
}