using System.Collections.Generic;

using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public class DwarfArtic : ISubrace
    {
        public bool IsUndead { get; set; }
        public int ArmorClass { get; set; }
        public int MaxLevel { get; set; }
        public int SR { get; set; }
        public MovementRate MoveRate { get; set; }
        public string CreatureArmor { get; set; }
        public string CreatureWeaponLeft { get; set; }
        public string CreatureWeaponRight { get; set; }
        public List<EffectType> Effects { get; set; }
        public List<Feat> FeatList { get; set; }

        public DwarfArtic()
        {
            IsUndead = false;
            ArmorClass = 0;
            MaxLevel = 58;
            SR = 0;
            MoveRate = MovementRate.CreatureDefault;
            CreatureArmor = "";
            CreatureWeaponLeft = "";
            CreatureWeaponRight = "";
            Effects = new List<EffectType>();
            FeatList = new List<Feat>();
        }
    }
}