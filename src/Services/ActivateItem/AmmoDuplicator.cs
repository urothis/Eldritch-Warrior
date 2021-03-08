using NWN.API;
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
            NwItem item = activeItemEvent.ActivatedItem;
            NwPlayer player = (NwPlayer)activeItemEvent.ItemActivator;

            switch (item.BaseItemType)
            {
                case BaseItemType.Arrow:
                case BaseItemType.Bolt:
                case BaseItemType.Bullet:
                    item.StackSize = 99;
                    item.PlotFlag = true;
                    break;
                case BaseItemType.Dart:
                case BaseItemType.Shuriken:
                case BaseItemType.ThrowingAxe:
                    item.StackSize = 50;
                    item.PlotFlag = true;
                    break;
                case BaseItemType.Grenade:
                    item.StackSize = 10;
                    item.PlotFlag = true;
                    break;
                default:
                    player.SendServerMessage($"Invalid Target {item.BaseItemType.ToString().ColorString(Color.WHITE)}".ColorString(Color.ORANGE));
                    return;
            }
            
            player.SendServerMessage($"{item.StackSize.ToString().ColorString(Color.SILVER)} replenished for {item.BaseItemType.ToString().ColorString(Color.WHITE)}");
        }
    }
}