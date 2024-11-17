using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GameCore
{
    public class AbilityInstruction_ModifyBaseStats : IAbilityInstruction
    {
        public BattleCharacterBaseStatsType WhichAbility;
        public float Value;
        public bool Flat;

        public void Execute(BattleEventInfo info)
        {
            foreach (var item in info.TargetInfos)
            {
                ref var bonus = ref item.Target.BaseStatsBonus;

                if (WhichAbility.HasFlag(BattleCharacterBaseStatsType.HP))
                {
                    Abc(ref bonus.SPD, item.Target.BaseStats.SPD);
                }

                if (WhichAbility.HasFlag(BattleCharacterBaseStatsType.ATK))
                {
                    Abc(ref bonus.ATK, item.Target.BaseStats.ATK);
                }

                if (WhichAbility.HasFlag(BattleCharacterBaseStatsType.DEF))
                {
                    Abc(ref bonus.DEF, item.Target.BaseStats.DEF);
                }

                if (WhichAbility.HasFlag(BattleCharacterBaseStatsType.SPD))
                {
                    Abc(ref bonus.SPD, item.Target.BaseStats.SPD);
                }

                info.BattleInstance.InvokeStatsModifiedEvent(item.Target);
            }
        }

        private void Abc(ref int val, int baseStat)
        {
            if (Flat)
            {
                val += (int)Value;
            }

            else
            {
                val += (int)(baseStat * Value);
            }
        }
    }
}
