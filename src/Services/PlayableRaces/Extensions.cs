using System;
using System.Linq;
using NLog;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;


namespace Services.PlayableRaces
{
    public static class Extensions
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static bool SubraceValid(this NwPlayer pc)
        {
            string subrace = pc.SubRace;

            switch (pc.RacialType)
            {
                case RacialType.Dwarf:
                    {
                        if (Roster.dwarf.Contains(subrace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.Elf:
                    {
                        if (Roster.elf.Contains(subrace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.Gnome:
                    {
                        if (Roster.gnome.Contains(subrace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.HalfElf:
                case RacialType.Human:
                    {
                        if (Roster.human.Contains(subrace) || Roster.planetouched.Contains(subrace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.Halfling:
                    {
                        if (Roster.halfling.Contains(subrace))
                        {
                            return true;
                        }
                        break;
                    }
                case RacialType.HalfOrc:
                    {
                        if (Roster.orc.Contains(subrace))
                        {
                            return true;
                        }
                        break;
                    }
                default: break;
            }
            return false;
        }

        public static void ApplySubrace(this ModuleEvents.OnClientEnter obj)
        {
            Log.Info("HELLO InitPlayableRace");
        }
    }
}
