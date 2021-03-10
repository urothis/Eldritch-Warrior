using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(IItemHandler))]
    public class AmmoDuplicator : IItemHandler
    {
        public string Tag => "itm_ammo_creator";

        public void HandleActivateItem(ModuleEvents.OnActivateItem activateItem)
        {
            if (activateItem.TargetObject is NwItem item && activateItem.ItemActivator is NwPlayer player)
            {
                switch (item.BaseItemType)
                {
                    case BaseItemType.Arrow:
                    case BaseItemType.Bolt:
                    case BaseItemType.Bullet:
                    case BaseItemType.Dart:
                    case BaseItemType.Shuriken:
                    case BaseItemType.ThrowingAxe:
                    case BaseItemType.Grenade:
                        item.Clone(player, "", true).PlotFlag = true;
                        player.SendServerMessage($"{item.StackSize.ToString().ColorString(Color.SILVER)} replenished for {item.BaseItemType.ToString().ColorString(Color.WHITE)}");
                        break;
                    default:
                        player.SendServerMessage($"Invalid Target {item.BaseItemType.ToString().ColorString(Color.WHITE)}".ColorString(Color.ORANGE));
                        break;
                }
            }
        }
    }
}