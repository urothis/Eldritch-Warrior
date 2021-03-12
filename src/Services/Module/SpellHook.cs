using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(SpellHook))]
    public class SpellHook
    {
        public SpellHook(ScriptEventService scriptEventService) => scriptEventService.SetHandler<SpellEvents.OnSpellCast>("spellhook", OnSpellHooked);

        public static void OnSpellHooked(SpellEvents.OnSpellCast spellCast)
        {
            if (spellCast.Caster is NwPlayer player)
            {
                Spell spell = spellCast.Spell;

                ReplenishCantrips(spellCast);

                if (player.Area.GetLocalVariable<int>("NO_CASTING").Value == 1 && spellCast.Harmful)
                {
                    //SetModuleOverrideSpellScriptFinished
                    player.SendServerMessage($"{"NO".ColorString(Color.RED)} {"offensive spellcasting".ColorString(Color.ORANGE)} in this area.");
                }

                BuffPets(spellCast, player, spell);
            }
        }

        private static void BuffPets(SpellEvents.OnSpellCast spellCast, NwPlayer player, Spell spell)
        {
            if (spellCast.TargetObject == player)
            {
                //Buff pets with same spell applied to caster.
                MetaMagic meta = spellCast.MetaMagicFeat;
                var path = ProjectilePathType.Default; ;

                switch (player.AssociateType)
                {
                    case AssociateType.AnimalCompanion: player.ActionCastSpellAt(spell, player.GetAssociate(AssociateType.AnimalCompanion), meta, true, 0, path, true); break;
                    case AssociateType.Dominated: player.ActionCastSpellAt(spell, player.GetAssociate(AssociateType.Dominated), meta, true, 0, path, true); break;
                    case AssociateType.Familiar: player.ActionCastSpellAt(spell, player.GetAssociate(AssociateType.Familiar), meta, true, 0, path, true); break;
                    case AssociateType.Henchman: player.ActionCastSpellAt(spell, player.GetAssociate(AssociateType.Henchman), meta, true, 0, path, true); break;
                    case AssociateType.Summoned: player.ActionCastSpellAt(spell, player.GetAssociate(AssociateType.Summoned), meta, true, 0, path, true); break;
                }
            }
        }

        private static void ReplenishCantrips(SpellEvents.OnSpellCast spellCast)
        {
            switch (spellCast.Spell)
            {
                case Spell.AcidSplash:
                case Spell.CureMinorWounds:
                case Spell.Daze:
                case Spell.ElectricJolt:
                case Spell.Flare:
                case Spell.InflictMinorWounds:
                case Spell.Light:
                case Spell.RayOfFrost:
                case Spell.Resistance:
                case Spell.Virtue: //NWNX_Creature_RestoreSpells
                    break;
            }
        }
    }
}