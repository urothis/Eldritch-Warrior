using System.Collections.Generic;
using NWN.API.Constants;
using NWN.API.Events;
namespace Services.PlayableRaces
{
    public class Dwarf : ISubrace
    {
        public int ModifyStrength { get; set; }
        public int ModifyDexterity { get; set; }
        public int ModifyConstitution { get; set; }
        public int ModifyIntelligence { get; set; }
        public int ModifyWisdom { get; set; }
        public int ModifyCharisma { get; set; }
        public bool IsUndead { get; set; }
        public int MaxLevel { get; set; }
        public int SR { get; set; }
        public string? HideResRef { get; set; }
        public string? WeaponLeft { get; set; }
        public string? WeaponRight { get; set; }
        public List<EffectType>? Effects { get; set; }
        public List<Feat>? FeatList { get; set; }
        public List<Alignment>? AlignmentsAllowed { get; set; }
        public List<ClassType>? FavoredClasses { get; set; }
        public int Hair { get; set; }
        public int Skin { get; set; }
        public CreaturePart Head { get; set; }
        public CreatureTailType Tail { get; set; }
        public CreatureWingType Wing { get; set; }

        public Dwarf(ModuleEvents.OnClientEnter obj)
        {
            switch (obj.Player.SubRace)
            {
                case "Artic Dwarf":
                    {
                        ModifyStrength = 4;
                        ModifyDexterity = -2;
                        ModifyConstitution = 2;
                        ModifyCharisma = -2;
                        MaxLevel = 58;
                        HideResRef = "";
                        Hair = 62;
                        Skin = 27;
                        FeatList?.Add(Feat.ImprovedUnarmedStrike);
                        FavoredClasses?.Add(ClassType.Ranger);
                    }
                    break;
            }
        }
    }
}