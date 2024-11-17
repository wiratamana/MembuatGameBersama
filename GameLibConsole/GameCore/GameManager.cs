using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Buffers;
using System.Collections.Generic;
using System;
using System.Xml.Linq;

namespace GameCore
{
    public static class GameManager
    {
        public static string Print()
        {
            //Const.CHARACTERS_NAME.Add(1, "Seele");
            //Const.CHARACTERS_NAME.Add(2, "Fu Xuan");
            //
            //Const.CHARACTERS_NAME.Add(1001, "Monster 1");
            //Const.CHARACTERS_NAME.Add(1002, "Monster 2");
            //Const.CHARACTERS_NAME.Add(1003, "Monster 2");
            //
            //Const.MODIFIER_NAME.Add(1, "Matrix of Prescience");

            AbilityDatabase.Abilities = new AbilityDataSet[2];
            AbilityDatabase.Abilities[0] = new AbilityDataSet
            {
                BasicATK = new AbilityData
                {
                    Name = "Thwack",
                    Instructions = new IAbilityInstruction[]
                    {
                        AbilityInstructionFactory.RegenerateEnergy(20.0f),
                        AbilityInstructionFactory.SetTargets(AbilityTargetWhos.EnemySingle),
                        AbilityInstructionFactory.RegisterDamage_ATK(1.1f, 10.0f),
                        AbilityInstructionFactory.ExecuteDamage(),
                    }
                },
                Skill = new AbilityData
                {
                    Name = "Sheathed Blade",
                    Instructions = new IAbilityInstruction[]
                    {
                        AbilityInstructionFactory.RegenerateEnergy(30.0f),
                        AbilityInstructionFactory.SetTargets(AbilityTargetWhos.Self),
                        AbilityInstructionFactory.ModifyBaseStats_SPD_Percentage(0.25f),
                        AbilityInstructionFactory.AddModifier_2Duration_NotStackable(2),
                        AbilityInstructionFactory.SetTargets(AbilityTargetWhos.EnemySingle),
                        AbilityInstructionFactory.RegisterDamage_ATK(2.42f, 20.0f),
                        AbilityInstructionFactory.ExecuteDamage(),
                    }
                },
            };

            AbilityDatabase.Abilities[1] = new AbilityDataSet
            {
                BasicATK = new AbilityData
                {
                    Name = "Novaburst",
                    Instructions = new IAbilityInstruction[]
                    {
                        AbilityInstructionFactory.RegenerateEnergy(20.0f),
                        AbilityInstructionFactory.SetTargets(AbilityTargetWhos.EnemySingle),
                        AbilityInstructionFactory.RegisterDamage_MaxHP(0.55f, 20.0f),
                        AbilityInstructionFactory.ExecuteDamage(),
                    }
                },
                Skill = new AbilityData
                {
                    Name = "Known by Stars, Shown by Hearts",
                    Instructions = new IAbilityInstruction[]
                    {
                        AbilityInstructionFactory.RegenerateEnergy(30.0f),
                        AbilityInstructionFactory.SetTargets(AbilityTargetWhos.AllyAllExcludeSelf),
                        AbilityInstructionFactory.AddModifier_3Duration_NotStackable(1),
                    }
                },
            };

            AbilityDatabase.Modifiers = new AbilityModifierData[3];
            AbilityDatabase.Modifiers[0] = new AbilityModifierData
            {
                Name = "Burning",
                ActiveWhen = AbilityModifierActiveCondition.BeforeGettingTurn,
                Instructions = new IAbilityInstruction[]
                {
                    AbilityInstructionFactory.SetTargets(AbilityTargetWhos.Self),
                    AbilityInstructionFactory.RegisterDamage_ATK(1.0f, 10.0f),
                    AbilityInstructionFactory.ExecuteDamage_DontInvokeEvent(),
                }
            };

            AbilityDatabase.Modifiers[1] = new AbilityModifierData
            {
                Name = "Matrix of Prescience (Damage Distributor)",
                ActiveWhen = AbilityModifierActiveCondition.WhenEnemyAttackingMe,
                Instructions = new IAbilityInstruction[]
                {
                    AbilityInstructionFactory.SaveTargetInfos(),
                    AbilityInstructionFactory.ModifyDamageInfoTargetsToModifierOwner(),
                    AbilityInstructionFactory.ModifyDamageInfo(0.65f),
                    AbilityInstructionFactory.ExecuteDamage_DontInvokeEvent(),
                    AbilityInstructionFactory.LoadTargetInfos(),
                    AbilityInstructionFactory.ModifyDamageInfo(0.35f),
                    AbilityInstructionFactory.ExecuteDamage_DontInvokeEvent(),
                    AbilityInstructionFactory.SetTargets(AbilityTargetWhos.None),
                }
            };

            AbilityDatabase.Modifiers[2] = new AbilityModifierData
            {
                Name = "Seele Increase Speeds",
                ActiveWhen = AbilityModifierActiveCondition.DuringEvaluation,
                Instructions = new IAbilityInstruction[]
                {
                    AbilityInstructionFactory.SetTargets(AbilityTargetWhos.Self),
                    AbilityInstructionFactory.ModifyBaseStats_SPD_Percentage(0.25f),
                }
            };

            var myCharacter1 = new BattleEntityPlayerCharacter
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
                    CRITDMG = 240,
                },
                DMGType = new DMGType
                {
                    PhysicalDMGBoost = 43,
                    PhysicalPENBoost = 0
                },
                SkillID = 0
            };

            var myCharacter2 = new BattleEntityPlayerCharacter
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
                    CRITDMG = 50,
                },
                DMGType = new DMGType
                {
                    QuantumDMGBoost = 43,
                    QuantumPENBoost = 0
                },

                SkillID = 1
            };

            var enemyCharacter1 = new BattleEntityEnemy
            {
                Name = "Monster 1",
                CharacterID = 1001,
                Level = 90,
                Weaknesses = ElementalType.Fire | ElementalType.Physical | ElementalType.Quantum,
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

            var enemyCharacter2 = new BattleEntityEnemy
            {
                Name = "Monster 2",
                CharacterID = 1002,
                Level = 90,
                Weaknesses = ElementalType.Fire | ElementalType.Physical | ElementalType.Quantum,
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

            var enemyCharacter3 = new BattleEntityEnemy
            {
                Name = "Monster 3",
                CharacterID = 1003,
                Level = 90,
                Weaknesses = ElementalType.Fire | ElementalType.Physical | ElementalType.Quantum,
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

            var myCharacters = new List<BattleEntityBase>() { myCharacter1, myCharacter2 };
            var enemyCharacters = new List<BattleEntityBase>() { enemyCharacter1, enemyCharacter2, enemyCharacter3 };

            BattleStateInfo battleState = new BattleStateInfo(1
                , new List<BattleCharacterInfo>()
                , new List<BattleCharacterInfo>()
                , new List<AbilityModifierInfo>());
            var battle = new Battle(myCharacters, enemyCharacters);
            battle.BattleEvent += Battle_BeforeActionStart;
            battle.BattleEvent += Battle_AfterDecidedWhatToDo;
            battle.BattleEvent += PrintBattleState;
            battle.ReceivedModifierEvent += Battle_ReceivedModifierEvent;
            battle.StatsModifiedEvent += Battle_StatsModifiedEvent; ;
            while (battleState.BattleResult.IsBattleEnded == false)
            {
                battle.SimulateOneTurn(ref battleState);
            }
            Console.WriteLine($"apakah player menang? {battleState.BattleResult.IsWon} | Action: {battleState.Action}");

            return "WiraTamana";
        }

        private static void Battle_StatsModifiedEvent(BattleCharacterInfo modifierInfo)
        {
            Console.WriteLine($"Battle_StatsModifiedEvent");
            Console.WriteLine($"   - {modifierInfo.Name.ToString()}");
            Console.WriteLine($"      - BaseStats:");
            Console.WriteLine($"         - HP : {modifierInfo.BaseStatsBonus.HP}");
            Console.WriteLine($"         - ATK: {modifierInfo.BaseStatsBonus.ATK}");
            Console.WriteLine($"         - DEF: {modifierInfo.BaseStatsBonus.DEF}");
            Console.WriteLine($"         - SPD: {modifierInfo.BaseStatsBonus.SPD}");
        }

        private static void Battle_ReceivedModifierEvent(AbilityModifierInfo modifierInfo)
        {
            Console.WriteLine($"Battle_StatsModifiedEvent");
            Console.WriteLine($"   - {modifierInfo.ModifierData.Name}");
            Console.WriteLine($"      - Owner : {modifierInfo.OwnerCharacter.Name.ToString()}");
            Console.WriteLine($"      - Target: {modifierInfo.TargetCharacter.Name.ToString()}");
        }

        private static void Battle_BeforeActionStart(BattleEventType evt, BattleEventInfo info)
        {
            if (evt != BattleEventType.BeforeActionStart)
            {
                return;
            }

            Console.WriteLine($"Turn: {info.Action}");
            Console.WriteLine($"-------------------------------------------------");
            PrintCharacterStatus(info);
        }

        private static void Battle_AfterDecidedWhatToDo(BattleEventType evt, BattleEventInfo info)
        {
            if (evt != BattleEventType.AfterDecideWhatToDo)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"EventType: {evt}");
            Console.WriteLine($"   - ExecutingTurn: {info.ExecutingTurn.Name.ToString()}");
            Console.WriteLine($"      - WhatToDo: {info.WhatToDoInThisTurn} ({info.ExecutingTurn.GetAbilityData(info.WhatToDoInThisTurn).Name})");
        }

        private static void PrintBattleState(BattleEventType evt, BattleEventInfo info)
        {
            if (evt != BattleEventType.BeforeActionEnd)
            {
                return;
            }
            return;
            Console.WriteLine();

            var record = info.TargetInfos[0];
            var meDamage = record.DamageInfo;
            var target = record.Target;
            var executingTurn = info.ExecutingTurn;

            var critical = string.Empty;
            var weakness = string.Empty;
            if (meDamage.IsCritical)
            {
                critical = "CRITICAL ";
            }
            if (meDamage.IsDamageMatchTargetWeaknessElement)
            {
                weakness = "WEAKNESS ";
            }

            if (executingTurn.IsPlayerCharacter)
            {
                var broken = meDamage.IsBecomeBrokenInThisTurn == false && target.IsBroken ? "BROKEN " : string.Empty;
                Console.WriteLine($"Turn {info.Action:00}: {GetCharacterNameWithMaxAndCurrentHP(info.Players[executingTurn.Id])} deal {meDamage.TotalDamage} {critical}{weakness}damage to {GetCharacterNameWithMaxAndCurrentHP(info.Enemies[target.Id])}");
            }
            else
            {
                Console.WriteLine($"Turn {info.Action:00}: {GetCharacterNameWithMaxAndCurrentHP(info.Enemies[executingTurn.Id])} deal {meDamage.TotalDamage} {critical}{weakness}damage to {GetCharacterNameWithMaxAndCurrentHP(info.Players[target.Id])}");
            }

            if (target.IsPlayerCharacter == false && meDamage.IsBecomeBrokenInThisTurn) 
            {
                Console.WriteLine($"Turn {info.Action:00}: {info.Enemies[target.Id].Name.ToString()} BREAK");
            }
            
            if (target.BattleInfo.IsDead)
            {
                if (target.IsPlayerCharacter) 
                {
                    Console.WriteLine($"Turn {info.Action:00}: {info.Players[target.Id].Name.ToString()} dead");
                }
                else
                {
                    Console.WriteLine($"Turn {info.Action:00}: {info.Enemies[target.Id].Name.ToString()} dead");
                }

            }

            foreach (var enemy in info.Enemies)
            {
                Console.WriteLine($"  -> Position: {(int)enemy.BattleInfo.TurnPosition}");
            }
        }

        private static string GetCharacterNameWithMaxAndCurrentHP(BattleCharacterInfo info)
        {
            return $"{info.Name.ToString()} ({info.BattleInfo.CurrentHP}/{info.BattleInfo.MaxHP})";
        }

        private static void PrintCharacterStatus(BattleEventInfo info)
        {
            Console.WriteLine("Player Characters");
            foreach (var item in info.Players)
            {
                Console.WriteLine($"   - {item.Name.ToString()}");
                Console.WriteLine($"      - Status:");
                Console.WriteLine($"         - HP : {item.BattleInfo.CurrentHP}/{item.BattleInfo.MaxHP}");
                Console.WriteLine($"         - ATK: {item.BaseStats.ATK} + {item.BaseStatsBonus.ATK}");
                Console.WriteLine($"         - DEF: {item.BaseStats.DEF} + {item.BaseStatsBonus.DEF}");
                Console.WriteLine($"         - SPD: {item.BaseStats.SPD} + {item.BaseStatsBonus.SPD}");

                Console.WriteLine($"      - Modifiers:");
                foreach (var mod in info.ModifierInfos)
                {
                    if (mod.TargetCharacter == item)
                    {
                        Console.WriteLine($"         - {mod.ModifierData.Name}");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Enemy Characters");
            foreach (var item in info.Enemies)
            {
                Console.WriteLine($"   - {item.Name.ToString()}");
                Console.WriteLine($"      - Status:");
                Console.WriteLine($"         - HP : {item.BattleInfo.CurrentHP}/{item.BattleInfo.MaxHP}");
                Console.WriteLine($"         - ATK: {item.BaseStats.ATK} + {item.BaseStatsBonus.ATK}");
                Console.WriteLine($"         - DEF: {item.BaseStats.DEF} + {item.BaseStatsBonus.DEF}");
                Console.WriteLine($"         - SPD: {item.BaseStats.SPD} + {item.BaseStatsBonus.SPD}");

                Console.WriteLine($"      - Modifiers:");
                foreach (var mod in info.ModifierInfos)
                {
                    if (mod.TargetCharacter == item)
                    {
                        Console.WriteLine($"         - {mod.ModifierData.Name} (RemainingDuration: {mod.DurationTurn})");
                    }
                }
            }
        }

        public static void ModifySpan(Span<int> span)
        {
            span[0] = 100;
        }
    }
}
