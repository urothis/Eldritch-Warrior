using System.Collections.Generic;
using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public interface IRaces
    {
        public bool IsUndead { get; set; }
        public int MaxLevel { get; set; }
        public int SR { get; set; }
        public MovementRate MoveRate { get; set; }
        public string CreatureArmor { get; set; }
        public string CreatureWeaponLeft { get; set; }
        public string CreatureWeaponRight { get; set; }
        public List<EffectType> Effects { get; set; }
        public List<Feat> FeatList { get; set; }

    }

    public struct AbilityModifier
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
    }
}