using System;
using NWN.API;
using NWN.API.Constants;

namespace Services.ActivateItem
{
    public static class Extensions
    {
        public static void SocketRunesToItem(this NwItem activatedItem, NwPlayer pc, NwItem item)
        {
            int IPType = item.GetLocalVariable<int>("IP_TYPE").Value;
            int IPSubType = item.GetLocalVariable<int>("IP_SUBTYPE").Value;
            int IPValue = item.GetLocalVariable<int>("IP_VALUE").Value;

            //Break script if we try to apply an item property to an item that wont take it.
            if (!IsCorrectItemtype(pc, item, IPType))
            {
                pc.SendServerMessage($"Cannot apply {activatedItem.Name.ColorString(Color.WHITE)} to {item.Name.ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));
                return;
            }
            //Restrictions (Keen.. etc obviously won't work for gloves or staffs and so on)
            else if (!IsCorrectGemType(IPType))
            {
                pc.SendServerMessage($"{item.Name.ColorString(Color.WHITE)} cannot be socketed to {activatedItem.Name.ColorString(Color.WHITE)}.");
            }
            //Item Properties i.e. haste, imp evasion, true seeing... etc should not work if already present
            else if (DoesNotStack(IPType, item))
            {

            }

            pc.SendServerMessage($"test good.");
        }

        private static bool DoesNotStack(int iPType, NwItem item)
        {
            var test = false;
            switch (iPType)
            {
                case 12:
                case 13:
                case 15:
                case 34:
                case 35:
                case 36:
                case 38:
                case 43:
                case 61:
                case 71:
                case 75:
                case 82: test = true; break;//test = item.HasItemProperty(ItemPropertyType.Haste); break;
            }
            return test;
        }

        private static bool IsCorrectGemType(int IPType)
        {
            switch (IPType)
            {
                case 6:
                case 16:
                case 36:
                case 43:
                case 67:
                case 82: return false;
                default: return true;
            }
        }
        private static bool IsCorrectItemtype(NwPlayer pc, NwItem item, int IPType)
        {
            switch (IPType)
            {
                case 34:
                case 45:
                case 61: return IsRangedWeapon(item);
                case 36:
                case 6:
                case 16:
                case 43:
                case 56:
                case 67:
                case 74:
                case 82: return IsMeleeWeapon(item);
                default:
                    {
                        if (IsRangedWeapon(item))
                        {
                            return true;
                        }
                        else
                        {
                            switch (item.BaseItemType)
                            {
                                case BaseItemType.Amulet:
                                case BaseItemType.Armor:
                                case BaseItemType.Belt:
                                case BaseItemType.Boots:
                                case BaseItemType.Bracer:
                                case BaseItemType.Cloak:
                                case BaseItemType.Gloves:
                                case BaseItemType.Helmet:
                                case BaseItemType.LargeShield:
                                case BaseItemType.MagicStaff:
                                case BaseItemType.SmallShield:
                                case BaseItemType.Ring:
                                case BaseItemType.TowerShield: return true;
                                default: return false;
                            }
                        }
                    }
            }
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
                default:
                    return false;
            }
        }

        private static bool IsMeleeWeapon(NwItem item)
        {
            switch (item.BaseItemType)
            {
                case BaseItemType.Bastardsword:
                case BaseItemType.Battleaxe:
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
                case BaseItemType.HeavyFlail:
                case BaseItemType.Kama:
                case BaseItemType.Katana:
                case BaseItemType.Kukri:
                case BaseItemType.LightFlail:
                case BaseItemType.LightHammer:
                case BaseItemType.LightMace:
                case BaseItemType.Longsword:
                case BaseItemType.MagicStaff:
                case BaseItemType.Morningstar:
                case BaseItemType.Quarterstaff:
                case BaseItemType.Rapier:
                case BaseItemType.Scimitar:
                case BaseItemType.Scythe:
                case BaseItemType.ShortSpear:
                case BaseItemType.Shortsword:
                case BaseItemType.Sickle:
                case BaseItemType.Trident:
                case BaseItemType.TwoBladedSword:
                case BaseItemType.Warhammer:
                case BaseItemType.Whip:
                    return true;
                default:
                    return false;
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
                case BaseItemType.Whip:
                    return true;
                default:
                    return false;
            }
        }
    }
}