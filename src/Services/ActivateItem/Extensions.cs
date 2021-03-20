using System;
using NLog;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;

namespace Services.ActivateItem
{
    public static class Extensions
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void SocketRunesToItem(this ModuleEvents.OnActivateItem activateItem, NwPlayer pc, NwItem target)
        {
            int IPType = target.GetLocalVariable<int>("IP_TYPE").Value;
            int IPSubType = target.GetLocalVariable<int>("IP_SUBTYPE").Value;
            int IPValue = target.GetLocalVariable<int>("IP_VALUE").Value;

            //Break script if we try to apply an target property to an target that wont take it.
            if (IsNotCorrectTargetType(target, IPType))
            {
                pc.SendServerMessage($"Cannot apply {activateItem.ActivatedItem.Name.ColorString(Color.WHITE)} to {target.Name.ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));
                logger.Info($"Cannot apply {activateItem.ActivatedItem.Name.ColorString(Color.WHITE)} to {target.Name.ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));
                return;
            }
            //Restrictions (Keen.. etc obviously won't work for gloves or staffs and so on)
            else if (!IsCorrectGemType(IPType))
            {
                pc.SendServerMessage($"{target.Name.ColorString(Color.WHITE)} cannot be socketed to {activateItem.ActivatedItem.Name.ColorString(Color.WHITE)}.");
                logger.Info($"{target.Name.ColorString(Color.WHITE)} cannot be socketed to {activateItem.ActivatedItem.Name.ColorString(Color.WHITE)}.");
                return;
            }
            //target Properties i.e. haste, imp evasion, true seeing... etc should not work if already present
            else if (DoesNotStack(IPType, target))
            {
                pc.SendServerMessage($"{target.Name.ColorString(Color.WHITE)} does not stack.".ColorString(Color.ORANGE));
                logger.Info($"{target.Name.ColorString(Color.WHITE)} does not stack.".ColorString(Color.ORANGE));
                return;
            }

            pc.SendServerMessage($"test good.");
        }

        private static bool DoesNotStack(int iPType, NwItem target)
        {
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
                case 82: return target.HasItemProperty((ItemPropertyType)iPType);
                default: return false;
            }
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
        private static bool IsNotCorrectTargetType(NwItem target, int IPType)
        {
            switch (IPType)
            {
                case 34:
                case 45:
                case 61:
                    {
                        if (IsRangedWeapon(target))
                            return true;
                        break;
                    }
                case 6:
                case 16:
                case 36:
                case 43:
                case 56:
                case 67:
                case 74:
                case 82:
                    {
                        if (IsMeleeWeapon(target))
                            return true;
                        break;
                    }
                case 0:
                case 1:
                case 12:
                case 13:
                case 15:
                case 22:
                case 23:
                case 35:
                case 38:
                case 39:
                case 40:
                case 51:
                case 52:
                case 75:
                case 71:
                    {
                        if (IsRangedWeapon(target))
                            return true;
                        switch (target.BaseItemType)
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
                        }
                        break;
                    }
                default: return false;
            }
            return false;
        }

        private static bool IsRangedWeapon(NwItem target)
        {
            switch (target.BaseItemType)
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

        private static bool IsMeleeWeapon(NwItem target)
        {
            switch (target.BaseItemType)
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
        public static bool CheckItemIsValidType(this NwItem target)
        {
            switch (target.BaseItemType)
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