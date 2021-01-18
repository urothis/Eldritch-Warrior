using System;
using System.Globalization;
using System.IO;
using System.Linq;

using JNogueira.Discord.Webhook.Client;

using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;


namespace Module
{
    [ServiceBinding(typeof(ModuleLoad))]
    public class ModuleLoad

    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static readonly int hours = 24;

        public ModuleLoad(NativeEventService nativeEventService, SchedulerService schedulerService)
        {
            schedulerService.ScheduleRepeating(ServerMessageEveryHour, TimeSpan.FromHours(1));
            schedulerService.Schedule(ServerMessage1439, TimeSpan.FromMinutes(1439));
            schedulerService.Schedule(ModuleLoadDiscord, TimeSpan.FromSeconds(1));

            nativeEventService.Subscribe<NwModule, ModuleEvents.OnModuleLoad>(NwModule.Instance, OnModuleLoad);
        }

        private void OnModuleLoad(ModuleEvents.OnModuleLoad eventInfo)
        {
            /* Print to console when we boot*/
            Console.WriteLine($"SERVER LOADED:{DateTime.Now.ToString(@"yyyy/MM/dd hh:mm:ss tt", new CultureInfo("en-US"))}");

            /* NWNX */
            Administration.GameOptions.RestoreSpellUses = true;

            /* Set Fog Color an Amount in all outdoor areas */
            SetAreaEnviroment();
        }

        private static async void ModuleLoadDiscord()
        {
            /*
                https://github.com/jlnpinheiro/discord-webhook-client
                Create an instance of the class DiscordWebhookClient with your Discord webhook URL.
            */
            Log.Info("START");
            DiscordWebhookClient client = new("url_goes_here");

            // Create your DiscordMessage with all parameters of your message.
            DiscordMessage message = new(
                "Discord Webhook Client sent this message! " + DiscordEmoji.Grinning,
                username: "Username",
                avatarUrl: "https://avatars3.githubusercontent.com/u/24236993?s=460&v=4",
                tts: false,
                embeds: new[]
                {
                    new DiscordMessageEmbed(
                        "Embed title " + DiscordEmoji.Thumbsup,
                        color: 0,
                        author: new DiscordMessageEmbedAuthor("Embed 1 author name"),
                        url: "https://github.com/jlnpinheiro/discord-webhook-client/",
                        description: "This is a embed description.",
                        fields: new[]
                        {
                            new DiscordMessageEmbedField("Field 1 name", "Field 1 value"),
                            new DiscordMessageEmbedField("Field 2 name", "Field 2 value")
                        },
                        thumbnail: new DiscordMessageEmbedThumbnail("https://avatars3.githubusercontent.com/u/24236993?s=460&v=4"),
                        image: new DiscordMessageEmbedImage("https://avatars3.githubusercontent.com/u/24236993?s=460&v=4"),
                        footer: new DiscordMessageEmbedFooter("This is a embed footer text", "https://avatars3.githubusercontent.com/u/24236993?s=460&v=4")
                    )
                }
            );
            Log.Info("BEFORE");
            // Send the message!
            await client.SendToDiscord(message);
            //Log.Info("END");
        }

        private static async void ServerMessage1439() => await NwModule.Instance.SpeakString($"Server reset in {"1".ColorString(Color.WHITE)} minute.".ColorString(Color.ROSE), TalkVolume.Shout);

        private static async void ServerMessageEveryHour()
        {
            switch (hours)
            {
                case >= 2:
                    await NwModule.Instance.SpeakString($"Server reset in {"hours".ColorString(Color.WHITE)}".ColorString(Color.ROSE), TalkVolume.Shout);
                    break;
                case 1:
                    await NwModule.Instance.SpeakString($"Server reset in {"hour".ColorString(Color.WHITE)}".ColorString(Color.ROSE), TalkVolume.Shout);
                    break;
                default:
                    Console.WriteLine($"Server reset.");
                    Administration.ShutdownServer();
                    break;
            }
        }

        private static void SetAreaEnviroment()
        {
            // Instantiate random number generator using system-supplied value as seed.
            Random random = new();

            /* Iterate all areas in module */
            foreach (NwArea area in NwModule.Instance.Areas.Where(area => !area.IsInterior))
            {
                area.SetFogColor(FogType.All, AreaSetFogColor(random));
                area.SetFogAmount(FogType.All, random.Next(1, 12));
                area.SkyBox = (Skybox)random.Next(Enum.GetNames(typeof(Skybox)).Length);
                area.Weather = (WeatherType)random.Next(Enum.GetNames(typeof(Skybox)).Length);
                AreaSkyBoxStormIcy(area);
            }
        }

        private static void AreaSkyBoxStormIcy(NwArea area)
        {
            if (area.SkyBox == Skybox.GrassStorm)
            {
                area.Weather = WeatherType.Rain;
            }

            if (area.SkyBox == Skybox.Icy)
            {
                area.Weather = WeatherType.Snow;
            }
        }

        private static FogColor AreaSetFogColor(Random random)
        {
            var values = Enum.GetValues(typeof(FogColor));
            FogColor fogColor = (FogColor)values.GetValue(random.Next(values.Length))!;
            return fogColor;
        }
    }
}
