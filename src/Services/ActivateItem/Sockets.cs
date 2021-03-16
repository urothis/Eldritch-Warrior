using System;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(Sockets))]
    public class Sockets : IItemHandler
    {
        public string Tag => "itm_socket";

        public void HandleActivateItem(ModuleEvents.OnActivateItem activateItem)
        {
            NwPlayer pc = (NwPlayer)activateItem.ItemActivator;
            var target = activateItem.TargetObject;

            if (pc.IsInCombat)
            {
                pc.SendServerMessage("Cannot use this item in combat.".ColorString(Color.ORANGE));
            }
            else if (pc != (NwPlayer)activateItem.ActivatedItem.Possessor)
            {
                pc.SendServerMessage("Target item is not in your possession.".ColorString(Color.ORANGE));
            }
            else if (target.Tag != Tag)
            {
                pc.SendServerMessage("Target item is not a socket item.".ColorString(Color.ORANGE));
            }
            else if (!CheckItemIsValidType(activateItem))
            {
                pc.SendServerMessage("Target item is invalid.".ColorString(Color.ORANGE));
            }
            else
            {
                //remove buffs
                //socket item
            }
        }

        public static bool CheckItemIsValidType(ModuleEvents.OnActivateItem item)
        {
            switch(item.ActivatedItem.BaseItemType)
            {
                case BaseItemType.Amulet:
                case BaseItemType.Armor:
                case BaseItemType.Bastardsword:
                case BaseItemType.Battleaxe:
                case BaseItemType.Belt:
                case BaseItemType.Boots:
                case BaseItemType.Bracer:
                case BaseItemType.Cloak:
                case BaseItemType.Club:
                case BaseItemType.Dagger:
                case BaseItemType.DireMace:
                case BaseItemType.Doubleaxe:
                case BaseItemType.DwarvenWaraxe:
                case BaseItemType.Gloves:
                case BaseItemType.Greataxe:
                case BaseItemType.Greatsword:
                case BaseItemType.Halberd:
                case BaseItemType.Handaxe:
                case BaseItemType.HeavyCrossbow:
                case BaseItemType.HeavyFlail:
                case BaseItemType.Helmet:
                case BaseItemType.Kama:
                case BaseItemType.Katana:
                case BaseItemType.Kukri:
                case BaseItemType.LargeShield:
                case BaseItemType.LightCrossbow:
                case BaseItemType.LightFlail:
                case BaseItemType.LightHammer:
                case BaseItemType.LightMace:
                case BaseItemType.Longbow:
                case BaseItemType.Longsword:
                case BaseItemType.MagicStaff:
                case BaseItemType.Morningstar:
                case BaseItemType.Quarterstaff:
                case BaseItemType.Rapier:
                case BaseItemType.Ring:
                case BaseItemType.Scimitar:
                case BaseItemType.Scythe:
                case BaseItemType.Shortbow:
                case BaseItemType.ShortSpear:
                case BaseItemType.Shortsword:
                case BaseItemType.Sickle:
                case BaseItemType.Sling:
                case BaseItemType.SmallShield:
                case BaseItemType.TowerShield:
                case BaseItemType.Trident:
                case BaseItemType.TwoBladedSword:
                case BaseItemType.Warhammer:
                case BaseItemType.Whip: return true;
                default: return false;
            }
        }
    }
}