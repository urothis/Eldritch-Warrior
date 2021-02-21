//using System;
using System;

using NWN.API;
using NWN.API.Constants;

namespace Services.PlayableRaces
{
    public static class Extensions
    {
        public static bool SubraceValid(this NwPlayer pc)
        {
            return pc.RacialType switch
            {
                RacialType.Dwarf when Array.Exists(Roster.dwarf, x => x == pc.SubRace) => true,
                RacialType.Elf when Array.Exists(Roster.elf, x => x == pc.SubRace) => true,
                RacialType.Gnome when Array.Exists(Roster.gnome, x => x == pc.SubRace) => true,
                RacialType.Halfling when Array.Exists(Roster.halfling, x => x == pc.SubRace) => true,
                RacialType.Human when Array.Exists(Roster.human, x => x == pc.SubRace) => true,
                RacialType.HalfOrc when Array.Exists(Roster.orc, x => x == pc.SubRace) => true,
                RacialType.All when Array.Exists(Roster.Planetouched, x => x == pc.SubRace) => true,
                _ => false
            };
        }
    }
}