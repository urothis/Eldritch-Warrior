using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.PlayableRaces
{
    public interface Subrace
    {
        public string Name { get; set; }
        public int MaxLevel { get; set; }
        public Alignment[] AlignmentRestriction { get; set; }
        public ClassType[] ClassesRestriction { get; set; }
        //  STR DEX CON INT WIS CHA
        public int[] AbilityPoints { get; set; }
    }
}
