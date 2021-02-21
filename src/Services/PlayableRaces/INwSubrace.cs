using System.Collections.Generic;

using NWN.API;
using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public interface INwSubrace
    {
        public TemplateData Data { get; }
        public void Apply(NwPlayer player);
        public void Init(NwPlayer player);
    }

    public struct AbilityModifier
    {
        public int Strength;
        public int Dexterity;
        public int Constitution;
        public int Intelligence;
        public int Wisdom;
        public int Charisma;
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

    public struct TemplateData
    {
        public string RawName;
        public string SubraceName;
        public RacialType Race;
        public RacialType[] RacesAllowed;
        public Alignment[] AlignmentsAllowed;
        public int MaxLevel;
        public int SR;
        public int SoundSet;
        public int PortraitID;
        public AppearanceType Appearance;
        public MovementRate MoveRate;
        public CreatureSize Size;
        public List<Feat> FeatList;
        public List<EffectType> Effects;
        public string HideResRef;
        public string Armor;
        public string WeaponLeft;
        public string WeaponRight;
        public bool IsUndead;
        public AbilityModifier StatModifier;
        public BodyParts bodyParts;
    }
}
