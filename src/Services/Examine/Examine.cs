using NWN.API;
using NWN.API.Constants;
using NWN.Services;

using NWNX.API.Events;
using NWNX.Services;

namespace Services.Examine
{
    [ServiceBinding(typeof(ExamineObject))]
    public class ExamineObject
    {
        public ExamineObject(NWNXEventService nWNX) => nWNX.Subscribe<ExamineEvents.OnExamineObjectBefore>(OnExamineObjectBefore);

        public static void OnExamineObjectBefore(ExamineEvents.OnExamineObjectBefore onExamineObject)
        {
            if (onExamineObject.Examinee is NwCreature creature && onExamineObject.Examiner.IsReactionTypeHostile(creature) && onExamineObject.Examiner.GetSkillRank(Skill.Lore) > creature.Level + 3 + (creature.Level / 8))
            {
                creature.Description = PrintCRValue(creature);
            }
        }

        private static string PrintCRValue(NwCreature npc) => $"CR Value: {npc.ChallengeRating}\n\nSTR: {npc.GetAbilityScore(Ability.Strength)}\nDEX: {npc.GetAbilityScore(Ability.Dexterity)}\nCON: {npc.GetAbilityScore(Ability.Constitution)}\nINT: {npc.GetAbilityScore(Ability.Intelligence)}\nWIS: {npc.GetAbilityScore(Ability.Wisdom)}\nCHA: {npc.GetAbilityScore(Ability.Charisma)}\nAC: {npc.AC}\nHP: {npc.HP}\nBAB: {npc.BaseAttackBonus}\nFortitude: {npc.GetBaseSavingThrow(SavingThrow.Fortitude)}\nReflex: {npc.GetBaseSavingThrow(SavingThrow.Reflex)}\nWill: {npc.GetBaseSavingThrow(SavingThrow.Will)}\nSR: {npc.SpellResistance}\n\n{npc.OriginalDescription}";
    }
}