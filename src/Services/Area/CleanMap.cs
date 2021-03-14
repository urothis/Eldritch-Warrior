using System.Linq;
//using NLog;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Area
{
    [ServiceBinding(typeof(CleanMap))]
    public class CleanMap
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CleanMap(ScriptEventService scriptEventService) => scriptEventService.SetHandler<AreaEvents.OnExit>("area_cleanup", Exit);

        private void Exit(AreaEvents.OnExit obj)
        {
            //Stop if players exist on map.
            if (obj.Area.FindObjectsOfTypeInArea<NwPlayer>().Any()) return;

            obj.CleanupCreaturesAndItems();
            obj.CleanupPlaceables();
        }
    }
}