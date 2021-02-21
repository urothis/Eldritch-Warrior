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
        //http://wiki.avlis.org/Dynamic_Dye_Color_Chart
        public int Hair { get; set; }
        public int Skin { get; set; }
        public CreaturePart Head { get; set; }
        public CreatureTailType Tail { get; set; }
        public CreatureWingType Wing { get; set; }
    }

    public struct TemplateData
    {
        public string RawName;
        public string SubraceName;
        public RacialType Race;
        public RacialType[] RacesAllowed;
        public Alignment[] AlignmentsAllowed;
        public bool IsUndead;
        public int MaxLevel;
        public int PortraitID;
        public int SoundSet;
        public int SR;
        public string HideResRef;
        public string WeaponLeft;
        public string WeaponRight;
        public AbilityModifier StatModifier;
        public AppearanceType Appearance;
        public BodyParts bodyParts;
        public CreatureSize Size;
        public MovementRate MoveRate;
        public List<EffectType> Effects;
        public List<Feat> FeatList;
    }
}
