using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GameCore
{
    public class AbilityModifierData : IModifierInstruction
    {
        public BattleCharacterInfo Owner;
        public BattleCharacterInfo Target;
        public string Name;
        public AbilityModifierActiveCondition ActiveWhen;
        public IAbilityInstruction[] Instructions;

        public void Callback(BattleEventType activeWhen, BattleEventInfo info)
        {
            
        }
    }
}
