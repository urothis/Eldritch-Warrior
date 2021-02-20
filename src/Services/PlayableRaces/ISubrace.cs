using System.Collections.Generic;

using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public interface ISubrace
    {
        public int MaxLevel { get; set; }
        public int SR { get; set; }
        public int SoundSet { get; set; }
        public int PortraitID { get; set; }
        public AppearanceType Appearance { get; set; }
        public MovementRate MoveRate { get; set; }
        public CreatureSize Size { get; set; }
        public List<Feat> FeatList { get; set; }
        public List<EffectType> Effects { get; set; }
        public string CreatureArmor { get; set; }
        public string CreatureWeaponLeft { get; set; }
        public string CreatureWeaponRight { get; set; }
        public bool IsUndead { get; set; }
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

    public struct BodyParts
    {
        public CreaturePart Head { get; set; }
        public int Hair { get; set; }
        //http://wiki.avlis.org/Dynamic_Dye_Color_Chart
        public int Skin { get; set; }
        public CreatureWingType Wing { get; set; }
        public CreatureTailType Tail { get; set; }
    }
}
