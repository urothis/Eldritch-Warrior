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
        private readonly int cooldownSeconds = 6;

        public CooldownHips(NWNXEventService nWNX)
        {
            nWNX.Subscribe<StealthEvents.OnEnterStealthBefore>(OnEnterStealthBefore);
            nWNX.Subscribe<StealthEvents.OnExitStealthAfter>(OnExitStealthAfter);

            usage = new Dictionary<Guid, DateTime>();
        }

        private void OnEnterStealthBefore(StealthEvents.OnEnterStealthBefore enterStealthBefore)
        {
            NwCreature pc = (NwCreature)enterStealthBefore.Player;
            DateTime timeThen;
            DateTime timeNow = DateTime.Now;

            if (!usage.ContainsKey(pc.UUID))
            {
                usage[pc.UUID] = DateTime.Now;
                logger.Info("HELLO OnEnterStealthBefore null");
                return;
            }
            else
            {
                timeThen = usage[pc.UUID];
            }

            if (!pc.HasFeatPrepared(Feat.HideInPlainSight) && timeNow.Second - timeThen.Second >= cooldownSeconds)
            {
                //enterStealthBefore.Skip = true;
                logger.Info("HELLO OnEnterStealthBefore");
            }
        }

        private void OnExitStealthAfter(StealthEvents.OnExitStealthAfter stealthAfter)
        {
            NwCreature pc = (NwCreature)stealthAfter.Player;
            usage[pc.UUID] = DateTime.Now;

            if (!pc.HasFeatPrepared(Feat.HideInPlainSight))
            {
                logger.Info("HELLO OnExitStealthAfter");
            }
        }
    }
}