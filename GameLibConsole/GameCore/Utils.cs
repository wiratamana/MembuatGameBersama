using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    internal static class Utils
    {
        public static Random Random = new Random();

        public static void DelayTargetAction(BattleTargetInfo info, float delayRate)
        {
            var delay = (int)(info.Target.BattleInfo.ActionValue * delayRate);
            info.SpeedModifyInfo = new BattleSpeedModifyInfo(delay);
        }

        public static void CalculateDamage(ref BattleEventInfo eventInfo
            , AbilityAttributeReference attributeReference
            , float abilityMultiplier)
        {
            // Outgoing DMG = Base DMG * DMG% Multiplier * DEF Multiplier * RES Multiplier * DMG Taken Multiplier * Universal DMG Reduction Multiplier * Weaken Multiplier
            // RES Multiplier = 100% - (RES% - RES PEN%)
            // DMG Taken Multiplier = 100% + Elemental DMG Taken% + All Type DMG Taken%
            // Universal DMG Reduction Multiplier = 100% * (1 - DMG Reduction_1) * (1 - DMG Reduction_2) 
            // Weaken Multiplier = 100% - Weaken%

            ref var attacker = ref eventInfo.ExecutingTurn;

            for (var i = 0; i < eventInfo.TargetInfos.Count; i++)
            {
                var target = eventInfo.TargetInfos[i];

                var isBecomeBrokenInThisTurn = false;
                var toughnessReduction = 10;

                var baseDamage = GetBaseDamage(ref attacker, attributeReference, abilityMultiplier, out bool isCritical);
                var damageMultiplier = GetDamageMultiplier(ref attacker);
                var defMultiplier = GetDefenseMultiplier(ref attacker, ref target.Target);
                var resMultiplier = GetResistanceMultiplier(ref attacker, ref target.Target);
                var vulnerabilityMultiplier = GetVulnerabilityMultiplier();
                var damageMitigationMultiplier = GetDamageMitigationMultiplier();
                var brokenMultiplier = GetBrokenDamageMultiplier(ref target.Target);

                var outgoingDamage = baseDamage * damageMultiplier * defMultiplier * resMultiplier * vulnerabilityMultiplier * damageMitigationMultiplier * brokenMultiplier;
                var isDamageMatchTargetWeaknessElement = IsAttackerDealWeakness(ref attacker, ref target.Target);

                if (isDamageMatchTargetWeaknessElement && target.Target.IsPlayerCharacter == false && target.Target.BattleInfo.Toughness > 0)
                {
                    var toughness = target.Target.BattleInfo.Toughness;
                    isBecomeBrokenInThisTurn = toughness - toughnessReduction <= 0;
                }

                target.DamageInfo = new BattleDamageInfo(attacker
                    , target.Target
                    , (int)Math.Ceiling(outgoingDamage)
                    , isCritical
                    , isDamageMatchTargetWeaknessElement
                    , isBecomeBrokenInThisTurn
                    , toughnessReduction);
            }          
        }

        public static void GetTargets(BattleEventInfo eventInfo
            , AbilityTargetWhos targetWhos)
        {
            var attacker = eventInfo.ExecutingTurn;
            var myCharacters = eventInfo.Players.Where(x => x.BattleInfo.IsDead == false).ToList();
            var enemyCharacters = eventInfo.Enemies.Where(x => x.BattleInfo.IsDead == false).ToList();
            if (attacker.IsEnemyCharacter)
            {
                var temp = myCharacters;
                myCharacters = enemyCharacters;
                enemyCharacters = temp;
            }

            var rndIdx = 0;
            switch (targetWhos)
            {
                case AbilityTargetWhos.None:
                    eventInfo.TargetInfos.Clear();
                    break;

                case AbilityTargetWhos.AllySingle:
                    rndIdx = Random.Next(0, myCharacters.Count);
                    eventInfo.TargetInfos.Add(new BattleTargetInfo() { Target = myCharacters[rndIdx] });
                    break;
                case AbilityTargetWhos.AllyAll:
                    for (int i = 0; i < myCharacters.Count; i++)
                    {
                        eventInfo.TargetInfos.Add(new BattleTargetInfo() { Target = myCharacters[i] });
                    }
                    break;
                case AbilityTargetWhos.AllyAllExcludeSelf:
                    for (int i = 0; i < myCharacters.Count; i++)
                    {
                        if (myCharacters[i] == attacker)
                        {
                            continue;
                        }

                        eventInfo.TargetInfos.Add(new BattleTargetInfo() { Target = myCharacters[i] });
                    }
                    break;
                case AbilityTargetWhos.AllyGroup:
                    break;
                case AbilityTargetWhos.EnemySingle:
                    rndIdx = Random.Next(0, enemyCharacters.Count);
                    eventInfo.TargetInfos.Add(new BattleTargetInfo() { Target = enemyCharacters[rndIdx] });
                    break;
                case AbilityTargetWhos.EnemyAll:
                    for (int i = 0; i < enemyCharacters.Count; i++)
                    {
                        eventInfo.TargetInfos.Add(new BattleTargetInfo() { Target = enemyCharacters[i] });
                    }
                    break;
                case AbilityTargetWhos.EnemyGroup:
                    break;
                case AbilityTargetWhos.Self:
                    eventInfo.TargetInfos.Add(new BattleTargetInfo() { Target = attacker });
                    break;
                case AbilityTargetWhos.Everyone:
                    for (int i = 0; i < myCharacters.Count; i++)
                    {
                        eventInfo.TargetInfos.Add(new BattleTargetInfo() { Target = myCharacters[i] });
                    }

                    for (int i = 0; i < enemyCharacters.Count; i++)
                    {
                        eventInfo.TargetInfos.Add(new BattleTargetInfo() { Target = enemyCharacters[i] });
                    }
                    break;

                case AbilityTargetWhos.Custom:
                    break;
            }
        }

        private static float GetBaseDamage(ref BattleCharacterInfo chr
            , AbilityAttributeReference attributeReference
            , float abilityMultiplier
            , out bool isCritcal)
        {
            // Base DMG = (Ability Multiplier + Extra Multiplier) * Scaling Attribute + Extra DMG
            var extraMultiplier = 0.0f;
            var scalingAttribute = GetAttributeReferenceValue(ref chr, attributeReference);
            var extraDamage = 0.0f;
            var baseDamage = (abilityMultiplier + extraMultiplier) * scalingAttribute + extraDamage;

            var critRateRandomNumber = 1 + Random.Next(0, 100);
            isCritcal = critRateRandomNumber <= chr.AdvanceStats.CRITRate;
            if (isCritcal)
            {
                baseDamage *= chr.AdvanceStats.CRITDMGMultiplier;
            }

            return baseDamage;
        }

        private static float GetDamageMultiplier(ref BattleCharacterInfo chr)
        {
            // DMG% Multiplier = 100% + Elemental DMG% + All Type DMG% + DoT DMG% + Other DMG%
            var elementalDamageMultiplier = GetElementalDamageMultiplier(ref chr);
            var allTypeDamageMultiplier = 0.0f;
            var dotDamageMultiplier = 0.0f;
            var otherDamageMultiplier = 0.0f;
            return 1.0f + elementalDamageMultiplier + allTypeDamageMultiplier + dotDamageMultiplier + otherDamageMultiplier;
        }

        private static float GetDefenseMultiplier(ref BattleCharacterInfo chrAttacker, ref BattleCharacterInfo chrTarget)
        {
            var defMultiplier = 1.0f;

            // DEF% can come from equipments, buffs or some passive skills
            var defBonus = 0.0f;
            var defReduction = 0.0f;
            var defIgnore = 0.0f;

            if (chrAttacker.IsPlayerCharacter)
            {
                //var baseDef = 200 + 10.0f * targetLevel;
                var attackerLevelDefValue = chrAttacker.Level + 20.0f;
                var targetLevelDefValue = chrTarget.Level + 20.0f;

                var denominator = targetLevelDefValue + attackerLevelDefValue;
                defMultiplier = attackerLevelDefValue / denominator;
                defMultiplier = (defMultiplier * ((float)Math.Exp(Math.Pow(defReduction, 1.666f))));
            }
            else
            {
                var baseDef = chrTarget.BaseStats.DEF;
                var defFlat = 0.0f;
                var defValue = baseDef * (1.0f + defBonus - (defReduction + defIgnore)) + defFlat;

                defMultiplier = 1.0f - (defValue / (defValue + 200 + 10 * chrAttacker.Level));
            }

            return defMultiplier;
        }

        private static float GetResistanceMultiplier(ref BattleCharacterInfo chrAttacker, ref BattleCharacterInfo chrTarget)
        {
            var resTaret = GetElementalResistanceMultiplier(ref chrTarget);
            var resPenTaret = GetElementalResistancePenetrationMultiplier(ref chrAttacker);
            var resMultiplier = 1.0f - (resTaret - resPenTaret);
            return resMultiplier;
        }

        private static float GetVulnerabilityMultiplier()
        {
            return 1.0f;
        }

        private static float GetDamageMitigationMultiplier()
        {
            return 1.0f;
        }

        private static float GetBrokenDamageMultiplier(ref BattleCharacterInfo chrTarget)
        {
            var isBroken = chrTarget.IsBroken;

            return isBroken ? Const.DAMAGE_MULTIPLIER_TO_BROKEN_ENEMY : Const.DAMAGE_MULTIPLIER_TO_NON_BROKEN_ENEMY;
        }

        private static bool IsAttackerDealWeakness(ref BattleCharacterInfo chrAttacker, ref BattleCharacterInfo chrTarget)
        {
            var attackerAttribute = chrAttacker.Attribute;
            var targetAttribute = chrTarget.Weakness;
            var result = targetAttribute.HasFlag(attackerAttribute);

            return result;
        }

        private static float GetAttributeReferenceValue(ref BattleCharacterInfo info
            , AbilityAttributeReference attributeReference)
        {
            switch (attributeReference)
            {
                case AbilityAttributeReference.CurrentHP:
                    return info.BattleInfo.CurrentHP;
                case AbilityAttributeReference.MaxHP:
                    return info.BattleInfo.MaxHP;
                case AbilityAttributeReference.ATK:
                    return info.BaseStats.ATK + info.BaseStatsBonus.ATK;
                case AbilityAttributeReference.DEF:
                    return info.BaseStats.DEF + info.BaseStatsBonus.DEF;
                case AbilityAttributeReference.SPD:
                    return info.BaseStats.SPD + info.BaseStatsBonus.SPD;
            }

            return info.BaseStats.ATK;
        }

        private static float GetElementalDamageMultiplier(ref BattleCharacterInfo chr)
        {
            switch (chr.Attribute)
            {
                case ElementalType.Physical:
                    return chr.DMGType.PhysicalDMGBoostMultiplier;
                case ElementalType.Fire:
                    return chr.DMGType.FireDMGBoostMultiplier;
                case ElementalType.Ice:
                    return chr.DMGType.IceDMGBoostMultiplier;
                case ElementalType.Wind:
                    return chr.DMGType.WindDMGBoostMultiplier;
                case ElementalType.Lightning:
                    return chr.DMGType.LightningDMGBoostMultiplier;
                case ElementalType.Imaginary:
                    return chr.DMGType.ImaginaryDMGBoostMultiplier;
                case ElementalType.Quantum:
                    return chr.DMGType.QuantumDMGBoostMultiplier;
            }

            return chr.DMGType.PhysicalDMGBoostMultiplier;
        }

        private static float GetElementalResistanceMultiplier(ref BattleCharacterInfo chr)
        {
            switch (chr.Attribute)
            {
                case ElementalType.Physical:
                    return chr.DMGType.PhysicalRESBoostMultiplier;
                case ElementalType.Fire:
                    return chr.DMGType.FireRESBoostMultiplier;
                case ElementalType.Ice:
                    return chr.DMGType.IceRESBoostMultiplier;
                case ElementalType.Wind:
                    return chr.DMGType.WindRESBoostMultiplier;
                case ElementalType.Lightning:
                    return chr.DMGType.LightningRESBoostMultiplier;
                case ElementalType.Imaginary:
                    return chr.DMGType.ImaginaryRESBoostMultiplier;
                case ElementalType.Quantum:
                    return chr.DMGType.QuantumRESBoostMultiplier;
            }

            return chr.DMGType.PhysicalRESBoostMultiplier;
        }
        private static float GetElementalResistancePenetrationMultiplier(ref BattleCharacterInfo chr)
        {
            switch (chr.Attribute)
            {
                case ElementalType.Physical:
                    return chr.DMGType.PhysicalPENBoostMultiplier;
                case ElementalType.Fire:
                    return chr.DMGType.FirePENBoostMultiplier;
                case ElementalType.Ice:
                    return chr.DMGType.IcePENBoostMultiplier;
                case ElementalType.Wind:
                    return chr.DMGType.WindPENBoostMultiplier;
                case ElementalType.Lightning:
                    return chr.DMGType.LightningPENBoostMultiplier;
                case ElementalType.Imaginary:
                    return chr.DMGType.ImaginaryPENBoostMultiplier;
                case ElementalType.Quantum:
                    return chr.DMGType.QuantumPENBoostMultiplier;
            }

            return chr.DMGType.PhysicalPENBoostMultiplier;
        }
    }
}
