using System.Collections.Generic;
using NWN.API;
using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public class IceDwarf : INwSubrace
    {
        TemplateData INwSubrace.Data =>
            new TemplateData
            {
                RawName = null,
                SubraceName = "Ice-Dwarf",
                Race = RacialType.Dwarf,
            };

        void INwSubrace.Apply(NwPlayer player)
        {
            throw new System.NotImplementedException();
        }

        void INwSubrace.Init(NwPlayer player)
        {
            throw new System.NotImplementedException();
        }
    }
}