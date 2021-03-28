using System;
using System.Collections.Generic;

using NWN.API;
using NWN.API.Constants;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Stealth
{
    [ServiceBinding(typeof(CooldownHipsExit))]
    public class CooldownHipsExit
    {
        internal Dictionary<Guid, DateTime> usage;

        public CooldownHipsExit(NWNXEventService nWNX)
        {
            nWNX.Subscribe<StealthEvents.OnExitStealthAfter>(OnExitStealthAfter);

            usage = new Dictionary<Guid, DateTime>();
        }

        private void OnExitStealthAfter(StealthEvents.OnExitStealthAfter stealthAfter)
        {
            NwCreature pc = (NwCreature)stealthAfter.Player;
            usage[pc.UUID] = DateTime.Now;

            if (pc.HasFeatPrepared(Feat.HideInPlainSight))
            {
                stealthAfter.Player.SendServerMessage($"\n{"FEAT_HIDE_IN_PLAIN_SIGHT".ColorString(Color.ORANGE)} cooldown active.\nOne Round.\n");
            }
        }
    }
}