using NWN.API;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(ItemUnEquip))]
    public class ItemUnEquip
    {
        public ItemUnEquip() => NwModule.Instance.OnPlayerUnequipItem += unequipItem =>
        {
            unequipItem.Item.PrintGPValueOnItem();

            if (unequipItem.UnequippedBy is NwPlayer nwPlayer)
            {
                nwPlayer.SaveCharacter();
            }
        };
    }
}