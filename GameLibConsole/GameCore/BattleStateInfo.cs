using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public class BattleStateInfo : IDisposable
    {
        internal static ArrayPool<BattleCharacterInfo> poolBattleCharacterInfo = ArrayPool<BattleCharacterInfo>.Shared;
        private readonly BattleCharacterInfo[] rentedArray;

        public readonly int Action;
        public readonly ArraySegment<BattleCharacterInfo> MyBattleInfo;
        public readonly ArraySegment<BattleCharacterInfo> EnemyBattleInfo;
        public readonly BattleResult BattleResult;
        public readonly List<AbilityModifierInfo> ModifierInfos;

        public BattleStateInfo(int action
            , List<BattleCharacterInfo> myBattleInfo
            , List<BattleCharacterInfo> enemyBattleInfo
            , List<AbilityModifierInfo> modifierInfos)
        {
            var myLength = myBattleInfo.Count;
            var enemyLength = enemyBattleInfo.Count;

            Action = action;
            rentedArray = poolBattleCharacterInfo.Rent(myLength + enemyLength);
            MyBattleInfo = new ArraySegment<BattleCharacterInfo>(rentedArray, 0, myLength);
            EnemyBattleInfo = new ArraySegment<BattleCharacterInfo>(rentedArray, myLength, enemyLength);
            BattleResult = default;
            ModifierInfos = modifierInfos;

            for (var i = 0; i < myLength; i++)
            {
                MyBattleInfo.Array[MyBattleInfo.Offset + i] = myBattleInfo[i];
            }

            for (var i = 0; i < enemyLength; i++)
            {
                EnemyBattleInfo.Array[EnemyBattleInfo.Offset + i] = enemyBattleInfo[i];
            }
        }

        public BattleStateInfo(BattleResult result
            , int action
            , List<BattleCharacterInfo> myBattleInfo
            , List<BattleCharacterInfo> enemyBattleInfo
            , List<AbilityModifierInfo> modifierInfos) 
                : this(action
                      , myBattleInfo
                      , enemyBattleInfo
                      , modifierInfos)
        {
            BattleResult = result;
        }

        public void InitBattleInfo(out List<BattleCharacterInfo> myBattleInfo, out List<BattleCharacterInfo> enemyBattleInfo)
        {
            myBattleInfo = new List<BattleCharacterInfo>(MyBattleInfo);
            enemyBattleInfo = new List<BattleCharacterInfo>(EnemyBattleInfo);
        }

        public void Dispose()
        {
            if (rentedArray == null)
            {
                return;
            }

            poolBattleCharacterInfo.Return(rentedArray);
        }
    }
}
