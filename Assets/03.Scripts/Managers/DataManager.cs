using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [field: SerializeField] public GameDataSO GameDataSO;
    [field: SerializeField] public PlayerSO PlayerSO;
    [field: SerializeField] public CastleSO CastleSO;
    [field: SerializeField] public EnemySO[] MeleeEnemySO;
    [field: SerializeField] public EnemySO[] RangedEnemySO;
    [field: SerializeField] public SkillSO[] MeleeSkillSO;
    [field: SerializeField] public SkillSO[] RangedSkillSO;
    [field: SerializeField] public SkillSO[] AreaSkillSO;
    [field: SerializeField] public int CurrentStageCoin;

    [Header("Start SKills")]
    [field: SerializeField] public SkillSO StartMeleeSkill;
    [field: SerializeField] public SkillSO StartRangedSkill;
    [field: SerializeField] public SkillSO StartAreaSkill;

    // 초기화
    public void Init()
    {
        CurrentStageCoin = 0;
    }

    // 메모리 해제
    public void Release()
    {

    }

    public void CoinUpdate(int value)
    {
        CurrentStageCoin += value;
    }

    public void DataReset()
    {
        // Game Data
        GameDataSO.Stage = 1;
        GameDataSO.Coin = 0;

        // Player
        PlayerSO.Level = 1;
        PlayerSO.MaxExp = 10f;
        PlayerSO.CurrentExp = 0f;
        PlayerSO.Atk = 10f;
        PlayerSO.Speed = 3f;
        PlayerSO.EquipMeleeSkill = StartMeleeSkill;
        PlayerSO.EquipRangedSkill = StartRangedSkill;
        PlayerSO.EquipAreaSkill = StartAreaSkill;

        // Castle
        CastleSO.Level = 1;
        CastleSO.MaxExp = 10f;
        CastleSO.CurrentExp = 0f;
        CastleSO.Atk = 5f;
        CastleSO.Hp = 100f;
        CastleSO.AttackCoolTime = 5f;

        // MeleeSkill
        // Strike
        MeleeSkillSO[0].AtkRatio = 1.5f;
        MeleeSkillSO[0].Level = 1;
        MeleeSkillSO[0].MaxUpgradeCount = 3;
        MeleeSkillSO[0].CurrentUpgradeCount = 0;
        MeleeSkillSO[0].IsEquip = false;
        // BoltSword
        MeleeSkillSO[1].AtkRatio = 1.9f;
        MeleeSkillSO[1].Level = 1;
        MeleeSkillSO[1].MaxUpgradeCount = 3;
        MeleeSkillSO[1].CurrentUpgradeCount = 0;
        MeleeSkillSO[1].IsEquip = false;
        // FireSword
        MeleeSkillSO[2].AtkRatio = 1.3f;
        MeleeSkillSO[2].Level = 1;
        MeleeSkillSO[2].MaxUpgradeCount = 3;
        MeleeSkillSO[2].CurrentUpgradeCount = 0;
        MeleeSkillSO[2].IsEquip = true;
        // WindSword
        MeleeSkillSO[3].AtkRatio = 1.2f;
        MeleeSkillSO[3].Level = 1;
        MeleeSkillSO[3].MaxUpgradeCount = 3;
        MeleeSkillSO[3].CurrentUpgradeCount = 0;
        MeleeSkillSO[3].IsEquip = false;
        // BloodStrike
        MeleeSkillSO[4].AtkRatio = 1.8f;
        MeleeSkillSO[4].Level = 1;
        MeleeSkillSO[4].MaxUpgradeCount = 3;
        MeleeSkillSO[4].CurrentUpgradeCount = 0;
        MeleeSkillSO[4].IsEquip = false;
        // HolyStrike
        MeleeSkillSO[5].AtkRatio = 1.6f;
        MeleeSkillSO[5].Level = 1;
        MeleeSkillSO[5].MaxUpgradeCount = 3;
        MeleeSkillSO[5].CurrentUpgradeCount = 0;
        MeleeSkillSO[5].IsEquip = false;
        // MagmaStrike
        MeleeSkillSO[6].AtkRatio = 1.4f;
        MeleeSkillSO[6].Level = 1;
        MeleeSkillSO[6].MaxUpgradeCount = 3;
        MeleeSkillSO[6].CurrentUpgradeCount = 0;
        MeleeSkillSO[6].IsEquip = false;

        // RangedSkill
        // Fireball
        RangedSkillSO[0].AtkRatio = 1.4f;
        RangedSkillSO[0].Level = 1;
        RangedSkillSO[0].MaxUpgradeCount = 3;
        RangedSkillSO[0].CurrentUpgradeCount = 0;
        RangedSkillSO[0].IsEquip = true;
        // Boltball
        RangedSkillSO[1].AtkRatio = 2.1f;
        RangedSkillSO[1].Level = 1;
        RangedSkillSO[1].MaxUpgradeCount = 3;
        RangedSkillSO[1].CurrentUpgradeCount = 0;
        RangedSkillSO[1].IsEquip = false;
        // Darkball
        RangedSkillSO[2].AtkRatio = 1.6f;
        RangedSkillSO[2].Level = 1;
        RangedSkillSO[2].MaxUpgradeCount = 3;
        RangedSkillSO[2].CurrentUpgradeCount = 0;
        RangedSkillSO[2].IsEquip = false;
        // BlueFireball
        RangedSkillSO[3].AtkRatio = 1.8f;
        RangedSkillSO[3].Level = 1;
        RangedSkillSO[3].MaxUpgradeCount = 3;
        RangedSkillSO[3].CurrentUpgradeCount = 0;
        RangedSkillSO[3].IsEquip = false;
        // Tornadoball
        RangedSkillSO[4].AtkRatio = 1.7f;
        RangedSkillSO[4].Level = 1;
        RangedSkillSO[4].MaxUpgradeCount = 3;
        RangedSkillSO[4].CurrentUpgradeCount = 0;
        RangedSkillSO[4].IsEquip = false;
        // Laser
        RangedSkillSO[5].AtkRatio = 1.5f;
        RangedSkillSO[5].Level = 1;
        RangedSkillSO[5].MaxUpgradeCount = 3;
        RangedSkillSO[5].CurrentUpgradeCount = 0;
        RangedSkillSO[5].IsEquip = false;

        // AreaSkill
        // BoltShower
        AreaSkillSO[0].AtkRatio = 1.9f;
        AreaSkillSO[0].Count = 7;
        AreaSkillSO[0].Interval = 1f;
        AreaSkillSO[0].Level = 1;
        AreaSkillSO[0].MaxUpgradeCount = 3;
        AreaSkillSO[0].CurrentUpgradeCount = 0;
        AreaSkillSO[0].IsEquip = false;
        // FireShower
        AreaSkillSO[1].AtkRatio = 1.3f;
        AreaSkillSO[1].Count = 7;
        AreaSkillSO[1].Interval = 1f;
        AreaSkillSO[0].Level = 1;
        AreaSkillSO[0].MaxUpgradeCount = 3;
        AreaSkillSO[0].CurrentUpgradeCount = 0;
        AreaSkillSO[0].IsEquip = true;
        // DarkRain
        AreaSkillSO[2].AtkRatio = 1.5f;
        AreaSkillSO[2].Count = 7;
        AreaSkillSO[2].Interval = 1f;
        AreaSkillSO[2].Level = 1;
        AreaSkillSO[2].MaxUpgradeCount = 3;
        AreaSkillSO[2].CurrentUpgradeCount = 0;
        AreaSkillSO[2].IsEquip = false;
        // BlueFireRain
        AreaSkillSO[3].AtkRatio = 1.7f;
        AreaSkillSO[3].Count = 7;
        AreaSkillSO[3].Interval = 1f;
        AreaSkillSO[3].Level = 1;
        AreaSkillSO[3].MaxUpgradeCount = 3;
        AreaSkillSO[3].CurrentUpgradeCount = 0;
        AreaSkillSO[3].IsEquip = false;
        // TornadoShower
        AreaSkillSO[4].AtkRatio = 1.6f;
        AreaSkillSO[4].Count = 7;
        AreaSkillSO[4].Interval = 1f;
        AreaSkillSO[4].Level = 1;
        AreaSkillSO[4].MaxUpgradeCount = 3;
        AreaSkillSO[4].CurrentUpgradeCount = 0;
        AreaSkillSO[4].IsEquip = false;
        // LaserBomb
        AreaSkillSO[5].AtkRatio = 1.4f;
        AreaSkillSO[5].Count = 7;
        AreaSkillSO[5].Interval = 1f;
        AreaSkillSO[5].Level = 1;
        AreaSkillSO[5].MaxUpgradeCount = 3;
        AreaSkillSO[5].CurrentUpgradeCount = 0;
        AreaSkillSO[5].IsEquip = false;

        // MeleeEnemy
        // Snail
        MeleeEnemySO[0].Atk = 10f;
        MeleeEnemySO[0].Hp = 20f;
        // Rock
        MeleeEnemySO[1].Atk = 5f;
        MeleeEnemySO[1].Hp = 30f;
        // Chicken
        MeleeEnemySO[2].Atk = 12f;
        MeleeEnemySO[2].Hp = 30f;
        // Mushroom
        MeleeEnemySO[3].Atk = 15f;
        MeleeEnemySO[3].Hp = 20f;
        // Bunny
        MeleeEnemySO[4].Atk = 8f;
        MeleeEnemySO[4].Hp = 20f;
        // Turtle
        MeleeEnemySO[5].Atk = 30f;
        MeleeEnemySO[5].Hp = 40f;
        // Rino
        MeleeEnemySO[6].Atk = 40f;
        MeleeEnemySO[6].Hp = 30f;

        // RangedEnemy
        // Plant
        RangedEnemySO[0].Atk = 3f;
        RangedEnemySO[0].Hp = 15f;
        // Trunk
        RangedEnemySO[1].Atk = 5f;
        RangedEnemySO[1].Hp = 18f;
        // Radish
        RangedEnemySO[2].Atk = 8f;
        RangedEnemySO[2].Hp = 20f;
        // Skull
        RangedEnemySO[3].Atk = 10f;
        RangedEnemySO[3].Hp = 25f;
    }
}
