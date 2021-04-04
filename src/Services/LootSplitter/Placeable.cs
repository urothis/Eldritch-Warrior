using System.Linq;
using NLog;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.LootSplitter
{
    [ServiceBinding(typeof(Placeable))]
    public class Placeable
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        [ScriptHandler("plc_sell_loot")]
        public void Disturbed(CallInfo callinfo)
        {
            logger.Info("1");
            //Process only objects added
            if (callinfo.TryGetEvent(out PlaceableEvents.OnDisturbed obj))
            {
                logger.Info("2");
                switch (obj.DisturbType)
                {
                    case InventoryDisturbType.Added:
                        DestroyItemForGold(obj);
                        logger.Info("*");
                        break;
                    case InventoryDisturbType.Removed:
                    case InventoryDisturbType.Stolen:
                        break;
                }
            }
        }

        private static void DestroyItemForGold(PlaceableEvents.OnDisturbed obj)
        {
            logger.Info("11");
            NwPlayer pc = (NwPlayer)obj.Disturber;
            logger.Info("22");
            if (obj.DisturbedItem.BaseItemType == BaseItemType.Gold)
            {
                logger.Info("33");
                GiveGoldEqually(pc, obj.DisturbedItem.StackSize);
                CloneDestroy(obj, pc);
            }
            else if (obj.DisturbedItem.HasInventory)
            {
                logger.Info("44");
                pc.FloatingTextString($"{pc.Name} cannot sell inventory items {pc.Name.ColorString(Color.WHITE)}!".ColorString(Color.ORANGE));
                CloneDestroy(obj, pc);

            }
            else if (obj.DisturbedItem.PlotFlag)
            {
                logger.Info("55");
                pc.FloatingTextString($"{pc.Name} cannot sell plot items {pc.Name.ColorString(Color.WHITE)}!".ColorString(Color.ORANGE));
                CloneDestroy(obj, pc);
            }
            else
            {
                logger.Info("66");
                int itemValue = obj.DisturbedItem.GoldValue / 10 > 0 ? obj.DisturbedItem.GoldValue / 10 : 1;

                pc.FloatingTextString($"{pc.Name} sold {pc.Name.ColorString(Color.WHITE)} for {itemValue}!".ColorString(Color.GREEN));
                GiveGoldEqually(pc, itemValue);
            }
        }

        private static void GiveGoldEqually(NwPlayer pc, int itemValue)
        {
            logger.Info("111");
            int goldDivided = itemValue / pc.PartyMembers.Count<NwPlayer>();
            logger.Info("222");
            pc.FloatingTextString($"{goldDivided.ToString().ColorString(Color.WHITE)} given to each player after splitting {itemValue.ToString().ColorString(Color.WHITE)} from {pc.Name.ColorString(Color.WHITE)}.");
            logger.Info("333");
            foreach (NwPlayer player in pc.PartyMembers)
            {
                logger.Info(pc.Name);
                player.GiveGold(goldDivided, true);
            }
        }

        private static void CloneDestroy(PlaceableEvents.OnDisturbed obj, NwPlayer pc) => obj.DisturbedItem.Clone(pc).Destroy();
    }
}