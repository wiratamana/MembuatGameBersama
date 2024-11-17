using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class AbilityInstruction_RegisterDamage : IAbilityInstruction
    {
        public float DamageMultiplier;
        public float ToughnessReduction;
        public AbilityAttributeReference AttributeReference;

        public void Execute(BattleEventInfo info)
        {
            Utils.CalculateDamage(ref info, AttributeReference, DamageMultiplier);
        }        
    }
}
