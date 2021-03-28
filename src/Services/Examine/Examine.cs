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
        private readonly EventService eventService;
        public ExamineObject(EventService eventService) => this.eventService = eventService;

        public void Examiner(NwPlayer player) => eventService.Subscribe<ExamineEvents.OnExamineObjectBefore, NWNXEventFactory>(player, onExamine)
                .Register<ExamineEvents.OnExamineObjectBefore>();

        public static void onExamine(ExamineEvents.OnExamineObjectBefore onExamineObject)
        {
            if (onExamineObject.Examinee is NwCreature creature && onExamineObject.Examiner.IsReactionTypeHostile(creature) && onExamineObject.Examiner.GetSkillRank(Skill.Lore) > creature.Level)
            {
                creature.Description = PrintCRValue(creature);
            }
        }

        private static string PrintCRValue(NwCreature npc) => $"CR Value: {npc.ChallengeRating}\n\nSTR: {npc.GetAbilityScore(Ability.Strength)}\nDEX: {npc.GetAbilityScore(Ability.Dexterity)}\nCON: {npc.GetAbilityScore(Ability.Constitution)}\nINT: {npc.GetAbilityScore(Ability.Intelligence)}\nWIS: {npc.GetAbilityScore(Ability.Wisdom)}\nCHA: {npc.GetAbilityScore(Ability.Charisma)}\nAC: {npc.AC}\nHP: {npc.HP}\nBAB: {npc.BaseAttackBonus}\nFortitude: {npc.GetBaseSavingThrow(SavingThrow.Fortitude)}\nReflex: {npc.GetBaseSavingThrow(SavingThrow.Reflex)}\nWill: {npc.GetBaseSavingThrow(SavingThrow.Will)}\nSR: {npc.SpellResistance}\n\n{npc.OriginalDescription}";
    }
}