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
        public static void SocketRunesToItem(this NwPlayer pc, ModuleEvents.OnActivateItem activateItem)
        {
            NwItem target = (NwItem)activateItem.TargetObject;
            NwItem rune = (NwItem)activateItem.ActivatedItem;

            int ipType = rune.GetLocalVariable<int>("IP_TYPE");

            //Break script if we try to apply an target property to an target that wont take it.
            if (!IsCorrectTargetType(target, ipType))
            {
                pc.SendServerMessage($"Cannot apply {rune.Name.ColorString(Color.WHITE)} to {target.Name.ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));
                logger.Info($"Cannot apply {rune.Name} to {target.Name}.");
            }
            //Restrictions (Keen.. etc obviously won't work for gloves or staffs and so on)
            else if (IsNotCorrectGemType(ipType, target))
            {
                pc.SendServerMessage($"{rune.Name.ColorString(Color.WHITE)} cannot be applied to {target.Name.ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));
                logger.Info($"{target.Name} cannot be socketed to {rune.Name}.");
            }
            //target Properties i.e. haste, imp evasion, true seeing... etc should not work if already present
            else if (DoesNotStack(ipType, target))
            {
                pc.SendServerMessage($"{rune.Name.ColorString(Color.WHITE)} does not stack with {target.Name.ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));
                logger.Info($"{rune} does not stack on {target.Name}");
            }
            else if (CheckUnlimitedAmmoType(ipType, target))
            {
                pc.SendServerMessage($"You cannot change or stack Unlimited Ammo type onto {target.Name.ColorString(Color.WHITE)}.".ColorString(Color.ORANGE));
                logger.Info($"You cannot change or stack Unlimited Ammo type onto {target.Name}");
            }
            else
            {
                Services.Module.Extensions.RemoveAllTemporaryItemProperties(target);
                target.AddItemProperty(ConvertipTypeToItemProperty(rune, ipType), EffectDuration.Permanent);
                pc.ActionUnequipItem(target);
                rune.Destroy();
            }
        }

        private static bool IsCorrectTargetType(NwItem target, int ipType)
        {
            int item = (int)target.BaseItemType;

            switch (ipType)
            {
                case 34: //ITEM_PROPERTY_EXTRA_RANGED_DAMAGE_TYPE
                case 45: //ITEM_PROPERTY_MIGHTY
                case 61: //ITEM_PROPERTY_UNLIMITED_AMMUNITION
                    {
                        if (IsRangedWeapon(target))
                        {
                            logger.Info($"IsCorrectTargetType | IsRangedWeapon | ipType:{ipType} | target:{target.Name}.");
                            return true;
                        }
                        break;
                    }
                case 6: //ITEM_PROPERTY_ENHANCEMENT_BONUS
                case 16: //ITEM_PROPERTY_DAMAGE_BONUS***
                case 36: //ITEM_PROPERTY_HOLY_AVENGER
                case 43: //ITEM_PROPERTY_KEEN
                case 56: //ITEM_PROPERTY_ATTACK_BONUS
                case 67: //ITEM_PROPERTY_REGENERATION_VAMPIRIC
                case 74: //ITEM_PROPERTY_MASSIVE_CRITICALS***
                case 82: //ITEM_PROPERTY_ONHITCASTSPELL
                    {
                        if (IsMeleeWeapon(target) || target.BaseItemType == BaseItemType.Gloves || target.BaseItemType == BaseItemType.MagicStaff)
                        {
                            logger.Info($"IsCorrectTargetType | IsMeleeWeapon | ipType:{ipType} | target:{target.Name}.");
                            return true;
                        }
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
                case 71:
                case 75:
                    {
                        if (IsRangedWeapon(target))
                        {
                            logger.Info($"IsCorrectTargetType | IsRangedWeapon | ipType:{ipType} | target:{target.Name}.");
                            return true;
                        }
                        switch ((BaseItemType)item)
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
                            case BaseItemType.TowerShield:
                                {
                                    logger.Info($"IsCorrectTargetType | item | ipType:{ipType} | target:{target.Name}.");
                                    return true;
                                }
                        }
                        break;
                    }
            }
            return false;
        }

        private static bool IsNotCorrectGemType(int ipType, NwItem target)
        {
            switch (ipType)
            {
                case 6: //ITEM_PROPERTY_ENHANCEMENT_BONUS
                case 36:
                case 43:
                    {
                        if (IsRangedWeapon(target) || target.BaseItemType == BaseItemType.Gloves)
                        {
                            logger.Info($"IsNotCorrectGemType | IsRangedWeapon, Gloves, MagicStaff | ipType:{ipType} | target:{target.Name}.");
                            return true;
                        }
                    }
                    break;
                case 16:
                    {
                        if (IsRangedWeapon(target))
                        {
                            logger.Info($"IsNotCorrectGemType | IsRangedWeapon | ipType:{ipType} | target:{target.Name}.");
                            return true;
                        }
                    }
                    break;
                case 67:
                    {
                        if (target.BaseItemType == BaseItemType.Gloves)
                        {
                            logger.Info($"IsNotCorrectGemType | Gloves | ipType:{ipType} | target:{target.Name}.");
                            return true;
                        }
                    }
                    break;
                case 82:
                    {
                        if (IsMeleeWeapon(target) && target.BaseItemType == BaseItemType.Armor && target.BaseItemType == BaseItemType.Gloves && target.BaseItemType == BaseItemType.MagicStaff)
                        {
                            logger.Info($"IsNotCorrectGemType | IsMeleeWeapon, Armor, Gloves, MagicStaff | ipType:{ipType} | target:{target.Name}.");
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }

        private static ItemProperty ConvertipTypeToItemProperty(NwItem target, int ipType)
        {
            int IPSubType = target.GetLocalVariable<int>("IP_SUBTYPE").Value;
            int IPValue = target.GetLocalVariable<int>("IP_VALUE").Value;

            switch (ipType)
            {
                case 0: return ItemProperty.AbilityBonus((IPAbility)IPSubType, IPValue);
                case 1: return ItemProperty.ACBonus(IPValue);
                case 56: return ItemProperty.AttackBonus(IPValue);
                case 12: return ItemProperty.BonusFeat((IPFeat)IPSubType);
                case 13: return ItemProperty.BonusLevelSpell((IPClass)IPSubType, (IPSpellLevel)IPValue);
                case 40: return ItemProperty.BonusSavingThrow((IPSaveBaseType)IPSubType, IPValue);
                case 39: return ItemProperty.BonusSpellResistance((IPSpellResistanceBonus)IPValue);
                case 15: return ItemProperty.CastSpell((IPCastSpell)IPSubType, (IPCastSpellNumUses)IPValue);
                case 16: return ItemProperty.DamageBonus((IPDamageType)IPSubType, (IPDamageBonus)IPValue);
                case 23: return ItemProperty.DamageResistance((IPDamageType)IPSubType, (IPDamageResist)IPValue);
                case 6: return ItemProperty.EnhancementBonus(IPValue);
                case 33: return ItemProperty.ExtraMeleeDamageType((IPDamageType)IPSubType);
                case 34: return ItemProperty.ExtraRangeDamageType((IPDamageType)IPSubType);
                case 75: return ItemProperty.FreeAction();
                case 35: return ItemProperty.Haste();
                case 36: return ItemProperty.HolyAvenger();
                case 38: return ItemProperty.ImprovedEvasion();
                case 43: return ItemProperty.Keen();
                case 74: return ItemProperty.MassiveCritical((IPDamageBonus)IPValue);
                case 45: return ItemProperty.MaxRangeStrengthMod(IPValue);
                case 82: return ItemProperty.OnHitCastSpell((IPCastSpell)IPSubType, (IPSpellLevel)IPValue);
                case 51: return ItemProperty.Regeneration(IPValue);
                case 52: return ItemProperty.SkillBonus((Skill)IPSubType, IPValue);
                case 71: return ItemProperty.TrueSeeing();
                case 61: return ItemProperty.UnlimitedAmmo((IPUnlimitedAmmoType)IPSubType);
                case 67: return ItemProperty.VampiricRegeneration(IPValue);
                default: throw new NotImplementedException();
            }
        }

        private static bool CheckUnlimitedAmmoType(int ipType, NwItem target) => ipType == 61 && target.HasItemProperty(ItemPropertyType.UnlimitedAmmunition);

        private static bool DoesNotStack(int ipType, NwItem target)
        {
            switch (ipType)
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
                case 82:
                    return target.HasItemProperty((ItemPropertyType)ipType);
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

        private static bool IsRangedWeapon(NwItem target)
        {
            switch (target.BaseItemType)
            {
                case BaseItemType.HeavyCrossbow:
                case BaseItemType.LightCrossbow:
                case BaseItemType.Longbow:
                case BaseItemType.Shortbow:
                case BaseItemType.Sling:
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
    }
}