using System.Text;

using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Module
{
    [ServiceBinding(typeof(ModuleItemAcquire))]

    public class ModuleItemAcquire
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ModuleItemAcquire(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnAcquireItem>(NwModule.Instance, OnAcquireItem);

        private static void OnAcquireItem(ModuleEvents.OnAcquireItem acquireItem)
        {
            PrintGPValueOnItem(acquireItem);
        }

        private static void PrintGPValueOnItem(ModuleEvents.OnAcquireItem acquireItem)
        {
            if (!acquireItem.AcquiredFrom.PlotFlag)
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append($"Gold Piece Value:{acquireItem.Item.GoldValue}");
                stringBuilder.Append("\n\n");
                stringBuilder.Append(acquireItem.Item.Description);
                acquireItem.Item.Description = stringBuilder.ToString();
            }
        }
    }
}