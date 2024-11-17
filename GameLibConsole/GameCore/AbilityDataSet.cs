using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class AbilityDataSet
    {
        public AbilityData BasicATK;
        public AbilityData Skill;
        public AbilityData Ultimate;
        public AbilityData Talent;
        public AbilityData Technique;

        public AbilityData GetAbilityData(AbilityType abilityType)
        {
            switch (abilityType)
            {
                case AbilityType.BasicATK:
                    return BasicATK;
                case AbilityType.Skill:
                    return Skill;
                case AbilityType.Ultimate:
                    return Ultimate;
            }

            return null;
        }
    }
}
