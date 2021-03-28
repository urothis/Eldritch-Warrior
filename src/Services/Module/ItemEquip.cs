//using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(ItemEquip))]
    public class ItemEquip
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ItemEquip()
        {
            NwModule.Instance.OnPlayerEquipItem += equipItem =>
            {
                equipItem.Item.PrintGPValueOnItem();

                if (equipItem.Player is NwPlayer nwPlayer)
                {
                    nwPlayer.SaveCharacter();
                }
            };
        }
    }
}