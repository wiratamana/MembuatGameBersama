using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class BattleInfo
    {
        public BattleCharacterInfo owner;

        public int MaxHP;
        public int CurrentHP;
        public float ActionValue => 10000.0f / (owner.BaseStats.SPD + owner.BaseStatsBonus.SPD);
        public float TurnPosition;
        public float Toughness;

        public bool IsDead => CurrentHP <= 0;

        public BattleInfo(in BattleCharacterInfo character)
        {
            owner = character;

            ref var baseStats = ref character.BaseStats;
            Toughness = 0;
            MaxHP = baseStats.HP;
            CurrentHP = MaxHP;
            TurnPosition = ActionValue;

            if (character.IsEnemyCharacter)
            {
                Toughness = character.ToughnessInfo.Toughness;
            }
        }
    }
}
