using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public interface IMonster : IRaces
    {
        public AppearanceType Appearance { get; set; }
        public CreatureSize Size { get; set; }
        public int SoundSet { get; set; }
        public int PortraitID { get; set; }
    }
}