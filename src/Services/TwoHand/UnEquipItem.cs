using NWN.API;
using NWN.API.Events;

using NWN.Services;

namespace Services.TwoHand
{
    [ServiceBinding(typeof(UnEquipItem))]
    public class UnEquipItem
    {
        public static void OnUnEquip() => NwModule.Instance.OnPlayerUnequipItem += unequipItem =>
        {
            unequipItem.UnequippedBy.RemoveBuff();
        };
    }
}
