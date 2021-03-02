using System;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

using NWNX.API.Events;
using NWNX.Services;


namespace Services.PlayableRaces
{
    [ServiceBinding(typeof(Engine))]
    public class Engine
    {
        public Engine(NativeEventService nativeEventService, NWNXEventService nWNX)
        {
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, ClientEnter);
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnPlayerRespawn>(NwModule.Instance, PlayerRespawn);
            nWNX.Subscribe<LevelEvents.OnLevelUpBefore>(LevelUpBefore);
        }

        private void LevelUpBefore(LevelEvents.OnLevelUpBefore obj)
        {
            throw new NotImplementedException();
        }

        private void PlayerRespawn(ModuleEvents.OnPlayerRespawn obj)
        {
            throw new NotImplementedException();
        }

        private void ClientEnter(ModuleEvents.OnClientEnter obj)
        {
            if (obj.Player.SubraceValid())
            {
                if (obj.Player.Xp == 0)
                {
                    switch (obj.Player.RacialType)
                    {
                        case RacialType.Dwarf:
                            var dwarf = new Dwarf(obj);

                            //Apply Ability Bonus);

                            if (dwarf.IsUndead)
                            {
                                dwarf.ApplyUndead(obj);
                            }

                            //ApplyItems(dwarf, obj);

                            obj.Player.SetColor(ColorChannel.Hair, dwarf.Hair);
                            obj.Player.SetColor(ColorChannel.Skin, dwarf.Skin);

                            //Add Feats
                            if (dwarf.FeatList?.Count > 0)
                            {
                                foreach (var feat in dwarf.FeatList)
                                {
                                    obj.Player.AddFeat(feat);
                                }
                            }
                            break;
                    }
                }
                else
                {
                    //Reapply Subrace
                }
            }
            else
            {
                obj.Player.SendServerMessage($"{"ERROR".ColorString(Color.RED)}!!! - INVALID SUBRACE NAME.");
            }
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
                if (obj.Player.GetItemInSlot(InventorySlot.CreatureLeftWeapon).IsValid)
                {
                    obj.Player.GetItemInSlot(InventorySlot.CreatureLeftWeapon).Destroy();
                }
                obj.Player.ActionEquipItem(NwItem.Create(race.WeaponLeft), InventorySlot.CreatureLeftWeapon);
            }

            if (!String.IsNullOrEmpty(race.WeaponRight))
            {
                if (obj.Player.GetItemInSlot(InventorySlot.CreatureRightWeapon).IsValid)
                {
                    obj.Player.GetItemInSlot(InventorySlot.CreatureRightWeapon).Destroy();
                }
                obj.Player.ActionEquipItem(NwItem.Create(race.WeaponRight), InventorySlot.CreatureRightWeapon);
            }
        }
    }
}
