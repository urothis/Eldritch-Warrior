using System.Collections.Generic;

using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public interface ISubrace : IRace
    {
        public new int ModifyStrength { get; set; }
        public new int ModifyDexterity { get; set; }
        public new int ModifyConstitution { get; set; }
        public new int ModifyIntelligence { get; set; }
        public new int ModifyWisdom { get; set; }
        public new int ModifyCharisma { get; set; }
        public new bool IsUndead { get; set; }
        public new int MaxLevel { get; set; }
        public new int SR { get; set; }
        public new string? HideResRef { get; set; }
        public new string? WeaponLeft { get; set; }
        public new string? WeaponRight { get; set; }
        public new List<EffectType>? Effects { get; set; }
        public new List<Feat>? FeatList { get; set; }
        public new List<Alignment>? AlignmentsAllowed { get; set; }

        //http://wiki.avlis.org/Dynamic_Dye_Color_Chart
        
        public int Hair { get; set; }
        public int Skin { get; set; }
        public CreaturePart Head { get; set; }
        public CreatureTailType Tail { get; set; }
        public CreatureWingType Wing { get; set; }
    }
}
