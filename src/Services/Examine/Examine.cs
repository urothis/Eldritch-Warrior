//using NLog;

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
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ExamineObject(NWNXEventService nWNX) => nWNX.Subscribe<ExamineEvents.OnExamineObjectBefore>(OnExamineObjectBefore);

        public static void OnExamineObjectBefore(ExamineEvents.OnExamineObjectBefore onExamineObject)
        {
            if (onExamineObject.Examiner is not NwPlayer || !onExamineObject.Examiner.IsDM) return;

            if (onExamineObject.Examiner.IsReactionTypeHostile((NwCreature)onExamineObject.Examinee))
            {
                onExamineObject.Examinee.Description = PrintCRValue((NwCreature)onExamineObject.Examinee);
            }
        }

        private static string PrintCRValue(NwCreature npc)
        {
            string description = npc.OriginalDescription;
            return $"CR Value: {npc.ChallengeRating}\n\nSTR: {npc.GetAbilityScore(Ability.Strength)}\nDEX: {npc.GetAbilityScore(Ability.Dexterity)}\nCON: {npc.GetAbilityScore(Ability.Constitution)}\nINT: {npc.GetAbilityScore(Ability.Intelligence)}\nWIS: {npc.GetAbilityScore(Ability.Wisdom)}\nCHA: {npc.GetAbilityScore(Ability.Charisma)}\nAC: {npc.AC}\nHP: {npc.HP}\nBAB: {npc.BaseAttackBonus}\nFortitude: {npc.GetBaseSavingThrow(SavingThrow.Fortitude)}\nReflex: {npc.GetBaseSavingThrow(SavingThrow.Reflex)}\nWill: {npc.GetBaseSavingThrow(SavingThrow.Will)}\nSR: {npc.SpellResistance}\n\n{description}";
        }
    }
}