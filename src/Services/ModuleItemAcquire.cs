using System.Linq;
using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NWN.API.Constants;
using NWN.API.Effect;

namespace Module
{
    [ServiceBinding(typeof(ModuleItemAcquire))]

    public class ModuleItemAcquire
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ModuleItemAcquire(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnAcquireItem>(NwModule.Instance, OnAcquireItem);

        private static void OnAcquireItem(ModuleEvents.OnAcquireItem acquireItem)
        {
            PrintGPValueOnItem(acquireItem);
            RemoveAllItemProperties(acquireItem);
        }

        private static void PrintGPValueOnItem(ModuleEvents.OnAcquireItem acquireItem)
        {
            if (!acquireItem.Item.PlotFlag)
            {
                acquireItem.Item.Description = $"{"Gold Piece Value:".ColorString(new Color(255, 255, 0))}{acquireItem.Item.GoldValue.ToString().ColorString(new Color(255, 165, 0))}\n\n{acquireItem.Item.OriginalDescription}";
            }
        }

        private static void RemoveAllItemProperties(ModuleEvents.OnAcquireItem acquireItem)
        {

        }
    }
}