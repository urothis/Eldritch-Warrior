using System.Collections.Generic;

using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public interface IMonster : IRace
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

        public int PortraitID { get; set; }
        public int SoundSet { get; set; }
        public AppearanceType Appearance { get; set; }
        public CreatureSize Size { get; set; }
        public MovementRate MoveRate { get; set; }
        public RacialType Race { get; set; }
        public List<EffectType>? Effects1 { get; set; }
        public List<Feat>? FeatList1 { get; set; }
    }
}
