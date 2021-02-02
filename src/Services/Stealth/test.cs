using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Stealth
{
    [ServiceBinding(typeof(EnterStealthAfter))]
    public class EnterStealthAfter
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EnterStealthAfter(NWNXEventService nWNX) => nWNX.Subscribe<StealthEvents.OnEnterStealthAfter>(OnEnterStealthAfter);

        private void OnEnterStealthBefore(StealthEvents.OnEnterStealthBefore enterStealthBefore)
        {
            var pc = (NwCreature)enterStealthBefore.Player;

            if (!enterStealthBefore.Player.StealthModeActive &&
            !pc.HasFeatPrepared(Feat.HideInPlainSight))
            {
                logger.Info("HELLO WORLD!");
            }
        }
    }
}