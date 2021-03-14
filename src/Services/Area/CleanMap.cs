using System;
using NLog;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Area
{
    [ServiceBinding(typeof(CleanMap))]
    public class CleanMap
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CleanMap(ScriptEventService scriptEventService) => scriptEventService.SetHandler<AreaEvents.OnExit>("area_cleanup", Exit);

        private void Exit(AreaEvents.OnExit obj)
        {
            foreach (NwGameObject trash in obj.Area.Objects)
            {
                if (trash is NwItem || trash is NwCreature)
                {
                    logger.Info(trash.Name);
                    trash.Destroy();
                }
            }
        }
    }
}