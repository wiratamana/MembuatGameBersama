using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public enum AbilityTargetWhos
    {
        None,

        AllySingle,
        AllyAll,
        AllyAllExcludeSelf,
        AllyGroup,

        EnemySingle,
        EnemyAll,
        EnemyGroup,

        Self,

        AbilityModifierOwner,

        Custom,

        Everyone,
    }
}
