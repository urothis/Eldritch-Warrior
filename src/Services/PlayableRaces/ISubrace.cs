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
        public string HideResRef { get; set; }
        public string Armor { get; set; }
        public string WeaponLeft { get; set; }
        public string WeaponRight { get; set; }
        public bool IsUndead { get; set; }

        public struct AbilityModifier
        {
            public int Strength { get; set; }
            public int Dexterity { get; set; }
            public int Constitution { get; set; }
            public int Intelligence { get; set; }
            public int Wisdom { get; set; }
            public int Charisma { get; set; }
        }

        struct BodyPart
        {
            public CreaturePart Head { get; set; }
            public ColorChannel Hair { get; set; }
            public ColorChannel Skin { get; set; }
            public CreatureWingType Wing { get; set; }
            public CreatureTailType Tail { get; set; }
        }

        
    }
}
