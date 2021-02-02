using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Stealth
{
    [ServiceBinding(typeof(ExitStealthAfter))]
    public class ExitStealthAfter
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ExitStealthAfter(NWNXEventService nWNX) => nWNX.Subscribe<StealthEvents.OnExitStealthAfter>(OnExitStealthAfter);

        private void OnExitStealthAfter(StealthEvents.OnExitStealthAfter stealthAfter)
        {
            var pc = (NwCreature)stealthAfter.Player;

            if (!stealthAfter.Player.StealthModeActive &&
            !pc.HasFeatPrepared(Feat.HideInPlainSight))
            {
                logger.Info("HELLO WORLD!");
            }
        }
    }
}