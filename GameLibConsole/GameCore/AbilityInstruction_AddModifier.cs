using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class AbilityInstruction_AddModifier : IAbilityInstruction
    {
        public int ModifierID;
        public int DurationTurn;
        public bool Stackable;

        public void Execute(BattleEventInfo info)
        {
            if (Stackable != true && info.ModifierInfos.Exists(x => x.ModifierID == ModifierID))
            {
                var current = info.ModifierInfos.Find(x => x.ModifierID == ModifierID);
                current.DurationTurn = DurationTurn;
                return;
            }

            foreach (var item in info.TargetInfos)
            {
                var modifierInfo = new AbilityModifierInfo
                {
                    ModifierID = ModifierID,
                    BattleInstance = info.BattleInstance,
                    DurationTurn = DurationTurn,
                    TargetCharacter = item.Target,
                    OwnerCharacter = info.ExecutingTurn
                };

                modifierInfo.InitModifierData();
                modifierInfo.ActiveWhen = modifierInfo.ModifierData.ActiveWhen;

                info.ModifierInfos.Add(modifierInfo);
                info.BattleInstance.BattleEvent += modifierInfo.ModifierData.Callback;

                info.BattleInstance.InvokeReceivedModifier(modifierInfo);
            }
        }
    }
}
