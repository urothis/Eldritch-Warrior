using System.Collections.Generic;
using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public class DwarfArtic : ISubrace
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

        public DwarfArtic()
        {
            MaxLevel = 58;
            FeatList = new List<Feat>();
            Effects = new List<EffectType>();
            CreatureArmor = "";
            CreatureWeaponLeft = "";
            CreatureWeaponRight = "";
        }
    }
}