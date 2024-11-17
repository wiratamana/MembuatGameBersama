using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public struct BattleDamageInfo
    {
        public int TotalDamage;
        public readonly bool IsCritical;
        public readonly bool IsDamageMatchTargetWeaknessElement;
        public readonly bool IsBecomeBrokenInThisTurn;
        public readonly int ToughnessReduction;
        public readonly BattleCharacterInfo DamageDealer;
        public readonly BattleCharacterInfo Target;

        public BattleDamageInfo(BattleCharacterInfo damageDealer
            , BattleCharacterInfo target
            , int totalDamage
            , bool isCritical
            , bool isDamageMatchTargetWeaknessElement
            , bool isBecomeBrokenInThisTurn
            , int toughnessReduction)
        {
            DamageDealer = damageDealer;
            Target = target;
            TotalDamage = totalDamage;
            IsCritical = isCritical;
            IsDamageMatchTargetWeaknessElement = isDamageMatchTargetWeaknessElement;
            IsBecomeBrokenInThisTurn = isBecomeBrokenInThisTurn;
            ToughnessReduction = toughnessReduction;
        }
    }
}
