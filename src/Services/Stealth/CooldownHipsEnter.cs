using System;
using System.Collections.Generic;
//using NLog;
using NWN.API;
using NWN.API.Constants;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Stealth
{
    [ServiceBinding(typeof(CooldownHipsEnter))]
    public class CooldownHipsEnter
    {
        internal Dictionary<Guid, DateTime> usage;
        private readonly int cooldownSeconds = 6;
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly EventService eventService;
        public CooldownHipsEnter(EventService eventService)
        {
            this.eventService = eventService;
            usage = new Dictionary<Guid, DateTime>();
        }

        public void Hipster(NwCreature creature) =>
            eventService.Subscribe<StealthEvents.OnEnterStealthBefore, NWNXEventFactory>(creature, EnterStealthBefore)
            .Register<StealthEvents.OnEnterStealthBefore>();

        private void EnterStealthBefore(StealthEvents.OnEnterStealthBefore enterStealthBefore)
        {
            NwCreature pc = (NwCreature)enterStealthBefore.Player;
            DateTime timeThen;

            if (!usage.ContainsKey(pc.UUID))
            {
                usage[pc.UUID] = DateTime.Now;
                return;
            }
            else
            {
                timeThen = usage[pc.UUID];
            }

            if (pc.HasFeatPrepared(Feat.HideInPlainSight) && DateTime.Now.Second - timeThen.Second >= cooldownSeconds)
            {
                enterStealthBefore.Skip = true;
                enterStealthBefore.Player.SendServerMessage($"\n{"FEAT_HIDE_IN_PLAIN_SIGHT".ColorString(Color.ORANGE)} cooldown active.\n Cooldown last one round.\n");
            }
        }
    }
}