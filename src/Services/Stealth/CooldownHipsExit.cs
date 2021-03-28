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
        private readonly EventService eventService;

        public CooldownHipsExit(EventService eventService)
        {
            this.eventService = eventService;
            usage = new Dictionary<Guid, DateTime>();
        }

        public void Sneaker(NwCreature creature) =>
            eventService.Subscribe<StealthEvents.OnExitStealthAfter, NWNXEventFactory>(creature, ExitStealthAfter)
                .Register<StealthEvents.OnExitStealthAfter>();

        public void ExitStealthAfter(StealthEvents.OnExitStealthAfter stealthAfter)
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