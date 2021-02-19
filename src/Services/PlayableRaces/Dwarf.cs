using System.Collections.Generic;
using NWN.API;
using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public abstract class Dwarf : ISubrace
    {
        public int MaxLevel { get; set; }
        public int SoundSet { get; set; }
        public int SR { get; set; }
        public int PortraitID { get; set; }
        public bool IsUndead { get; set; }
        public AppearanceType Appearance { get; set; }
        public MovementRate MoveRate { get; set; }
        public CreatureSize Size { get; set; }
        public abstract List<Feat> FeatList { get; set; }
        public abstract List<EffectType> Effects { get; set; }
        public abstract string HideResRef { get; set; }
        public abstract string Armor { get; set; }
        public abstract string WeaponLeft { get; set; }
        public abstract string WeaponRight { get; set; }
    }

    public class Artic : Dwarf
    {
        public Artic()
        {
            MaxLevel = 58;
            IsUndead = false;
            HideResRef = "";
        }

        public override List<Feat> FeatList { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override List<EffectType> Effects { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override string HideResRef { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override string Armor { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override string WeaponLeft { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override string WeaponRight { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}