using GameCore;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TestDll
{

    [UnityEditor.MenuItem("TamanaGanteng/TestDll")]
    private static void Test()
    {
        AbilityDatabase.Abilities = new AbilityDataSet[2];
        AbilityDatabase.Abilities[0] = new AbilityDataSet
        {
            BasicATK = new AbilityData
            {
                Name = "Thwack",
                Instructions = new IAbilityInstruction[4]
                {
                    AbilityInstructionFactory.RegenerateEnergy(20f),
                    AbilityInstructionFactory.SetTargets(AbilityTargetWhos.EnemySingle),
                    AbilityInstructionFactory.RegisterDamage_ATK(1.1f, 10f),
                    AbilityInstructionFactory.ExecuteDamage()
                }
            },
            Skill = new AbilityData
            {
                Name = "Sheathed Blade",
                Instructions = new IAbilityInstruction[7]
                {
                    AbilityInstructionFactory.RegenerateEnergy(30f),
                    AbilityInstructionFactory.SetTargets(AbilityTargetWhos.Self),
                    AbilityInstructionFactory.ModifyBaseStats_SPD_Percentage(0.25f),
                    AbilityInstructionFactory.AddModifier_2Duration_NotStackable(2),
                    AbilityInstructionFactory.SetTargets(AbilityTargetWhos.EnemySingle),
                    AbilityInstructionFactory.RegisterDamage_ATK(2.42f, 20f),
                    AbilityInstructionFactory.ExecuteDamage()
                }
            }
        };
        AbilityDatabase.Abilities[1] = new AbilityDataSet
        {
            BasicATK = new AbilityData
            {
                Name = "Novaburst",
                Instructions = new IAbilityInstruction[4]
                {
                    AbilityInstructionFactory.RegenerateEnergy(20f),
                    AbilityInstructionFactory.SetTargets(AbilityTargetWhos.EnemySingle),
                    AbilityInstructionFactory.RegisterDamage_MaxHP(0.55f, 20f),
                    AbilityInstructionFactory.ExecuteDamage()
                }
            },
            Skill = new AbilityData
            {
                Name = "Known by Stars, Shown by Hearts",
                Instructions = new IAbilityInstruction[3]
                {
                    AbilityInstructionFactory.RegenerateEnergy(30f),
                    AbilityInstructionFactory.SetTargets(AbilityTargetWhos.AllyAllExcludeSelf),
                    AbilityInstructionFactory.AddModifier_3Duration_NotStackable(1)
                }
            }
        };
        AbilityDatabase.Modifiers = new AbilityModifierData[3];
        AbilityDatabase.Modifiers[0] = new AbilityModifierData
        {
            Name = "Burning",
            ActiveWhen = AbilityModifierActiveCondition.BeforeGettingTurn,
            Instructions = new IAbilityInstruction[3]
            {
                AbilityInstructionFactory.SetTargets(AbilityTargetWhos.Self),
                AbilityInstructionFactory.RegisterDamage_ATK(1f, 10f),
                AbilityInstructionFactory.ExecuteDamage_DontInvokeEvent()
            }
        };
        AbilityDatabase.Modifiers[1] = new AbilityModifierData
        {
            Name = "Matrix of Prescience (Damage Distributor)",
            ActiveWhen = AbilityModifierActiveCondition.WhenEnemyAttackingMe,
            Instructions = new IAbilityInstruction[8]
            {
                AbilityInstructionFactory.SaveTargetInfos(),
                AbilityInstructionFactory.ModifyDamageInfoTargetsToModifierOwner(),
                AbilityInstructionFactory.ModifyDamageInfo(0.65f),
                AbilityInstructionFactory.ExecuteDamage_DontInvokeEvent(),
                AbilityInstructionFactory.LoadTargetInfos(),
                AbilityInstructionFactory.ModifyDamageInfo(0.35f),
                AbilityInstructionFactory.ExecuteDamage_DontInvokeEvent(),
                AbilityInstructionFactory.SetTargets(AbilityTargetWhos.None)
            }
        };
        AbilityDatabase.Modifiers[2] = new AbilityModifierData
        {
            Name = "Seele Increase Speeds",
            ActiveWhen = AbilityModifierActiveCondition.DuringEvaluation,
            Instructions = new IAbilityInstruction[2]
            {
                AbilityInstructionFactory.SetTargets(AbilityTargetWhos.Self),
                AbilityInstructionFactory.ModifyBaseStats_SPD_Percentage(0.25f)
            }
        };
        BattleEntityPlayerCharacter item = new BattleEntityPlayerCharacter
        {
            Name = "Seele",
            CharacterID = 1,
            Level = 80,
            Attribute = ElementalType.Physical,
            BaseStats = new BaseStats
            {
                HP = 11000,
                ATK = 2000,
                DEF = 1000,
                SPD = 120
            },
            AdvanceStats = new AdvanceStats
            {
                CRITRate = 80,
                CRITDMG = 240
            },
            DMGType = new DMGType
            {
                PhysicalDMGBoost = 43,
                PhysicalPENBoost = 0
            },
            SkillID = 0
        };
        BattleEntityPlayerCharacter item2 = new BattleEntityPlayerCharacter
        {
            Name = "Fu Xuan",
            CharacterID = 2,
            Level = 80,
            Attribute = ElementalType.Quantum,
            BaseStats = new BaseStats
            {
                HP = 20000,
                ATK = 1000,
                DEF = 3000,
                SPD = 150
            },
            AdvanceStats = new AdvanceStats
            {
                CRITRate = 15,
                CRITDMG = 50
            },
            DMGType = new DMGType
            {
                QuantumDMGBoost = 43,
                QuantumPENBoost = 0
            },
            SkillID = 1
        };
        BattleEntityEnemy item3 = new BattleEntityEnemy
        {
            Name = "Monster 1",
            CharacterID = 1001,
            Level = 90,
            Weaknesses = (ElementalType.Physical | ElementalType.Fire | ElementalType.Quantum),
            ToughnessInfo = new ToughnessInfo(30),
            BaseStats = new BaseStats
            {
                HP = 15000,
                ATK = 1600,
                SPD = 100
            },
            DMGType = new DMGType
            {
                PhysicalRESBoost = 20,
                QuantumRESBoost = 20
            }
        };
        BattleEntityEnemy item4 = new BattleEntityEnemy
        {
            Name = "Monster 2",
            CharacterID = 1002,
            Level = 90,
            Weaknesses = (ElementalType.Physical | ElementalType.Fire | ElementalType.Quantum),
            ToughnessInfo = new ToughnessInfo(30),
            BaseStats = new BaseStats
            {
                HP = 15000,
                ATK = 1600,
                SPD = 100
            },
            DMGType = new DMGType
            {
                PhysicalRESBoost = 20,
                QuantumRESBoost = 20
            }
        };
        BattleEntityEnemy item5 = new BattleEntityEnemy
        {
            Name = "Monster 3",
            CharacterID = 1003,
            Level = 90,
            Weaknesses = (ElementalType.Physical | ElementalType.Fire | ElementalType.Quantum),
            ToughnessInfo = new ToughnessInfo(30),
            BaseStats = new BaseStats
            {
                HP = 15000,
                ATK = 1600,
                SPD = 100
            },
            DMGType = new DMGType
            {
                PhysicalRESBoost = 20,
                QuantumRESBoost = 20
            }
        };

        List<BattleEntityBase> playerCharacters = new List<BattleEntityBase> { item, item2 };
        List<BattleEntityBase> enemyCharacters = new List<BattleEntityBase> { item3, item4, item5 };
        BattleStateInfo lastBattleState = new BattleStateInfo(1, new List<BattleCharacterInfo>(), new List<BattleCharacterInfo>(), new List<AbilityModifierInfo>());
        Battle battle = new Battle(playerCharacters, enemyCharacters);
        battle.BattleEvent += Battle_BeforeActionStart;
        battle.BattleEvent += Battle_AfterDecidedWhatToDo;
        battle.BattleEvent += PrintBattleState;
        battle.ReceivedModifierEvent += Battle_ReceivedModifierEvent;
        battle.StatsModifiedEvent += Battle_StatsModifiedEvent;
        while (!lastBattleState.BattleResult.IsBattleEnded)
        {
            battle.SimulateOneTurn(ref lastBattleState);
        }

        Debug.Log($"Menang? {lastBattleState.BattleResult.IsWon}");
    }

    private static void Battle_StatsModifiedEvent(BattleCharacterInfo modifierInfo)
    {
        Debug.Log("Battle_StatsModifiedEvent");
        Debug.Log($"   - {modifierInfo.Name.ToString()}");
        Debug.Log("      - BaseStats:");
        Debug.Log($"         - HP : {modifierInfo.BaseStatsBonus.HP}");
        Debug.Log($"         - ATK: {modifierInfo.BaseStatsBonus.ATK}");
        Debug.Log($"         - DEF: {modifierInfo.BaseStatsBonus.DEF}");
        Debug.Log($"         - SPD: {modifierInfo.BaseStatsBonus.SPD}");
    }

    private static void Battle_ReceivedModifierEvent(AbilityModifierInfo modifierInfo)
    {
        Debug.Log("Battle_StatsModifiedEvent");
        Debug.Log("   - " + modifierInfo.ModifierData.Name);
        Debug.Log($"      - Owner : {modifierInfo.OwnerCharacter.Name.ToString()}");
        Debug.Log($"      - Target: {modifierInfo.TargetCharacter.Name.ToString()}");
    }

    private static void Battle_BeforeActionStart(BattleEventType evt, BattleEventInfo info)
    {
        if (evt == BattleEventType.BeforeActionStart)
        {
            Debug.Log($"Turn: {info.Action}");
            Debug.Log("-------------------------------------------------");
            PrintCharacterStatus(info);
        }
    }

    private static void Battle_AfterDecidedWhatToDo(BattleEventType evt, BattleEventInfo info)
    {
        if (evt == BattleEventType.AfterDecideWhatToDo)
        {
            Debug.Log("");
            Debug.Log($"EventType: {evt}");
            Debug.Log("   - ExecutingTurn: " + info.ExecutingTurn.Name.ToString());
            Debug.Log($"      - WhatToDo: {info.WhatToDoInThisTurn} ({info.ExecutingTurn.GetAbilityData(info.WhatToDoInThisTurn).Name})");
        }
    }

    private static void PrintBattleState(BattleEventType evt, BattleEventInfo info)
    {
        if (evt == BattleEventType.BeforeActionEnd)
        {
        }
    }

    private static string GetCharacterNameWithMaxAndCurrentHP(BattleCharacterInfo info)
    {
        return $"{info.Name.ToString()} ({info.BattleInfo.CurrentHP}/{info.BattleInfo.MaxHP})";
    }

    private static void PrintCharacterStatus(BattleEventInfo info)
    {
        Debug.Log("Player Characters");
        foreach (BattleCharacterInfo player in info.Players)
        {
            Debug.Log($"   - {player.Name.ToString()}");
            Debug.Log("      - Status:");
            Debug.Log($"         - HP : {player.BattleInfo.CurrentHP}/{player.BattleInfo.MaxHP}");
            Debug.Log($"         - ATK: {player.BaseStats.ATK} + {player.BaseStatsBonus.ATK}");
            Debug.Log($"         - DEF: {player.BaseStats.DEF} + {player.BaseStatsBonus.DEF}");
            Debug.Log($"         - SPD: {player.BaseStats.SPD} + {player.BaseStatsBonus.SPD}");
            Debug.Log("      - Modifiers:");
            foreach (AbilityModifierInfo modifierInfo in info.ModifierInfos)
            {
                if (modifierInfo.TargetCharacter == player)
                {
                    Debug.Log("         - " + modifierInfo.ModifierData.Name);
                }
            }
        }

        Debug.Log("");
        Debug.Log("Enemy Characters");
        foreach (BattleCharacterInfo enemy in info.Enemies)
        {
            Debug.Log($"   - {enemy.Name.ToString()}");
            Debug.Log("      - Status:");
            Debug.Log($"         - HP : {enemy.BattleInfo.CurrentHP}/{enemy.BattleInfo.MaxHP}");
            Debug.Log($"         - ATK: {enemy.BaseStats.ATK} + {enemy.BaseStatsBonus.ATK}");
            Debug.Log($"         - DEF: {enemy.BaseStats.DEF} + {enemy.BaseStatsBonus.DEF}");
            Debug.Log($"         - SPD: {enemy.BaseStats.SPD} + {enemy.BaseStatsBonus.SPD}");
            Debug.Log("      - Modifiers:");
            foreach (AbilityModifierInfo modifierInfo2 in info.ModifierInfos)
            {
                if (modifierInfo2.TargetCharacter == enemy)
                {
                    Debug.Log($"         - {modifierInfo2.ModifierData.Name} (RemainingDuration: {modifierInfo2.DurationTurn})");
                }
            }
        }
    }

}
