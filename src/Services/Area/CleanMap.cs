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

        [ScriptHandler("area_cleanup")]
        private static void Exit(AreaEvents.OnExit obj)
        {
            //Stop if players exist on map.
            if (!obj.Area.FindObjectsOfTypeInArea<NwPlayer>().Any())
            {
                obj.CleanupCreaturesAndItems();
                obj.CleanupPlaceables();
                obj.CloseDoors();
            }
        }
    }
}