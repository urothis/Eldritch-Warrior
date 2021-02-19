using System.Collections.Generic;
using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public abstract class Dwarf : ISubrace
    {
        public int MaxLevel { get; set; }
        public int SR { get; set; }
        public int SoundSet { get; set; }
        public int PortraitID { get; set; }
        public bool IsUndead { get; set; } 
        public AppearanceType Appearance { get; set; }
        public RacialType RaceType { get; set; }
        public MovementRate MoveRate { get; set; }
        public CreatureSize Size { get; set; }
        public abstract List<Feat> FeatList { get; set; }
        public abstract List<EffectType> Effects { get; set; }
        public abstract string HideResRef { get; set; }
        public abstract string Armor { get; set; }
        public abstract string WeaponLeft { get; set; }
        public abstract string WeaponRight { get; set; }
    }
}