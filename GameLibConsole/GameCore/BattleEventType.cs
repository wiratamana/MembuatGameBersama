using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public enum BattleEventType
    {
        BeforeGetInfoWithLowestActionValue,
        AfterGetInfoWithLowestActionValue,

        BeforeUpdateTurnPositionBattleEntity,
        AfterUpdateTurnPositionBattleEntity,

        BeforeModifierEvaluation,
        AfterModifierEvaluation,

        BeforeDecideWhatToDo,
        AfterDecideWhatToDo,

        BeforeExecuteTurn,
        AfterExecuteTurn,

        BeforeActionStart,
        BeforeActionEnd,

        BeforeDamageExecution,
        AfterDamageExecution
    }
}
