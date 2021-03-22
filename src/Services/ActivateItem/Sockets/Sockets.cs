using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(IItemHandler))]
    public class Sockets : IItemHandler
    {
        public string Tag => "sf_sockets";

        public void HandleActivateItem(ModuleEvents.OnActivateItem activateItem)
        {
            NwPlayer pc = (NwPlayer)activateItem.ItemActivator;
            NwItem target = (NwItem)activateItem.TargetObject;

            if (pc.IsInCombat)
            {
                pc.SendServerMessage("Cannot use this item in combat.".ColorString(Color.ORANGE));
            }
            else if (pc != (NwPlayer)activateItem.ActivatedItem.Possessor)
            {
                pc.SendServerMessage("Target item is not in your possession.".ColorString(Color.ORANGE));
            }
            else if (!target.CheckItemIsValidType())
            {
                pc.SendServerMessage("Target item is invalid.".ColorString(Color.ORANGE));
            }
            else
            {
                Services.Module.Extensions.RemoveAllTemporaryItemProperties((NwItem)target);
                activateItem.SocketRunesToItem(pc, target);
            }
        }
    }
}