using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Text;

namespace GameCore
{
    public class AbilityInstruction_SetTargets : IAbilityInstruction
    {
        public AbilityTargetWhos TargetWhos;

        public void Execute(BattleEventInfo info)
        {
            info.TargetInfos.Clear();
            if (TargetWhos == AbilityTargetWhos.AbilityModifierOwner)
            {
                info.TargetInfos.Add(new BattleTargetInfo() 
                { 
                    Target = info.ModifierInfoInstanceDuringExecution.OwnerCharacter 
                });
                return;
            }

            if (TargetWhos == AbilityTargetWhos.Custom)
            {
                return;
            }
            
            Utils.GetTargets(info, TargetWhos);
        }
    }
}
