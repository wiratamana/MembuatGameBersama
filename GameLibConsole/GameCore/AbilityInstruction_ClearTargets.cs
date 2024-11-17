using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class AbilityInstruction_ClearTargets : IAbilityInstruction
    {
        public void Execute(BattleEventInfo info)
        {
            info.TargetInfos.Clear();
        }
    }
}
