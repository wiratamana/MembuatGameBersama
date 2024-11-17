using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class BattleEntityPlayerCharacter : BattleEntityBase
    {
        public ElementalType Attribute;

        public int SkillID;

    }

    [Flags]
    public enum ElementalType
    {
        None = 0,
        Physical = 1 << 0,
        Fire = 1 << 1,
        Ice = 1 << 2,
        Wind = 1 << 3,
        Lightning = 1 << 4,
        Imaginary = 1 << 5,
        Quantum = 1 << 6
    }
}
