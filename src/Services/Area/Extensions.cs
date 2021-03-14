using System.Linq;
//using NLog;
using NWN.API;
using NWN.API.Events;

namespace Services.Area
{
    public static class Extensions
    {
        public static void CleanupCreaturesAndItems(this AreaEvents.OnExit obj)
        {
            foreach (NwGameObject trash in obj.Area.Objects.Where(t => t is NwItem || t is NwCreature))
            {
                if (trash is NwCreature creature)
                {
                    foreach (NwItem item in creature.Inventory.Items)
                    {
                        item.Destroy();
                    }
                }
                trash.Destroy();
            }
        }

        public static void CleanupPlaceables(this AreaEvents.OnExit obj)
        {
            foreach (var item in obj.Area.FindObjectsOfTypeInArea<NwPlaceable>().Where(i => i.HasInventory))
            {
                while (item.Inventory.Items.Any())
                {
                    item.Destroy();
                }
            }
        }
    }
}