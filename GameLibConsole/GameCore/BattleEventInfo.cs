using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameCore
{
    public class BattleEventInfo
    {
        public Battle BattleInstance;
        public List<BattleCharacterInfo> Players;
        public List<BattleCharacterInfo> Enemies;
        public List<BattleTargetInfo> TargetInfos;

        public List<AbilityModifierInfo> ModifierInfos;

        public BattleCharacterInfo ExecutingTurn;
        public AbilityModifierInfo ModifierInfoInstanceDuringExecution;

        public float TurnPositionUpdateValue;
        public AbilityType WhatToDoInThisTurn;
        public int Action;

        internal List<BattleTargetInfo> SavedTargetInfos;
    }
}
