using System.Collections.Generic;

using NWN.API.Constants;
using NWN.API.Events;

namespace Services.PlayableRaces
{
    public interface IRace
    {
        public bool IsUndead { get; set; }
        public int ModifyStrength { get; set; }
        public int ModifyDexterity { get; set; }
        public int ModifyConstitution { get; set; }
        public int ModifyIntelligence { get; set; }
        public int ModifyWisdom { get; set; }
        public int ModifyCharisma { get; set; }
        public int? Hair { get; set; }
        public int? Skin { get; set; }
        public int MaxLevel { get; set; }
        public int SR { get; set; }
        public int? PortraitID { get; set; }
        public int? SoundSet { get; set; }
        public string? HideResRef { get; set; }
        public string? WeaponLeft { get; set; }
        public string? WeaponRight { get; set; }
        public CreaturePart? Head { get; set; }
        public CreatureSize? ChangeSize { get; set; }
        public CreatureTailType? Tail { get; set; }
        public CreatureWingType? Wing { get; set; }
        public MovementRate MoveRate { get; set; }
        public RacialType Race { get; set; }
        public List<Alignment>? AlignmentsAllowed { get; set; }
        public AppearanceType Appearance { get; set; }
        public List<ClassType>? FavoredClasses { get; set; }
        public List<EffectType>? Effects { get; set; }
        public List<Feat>? FeatList { get; set; }

        public void ApplyUndead(ModuleEvents.OnClientEnter obj);
        public void ApplyItems(ModuleEvents.OnClientEnter obj);
        public void ApplyAppearance(ModuleEvents.OnClientEnter obj);
        public void ApplyFeats(ModuleEvents.OnClientEnter obj);
    }
}
