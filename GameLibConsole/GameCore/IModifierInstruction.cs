using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public interface IModifierInstruction
    {
        void Callback(BattleEventType activeWhen, BattleEventInfo info);
    }
}
