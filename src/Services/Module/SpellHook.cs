using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

namespace Services.Module
{
    [ServiceBinding(typeof(SpellHook))]
    public class SpellHook
    {
        public SpellHook(ScriptEventService scriptEventService)
        {
            scriptEventService.SetHandler<SpellEvents.OnSpellCast>("spellhook", OnSpellHooked);
        }

        public static void OnSpellHooked(SpellEvents.OnSpellCast spellCast)
        {
            if (spellCast.Caster is not NwPlayer) return;

            ReplenishCantrips(spellCast);

            if (spellCast.Caster.Area.GetLocalVariable<int>("NO_CASTING").Value == 1)
            {

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