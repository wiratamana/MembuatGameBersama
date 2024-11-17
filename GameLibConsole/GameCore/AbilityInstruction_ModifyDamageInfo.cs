using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class AbilityInstruction_ModifyDamageInfo : IAbilityInstruction
    {
        public float Value;

        public void Execute(BattleEventInfo info)
        {
            foreach (var target in info.TargetInfos)
            {
                var totalDamage = target.DamageInfo.TotalDamage * Value;
                target.DamageInfo.TotalDamage = (int)totalDamage;
            }
        }
    }
}
