//using NLog;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Item
{
    [ServiceBinding(typeof(Acquire))]
    public class Acquire
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Acquire()
        {
            NwModule.Instance.OnAcquireItem += acquireItem => 
            {

            };
        }
    }
}