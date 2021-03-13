using System;
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
            System.Console.WriteLine($"{obj.ExitingObject.Name}");
        }
    }
}