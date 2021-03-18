using System.Globalization;
using NWN.API;
using NWN.API.Constants;

namespace Services.ActivateItem
{
    public static class Extensions
    {
        public static void SocketRunesToItem(this NwItem activatedItem, NwPlayer pc, NwItem item)
        {
            //Break script if we try to apply an item property to an item that wont take it.
            if (!IsCorrectItemtype(pc, item, activatedItem.Name))
            {
                pc.SendServerMessage($"Cannot apply {activatedItem.Name.ColorString(Color.WHITE)} to {item.Name.ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));
                return;
            }
            pc.SendServerMessage($"test good.");
        }

        private static bool IsCorrectItemtype(NwPlayer pc, NwItem item)
        {
            //bank.GetLocalVariable<string>("CHEST_TAG").Value = objectTag;
            int IPType = item.GetLocalVariable<int>("IP_TYPE").Value;
            int IPSubType = item.GetLocalVariable<int>("IP_SUBTYPE").Value;
            int IPValue = item.GetLocalVariable<int>("IP_VALUE").Value;

        }

        private static bool IsRangedWeapon(NwItem item)
        {
            switch (item.BaseItemType)
            {
                case BaseItemType.HeavyCrossbow:
                case BaseItemType.LightCrossbow:
                case BaseItemType.Longbow:
                case BaseItemType.Shortbow:
                    return true;
                default: return false;
            }
        }

        public static bool CheckItemIsValidType(this NwItem item)
        {
            switch (item.BaseItemType)
            {
                case BaseItemType.Amulet:
                case BaseItemType.Armor:
                case BaseItemType.Bastardsword:
                case BaseItemType.Battleaxe:
                case BaseItemType.Belt:
                case BaseItemType.Boots:
                case BaseItemType.Bracer:
                case BaseItemType.Cloak:
                case BaseItemType.Club:
                case BaseItemType.Dagger:
                case BaseItemType.DireMace:
                case BaseItemType.Doubleaxe:
                case BaseItemType.DwarvenWaraxe:
                case BaseItemType.Gloves:
                case BaseItemType.Greataxe:
                case BaseItemType.Greatsword:
                case BaseItemType.Halberd:
                case BaseItemType.Handaxe:
                case BaseItemType.HeavyCrossbow:
                case BaseItemType.HeavyFlail:
                case BaseItemType.Helmet:
                case BaseItemType.Kama:
                case BaseItemType.Katana:
                case BaseItemType.Kukri:
                case BaseItemType.LargeShield:
                case BaseItemType.LightCrossbow:
                case BaseItemType.LightFlail:
                case BaseItemType.LightHammer:
                case BaseItemType.LightMace:
                case BaseItemType.Longbow:
                case BaseItemType.Longsword:
                case BaseItemType.MagicStaff:
                case BaseItemType.Morningstar:
                case BaseItemType.Quarterstaff:
                case BaseItemType.Rapier:
                case BaseItemType.Ring:
                case BaseItemType.Scimitar:
                case BaseItemType.Scythe:
                case BaseItemType.Shortbow:
                case BaseItemType.ShortSpear:
                case BaseItemType.Shortsword:
                case BaseItemType.Sickle:
                case BaseItemType.Sling:
                case BaseItemType.SmallShield:
                case BaseItemType.TowerShield:
                case BaseItemType.Trident:
                case BaseItemType.TwoBladedSword:
                case BaseItemType.Warhammer:
                case BaseItemType.Whip: return true;
                default: return false;
            }
        }
    }
}