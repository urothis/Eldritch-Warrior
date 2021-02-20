using System.Collections.Generic;

using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public interface ISubrace : IRaces
    {

    }

    public struct BodyParts
    {
        public CreaturePart Head { get; set; }
        public int Hair { get; set; }
        //http://wiki.avlis.org/Dynamic_Dye_Color_Chart
        public int Skin { get; set; }
        public CreatureWingType Wing { get; set; }
        public CreatureTailType Tail { get; set; }
    }
}
