using System;
using System.Collections.Generic;
using NWN.API;
using NWN.API.Constants;
using NWN.Services;
using NLog;

namespace Services.Cooldown
{
    public class Knockdown
    {
        private double limit = 6.0;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private Dictionary<Guid, DateTime> LastUsed { get; set; }

        public Knockdown(EventService eventService)
        {
            Log.Info($"{eventService.ToString()} triggered.");
            LastUsed = new Dictionary<Guid, DateTime>();
            Log.Info($"LastUsed == {LastUsed}");

            NwModule.Instance.OnUseFeat += onFeatUse =>
            {
                Log.Info($"onFeatUse == {onFeatUse.ToString()}");
                DateTime now = DateTime.UtcNow;
                Log.Info($"now == {now}");

                if (onFeatUse.Feat == Feat.Knockdown && onFeatUse.Creature is NwPlayer player)
                {
                    Log.Info($"onFeatUse.Feat == {onFeatUse.Feat}");

                    if (LastUsed.TryGetValue(player.UUID, out var lastUsed))
                    {
                        Log.Info($"lastUsed == {lastUsed}");

                        if ((DateTime.Now - lastUsed).TotalSeconds <= limit)
                        {
                            Log.Info($"limit == {limit}");
                            onFeatUse.PreventFeatUse = true;
                            Log.Info($"onFeatUse.PreventFeatUse == {onFeatUse.PreventFeatUse}");

                        }
                    }
                    else
                    {
                        Log.Info($"limit == {limit}");
                        LastUsed[player.UUID] = DateTime.UtcNow;
                        Log.Info($"LastUsed[player.UUID] == {LastUsed[player.UUID]}");
                    }
                }
            };
        }
    }
}