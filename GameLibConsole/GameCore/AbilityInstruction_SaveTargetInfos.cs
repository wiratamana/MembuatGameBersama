using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class AbilityInstruction_SaveTargetInfos : IAbilityInstruction
    {
        public void Execute(BattleEventInfo info)
        {
            var currentTargetInfos = info.TargetInfos;

            if (info.SavedTargetInfos == null)
            {
                info.SavedTargetInfos = new List<BattleTargetInfo>(currentTargetInfos.Count);
            }
            else
            {
                info.SavedTargetInfos.Clear();
                info.SavedTargetInfos.Capacity = currentTargetInfos.Count;
            }

            foreach (var item in currentTargetInfos)
            {
                var newTargetInfo = new BattleTargetInfo
                {
                    DamageInfo = item.DamageInfo,
                    SpeedModifyInfo = item.SpeedModifyInfo,
                    Target = item.Target,
                };

                info.SavedTargetInfos.Add(newTargetInfo);
            }
        }
    }
}
