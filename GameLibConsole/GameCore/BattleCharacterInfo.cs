using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class BattleCharacterInfo
    {
        public readonly BattleEntityBase battleEntity;
        private BaseStats bonusBaseStats;

        public readonly int Id;
        public int CharacterID => battleEntity.CharacterID;
        public bool IsPlayerCharacter => battleEntity is BattleEntityPlayerCharacter;
        public int Level => battleEntity.Level;
        public ref BaseStats BaseStats => ref battleEntity.BaseStats;
        public ref BaseStats BaseStatsBonus => ref bonusBaseStats;
        public ref AdvanceStats AdvanceStats => ref battleEntity.AdvanceStats;
        public ref DMGType DMGType => ref battleEntity.DMGType;
        public ElementalType Weakness
        {
            get
            {
                if (battleEntity is BattleEntityEnemy enemy)
                {
                    return enemy.Weaknesses;
                }

                return ElementalType.None;
            }
        }
        public ElementalType Attribute
        {
            get
            {
                if (battleEntity is BattleEntityPlayerCharacter player)
                {
                    return player.Attribute;
                }

                return ElementalType.None;
            }
        }
        public ref readonly ToughnessInfo ToughnessInfo
        {
            get
            {
                if (battleEntity is BattleEntityEnemy enemy)
                {
                    return ref enemy.ToughnessInfo;
                }

                return ref Unsafe.NullRef<ToughnessInfo>();
            }
        }
        public readonly int SkillID;

        public BattleInfo BattleInfo;
        public bool IsBroken => IsEnemyCharacter && BattleInfo.Toughness <= 0;
        public bool IsEnemyCharacter => IsPlayerCharacter == false;

        public AbilityData GetAbilityData(AbilityType type)
        {
            if (IsPlayerCharacter)
            {
                return AbilityDatabase.Abilities[SkillID].GetAbilityData(type);
            }

            return AbilityDatabase.EnemyBasicATK;
        }

        internal BattleCharacterInfo(int id, in BattleEntityBase character, int skillID) : this(id, in character)
        {
            SkillID = skillID;
        }

        internal BattleCharacterInfo(int id, in BattleEntityBase character)
        {
            battleEntity = character;
            BattleInfo = new BattleInfo(this);

            Id = id;
            SkillID = -1;
        }

        public ReadOnlySpan<char> Name => battleEntity.Name.AsSpan();
    }
}
