using System.Collections.Generic;

using NWN.API.Constants;

namespace Services.PlayableRaces
{
    internal class DwarfArtic : ISubrace
    {
        internal DwarfArtic(bool isUndead, int maxLevel, int sR, int soundSet, int portraitID, AppearanceType appearance, MovementRate moveRate, string creatureArmor, string creatureWeaponLeft, string creatureWeaponRight)
        {
            IsUndead = isUndead;
            MaxLevel = maxLevel;
            SR = sR;
            SoundSet = soundSet;
            PortraitID = portraitID;
            Appearance = appearance;
            MoveRate = moveRate;
            CreatureArmor = creatureArmor;
            CreatureWeaponLeft = creatureWeaponLeft;
            CreatureWeaponRight = creatureWeaponRight;
        }

        public bool IsUndead { get; set; }
        public int MaxLevel { get; set; }
        public int SR { get; set; }
        public int SoundSet { get; set; }
        public int PortraitID { get; set; }
        public AppearanceType Appearance { get; set; }
        public CreatureSize Size { get; set; }
        public MovementRate MoveRate { get; set; }
        public string CreatureArmor { get; set; }
        public string CreatureWeaponLeft { get; set; }
        public string CreatureWeaponRight { get; set; }

    }
}