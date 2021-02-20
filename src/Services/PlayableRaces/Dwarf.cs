using System.Collections.Generic;
using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public abstract class Dwarf
    {
        private string hideResRef = "";
        private string creatureArmor = "";
        private string creatureWeaponLeft = "";
        private string creatureWeaponRight = "";
        private List<Feat> featList = new List<Feat>();
        private List<EffectType> effects = new List<EffectType>();

        public bool IsUndead { get; set; }
        public int MaxLevel { get; set; }
        public int SoundSet { get; set; }
        public int SR { get; set; }
        public int PortraitID { get; set; }
        public string HideResRef { get => hideResRef; set => hideResRef = value; }
        public string CreatureArmor { get => creatureArmor; set => creatureArmor = value; }
        public string CreatureWeaponLeft { get => creatureWeaponLeft; set => creatureWeaponLeft = value; }
        public string CreatureWeaponRight { get => creatureWeaponRight; set => creatureWeaponRight = value; }
        public List<EffectType> Effects { get => effects; set => effects = value; }
        public List<Feat> FeatList { get => featList; set => featList = value; }
        public AppearanceType Appearance { get; set; }
        public CreatureSize Size { get; set; }
        public MovementRate MoveRate { get; set; }
    }
}