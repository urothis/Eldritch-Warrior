using NWN.API;

using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public class DwarfArtic : INwSubrace
    {
        TemplateData INwSubrace.Data =>
            new TemplateData
            {
                RawName = "Dwarf-Artic",
                SubraceName = "Dwarf Artic",
                Race = RacialType.Dwarf,
            };

        public void Apply(NwPlayer player)
        {
            throw new System.NotImplementedException();
        }

        void INwSubrace.Init(NwPlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}