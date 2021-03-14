using System;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Area
{
    [ServiceBinding(typeof(CleanMap))]
    public class CleanMap
    {
        public CleanMap(ScriptEventService scriptEventService) => scriptEventService.SetHandler<AreaEvents.OnExit>("area_cleanup", Exit);

        private void Exit(AreaEvents.OnExit obj)
        {
            NwPlayer pc = (NwPlayer)obj.ExitingObject;

            foreach (var item in obj.Area.Objects)
            {
                System.Console.WriteLine(item.Name);
            }
        }
    }
}