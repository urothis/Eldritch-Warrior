using NWN.API;
using NWN.Services;

namespace Services.TwoHand
{
    [ServiceBinding(typeof(EquipItem))]
    public class EquipItem
    {
        public static void OnEquip() => NwModule.Instance.OnPlayerEquipItem += equipItem =>
        {
            var pc = equipItem.Player;

            if (!pc.CheckCreatureSize() || !pc.HasShieldEquipped() && pc.CheckCreaturekSizeAndWeapon())
            {
                pc.AddBuff();
            }
        };
    }
}
