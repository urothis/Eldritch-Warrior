using System.Collections.Generic;
using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public interface IRace
    {
        int ModifyStrength { get; set; }
        int ModifyDexterity { get; set; }
        int ModifyConstitution { get; set; }
        int ModifyIntelligence { get; set; }
        int ModifyWisdom { get; set; }
        int ModifyCharisma { get; set; }
        bool IsUndead { get; set; }
        int MaxLevel { get; set; }
        int SR { get; set; }
        string? HideResRef { get; set; }
        string? WeaponLeft { get; set; }
        string? WeaponRight { get; set; }
        List<EffectType>? Effects { get; set; }
        List<Feat>? FeatList { get; set; }
        List<Alignment>? AlignmentsAllowed { get; set; }
        List<ClassType>? FavoredClasses {get; set; }
    }
}