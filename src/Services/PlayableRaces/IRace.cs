using System;
using System.Collections.Generic;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.API;

namespace Services.PlayableRaces
{
    public interface IRace
    {
        int ModifyStrength { get; set; }
        int ModifyDexterity { get; set; }
        int ModifyConstitution { get; set; }
        int ModifyIntelligence { get; set; }
        int ModifyWisdom { get; set; }
        int ModifyCharisma { get; set; }
        bool IsUndead { get; set; }
        int MaxLevel { get; set; }
        int SR { get; set; }
        string? HideResRef { get; set; }
        string? WeaponLeft { get; set; }
        string? WeaponRight { get; set; }
        List<EffectType>? Effects { get; set; }
        List<Feat>? FeatList { get; set; }
        List<Alignment>? AlignmentsAllowed { get; set; }
        List<ClassType>? FavoredClasses { get; set; }

        public static void ApplyUndead(IRace race)
        {

        }

        public static void ApplyItems(IRace race, ModuleEvents.OnClientEnter obj)
        {
            if (!String.IsNullOrEmpty(race.HideResRef))
            {
                if (obj.Player.GetItemInSlot(InventorySlot.CreatureSkin).IsValid)
                {
                    obj.Player.GetItemInSlot(InventorySlot.CreatureSkin).Destroy();
                }
                obj.Player.ActionEquipItem(NwItem.Create(race.HideResRef), InventorySlot.CreatureSkin);
            }
            if (!String.IsNullOrEmpty(race.WeaponLeft))
            {

            }
            if (!String.IsNullOrEmpty(race.WeaponRight))
            {

            }
        }
    }
}