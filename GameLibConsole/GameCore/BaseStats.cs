using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public struct BaseStats
    {
        public int HP;
        public int ATK;
        public int DEF;
        public int SPD;

        public void Reset()
        {
            HP = 0; ATK = 0; DEF = 0; SPD = 0;
        }
    }
}
