using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    [Flags]
    public enum BattleCharacterBaseStatsType
    {
        None = 0,
        HP = 1 << 0,
        ATK = 1 << 1,
        DEF = 1 << 2,
        SPD = 1 << 3,
    }
}
