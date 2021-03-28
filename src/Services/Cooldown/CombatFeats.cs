using NLog;
using NWN.API;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Cooldown
{
    [ServiceBinding(typeof(CombatFeats))]

    public class CombatFeats
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly EventService eventService;

        public CombatFeats(EventService eventService) => this.eventService = eventService;

        public void ObserverPlayer(NwPlayer player) => eventService.Subscribe<FeatUseEvents.OnUseFeatBefore, NWNXEventFactory>(player, UseFeatBefore)
                .Register<FeatUseEvents.OnUseFeatBefore>();

        private void UseFeatBefore(FeatUseEvents.OnUseFeatBefore featBefore) => Log.Info($"{featBefore.FeatUser.Name} used {featBefore.Feat.ToString()}.");
    }
}