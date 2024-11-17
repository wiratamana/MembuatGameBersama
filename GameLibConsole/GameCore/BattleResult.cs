using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public readonly struct BattleResult
    {
        public readonly bool IsBattleEnded;
        public readonly bool IsWon;

        public BattleResult(bool isWon)
        {
            IsBattleEnded = true;
            IsWon = isWon;
        }
    }
}
