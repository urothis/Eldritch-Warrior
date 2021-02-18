using System;
using System.Collections.Generic;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;

using NWN.Services;

namespace Services.PlayableRaces
{
    interface ISubrace
    {
        public int MaxLevel { get; set; }
        public int SR { get; set; }
        public int SoundSet { get; set; }
        public int PortraitID { get; set; }
        public AppearanceType Appearance { get; set; }
        public RacialType RaceType { get; set; }
        public MovementRate MoveRate { get; set; }
        public CreatureSize Size { get; set; }
        public List<Feat> FeatList { get; set; }
        public List<EffectType> Effects { get; set; }
        public string HideResRef { get; set; }
        public string Armor { get; set; }
        public string WeaponLeft { get; set; }
        public string WeaponRight { get; set; }

        struct Ability
        {
            public int Strength { get; set; }
            public int Dexterity { get; set; }
            public int Constitution { get; set; }
            public int Intelligence { get; set; }
            public int Wisdom { get; set; }
            public int Charisma { get; set; }
        }

        struct BodyPart
        {
            public CreaturePart Head { get; set; }
            public ColorChannel Hair { get; set; }
            public ColorChannel Skin { get; set; }
            public CreatureWingType Wing { get; set; }
            public CreatureTailType Tail { get; set; }
        }
    }
}

/*
using System;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;

using NWN.Services;

namespace Services.PlayableRaces
{
	public class exampleSubrace : Subrace
	{
		public abstract string Name { get; set; }
		public abstract Stats statBonus { new SubRaceData(1,1,1,1,1,1); set; }

		public abstract void SetStats(NwPlayer pc)
		{
			return;
		}
}


public struct AbilityScore
{
	public byte Strength;
	public byte Dexterity;
	public byte Constitution;
	public byte Intelligence;
	public byte Wisdom;
	public byte Charisma;
}

public struct SubRaceData {
	public string Name;
	public AbilityScoreBonus stat;
	public byte MaxLevel;
	public byte SR;
	public string HideResRef;
}

interface ISubrace
{
	public string Name
	{
		get;
		set;
	}

	public AbilityScoreBonus statBonus
	{
		get;
		set;
	}

}

public abstract class Subrace : ISubrace
{
	public abstract string Name { get; set; }
	public abstract AbilityScoreBonus statBonus { get; set; }
}
}
*/
