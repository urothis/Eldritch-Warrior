using NWN.API;
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
                equip.Item.PrintGPValueOnItem();
                Module.Extensions.SaveCharacter((NwPlayer)equip.Player);
            };
        }
    }
}