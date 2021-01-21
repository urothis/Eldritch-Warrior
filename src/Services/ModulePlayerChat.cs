using System;
using System.Text;

using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

namespace Module
{
    [ServiceBinding(typeof(ModulePlayerChat))]
    public class ModulePlayerChat
    {
        private static readonly char playerWildcard = '!';
        private static readonly char emoteWildcard = '$';

        private static readonly string notReady = "Feature not implemented.";

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModulePlayerChat(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerChat>(NwModule.Instance, PlayerChat);

        // handle chat commands
        private void PlayerChat(ModuleEvents.OnPlayerChat playerChat) => ChatTools(playerChat);

        private static void ChatTools(ModuleEvents.OnPlayerChat chat)
        {
            if (chat.Message.StartsWith(playerWildcard) || chat.Message.StartsWith(emoteWildcard))
            {
                chat.Message = chat.Message[1..];
                chat.Message = chat.Message.ToLower();
                string[] chatArray = chat.Message.Split(' ');

                switch (chatArray[0])
                {
                    case "roster":
                        Roster(chat);
                        break;
                    case "armbone":
                        SetArmBone(chat);
                        break;
                    case "armskin":
                        SetArmNormal(chat);
                        break;
                    case "head":
                        SetHead(chat, chatArray);
                        break;
                    case "portrait":
                        SetPortrait(chat, chatArray);
                        break;
                    case "voice":
                        SetVoice(chat, chatArray);
                        break;
                    case "skin":
                        SetSkin(chat, chatArray);
                        break;
                    case "hair":
                        SetHair(chat, chatArray);
                        break;
                    case "tattoocolor1":
                        SetTattooColor1(chat, chatArray);
                        break;
                    case "tattoocolor2":
                        SetTattooColor2(chat, chatArray);
                        break;
                    case "tail":
                        SetTail(chat, chatArray);
                        break;
                    case "wings":
                        SetWings(chat, chatArray);
                        break;
                    case "alignment":
                        SetAlignment(chat, chatArray);
                        break;
                    case "resetlevel":
                        ResetLevel(chat, chatArray);
                        break;
                    case "dice":
                        RollDice(chat, chatArray);
                        break;
                    case "status":
                        SetStatus(chat, chatArray);
                        break;
                    case "lfg":
                        NwModule.Instance.SpeakString($"{chat.Sender.Name.ColorString(Color.WHITE)} is looking for a party!", TalkVolume.Shout);
                        break;
                    case "save":
                        chat.Sender.ExportCharacter();
                        chat.Sender.SendServerMessage($"{chat.Sender.GetBicFileName()}.bic saved".ColorString(Color.GREEN));
                        break;
                }
            }
        }

        private static StringBuilder Roster(ModuleEvents.OnPlayerChat chat)
        {
            int playerCount = 0;
            int dmCount = 0;
            StringBuilder stringBuilder = new("Players Online.\n".ColorString(Color.PINK));

            foreach (NwPlayer player in NwModule.Instance.Players)
            {
                if (player.IsDM)
                {
                    dmCount++;
                }
                else
                {
                    playerCount++;
                    stringBuilder.Append($"{chat.Sender.Name.ColorString(Color.PINK)} | {chat.Sender.Area.Name}\n".ColorString(Color.WHITE));
                }
            }

            stringBuilder.Append($"Player Online | {playerCount.ToString().ColorString(Color.WHITE)}\n".ColorString(Color.PINK));
            stringBuilder.Append($"DM Online | {dmCount.ToString().ColorString(Color.WHITE)}\n".ColorString(Color.PINK));
            stringBuilder.Append($"Total Online | {(playerCount + dmCount).ToString().ColorString(Color.WHITE)}\n".ColorString(Color.PINK));

            chat.Sender.SendServerMessage(stringBuilder.ToString());
            return stringBuilder;
        }

        private static void ResetLevel(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            int xp = chat.Sender.Xp;

            if (chatArray[1].Equals("one"))
            {
                int hd = chat.Sender.Level;
                chat.Sender.Xp = (hd * (hd - 1) / 2 * 1000) - 1;
            }
            else if (chatArray[1].Equals("all"))
            {
                chat.Sender.Xp = 0;
            }

            chat.Sender.Xp = xp;
            chat.Sender.SendServerMessage($"{chat.Sender.Name.ColorString(Color.WHITE)} has reset {chatArray[1]} {(chatArray[1].Equals("one") ? "level" : "levels")}.".ColorString(Color.GREEN));
            chat.Sender.ExportCharacter();
        }

        private static void SetStatus(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            System.Collections.Generic.IEnumerable<NwPlayer> server = NwModule.Instance.Players;

            if (chatArray[1].Equals("like"))
            {
                foreach (NwPlayer player in server)
                {
                    player.SetPCReputation(true, chat.Sender);
                }

            }
            else if (chatArray[1].Equals("dislike"))
            {
                foreach (NwPlayer player in server)
                {
                    player.SetPCReputation(false, chat.Sender);
                }
            }
        }

        private static void RollDice(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                Random random = new();
                int dice = random.Next(1, n);
                chat.Message = $" rolled a d{n} and got {dice}.";
            }
        }

        private static int SetPortrait(ModuleEvents.OnPlayerChat chat, string[] chatArray) => int.TryParse(chatArray[1], out int n) ? (chat.Sender.PortraitId = n) : 0;

        private static string SetVoice(ModuleEvents.OnPlayerChat chat, string[] chatArray) => int.TryParse(chatArray[1], out _) ? (chat.Message = notReady) : chat.Message;

        private static void SetSkin(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.SetColor(ColorChannel.Skin, n);
            }
        }

        private static void SetHair(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.SetColor(ColorChannel.Hair, n);
            }
        }

        private static void SetTattooColor1(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.SetColor(ColorChannel.Tattoo1, n);
            }
        }

        private static void SetTattooColor2(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.SetColor(ColorChannel.Tattoo2, n);
            }
        }

        private static void SetArmBone(ModuleEvents.OnPlayerChat chat)
        {
            chat.Sender.SetCreatureBodyPart(CreaturePart.LeftBicep, CreatureModelType.Undead);
            chat.Sender.SetCreatureBodyPart(CreaturePart.LeftForearm, CreatureModelType.Undead);
            chat.Sender.SetCreatureBodyPart(CreaturePart.LeftHand, CreatureModelType.Undead);
        }

        private static void SetArmNormal(ModuleEvents.OnPlayerChat chat)
        {
            chat.Sender.SetCreatureBodyPart(CreaturePart.LeftBicep, CreatureModelType.Skin);
            chat.Sender.SetCreatureBodyPart(CreaturePart.LeftForearm, CreatureModelType.Skin);
            chat.Sender.SetCreatureBodyPart(CreaturePart.LeftHand, CreatureModelType.Skin);
        }

        private static void SetHead(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                chat.Sender.SetCreatureBodyPart(CreaturePart.Head, (CreatureModelType)n);
            }
        }

        private static CreatureTailType SetTail(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (chatArray[1].Equals("bone"))
            {
                return chat.Sender.TailType = CreatureTailType.Bone;
            }
            else if (chatArray[1].Equals("devil"))
            {
                return chat.Sender.TailType = CreatureTailType.Devil;
            }
            return chatArray[1].Equals("lizard")
                ? (chat.Sender.TailType = CreatureTailType.Lizard)
                : (chat.Sender.TailType = CreatureTailType.None);
        }

        private static CreatureWingType SetWings(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (chatArray[1].Equals("angel"))
            {
                return chat.Sender.WingType = CreatureWingType.Angel;
            }
            else if (chatArray[1].Equals("bat"))
            {
                return chat.Sender.WingType = CreatureWingType.Bat;
            }
            else if (chatArray[1].Equals("bird"))
            {
                return chat.Sender.WingType = CreatureWingType.Bird;
            }
            else if (chatArray[1].Equals("butterfly"))
            {
                return chat.Sender.WingType = CreatureWingType.Butterfly;
            }
            else if (chatArray[1].Equals("demon"))
            {
                return chat.Sender.WingType = CreatureWingType.Demon;
            }
            else
            {
                return chatArray[1].Equals("dragon")
                    ? (chat.Sender.WingType = CreatureWingType.Dragon)
                    : (chat.Sender.WingType = CreatureWingType.None);
            }
        }

        private static void SetAlignment(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (chatArray[1].Equals("chaotic"))
            {
                chat.Sender.LawChaosValue = 0;
            }
            else if (chatArray[1].Equals("evil"))
            {
                chat.Sender.GoodEvilValue = 0;
            }
            else if (chatArray[1].Equals("good"))
            {
                chat.Sender.GoodEvilValue = 100;
            }
            else if (chatArray[1].Equals("lawful"))
            {
                chat.Sender.LawChaosValue = 100;
            }
            else if (chatArray[1].Equals("neutral"))
            {
                if (chatArray[2].Equals("1"))
                {
                    chat.Sender.LawChaosValue = 50;
                }
                else if (chatArray[2].Equals("2"))
                {
                    chat.Sender.GoodEvilValue = 50;
                }
            }
        }
    }
}