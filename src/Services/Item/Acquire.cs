//using NLog;
using NWN.API;
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
                if (acquireItem.Item is NwItem item)
                {
                    item.PrintGPValueOnItem();
                    item.RemoveAllTemporaryItemProperties();
                    item.NotifyLoot();
                }

                /* This is to short circuit the rest of this code if we are DM */
                if (acquireItem.AcquiredBy is NwPlayer { IsDM: true }) return;

                if (acquireItem.AcquiredBy is NwPlayer playerA && acquireItem.AcquiredFrom is NwPlayer playerB)
                {
                    playerA.FixBarterExploit(playerB);
                }
            };
        }
    }
}