using System.Linq;
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

        [ScriptHandler("area_cleanup")]
        public void Exit(CallInfo callinfo)
        {
            logger.Info("1");
            //Verbose(callinfo);
            if (callinfo.TryGetEvent(out AreaEvents.OnExit onLeave))
            {
                logger.Info("2");
                if (!onLeave.Area.FindObjectsOfTypeInArea<NwPlayer>().Any())
                {
                    logger.Info("3");
                    onLeave.CleanupCreaturesAndItems();
                    logger.Info("4");
                    onLeave.CleanupPlaceables();
                    logger.Info("5");
                    onLeave.CloseDoors();
                    logger.Info("6");
                }
            }
        }
    }
}