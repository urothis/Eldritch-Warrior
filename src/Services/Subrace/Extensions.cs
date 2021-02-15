//using NLog;

using System;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.Subrace
{
    public static class Extensions
    {
        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static void SubRaceEnter(this ModuleEvents.OnClientEnter enter)
        {
            //  The player didn't input a subrace so stop here
            if (enter.Player.SubRace.ToString().Length == 0)
            {
                return;
            }

            //  If valid Subrace proceed else tell player subrace is not valid
            if (enter.Player.SubraceValid())
            {
                return;
            }
            else
            {
                enter.Player.SendServerMessage($"\n{"ERROR".ColorString(Color.RED)}: The subrace name you have entered doesn't exist.\n");
            }
        }

        public static bool SubraceValid(this NwPlayer pc)
        {
            if (Array.Exists(Roster.dwarf, x => x == pc.SubRace))
            {
                return true;
            }
            else if (Array.Exists(Roster.elf, x => x == pc.SubRace))
            {
                return true;
            }
            else if (Array.Exists(Roster.gnome, x => x == pc.SubRace))
            {
                return true;
            }
            else if (Array.Exists(Roster.halfling, x => x == pc.SubRace))
            {
                return true;
            }
            else if (Array.Exists(Roster.human, x => x == pc.SubRace))
            {
                return true;
            }
            else if (Array.Exists(Roster.orc, x => x == pc.SubRace))
            {
                return true;
            }
            else if (Array.Exists(Roster.Planetouched, x => x == pc.SubRace))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}