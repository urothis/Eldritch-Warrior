using System;
using System.Collections.Generic;
using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;

namespace Services.PlayableRaces
{
    public class Dwarf : IRace
    {
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
        public int? PortraitID { get; set; }
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


        public Dwarf(ModuleEvents.OnClientEnter obj)
        {
            switch (obj.Player.SubRace)
            {
                case "Artic Dwarf": DwarfArtic(); break;
            }
        }

        private void DwarfArtic()
        {
            ModifyStrength = 4;
            ModifyDexterity = -2;
            ModifyConstitution = 2;
            ModifyCharisma = -2;
            MaxLevel = 58;
            HideResRef = "";
            Hair = 62;
            Skin = 27;
            FeatList?.Add(Feat.ImprovedUnarmedStrike);
            FavoredClasses?.Add(ClassType.Ranger);
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
            }

            if (Skin is not null)
            {
                obj.Player.SetColor(ColorChannel.Hair, (int)Skin);
            }

            if (Head is not null)
            {
                obj.Player.SetCreatureBodyPart(CreaturePart.Head, (CreatureModelType)Head);
            }

            if (PortraitID is not null)
            {
                obj.Player.PortraitId = (int)PortraitID;
            }

            if (SoundSet is not null)
            {
                //NWNX_Creature_SetSoundset
            }

            if (ChangeSize is not null)
            {
                //NWNX_Creature_SetSize
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
    }
}