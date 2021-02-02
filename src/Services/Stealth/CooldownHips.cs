using System;
using System.Collections.Generic;

using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Stealth
{
    [ServiceBinding(typeof(CooldownHips))]
    public class CooldownHips
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        internal Dictionary<Guid, DateTime> usage;
        private readonly int cooldownTimer = 6;
        public CooldownHips(NWNXEventService nWNX)
        {
            nWNX.Subscribe<StealthEvents.OnEnterStealthBefore>(OnEnterStealthBefore);
            nWNX.Subscribe<StealthEvents.OnExitStealthAfter>(OnExitStealthAfter);

            usage = new Dictionary<Guid, DateTime>();
        }

        private void OnEnterStealthBefore(StealthEvents.OnEnterStealthBefore enterStealthBefore)
        {
            NwCreature? pc = (NwCreature)enterStealthBefore.Player;
            DateTime getValue = usage[pc.UUID];
            usage[pc.UUID] = DateTime.Now;
            int timeLapsed = usage[pc.UUID].Second - getValue.Second;

            if (!enterStealthBefore.Player.StealthModeActive &&
            !pc.HasFeatPrepared(Feat.HideInPlainSight) &&
            timeLapsed - cooldownTimer >= 0)
            {
                //NWNX_Events_SkipEvent();
                logger.Info("hello worldddddddddddddddddddddddddddddddddddddddddd");
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