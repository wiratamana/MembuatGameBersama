using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public struct BattleSpeedModifyInfo
    {
        public readonly float DelayValue;

        public BattleSpeedModifyInfo(float delayValue)
        {
            DelayValue = delayValue;
        }
    }
}
