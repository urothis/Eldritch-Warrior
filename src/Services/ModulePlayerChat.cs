//using NLog;

using NWN.API;
using NWN.API.Events;
using NWN.Services;
using NWN.API.Constants;

using NWNX.API;
using System;

namespace Module
{
    [ServiceBinding(typeof(ModulePlayerChat))]
    public class ModulePlayerChat
    {
        private static readonly char playerWildcard = '!';
        private static readonly char emoteWildcard = '$';

        private static readonly string notReady = "Feature not implemented.";

        //private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModulePlayerChat(NativeEventService native) =>
            native.Subscribe<NwModule, ModuleEvents.OnPlayerChat>(NwModule.Instance, PlayerChat);

        // handle chat commands
        private void PlayerChat(ModuleEvents.OnPlayerChat playerChat) => ChatTools(playerChat);

        private static void ChatTools(ModuleEvents.OnPlayerChat chat)
        {
            string[] chatArray;

            if (chat.Message.StartsWith(playerWildcard))
            {
                chat.Message = chat.Message[1..];
                chatArray = chat.Message.Split(' ');

                if (chatArray[0].Equals("portrait", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetPortrait(chat, chatArray);
                }
                else if (chatArray[0].Equals("voice", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetVoice(chat, chatArray);
                }
                else if (chatArray[0].Equals("skin", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetSkin(chat, chatArray);
                }
                else if (chatArray[0].Equals("hair", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetHair(chat, chatArray);
                }
                else if (chatArray[0].Equals("tattoocolor1", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetTattooColor1(chat, chatArray);
                }
                else if (chatArray[0].Equals("tattoocolor2", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetTattooColor2(chat, chatArray);
                }
                else if (chatArray[0].Equals("armbone", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetArmBone(chat);
                }
                else if (chatArray[0].Equals("armskin", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetArmNormal(chat);
                }
                else if (chatArray[0].Equals("head", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetHead(chat, chatArray);
                }
                else if (chatArray[0].Equals("tail", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetTail(chat, chatArray);
                }
                else if (chatArray[0].Equals("wings", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetWings(chat, chatArray);
                }
                else if (chatArray[0].Equals("alignment", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetAlignment(chat, chatArray);
                }
            }
            else if (chat.Message.StartsWith(emoteWildcard))
            {
                chat.Message = chat.Message[1..];
                chatArray = chat.Message.Split(' ');

                if (chatArray[0].Equals("dice", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    RollDice(chat, chatArray);
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
            if (chatArray[1].Equals("bone", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return chat.Sender.TailType = CreatureTailType.Bone;
            }
            else if (chatArray[1].Equals("devil", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return chat.Sender.TailType = CreatureTailType.Devil;
            }
            return chatArray[1].Equals("lizard", System.StringComparison.InvariantCultureIgnoreCase)
                ? (chat.Sender.TailType = CreatureTailType.Lizard)
                : (chat.Sender.TailType = CreatureTailType.None);
        }

        private static CreatureWingType SetWings(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (chatArray[1].Equals("angel", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return chat.Sender.WingType = CreatureWingType.Angel;
            }
            else if (chatArray[1].Equals("bat", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return chat.Sender.WingType = CreatureWingType.Bat;
            }
            else if (chatArray[1].Equals("bird", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return chat.Sender.WingType = CreatureWingType.Bird;
            }
            else if (chatArray[1].Equals("butterfly", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return chat.Sender.WingType = CreatureWingType.Butterfly;
            }
            else if (chatArray[1].Equals("demon", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return chat.Sender.WingType = CreatureWingType.Demon;
            }
            else
            {
                return chatArray[1].Equals("dragon", System.StringComparison.InvariantCultureIgnoreCase)
                    ? (chat.Sender.WingType = CreatureWingType.Dragon)
                    : (chat.Sender.WingType = CreatureWingType.None);
            }
        }

        private static void SetAlignment(ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (chatArray[1].Equals("chaotic", System.StringComparison.InvariantCultureIgnoreCase))
            {
                chat.Sender.LawChaosValue = 0;
            }
            else if (chatArray[1].Equals("evil", System.StringComparison.InvariantCultureIgnoreCase))
            {
                chat.Sender.GoodEvilValue = 0;
            }
            else if (chatArray[1].Equals("good", System.StringComparison.InvariantCultureIgnoreCase))
            {
                chat.Sender.GoodEvilValue = 100;
            }
            else if (chatArray[1].Equals("lawful", System.StringComparison.InvariantCultureIgnoreCase))
            {
                chat.Sender.LawChaosValue = 100;
            }
            else if (chatArray[1].Equals("neutral", System.StringComparison.InvariantCultureIgnoreCase))
            {
                if (chatArray[2].Equals("1", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    chat.Sender.LawChaosValue = 50;
                }
                else if (chatArray[2].Equals("2", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    chat.Sender.GoodEvilValue = 50;
                }
            }
        }
    }
}