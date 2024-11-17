using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public static class AbilityInstructionFactory
    {
        public static AbilityInstruction_RegisterDamage RegisterDamage_MaxHP(float damageMultiplier, float toughnessReduction)
        {
            return new AbilityInstruction_RegisterDamage
            {
                DamageMultiplier = damageMultiplier,
                ToughnessReduction = toughnessReduction,
                AttributeReference = AbilityAttributeReference.MaxHP,
            };
        }

        public static AbilityInstruction_RegisterDamage RegisterDamage_ATK(float damageMultiplier, float toughnessReduction)
        {
            return new AbilityInstruction_RegisterDamage
            {
                DamageMultiplier = damageMultiplier,
                ToughnessReduction = toughnessReduction,
                AttributeReference = AbilityAttributeReference.ATK,
            };
        }

        public static AbilityInstruction_RegenerateEnergy RegenerateEnergy(float regenerateValue)
        {
            return new AbilityInstruction_RegenerateEnergy
            {
                RegenerateValue = regenerateValue,
            };
        }

        public static AbilityInstruction_AddModifier AddModifier(int modifierID, int duration, bool stackable)
        {
            return new AbilityInstruction_AddModifier
            {
                ModifierID = modifierID,
                DurationTurn = duration,
                Stackable = stackable,
            };
        }

        public static AbilityInstruction_AddModifier AddModifier_2Duration_NotStackable(int modifierID)
        {
            return AddModifier(modifierID, 2, false);
        }

        public static AbilityInstruction_AddModifier AddModifier_3Duration_NotStackable(int modifierID)
        {
            return AddModifier(modifierID, 3, false);
        }

        public static AbilityInstruction_AddModifier AddModifier_3Duration_Stackable(int modifierID)
        {
            return AddModifier(modifierID, 3, true);
        }

        public static AbilityInstruction_SetTargets SetTargets(AbilityTargetWhos targetWhos)
        {
            return new AbilityInstruction_SetTargets
            {
                TargetWhos = targetWhos,
            };
        }

        public static AbilityInstruction_ClearTargets ClearTargets(AbilityTargetWhos targetWhos)
        {
            return new AbilityInstruction_ClearTargets();
        }

        public static AbilityInstruction_ModifyDamageInfo ModifyDamageInfo(float value)
        {
            return new AbilityInstruction_ModifyDamageInfo
            {
                Value = value,
            };
        }

        public static AbilityInstruction_ModifyBaseStats ModifyBaseStats(BattleCharacterBaseStatsType whichAbility
            , float value
            , bool flat)
        {
            return new AbilityInstruction_ModifyBaseStats
            {
                WhichAbility = whichAbility,
                Value = value,
                Flat = flat
            };
        }

        public static AbilityInstruction_ModifyBaseStats ModifyBaseStats_SPD_Flat(int value)
        {
            return new AbilityInstruction_ModifyBaseStats
            {
                WhichAbility = BattleCharacterBaseStatsType.SPD,
                Value = value,
                Flat = true
            };
        }

        public static AbilityInstruction_ModifyBaseStats ModifyBaseStats_SPD_Percentage(float value)
        {
            return new AbilityInstruction_ModifyBaseStats
            {
                WhichAbility = BattleCharacterBaseStatsType.SPD,
                Value = value,
                Flat = false
            };
        }

        public static AbilityInstruction_ModifyBaseStats ModifyBaseStats_ATK_Flat(int value)
        {
            return new AbilityInstruction_ModifyBaseStats
            {
                WhichAbility = BattleCharacterBaseStatsType.ATK,
                Value = value,
                Flat = true
            };
        }

        public static AbilityInstruction_ModifyBaseStats ModifyBaseStats_ATK_Percentage(float value)
        {
            return new AbilityInstruction_ModifyBaseStats
            {
                WhichAbility = BattleCharacterBaseStatsType.ATK,
                Value = value,
                Flat = false
            };
        }

        public static AbilityInstruction_SaveTargetInfos SaveTargetInfos()
        {
            return new AbilityInstruction_SaveTargetInfos();
        }

        public static AbilityInstruction_LoadTargetInfos LoadTargetInfos()
        {
            return new AbilityInstruction_LoadTargetInfos();
        }

        public static AbilityInstruction_ModifyTargetsToModifierOwner ModifyDamageInfoTargetsToModifierOwner()
        {
            return new AbilityInstruction_ModifyTargetsToModifierOwner();
        }

        public static AbilityInstruction_ExecuteDamage ExecuteDamage()
        {
            return new AbilityInstruction_ExecuteDamage 
            {
                InvokeEvent = true
            };
        }

        public static AbilityInstruction_ExecuteDamage ExecuteDamage_DontInvokeEvent()
        {
            return new AbilityInstruction_ExecuteDamage
            {
                InvokeEvent = false
            };
        }
    }
}
