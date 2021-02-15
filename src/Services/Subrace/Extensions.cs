//using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Subrace
{
    public static class Extensions
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static void SubRaceEnter(this ModuleEvents.OnClientEnter enter)
        {
            if (enter.Player.SubRace.ToString().Length == 0)
            {
                return;
            }
        }
    }
}