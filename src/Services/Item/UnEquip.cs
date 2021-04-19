using NWN.API;
using NWN.Services;

namespace Services.Item
{
    [ServiceBinding(typeof(UnEquip))]
    public class UnEquip
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public UnEquip()
        {
            NwModule.Instance.OnPlayerUnequipItem += unEquip =>
            {
                unEquip.Item.PrintGPValueOnItem();
            };
        }
    }
}