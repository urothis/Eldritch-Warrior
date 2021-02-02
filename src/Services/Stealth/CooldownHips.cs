using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Stealth
{
    [ServiceBinding(typeof(CooldownHips))]
    public class CooldownHips
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        //private static Dictionary<GUID, DateTime> Usage;

        public CooldownHips(NWNXEventService nWNX)
        {
            nWNX.Subscribe<StealthEvents.OnEnterStealthBefore>(OnEnterStealthBefore);
            nWNX.Subscribe<StealthEvents.OnExitStealthAfter>(OnExitStealthAfter);

            //usage = new Dictionary<GUID, DateTime>();
        }

        private void OnEnterStealthBefore(StealthEvents.OnEnterStealthBefore enterStealthBefore)
        {
            var pc = (NwCreature)enterStealthBefore.Player;

            if (!enterStealthBefore.Player.StealthModeActive &&
            !pc.HasFeatPrepared(Feat.HideInPlainSight))
            {
                //var getValue = usage[Player.UUID];
                //usage[Player.UUID] = new DateTime().Now();
                //NWNX_Events_SkipEvent();
            }
        }

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