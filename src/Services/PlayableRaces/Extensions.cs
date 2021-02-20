//using System;
using System;
using System.Collections.Generic;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

namespace Services.PlayableRaces
{
    public static class Extensions
    {
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
                if (enter.Player.GetCampaignVariable<string>("SUBRACE", enter.Player.UUID.ToUUIDString()).Value == string.Empty)
                {
                    //New Character apply everything otherwise reapply subrace
                    enter.Player.GetCampaignVariable<string>("SUBRACE", enter.Player.UUID.ToUUIDString()).Value = enter.Player.UUID.ToUUIDString();
                    enter.Player.TransformSubrace();
                }
                else
                {
                    //Reapply subrace
                }
            }
            else
            {
                enter.Player.SendServerMessage($"\n{"ERROR".ColorString(Color.RED)}: The subrace name you have entered doesn't exist.\n");
            }
        }

        private static void TransformSubrace(this NwCreature player)
        {
            switch (player.SubRace)
            {
                case "Dwarf-Artic":
                    break;
                default:
                    break;
            }
        }

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