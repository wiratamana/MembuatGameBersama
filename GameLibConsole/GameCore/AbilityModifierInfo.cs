using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameCore
{
    public class AbilityModifierInfo
    {
        public BattleCharacterInfo OwnerCharacter;
        public BattleCharacterInfo TargetCharacter;

        public int ModifierID;
        public AbilityModifierData ModifierData;
        public Battle BattleInstance;

        public int DurationTurn;
        public AbilityModifierActiveCondition ActiveWhen;

        public void InitModifierData()
        {
            var src = AbilityDatabase.Modifiers[ModifierID];
            ModifierData = new AbilityModifierData
            {
                Owner = OwnerCharacter,
                Target = TargetCharacter,
                Name = src.Name,
                ActiveWhen = src.ActiveWhen,
                Instructions = src.Instructions,
            };
        }
    }
}
