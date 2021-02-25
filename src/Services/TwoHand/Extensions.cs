using NWN.API;
using NWN.API.Constants;

namespace Services.TwoHand
{
    public static class Extensions
    {
        public static void AddBuff(this NwCreature creature)
        {
            creature.ApplyEffect(EffectDuration.Permanent, Effect.ACIncrease(5, ACBonus.ShieldEnchantment));
            creature.ApplyEffect(EffectDuration.Instant, Effect.VisualEffect(VfxType.ImpAcBonus));
        }

        public static void RemoveBuff(this NwCreature creature) => creature.RemoveEffect(Effect.ACIncrease(5, ACBonus.ShieldEnchantment));

        public static bool StopScript(this NwCreature creature) => creature.CheckCreatureSize() || creature.HasShieldEquipped();

        public static bool CheckCreaturekSizeAndWeapon(this NwCreature creature)
        {
            if (creature.Size.Equals(CreatureSize.Medium) && HasTwoHandLargeWeapon(creature))
            {
                return true;
            }
            else if (creature.Size.Equals(CreatureSize.Small) && HasMediumWeapon(creature))
            {
                return true;
            }
            else if (creature.Size.Equals(CreatureSize.Tiny) && HasSmallmWeapon(creature))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckCreatureSize(this NwCreature creature) => creature.Size switch
        {
            CreatureSize.Huge or CreatureSize.Invalid or CreatureSize.Large => true,
            _ => false,
        };

        private static bool HasShieldEquipped(this NwCreature creature) => creature.GetItemInSlot(InventorySlot.LeftHand)?.BaseItemType is
            BaseItemType.LargeShield or
            BaseItemType.SmallShield or
            BaseItemType.TowerShield;

        private static bool HasSmallmWeapon(NwCreature creature) => creature.GetItemInSlot(InventorySlot.RightHand)?.BaseItemType is
            BaseItemType.Handaxe or
            BaseItemType.Kama or
            BaseItemType.LightCrossbow or
            BaseItemType.LightHammer or
            BaseItemType.LightMace or
            BaseItemType.Shortsword or
            BaseItemType.Sickle or
            BaseItemType.Sling or
            BaseItemType.ThrowingAxe;

        private static bool HasMediumWeapon(NwCreature creature) => creature.GetItemInSlot(InventorySlot.RightHand)?.BaseItemType is
            BaseItemType.Battleaxe or
            BaseItemType.Bastardsword or
            BaseItemType.Club or
            BaseItemType.DwarvenWaraxe or
            BaseItemType.HeavyCrossbow or
            BaseItemType.Katana or
            BaseItemType.Morningstar or
            BaseItemType.LightFlail or
            BaseItemType.Longsword or
            BaseItemType.MagicStaff or
            BaseItemType.Rapier or
            BaseItemType.Scimitar or
            BaseItemType.Shortbow or
            BaseItemType.Warhammer;

        private static bool HasTwoHandLargeWeapon(NwCreature creature) => creature.GetItemInSlot(InventorySlot.RightHand)?.BaseItemType is
            BaseItemType.DireMace or
            BaseItemType.Doubleaxe or
            BaseItemType.Greataxe or
            BaseItemType.Greatsword or
            BaseItemType.Halberd or
            BaseItemType.HeavyFlail or
            BaseItemType.Longbow or
            BaseItemType.Quarterstaff or
            BaseItemType.Scythe or
            BaseItemType.ShortSpear or
            BaseItemType.Trident or
            BaseItemType.TwoBladedSword;
    }
}