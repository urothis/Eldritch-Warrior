using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NLog.Fluent;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;

namespace Services.ChatSystem
{
    public static class Extensions
    {
        private static readonly char playerWildcard = '!';
        private static readonly string notReady = "Feature not implemented.";

        public static int SetPortrait(this NwPlayer player, string[] chatArray) => int.TryParse(chatArray[1], out int n) ? (player.PortraitId = n) : 0;
        public static string SetVoice(this ModuleEvents.OnPlayerChat chat, string[] chatArray) => int.TryParse(chatArray[1], out _) ? (notReady) : chat.Message;
        public static bool TriggerChatTools(this ModuleEvents.OnPlayerChat chat) => chat.Message.StartsWith(playerWildcard);

        public static StringBuilder Roster(this NwPlayer player)
        {
            int playerCount = 0;
            int dmCount = 0;
            StringBuilder stringBuilder = new("Players Online.\n".ColorString(Color.PINK));

            foreach (NwPlayer pc in NwModule.Instance.Players)
            {
                if (pc.IsDM)
                {
                    dmCount++;
                }
                else
                {
                    playerCount++;
                    stringBuilder.Append($"{pc.Name.ColorString(Color.PINK)} | {pc.Area.Name}\n".ColorString(Color.WHITE));
                }
            }

            stringBuilder.Append($"Player Online | {playerCount.ToString().ColorString(Color.WHITE)}\n".ColorString(Color.PINK));
            stringBuilder.Append($"DM Online | {dmCount.ToString().ColorString(Color.WHITE)}\n".ColorString(Color.PINK));
            stringBuilder.Append($"Total Online | {(playerCount + dmCount).ToString().ColorString(Color.WHITE)}\n".ColorString(Color.PINK));

            player.SendServerMessage(stringBuilder.ToString());
            return stringBuilder;
        }

        public static void SetArmBone(this NwPlayer player)
        {
            player.SetCreatureBodyPart(CreaturePart.LeftBicep, CreatureModelType.Undead);
            player.SetCreatureBodyPart(CreaturePart.LeftForearm, CreatureModelType.Undead);
            player.SetCreatureBodyPart(CreaturePart.LeftHand, CreatureModelType.Undead);
        }

        public static void SetArmNormal(this NwPlayer player)
        {
            player.SetCreatureBodyPart(CreaturePart.LeftBicep, CreatureModelType.Skin);
            player.SetCreatureBodyPart(CreaturePart.LeftForearm, CreatureModelType.Skin);
            player.SetCreatureBodyPart(CreaturePart.LeftHand, CreatureModelType.Skin);
        }

        public static CreatureTailType SetTail(this NwPlayer player, string[] chatArray)
        {
            return (chatArray[1]) switch
            {
                "bone" => player.TailType = CreatureTailType.Bone,
                "devil" => player.TailType = CreatureTailType.Devil,
                "lizard" => player.TailType = CreatureTailType.Lizard,
                _ => player.TailType = CreatureTailType.None,
            };
        }

        public static CreatureWingType SetWings(this NwPlayer player, string[] chatArray)
        {
            return (chatArray[1]) switch
            {
                "angel" => player.WingType = CreatureWingType.Angel,
                "bat" => player.WingType = CreatureWingType.Bat,
                "bird" => player.WingType = CreatureWingType.Bird,
                "butterfly" => player.WingType = CreatureWingType.Butterfly,
                "demon" => player.WingType = CreatureWingType.Demon,
                "dragon" => player.WingType = CreatureWingType.Dragon,
                _ => player.WingType = CreatureWingType.None,
            };
        }

        public static NwPlayer SetAlignment(this NwPlayer player, string[] chatArray)
        {
            switch (chatArray[1])
            {
                case "chaotic":
                    player.LawChaosValue = 0;
                    break;
                case "evil":
                    player.GoodEvilValue = 0;
                    break;
                case "good":
                    player.GoodEvilValue = 100;
                    break;
                case "lawful":
                    player.LawChaosValue = 100;
                    break;
                case "neutral":
                    if (chatArray[2].Equals("1"))
                    {
                        player.LawChaosValue = 50; break;
                    }
                    else if (chatArray[2].Equals("2"))
                    {
                        player.GoodEvilValue = 50; break;
                    }
                    else
                    {

                    }
                    break;
                default:
                    break;
            }
            return player;
        }

        public static NwItem SetVisual(this NwPlayer player, string[] chatArray)
        {
            if (player.GetItemInSlot(InventorySlot.RightHand).IsValid && player.GetItemInSlot(InventorySlot.RightHand).ItemProperties.Any(x => x.PropertyType == ItemPropertyType.VisualEffect))
            {
                foreach (NWN.API.ItemProperty property in player.GetItemInSlot(InventorySlot.RightHand).ItemProperties.Where<ItemProperty>(x => x.PropertyType == ItemPropertyType.VisualEffect))
                {
                    player.GetItemInSlot(InventorySlot.RightHand).RemoveItemProperty(property);
                }
            }

            switch (chatArray[1])
            {
                case "acid": player.GetItemInSlot(InventorySlot.RightHand).AddItemProperty(NWN.API.ItemProperty.VisualEffect(ItemVisual.Acid), EffectDuration.Permanent); break;
                case "cold": player.GetItemInSlot(InventorySlot.RightHand).AddItemProperty(NWN.API.ItemProperty.VisualEffect(ItemVisual.Cold), EffectDuration.Permanent); break;
                case "electric": player.GetItemInSlot(InventorySlot.RightHand).AddItemProperty(NWN.API.ItemProperty.VisualEffect(ItemVisual.Electrical), EffectDuration.Permanent); break;
                case "evil": player.GetItemInSlot(InventorySlot.RightHand).AddItemProperty(NWN.API.ItemProperty.VisualEffect(ItemVisual.Evil), EffectDuration.Permanent); break;
                case "fire": player.GetItemInSlot(InventorySlot.RightHand).AddItemProperty(NWN.API.ItemProperty.VisualEffect(ItemVisual.Fire), EffectDuration.Permanent); break;
                case "holy": player.GetItemInSlot(InventorySlot.RightHand).AddItemProperty(NWN.API.ItemProperty.VisualEffect(ItemVisual.Holy), EffectDuration.Permanent); break;
                case "sonic": player.GetItemInSlot(InventorySlot.RightHand).AddItemProperty(NWN.API.ItemProperty.VisualEffect(ItemVisual.Sonic), EffectDuration.Permanent); break;
                default: break;
            }

            return player.GetItemInSlot(InventorySlot.RightHand);
        }

        public static void SetEyes(this NwPlayer player, string[] chatArray)
        {
            VfxType eyes = VfxType.EyesCynTroglodyte;
            string color = chatArray[1];

            switch (player.RacialType)
            {
                case RacialType.Dwarf:
                    switch (color)
                    {
                        case "cyan": eyes = player.Gender == Gender.Female ? VfxType.EyesCynDwarfFemale : VfxType.EyesCynDwarfMale; break;
                        case "green": eyes = player.Gender == Gender.Female ? VfxType.EyesGreenDwarfFemale : VfxType.EyesGreenDwarfMale; break;
                        case "orange": eyes = player.Gender == Gender.Female ? VfxType.EyesOrgDwarfFemale : VfxType.EyesOrgDwarfMale; break;
                        case "purple": eyes = player.Gender == Gender.Female ? VfxType.EyesPurDwarfFemale : VfxType.EyesPurDwarfMale; break;
                        case "red": eyes = player.Gender == Gender.Female ? VfxType.EyesRedFlameDwarfFemale : VfxType.EyesRedFlameDwarfMale; break;
                        case "white": eyes = player.Gender == Gender.Female ? VfxType.EyesWhtDwarfFemale : VfxType.EyesWhtDwarfMale; break;
                        case "yellow": eyes = player.Gender == Gender.Female ? VfxType.EyesYelDwarfFemale : VfxType.EyesYelDwarfMale; break;
                    }
                    break;
                case RacialType.Elf:
                    switch (color)
                    {
                        case "cyan": eyes = player.Gender == Gender.Female ? VfxType.EyesCynElfFemale : VfxType.EyesCynElfMale; break;
                        case "green": eyes = player.Gender == Gender.Female ? VfxType.EyesGreenElfFemale : VfxType.EyesGreenElfMale; break;
                        case "orange": eyes = player.Gender == Gender.Female ? VfxType.EyesOrgElfFemale : VfxType.EyesOrgElfMale; break;
                        case "purple": eyes = player.Gender == Gender.Female ? VfxType.EyesPurElfFemale : VfxType.EyesPurElfMale; break;
                        case "red": eyes = player.Gender == Gender.Female ? VfxType.EyesRedFlameElfFemale : VfxType.EyesRedFlameElfMale; break;
                        case "white": eyes = player.Gender == Gender.Female ? VfxType.EyesWhtElfFemale : VfxType.EyesWhtHalflingMale; break;
                        case "yellow": eyes = player.Gender == Gender.Female ? VfxType.EyesYelElfFemale : VfxType.EyesYelElfMale; break;
                        default: break;
                    }
                    break;
                case RacialType.Gnome:
                    switch (color)
                    {
                        case "cyan": eyes = player.Gender == Gender.Female ? VfxType.EyesCynGnomeFemale : VfxType.EyesCynGnomeMale; break;
                        case "green": eyes = player.Gender == Gender.Female ? VfxType.EyesGreenGnomeFemale : VfxType.EyesGreenGnomeMale; break;
                        case "orange": eyes = player.Gender == Gender.Female ? VfxType.EyesOrgGnomeFemale : VfxType.EyesOrgGnomeMale; break;
                        case "purple": eyes = player.Gender == Gender.Female ? VfxType.EyesPurGnomeFemale : VfxType.EyesPurGnomeMale; break;
                        case "red": eyes = player.Gender == Gender.Female ? VfxType.EyesRedFlameGnomeFemale : VfxType.EyesRedFlameGnomeMale; break;
                        case "white": eyes = player.Gender == Gender.Female ? VfxType.EyesWhtGnomeFemale : VfxType.EyesWhtGnomeMale; break;
                        case "yellow": eyes = player.Gender == Gender.Female ? VfxType.EyesYelGnomeFemale : VfxType.EyesYelGnomeMale; break;
                        default: break;
                    }
                    break;
                case RacialType.Halfling:
                    switch (color)
                    {
                        case "cyan": eyes = player.Gender == Gender.Female ? VfxType.EyesCynHalflingFemale : VfxType.EyesCynHalflingMale; ; break;
                        case "green": eyes = player.Gender == Gender.Female ? VfxType.EyesGreenHalflingFemale : VfxType.EyesGreenHalflingMale; break;
                        case "orange": eyes = player.Gender == Gender.Female ? VfxType.EyesOrgHalflingFemale : VfxType.EyesOrgHalflingMale; break;
                        case "purple": eyes = player.Gender == Gender.Female ? VfxType.EyesPurHalflingFemale : VfxType.EyesPurHalflingMale; break;
                        case "red": eyes = player.Gender == Gender.Female ? VfxType.EyesRedFlameHalflingFemale : VfxType.EyesRedFlameHalflingMale; break;
                        case "white": eyes = player.Gender == Gender.Female ? VfxType.EyesWhtHalflingFemale : VfxType.EyesWhtHalflingMale; break;
                        case "yellow": eyes = player.Gender == Gender.Female ? VfxType.EyesYelHalflingFemale : VfxType.EyesYelHalflingMale; break;
                        default: break;
                    }
                    break;
                case RacialType.HalfOrc:
                    switch (color)
                    {
                        case "cyan": eyes = player.Gender == Gender.Female ? VfxType.EyesCynHalforcFemale : VfxType.EyesCynHalforcMale; break;
                        case "green": eyes = player.Gender == Gender.Female ? VfxType.EyesGreenHalforcFemale : VfxType.EyesGreenHalforcMale; break;
                        case "orange": eyes = player.Gender == Gender.Female ? VfxType.EyesOrgHalforcFemale : VfxType.EyesOrgHalforcMale; break;
                        case "purple": eyes = player.Gender == Gender.Female ? VfxType.EyesPurHalforcFemale : VfxType.EyesPurHalforcMale; break;
                        case "red": eyes = player.Gender == Gender.Female ? VfxType.EyesRedFlameHalforcFemale : VfxType.EyesRedFlameHalforcMale; break;
                        case "white": eyes = player.Gender == Gender.Female ? VfxType.EyesWhtHalforcFemale : VfxType.EyesWhtHalforcMale; break;
                        case "yellow": eyes = player.Gender == Gender.Female ? VfxType.EyesYelHalforcFemale : VfxType.EyesYelHalforcMale; break;
                        default: break;
                    }
                    break;
                case RacialType.Human:
                    switch (color)
                    {
                        case "cyan": eyes = player.Gender == Gender.Female ? VfxType.EyesCynHumanFemale : VfxType.EyesCynHumanMale; break;
                        case "green": eyes = player.Gender == Gender.Female ? VfxType.EyesGreenHumanFemale : VfxType.EyesGreenHumanMale; break;
                        case "orange": eyes = player.Gender == Gender.Female ? VfxType.EyesOrgHumanFemale : VfxType.EyesOrgHumanMale; break;
                        case "purple": eyes = player.Gender == Gender.Female ? VfxType.EyesPurHumanFemale : VfxType.EyesPurHumanMale; break;
                        case "red": eyes = player.Gender == Gender.Female ? VfxType.EyesRedFlameHumanFemale : VfxType.EyesRedFlameHumanMale; break;
                        case "white": eyes = player.Gender == Gender.Female ? VfxType.EyesWhtHumanFemale : VfxType.EyesWhtHumanMale; break;
                        case "yellow": eyes = player.Gender == Gender.Female ? VfxType.EyesYelHumanFemale : VfxType.EyesYelHumanMale; break;
                        default: break;
                    }
                    break;
                default:
                    switch (color)
                    {
                        case "cyan": eyes = VfxType.EyesCynTroglodyte; break;
                        case "green": eyes = VfxType.EyesCynTroglodyte; break;
                        case "orange": eyes = VfxType.EyesCynTroglodyte; break;
                        case "purple": eyes = VfxType.EyesCynTroglodyte; break;
                        case "red": eyes = VfxType.EyesCynTroglodyte; break;
                        case "white": eyes = VfxType.EyesCynTroglodyte; break;
                        case "yellow": eyes = VfxType.EyesCynTroglodyte; break;
                        default: break;
                    }
                    break;
            }

            player.ApplyEffect(EffectDuration.Permanent, NWN.API.Effect.VisualEffect(eyes));
        }

        public static void Emote(this NwPlayer player, string[] chatArray)
        {
            if (float.TryParse(chatArray[2].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out float animSpeed))
            {
                switch (chatArray[1])
                {
                    case "bow": player.PlayAnimation(Animation.FireForgetBow, animSpeed); break;
                    case "duck": player.PlayAnimation(Animation.FireForgetDodgeDuck, animSpeed); break;
                    case "dodge": player.PlayAnimation(Animation.FireForgetDodgeDuck, animSpeed); break;
                    case "drink": player.PlayAnimation(Animation.FireForgetDrink, animSpeed); break;
                    case "greet": player.PlayAnimation(Animation.FireForgetGreeting, animSpeed); break;
                    case "left": player.PlayAnimation(Animation.FireForgetHeadTurnLeft, animSpeed); break;
                    case "right": player.PlayAnimation(Animation.FireForgetHeadTurnRight, animSpeed); break;
                    case "bored": player.PlayAnimation(Animation.FireForgetPauseBored, animSpeed); break;
                    case "scratch": player.PlayAnimation(Animation.FireForgetPauseScratchHead, animSpeed); break;
                    case "read": player.PlayAnimation(Animation.FireForgetRead, animSpeed); break;
                    case "saulte": player.PlayAnimation(Animation.FireForgetSalute, animSpeed); break;
                    case "spasm": player.PlayAnimation(Animation.FireForgetSpasm, animSpeed); break;
                    case "steal": player.PlayAnimation(Animation.FireForgetSteal, animSpeed); break;
                    case "taunt": player.PlayAnimation(Animation.FireForgetTaunt, animSpeed); break;
                    case "v1": player.PlayAnimation(Animation.FireForgetVictory1, animSpeed); break;
                    case "v2": player.PlayAnimation(Animation.FireForgetVictory2, animSpeed); break;
                    case "v3": player.PlayAnimation(Animation.FireForgetVictory3, animSpeed); break;
                    case "c1": player.PlayAnimation(Animation.LoopingConjure1, animSpeed); break;
                    case "c2": player.PlayAnimation(Animation.LoopingConjure2, animSpeed); break;
                    case "back": player.PlayAnimation(Animation.LoopingDeadBack, animSpeed); break;
                    case "front": player.PlayAnimation(Animation.LoopingDeadFront, animSpeed); break;
                    case "low": player.PlayAnimation(Animation.LoopingGetLow, animSpeed); break;
                    case "mid": player.PlayAnimation(Animation.LoopingGetMid, animSpeed); break;
                    case "listen": player.PlayAnimation(Animation.LoopingListen, animSpeed); break;
                    case "look": player.PlayAnimation(Animation.LoopingLookFar, animSpeed); break;
                    case "meditate": player.PlayAnimation(Animation.LoopingMeditate, animSpeed); break;
                    case "p1": player.PlayAnimation(Animation.FireForgetPauseBored, animSpeed); break;
                    case "p2": player.PlayAnimation(Animation.FireForgetPauseScratchHead, animSpeed); break;
                    case "drunk": player.PlayAnimation(Animation.LoopingPauseDrunk, animSpeed); break;
                    case "tired": player.PlayAnimation(Animation.LoopingPauseTired, animSpeed); break;
                    case "squat": player.PlayAnimation(Animation.LoopingSitChair, animSpeed); break;
                    case "sit": player.PlayAnimation(Animation.LoopingSitCross, animSpeed); break;
                    case "spasming": player.PlayAnimation(Animation.LoopingSpasm, animSpeed); break;
                    case "forceful": player.PlayAnimation(Animation.LoopingTalkForceful, animSpeed); break;
                    case "lol": player.PlayAnimation(Animation.LoopingTalkLaughing, animSpeed); break;
                    case "normal": player.PlayAnimation(Animation.LoopingTalkNormal, animSpeed); break;
                    case "beg": player.PlayAnimation(Animation.LoopingTalkPleading, animSpeed); break;
                    case "worship": player.PlayAnimation(Animation.LoopingWorship, animSpeed); break;
                    default: break;
                }
            }
        }

        public static int ResetLevel(this NwPlayer player, string[] chatArray)
        {
            int xp = player.Xp;

            if (chatArray[1].Equals("one"))
            {
                int hd = player.Level;
                player.Xp = (hd * (hd - 1) / 2 * 1000) - 1;
            }
            else if (chatArray[1].Equals("all"))
            {
                player.Xp = 0;
            }
            else
            {

            }

            player.SendServerMessage($"{player.Name.ColorString(Color.WHITE)} has reset {chatArray[1]} {(chatArray[1].Equals("one") ? "level" : "levels")}.".ColorString(Color.GREEN));
            player.ExportCharacter();
            return player.Xp = xp;
        }

        public static void SetStatus(this NwPlayer player, string[] chatArray)
        {
            IEnumerable<NwPlayer> server = NwModule.Instance.Players;

            if (chatArray[1].Equals("like"))
            {
                foreach (NwPlayer pc in server)
                {
                    pc.SetPCReputation(true, player);
                }

            }
            else if (chatArray[1].Equals("dislike"))
            {
                foreach (NwPlayer pc in server)
                {
                    player.SetPCReputation(false, pc);
                }
            }
            else
            {

            }
        }

        public static string RollDice(this ModuleEvents.OnPlayerChat chat, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                Random random = new();
                int dice = random.Next(1, n);
                return chat.Message = $" rolled a d{n} and got {dice}.";
            }
            else
            {
                Log.Info(nameof(RollDice));
                return $"ERROR | RollDice{chat}{chatArray[1]}";
            }
        }

        public static void SetSkin(this NwPlayer player, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                player.SetColor(ColorChannel.Skin, n);
            }
        }

        public static void SetHair(this NwPlayer player, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                player.SetColor(ColorChannel.Hair, n);
            }
        }

        public static void SetTattooColor1(this NwPlayer player, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                player.SetColor(ColorChannel.Tattoo1, n);
            }
        }

        public static void SetTattooColor2(this NwPlayer player, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                player.SetColor(ColorChannel.Tattoo2, n);
            }
        }

        public static void SetHead(this NwPlayer player, string[] chatArray)
        {
            if (int.TryParse(chatArray[1], out int n))
            {
                player.SetCreatureBodyPart(CreaturePart.Head, (CreatureModelType)n);
            }
        }
    }
}