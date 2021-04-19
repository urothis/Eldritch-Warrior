//using NLog;
using NWN.API;

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