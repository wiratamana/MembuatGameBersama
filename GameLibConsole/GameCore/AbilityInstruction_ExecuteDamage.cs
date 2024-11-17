using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GameCore
{
    public class AbilityInstruction_ExecuteDamage : IAbilityInstruction
    {
        public bool InvokeEvent;

        public void Execute(BattleEventInfo info)
        {
            if (InvokeEvent)
            {
                foreach (var item in info.ModifierInfos)
                {
                    if (item.ModifierData.ActiveWhen == AbilityModifierActiveCondition.WhenEnemyAttackingMe)
                    {
                        foreach (var target in info.TargetInfos)
                        {
                            if (target.Target == item.TargetCharacter)
                            {
                                info.ModifierInfoInstanceDuringExecution = item;
                                foreach (var instruction in item.ModifierData.Instructions)
                                {
                                    instruction.Execute(info);
                                }
                                info.ModifierInfoInstanceDuringExecution = null;
                            }
                        }
                    }
                }
            }

            info.BattleInstance.ExecuteAction(info);
        }
    }
}
