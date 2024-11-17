using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class AbilityInstruction_LoadTargetInfos : IAbilityInstruction
    {
        public void Execute(BattleEventInfo info)
        {
            info.TargetInfos = info.SavedTargetInfos;
        }
    }
}
