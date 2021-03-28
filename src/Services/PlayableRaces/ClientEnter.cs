using NWN.API;
using NWN.API.Constants;
using NWN.Services;

namespace Services.PlayableRaces
{
    [ServiceBinding(typeof(ClientEnter))]
    public class ClientEnter
    {
        public ClientEnter() => NwModule.Instance.OnClientEnter += obj =>
        {
            if (obj.Player.SubRace.Length != 0)
            {
                if (obj.Player.SubraceValid())
                {
                    if (obj.Player.Xp == 0)
                    {
                        switch (obj.Player.RacialType)
                        {
                            case RacialType.Dwarf:
                                Dwarf dwarf = new Dwarf(obj);
                                dwarf.ApplyPlayableRace(obj);
                                break;
                            case RacialType.Elf:
                                Elf elf = new Elf(obj);
                                elf.ApplyAppearance(obj);
                                break;
                            case RacialType.Gnome:
                                Gnome gnome = new Gnome(obj);
                                gnome.ApplyAppearance(obj);
                                break;
                            case RacialType.Halfling:
                                Halfling halfling = new Halfling(obj);
                                halfling.ApplyAppearance(obj);
                                break;
                            case RacialType.Human:
                            case RacialType.HalfElf:
                                Human human = new Human(obj);
                                human.ApplyAppearance(obj);
                                break;
                        }
                    }
                    else
                    {
                        //Reapply Subrace
                    }
                }
                else
                {
                    obj.Player.SendServerMessage($"{"ERROR".ColorString(Color.RED)}!!! - INVALID SUBRACE NAME.");
                }
            }
        };
    }
}