using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design;

namespace GameCore
{
    public class Battle
    {
        public delegate void BattleEventDelegate(BattleEventType evt, BattleEventInfo info);
        public delegate void ReceivedModifierDelegate(AbilityModifierInfo modifierInfo);
        public delegate void StatsModifiedDelegate(BattleCharacterInfo modifierInfo);

        public List<BattleEntityBase> PlayerCharacters;
        public List<BattleEntityBase> EnemyCharacters;
        public int Action;

        private readonly Random Random;

        public event BattleEventDelegate BattleEvent;
        public event ReceivedModifierDelegate ReceivedModifierEvent;
        public event StatsModifiedDelegate StatsModifiedEvent;

        public Battle(List<BattleEntityBase> playerCharacters
            , List<BattleEntityBase> enemyCharacters)
        {
            PlayerCharacters = playerCharacters;
            EnemyCharacters = enemyCharacters;
            Random = new Random();
            Action = 0;
        }

        public void SimulateOneTurn(ref BattleStateInfo lastBattleState)
        {
            var playerCnt = PlayerCharacters.Count;
            var enemyCnt = EnemyCharacters.Count;

            List<BattleCharacterInfo> myCharacters;
            List<BattleCharacterInfo> enemyCharacters;
            var targetInfos = new List<BattleTargetInfo>();

            if (Action == 0)
            {
                Init(PlayerCharacters, out myCharacters);
                Init(EnemyCharacters, out enemyCharacters);
            }
            else
            {
                lastBattleState.Dispose();
                lastBattleState.InitBattleInfo(out myCharacters, out enemyCharacters);
            }

            var currentExecutingTurn = myCharacters[0];
            var battleEventInfo = new BattleEventInfo
            {
                Players = myCharacters,
                Enemies = enemyCharacters,
                TargetInfos = targetInfos,
                ModifierInfos = lastBattleState.ModifierInfos,
                Action = Action,
                BattleInstance = this,
            };

            if (battleEventInfo.ModifierInfos == null)
            {
                battleEventInfo.ModifierInfos = new List<AbilityModifierInfo>();
            }

            Action++;
            BattleEvent?.Invoke(BattleEventType.BeforeActionStart, battleEventInfo);

            GetInfoWithLowestActionValue(battleEventInfo);
            EvaluateModifiers(battleEventInfo);
            UpdateTurnPositionBattleEntities(battleEventInfo);
            DecideWhatToDo(battleEventInfo);
            ExecuteTurn(battleEventInfo);

            BattleEvent?.Invoke(BattleEventType.BeforeActionEnd, battleEventInfo);

            if (IsAllDead(enemyCharacters))
            {
                lastBattleState = new BattleStateInfo(new BattleResult(true), Action, myCharacters, enemyCharacters, battleEventInfo.ModifierInfos);
                return;
            }
            else if (IsAllDead(myCharacters))
            {
                lastBattleState = new BattleStateInfo(new BattleResult(false), Action, myCharacters, enemyCharacters, battleEventInfo.ModifierInfos);
                return;
            }

            lastBattleState = new BattleStateInfo(Action, myCharacters, enemyCharacters, battleEventInfo.ModifierInfos);
        }

        private void DecideWhatToDo(BattleEventInfo battleEventInfo)
        {
            BattleEvent?.Invoke(BattleEventType.BeforeDecideWhatToDo, battleEventInfo);

            if (battleEventInfo.ExecutingTurn.Name == "Seele")
            {
                battleEventInfo.WhatToDoInThisTurn = AbilityType.Skill;
            }
            else if (battleEventInfo.ExecutingTurn.Name == "Fu Xuan")
            {
                var buffExist = battleEventInfo.ModifierInfos.Exists(x => x.ModifierData.Name.Contains("Matrix"));
                if (buffExist) 
                {
                    battleEventInfo.WhatToDoInThisTurn = AbilityType.BasicATK;
                }
                else
                {
                    battleEventInfo.WhatToDoInThisTurn = AbilityType.Skill;
                }
            }
            else
            {
                battleEventInfo.WhatToDoInThisTurn = AbilityType.BasicATK;
            }

            BattleEvent?.Invoke(BattleEventType.AfterDecideWhatToDo, battleEventInfo);
        }

        private bool IsAllDead(List<BattleCharacterInfo> battleInfos)
        {
            var cnt = battleInfos.Count ;
            for (var i = 0; i < cnt; i++)
            {
                var info = battleInfos[i];
                if (info.BattleInfo.IsDead == false)
                {
                    return false;
                }
            }

            return true;
        }

        private void ExecuteTurn(BattleEventInfo eventInfo)
        {
            BattleEvent?.Invoke(BattleEventType.BeforeExecuteTurn, eventInfo);

            ref var executingTurn = ref eventInfo.ExecutingTurn;
            var ability = executingTurn.GetAbilityData(eventInfo.WhatToDoInThisTurn);
            var abilitySpan = ability.Instructions.AsSpan();
            for (int i = 0; i < abilitySpan.Length; i++)
            {
                abilitySpan[i].Execute(eventInfo);
            }
            
            executingTurn.BattleInfo.TurnPosition = executingTurn.BattleInfo.ActionValue;

            BattleEvent?.Invoke(BattleEventType.AfterExecuteTurn, eventInfo);
        }

        internal void ExecuteAction(BattleEventInfo eventInfo)
        {
            if (eventInfo.TargetInfos.Count <= 0)
            {
                return;
            }

            ref var executingTurn = ref eventInfo.ExecutingTurn;
            for (var i = 0; i < eventInfo.TargetInfos.Count; i++)
            {
                var targetInfo = eventInfo.TargetInfos[i];
                var target = targetInfo.Target;
                var meDamage = targetInfo.DamageInfo;

                Console.WriteLine($"ExecuteDamage -> {executingTurn.Name.ToString()} deal {meDamage.TotalDamage} damage to {target.Name.ToString()}");
                target.BattleInfo.CurrentHP = Math.Max(target.BattleInfo.CurrentHP - meDamage.TotalDamage, 0);

                if (target.IsEnemyCharacter && meDamage.IsDamageMatchTargetWeaknessElement)
                {
                    target.BattleInfo.Toughness -= meDamage.ToughnessReduction;
                }

                if (meDamage.IsBecomeBrokenInThisTurn)
                {
                    Utils.DelayTargetAction(targetInfo, Const.BROKEN_SPEED_DELAY);

                    target.BattleInfo.TurnPosition += targetInfo.SpeedModifyInfo.DelayValue;
                }
            }
        }

        private void GetInfoWithLowestActionValue(BattleEventInfo eventInfo)
        {
            BattleEvent?.Invoke(BattleEventType.BeforeGetInfoWithLowestActionValue, eventInfo);

            var retval = eventInfo.Players[0];
            var turnPosition = float.MaxValue;

            var cnt = eventInfo.Players.Count;
            for (int i = 0; i < cnt; i++)
            {
                var info = eventInfo.Players[i];
                var battleInfo = info.BattleInfo;
                if (battleInfo.IsDead)
                {
                    continue;
                }

                if (battleInfo.TurnPosition < turnPosition)
                {
                    turnPosition = battleInfo.TurnPosition;
                    retval = info;
                }
            }

            cnt = eventInfo.Enemies.Count;
            for (int i = 0; i < cnt; i++)
            {
                var info = eventInfo.Enemies[i];
                var battleInfo = info.BattleInfo;
                if (battleInfo.IsDead)
                {
                    continue;
                }

                if (battleInfo.TurnPosition < turnPosition)
                {
                    turnPosition = battleInfo.TurnPosition;
                    retval = info;
                }
            }

            eventInfo.ExecutingTurn = retval;
            eventInfo.ExecutingTurn.BaseStatsBonus.Reset();
            BattleEvent?.Invoke(BattleEventType.AfterGetInfoWithLowestActionValue, eventInfo);
        }

        private void UpdateTurnPositionBattleEntities(BattleEventInfo eventInfo)
        {
            var cnt = eventInfo.Players.Count;
            var turnPosition = eventInfo.ExecutingTurn.BattleInfo.TurnPosition;
            eventInfo.TurnPositionUpdateValue = turnPosition;

            BattleEvent?.Invoke(BattleEventType.BeforeUpdateTurnPositionBattleEntity, eventInfo);
            {
                for (int i = 0; i < cnt; i++)
                {
                    var info = eventInfo.Players[i];
                    var newTurnPosition = Math.Max(info.BattleInfo.TurnPosition - turnPosition, 0);

                    info.BattleInfo.TurnPosition = newTurnPosition;
                }

                cnt = eventInfo.Enemies.Count;
                for (int i = 0; i < cnt; i++)
                {
                    var info = eventInfo.Enemies[i];
                    var newTurnPosition = Math.Max(info.BattleInfo.TurnPosition - turnPosition, 0);

                    info.BattleInfo.TurnPosition = newTurnPosition;
                }
            }
            BattleEvent?.Invoke(BattleEventType.AfterUpdateTurnPositionBattleEntity, eventInfo);

            if (eventInfo.ExecutingTurn.IsBroken)
            {
                var toughness = (float)eventInfo.ExecutingTurn.ToughnessInfo.Toughness;
                eventInfo.ExecutingTurn.BattleInfo.Toughness = toughness;
            }
        }

        private void Init(List<BattleEntityBase> characters, out List<BattleCharacterInfo> myInfo)
        {
            myInfo = new List<BattleCharacterInfo>();

            var count = characters.Count;
            for (int i = 0; i < count; i++)
            {
                var character = characters[i];

                if (character is BattleEntityPlayerCharacter playerCharacter)
                {
                    myInfo.Add(new BattleCharacterInfo(i, character, playerCharacter.SkillID));
                }
                else
                {
                    myInfo.Add(new BattleCharacterInfo(i, character));
                }
            }
        }

        private void EvaluateModifiers(BattleEventInfo info)
        {
            var executing = info.ExecutingTurn;
            for (int i = info.ModifierInfos.Count - 1; i >= 0; i--)
            {
                var item = info.ModifierInfos[i];
                if (item.TargetCharacter != executing)
                {
                    continue;
                }

                if (item.ModifierData.ActiveWhen == AbilityModifierActiveCondition.DuringEvaluation)
                {
                    info.ModifierInfoInstanceDuringExecution = item;
                    foreach (var instruction in item.ModifierData.Instructions)
                    {
                        instruction.Execute(info);
                    }
                    info.ModifierInfoInstanceDuringExecution = null;
                }

                item.DurationTurn--;
                if (item.DurationTurn == 0)
                {
                    info.ModifierInfos.RemoveAt(i);
                }
            }
        }

        internal void InvokeReceivedModifier(AbilityModifierInfo modifier)
        {
            ReceivedModifierEvent?.Invoke(modifier);
        }

        internal void InvokeStatsModifiedEvent(BattleCharacterInfo info)
        {
            StatsModifiedEvent?.Invoke(info);
        }
    }
}
