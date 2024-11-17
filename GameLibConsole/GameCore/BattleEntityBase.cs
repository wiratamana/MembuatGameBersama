using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public abstract class BattleEntityBase
    {
        public string Name;
        public int CharacterID;
        public int Level;
        public BaseStats BaseStats;
        public AdvanceStats AdvanceStats;
        public DMGType DMGType;
    }
}
