using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class AbilityInstruction_ModifyTargetsToModifierOwner : IAbilityInstruction
    {
        public void Execute(BattleEventInfo info)
        {
            foreach (var i in info.TargetInfos)
            {
                i.Target = info.ModifierInfoInstanceDuringExecution.OwnerCharacter;
            }
        }
    }
}
