using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public static class AbilityDatabase
    {
        public static AbilityDataSet[] Abilities;

        public static AbilityData EnemyBasicATK = new AbilityData
        {
            Name = "Basic ATK",
            Instructions = new IAbilityInstruction[]
            {
                AbilityInstructionFactory.SetTargets(AbilityTargetWhos.EnemySingle),
                AbilityInstructionFactory.RegisterDamage_ATK(1.0f, 0.0f),
                AbilityInstructionFactory.ExecuteDamage()
            }
        };

        public static AbilityModifierData[] Modifiers;
    }
}
