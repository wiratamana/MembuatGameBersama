using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public struct AdvanceStats
    {
        public int CRITRate;
        public int CRITDMG;
        public int BreakEffect;
        public int OutgoingHealingBoost;
        public int MaxEnergy;
        public int EnergyRegenerationRate;
        public int EffectHitRate;
        public int EffectRES;

        public float CRITDMGMultiplier => 1.0f + (CRITDMG / 100.0f);
    }
}
