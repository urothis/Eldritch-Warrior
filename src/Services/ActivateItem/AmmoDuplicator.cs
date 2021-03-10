/*using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NWN.API.Constants;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(AmmoDuplicator))]
    public class AmmoDuplicator
    {
        public AmmoDuplicator(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnActivateItem>(NwModule.Instance, ActivateItem);

        private void ActivateItem(ModuleEvents.OnActivateItem activeItemEvent)
        {
            NwItem item = (NwItem)activeItemEvent.TargetObject;
            NwPlayer player = (NwPlayer)activeItemEvent.ItemActivator;

            if (activeItemEvent.ActivatedItem.Tag.Equals("itm_ammo_creator"))
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
                        break;
                    default:
                        player.SendServerMessage($"Invalid Target {item.BaseItemType.ToString().ColorString(Color.WHITE)}".ColorString(Color.ORANGE));
                        return;
                }
            }
            player.SendServerMessage($"{item.StackSize.ToString().ColorString(Color.SILVER)} replenished for {item.BaseItemType.ToString().ColorString(Color.WHITE)}");
        }
    }
}*/