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
            NwGameObject target = activateItem.TargetObject;

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
            else if (!activateItem.CheckItemIsValidType())
            {
                pc.SendServerMessage("Target item is invalid.".ColorString(Color.ORANGE));
            }
            else
            {
                NwItem item = (NwItem)target;
                Services.Module.Extensions.RemoveAllTemporaryItemProperties(item);
                activateItem.ActivatedItem.SocketGemToItem(pc, item);
            }
        }
    }
}