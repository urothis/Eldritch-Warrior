using System;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;

namespace Services.PlayableRaces
{
    public static class Extensions
    {
        public static bool SubraceValid(this NwPlayer pc)
        {
            return pc.RacialType switch
            {
                RacialType.Dwarf when Array.Exists(Roster.dwarf, x => x.Equals(pc.SubRace, StringComparison.InvariantCultureIgnoreCase)) => true,
                RacialType.Elf when Array.Exists(Roster.elf, x => x.Equals(pc.SubRace, StringComparison.InvariantCultureIgnoreCase)) => true,
                RacialType.Gnome when Array.Exists(Roster.gnome, x => x.Equals(pc.SubRace, StringComparison.InvariantCultureIgnoreCase)) => true,
                RacialType.Halfling when Array.Exists(Roster.halfling, x => x.Equals(pc.SubRace, StringComparison.InvariantCultureIgnoreCase)) => true,
                RacialType.Human when Array.Exists(Roster.human, x => x.Equals(pc.SubRace, StringComparison.InvariantCultureIgnoreCase)) => true,
                RacialType.HalfOrc when Array.Exists(Roster.orc, x => x.Equals(pc.SubRace, StringComparison.InvariantCultureIgnoreCase)) => true,
                RacialType.All when Array.Exists(Roster.Planetouched, x => x.Equals(pc.SubRace, StringComparison.InvariantCultureIgnoreCase)) => true,
                _ => false
            };
        }

        public static void InitPlayableRace(this ModuleEvents.OnClientEnter obj)
        {
        }
    }
}