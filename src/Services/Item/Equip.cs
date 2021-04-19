using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Item
{
    [ServiceBinding(typeof(Equip))]
    public class Equip
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Equip()
        {
            NwModule.Instance.OnPlayerEquipItem += equip =>
            {
                if (equip.Item is NwItem item)
                {

                }
            };
        }
    }
}