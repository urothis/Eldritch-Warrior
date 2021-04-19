//using NLog;
using NWN.API;
using NWN.API.Events;
using NWN.Services;
using Services.Module;

namespace Services.Item
{
    public class UnAcquire
    {
        public UnAcquire()
        {
            NwModule.Instance.OnUnacquireItem += unAcquire =>
            {
                unAcquire.Item.PrintGPValueOnItem();
                unAcquire.Item.RemoveAllTemporaryItemProperties();
            };
        }
    }
}