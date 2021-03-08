using System;
using System.Collections.Generic;
using NLog;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;


namespace Services.PlayableRaces
{
    public class Gnome : IRace
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public bool IsUndead { get; set; }
        public int ModifyStrength { get; set; }
        public int ModifyDexterity { get; set; }
        public int ModifyConstitution { get; set; }
        public int ModifyIntelligence { get; set; }
        public int ModifyWisdom { get; set; }
        public int ModifyCharisma { get; set; }
        public int? Hair { get; set; }
        public int? Skin { get; set; }
        public int MaxLevel { get; set; }
        public int SR { get; set; }
        public string? PortraitID { get; set; }
        public int? SoundSet { get; set; }
        public string? HideResRef { get; set; }
        public string? WeaponLeft { get; set; }
        public string? WeaponRight { get; set; }
        public CreaturePart? Head { get; set; }
        public CreatureSize? ChangeSize { get; set; }
        public CreatureTailType? Tail { get; set; }
        public CreatureWingType? Wing { get; set; }
        public MovementRate? MoveRate { get; set; }
        public RacialType? Race { get; set; }
        public List<Alignment>? AlignmentsAllowed { get; set; }
        public AppearanceType? Appearance { get; set; }
        public List<ClassType>? FavoredClasses { get; set; }
        public List<Effect>? Effects { get; set; }
        public List<Feat>? FeatList { get; set; }


        public Gnome(ModuleEvents.OnClientEnter obj)
        {
            switch (obj.Player.SubRace)
            {
                case "Deep Gnome": DeepGnome(obj); break;
            }
        }

        private void DeepGnome(ModuleEvents.OnClientEnter obj)
        {
            Appearance = obj.Player.Gender.Equals(Gender.Female) ? Appearance = (AppearanceType?)424 : (AppearanceType?)423;
            MaxLevel = 57;
            FavoredClasses?.Add(ClassType.Wizard);
            ModifyStrength = -2;
            ModifyDexterity = 2;
            ModifyWisdom = 2;
            ModifyCharisma = -4;
            SR = 11 + obj.Player.Level;
            PortraitID = obj.Player.Gender.Equals(Gender.Female) ? PortraitID = "po_gn_f_03_" : "po_gn_m_02_";
        }

        public void ApplyUndead(ModuleEvents.OnClientEnter obj)
        {
            if (IsUndead)
            {

            }
        }

        public void ApplyAppearance(ModuleEvents.OnClientEnter obj)
        {
            if (Hair is not null)
            {
                obj.Player.SetColor(ColorChannel.Hair, (int)Hair);
                Log.Info("Hair");
            }

            if (Skin is not null)
            {
                obj.Player.SetColor(ColorChannel.Hair, (int)Skin);
                Log.Info("Skin");
            }

            if (Head is not null)
            {
                obj.Player.SetCreatureBodyPart(CreaturePart.Head, (CreatureModelType)Head);
                Log.Info("Head");
            }

            if (PortraitID is not null)
            {
                obj.Player.PortraitResRef = PortraitID;
                Log.Info("PortraitID");

            }

            if (SoundSet is not null)
            {
                //NWNX_Creature_SetSoundset
                Log.Info("SoundSet");

            }

            if (ChangeSize is not null)
            {
                //NWNX_Creature_SetSize
                Log.Info("ChangeSize");

            }

            if (Tail is not null)
            {
                obj.Player.TailType = (CreatureTailType)Tail;
            }

            if (Wing is not null)
            {
                obj.Player.WingType = (CreatureWingType)Wing;
            }

            if (MoveRate is not null)
            {
                //NWNX_Creature_SetMovementRate
            }

            if (Race is not null)
            {
                //NWNX_Creature_SetRacialType
            }

            if (Appearance is not null)
            {
                obj.Player.CreatureAppearanceType = (AppearanceType)Appearance;
            }
        }

        public void ApplyItems(ModuleEvents.OnClientEnter obj)
        {
            if (!String.IsNullOrEmpty(HideResRef))
            {
                if (obj.Player.GetItemInSlot(InventorySlot.CreatureSkin).IsValid)
                {
                    obj.Player.GetItemInSlot(InventorySlot.CreatureSkin).Destroy();
                }

                obj.Player.ActionEquipItem(NwItem.Create(HideResRef), InventorySlot.CreatureSkin);
            }

            if (!String.IsNullOrEmpty(WeaponLeft))
            {
                if (obj.Player.GetItemInSlot(InventorySlot.CreatureLeftWeapon).IsValid)
                {
                    obj.Player.GetItemInSlot(InventorySlot.CreatureLeftWeapon).Destroy();
                }

                obj.Player.ActionEquipItem(NwItem.Create(WeaponLeft), InventorySlot.CreatureLeftWeapon);
            }

            if (!String.IsNullOrEmpty(WeaponRight))
            {
                if (obj.Player.GetItemInSlot(InventorySlot.CreatureRightWeapon).IsValid)
                {
                    obj.Player.GetItemInSlot(InventorySlot.CreatureRightWeapon).Destroy();
                }

                obj.Player.ActionEquipItem(NwItem.Create(WeaponRight), InventorySlot.CreatureRightWeapon);
            }
        }

        public void ApplyFeats(ModuleEvents.OnClientEnter obj)
        {
            if (FeatList?.Count < 1) return;

            foreach (var feat in FeatList!)
            {
                obj.Player.AddFeat(feat);
                Log.Info($"{feat} FeatList");
            }
        }

        public void ApplyEffects(ModuleEvents.OnClientEnter obj)
        {
            if (Effects?.Count < 1) return;

            foreach (var effect in Effects!)
            {
                obj.Player.ApplyEffect(EffectDuration.Permanent, effect);
            }
        }

        public void ApplyPlayableRace(ModuleEvents.OnClientEnter obj)
        {
            //SetStats
            ApplyUndead(obj);
            ApplyAppearance(obj);
            ApplyItems(obj);
            ApplyFeats(obj);
            ApplyEffects(obj);
        }
    }
}