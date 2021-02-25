using System;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NLog;
using System.Linq;

namespace Services.PlayableRaces
{
    public static class Extensions
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static bool SubraceValid(this NwPlayer pc)
        {
            switch (pc.RacialType)
            {
                case RacialType.Dwarf:
                    {
                        if (Roster.dwarf.Contains(pc.SubRace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.Elf:
                    {
                        if (Roster.elf.Contains(pc.SubRace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.Gnome:
                    {
                        if (Roster.gnome.Contains(pc.SubRace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.HalfElf:
                case RacialType.Human:
                    {
                        if (Roster.human.Contains(pc.SubRace) || Roster.planetouched.Contains(pc.SubRace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.Halfling:
                    {
                        if (Roster.halfling.Contains(pc.SubRace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.HalfOrc:
                    {
                        if (Roster.orc.Contains(pc.SubRace))
                        {
                            return true;
                        }
                        break;
                    }
                default: break;
            }
            return false;
        }

        public static void InitPlayableRace(this ModuleEvents.OnClientEnter obj)
        {
            Log.Info("HELLO InitPlayableRace");
        }
    }
}
