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
        // BoltSword
        MeleeSkillSO[1].AtkRatio = 1.9f;
        // FireSword
        MeleeSkillSO[2].AtkRatio = 1.3f;
        // WindSword
        MeleeSkillSO[3].AtkRatio = 1.2f;
        // BloodStrike
        MeleeSkillSO[4].AtkRatio = 1.8f;
        // HolyStrike
        MeleeSkillSO[5].AtkRatio = 1.6f;
        // MagmaStrike
        MeleeSkillSO[6].AtkRatio = 1.4f;

        // RangedSkill
        // Fireball
        RangedSkillSO[0].AtkRatio = 1.4f;
        // Boltball
        RangedSkillSO[1].AtkRatio = 2.1f;
        // Darkball
        RangedSkillSO[2].AtkRatio = 1.6f;
        // BlueFireball
        RangedSkillSO[3].AtkRatio = 1.8f;
        // Tornadoball
        RangedSkillSO[4].AtkRatio = 1.7f;
        // Laser
        RangedSkillSO[5].AtkRatio = 1.5f;

        // AreaSkill
        // BoltShower
        AreaSkillSO[0].AtkRatio = 2f;
        AreaSkillSO[0].Count = 7;
        AreaSkillSO[0].Interval = 1f;
        // FireShower
        AreaSkillSO[1].AtkRatio = 1.5f;
        AreaSkillSO[1].Count = 7;
        AreaSkillSO[1].Interval = 1f;
        // DarkRain
        AreaSkillSO[2].AtkRatio = 1.3f;
        AreaSkillSO[2].Count = 7;
        AreaSkillSO[2].Interval = 1f;
        // BlueFireRain
        AreaSkillSO[3].AtkRatio = 1.8f;
        AreaSkillSO[3].Count = 7;
        AreaSkillSO[3].Interval = 1f;
        // TornadoShower
        AreaSkillSO[4].AtkRatio = 1.7f;
        AreaSkillSO[4].Count = 7;
        AreaSkillSO[4].Interval = 1f;
        // LaserBomb
        AreaSkillSO[5].AtkRatio = 1.4f;
        AreaSkillSO[5].Count = 7;
        AreaSkillSO[5].Interval = 1f;

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
        RangedEnemySO[3].Hp = 20f;
    }
}
