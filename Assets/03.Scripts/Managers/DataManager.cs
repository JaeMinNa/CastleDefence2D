using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    [Header("GameData")]
    public int Stage = 1;
    public int Coin = 0;
    public int SkillDrawCount = 0;

    [Header("Sound")]
    public float BGMVolume = 0;
    public float SFXVolume = 0;

}

[System.Serializable]
public class PlayerData
{
    [Header("Stats")]
    public int Level = 1;
    public float MaxExp = 10;
    public float CurrentExp = 0;
    public float Speed = 3;
    public float Atk = 10;
    public float SkillTime = 3;
    public float AttackCoolTime = 1.5f;
    public float NuckbackPower = 2;

    [Header("Inventory")]
    public List<SkillData> SkillInventory;

    [Header("Equip Skill")]
    public SkillData EquipMeleeSkillData;
    public SkillData EquipRangedSkillData;
    public SkillData EquipAreaSkillData;
}

[System.Serializable]
public class CastleData
{
    [Header("Stats")]
    public int Level = 1;
    public float MaxExp = 10;
    public float CurrentExp = 0;
    public float Hp = 100;
    public float Speed = 10;
    public float Atk = 5;
    public float AttackCoolTime = 5f;
    public float NuckbackPower = 0.9f;
}

[System.Serializable]
public class DataWrapper
{
    public EnemyData[] EnemyData;
    public SkillData[] SkillData;
}

[System.Serializable]
public class EnemyData
{
    public enum AttackType
    {
        Melee,
        Ranged,
    }

    [Header("Common Stats")]
    public string Tag;
    public AttackType Type;
    public float Hp;
    public float Speed;
    public float Atk;
    public int Price;

    [Header("Ranged Stats")]
    public string BulletTag;
    public float BulletSpeed;
    public float Distance;
    public Vector3 BulletPosition;
}

[System.Serializable]
public class SkillData
{
    public enum SkillType
    {
        Melee,
        Ranged,
        Area,
    }

    public enum SkillRank
    {
        B,
        A,
        S,
    }

    [Header("Common Stats")]
    public string Tag;
    public SkillType Type;
    public SkillRank Rank;
    public string Description;
    public int Level;
    public int MaxUpgradeCount;
    public int CurrentUpgradeCount;
    public float AtkRatio;
    public float NuckbackPower;
    public bool IsGet;
    public bool IsEquip;
    public Sprite Icon;
    public GameObject SkillPrefab;
    public Vector3 StartPosition;

    [Header("Melee Stats")]
    public string ColliderName;

    [Header("Ranged / Area Stats")]
    public string SkillExplosionTag;
    public float Speed;
    public float ExplosionRange;
    public Vector3 HitRangePosition;

    [Header("Area Stats")]
    public float Range;
    public int Count;
    public float Interval;
}

public class DataManager : MonoBehaviour
{
    //[field: SerializeField] public GameDataSO GameDataSO;
    //[field: SerializeField] public PlayerSO PlayerSO;
    //[field: SerializeField] public CastleSO CastleSO;
    //[field: SerializeField] public EnemySO[] MeleeEnemySO;
    //[field: SerializeField] public EnemySO[] RangedEnemySO;
    //[field: SerializeField] public SkillSO[] MeleeSkillSO;
    //[field: SerializeField] public SkillSO[] RangedSkillSO;
    //[field: SerializeField] public SkillSO[] AreaSkillSO;
    [field: SerializeField] public int CurrentStageCoin;

    [Header("Start SKills")]
    [field: SerializeField] public SkillSO StartMeleeSkill;
    [field: SerializeField] public SkillSO StartRangedSkill;
    [field: SerializeField] public SkillSO StartAreaSkill;

    public GameData GameData;
    public PlayerData PlayerData;
    public CastleData CastleData;
    public DataWrapper DataWrapper;

    // 초기화
    public void Init()
    {
        DataLoad();
        SetInventory();
        SetEquip();
        CurrentStageCoin = 0;
    }

    // 메모리 해제
    public void Release()
    {

    }

    private void SetInventory()
    {
        PlayerData.SkillInventory.Clear();

        for (int i = 0; i < DataWrapper.SkillData.Length; i++)
        {
            if (DataWrapper.SkillData[i].IsGet)
            {
                PlayerData.SkillInventory.Add(DataWrapper.SkillData[i]);
            }
        }
    }

    private void SetEquip()
    {
        for (int i = 0; i < PlayerData.SkillInventory.Count; i++)
        {
            if (PlayerData.SkillInventory[i].IsEquip)
            {
                if (PlayerData.SkillInventory[i].Type == SkillData.SkillType.Melee)
                {
                    PlayerData.EquipMeleeSkillData = PlayerData.SkillInventory[i];
                }
                else if (PlayerData.SkillInventory[i].Type == SkillData.SkillType.Ranged)
                {
                    PlayerData.EquipRangedSkillData = PlayerData.SkillInventory[i];
                }
                else
                {
                    PlayerData.EquipAreaSkillData = PlayerData.SkillInventory[i];
                }
            }
        }
    }

    // GameData
    [ContextMenu("To Json GameData")]
    void SaveGameDataToJson()
    {
        string jsonData = JsonUtility.ToJson(GameData, true);
        string path = Path.Combine(Application.dataPath + "/Resources/Json/", "GameData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json GameData")]
    void LoadGameDataFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Json/", "GameData.json");
        string jsonData = File.ReadAllText(path);
        GameData = JsonUtility.FromJson<GameData>(jsonData);
    }

    // PlayerData
    [ContextMenu("To Json PlayerData")]
    void SavePlayerDataToJson()
    {
        string jsonData = JsonUtility.ToJson(PlayerData, true);
        string path = Path.Combine(Application.dataPath + "/Resources/Json/", "PlayerData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json PlayerData")]
    void LoadPlayerDataFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Json/", "PlayerData.json");
        string jsonData = File.ReadAllText(path);
        PlayerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }

    // CastleData
    [ContextMenu("To Json CastleData")]
    void SaveCastleDataToJson()
    {
        string jsonData = JsonUtility.ToJson(CastleData, true);
        string path = Path.Combine(Application.dataPath + "/Resources/Json/", "CastleData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json CastleData")]
    void LoadCastleDataFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Json/", "CastleData.json");
        string jsonData = File.ReadAllText(path);
        CastleData = JsonUtility.FromJson<CastleData>(jsonData);
    }

    // DataWrapper
    [ContextMenu("To Json DataWrapper")]
    void SaveDataWrapperToJson()
    {
        string jsonData = JsonUtility.ToJson(DataWrapper, true);
        string path = Path.Combine(Application.dataPath + "/Resources/Json/", "DataWrapper.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json DataWrapper")]
    void LoadDataWrapperFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Json/", "DataWrapper.json");
        string jsonData = File.ReadAllText(path);
        DataWrapper = JsonUtility.FromJson<DataWrapper>(jsonData);

    }
 

    public void DataSave()
    {
        SaveGameDataToJson();
        SavePlayerDataToJson();
        SaveCastleDataToJson();
        SaveDataWrapperToJson();
    }

    public void DataLoad()
    {
        LoadGameDataFromJson();
        LoadPlayerDataFromJson();
        LoadCastleDataFromJson();
        LoadDataWrapperFromJson();
    }

    public void CoinUpdate(int value)
    {
        CurrentStageCoin += value;
    }

    //public void DataReset()
    //{
    //    // Game Data
    //    GameDataSO.Stage = 1;
    //    GameDataSO.Coin = 0;
    //    GameDataSO.SkillDrawCount = 0;

    //    // Player
    //    PlayerSO.Level = 1;
    //    PlayerSO.MaxExp = 10f;
    //    PlayerSO.CurrentExp = 0f;
    //    PlayerSO.Atk = 10f;
    //    PlayerSO.Speed = 3f;
    //    PlayerSO.EquipMeleeSkill = StartMeleeSkill;
    //    PlayerSO.EquipRangedSkill = StartRangedSkill;
    //    PlayerSO.EquipAreaSkill = StartAreaSkill;
    //    PlayerSO.SkillInventroy.Clear();
    //    PlayerSO.SkillInventroy.Add(StartMeleeSkill);
    //    PlayerSO.SkillInventroy.Add(StartRangedSkill);
    //    PlayerSO.SkillInventroy.Add(StartAreaSkill);

    //    // Castle
    //    CastleSO.Level = 1;
    //    CastleSO.MaxExp = 10f;
    //    CastleSO.CurrentExp = 0f;
    //    CastleSO.Atk = 5f;
    //    CastleSO.Hp = 100f;
    //    CastleSO.AttackCoolTime = 5f;

    //    // MeleeSkill
    //    // Strike
    //    MeleeSkillSO[0].AtkRatio = 1.5f;
    //    MeleeSkillSO[0].Level = 1;
    //    MeleeSkillSO[0].MaxUpgradeCount = 3;
    //    MeleeSkillSO[0].CurrentUpgradeCount = 0;
    //    MeleeSkillSO[0].IsEquip = false;
    //    MeleeSkillSO[0].IsGet = false;
    //    // BoltSword
    //    MeleeSkillSO[1].AtkRatio = 1.9f;
    //    MeleeSkillSO[1].Level = 1;
    //    MeleeSkillSO[1].MaxUpgradeCount = 3;
    //    MeleeSkillSO[1].CurrentUpgradeCount = 0;
    //    MeleeSkillSO[1].IsEquip = false;
    //    MeleeSkillSO[1].IsGet = false;
    //    // FireSword
    //    MeleeSkillSO[2].AtkRatio = 1.3f;
    //    MeleeSkillSO[2].Level = 1;
    //    MeleeSkillSO[2].MaxUpgradeCount = 3;
    //    MeleeSkillSO[2].CurrentUpgradeCount = 1;
    //    MeleeSkillSO[2].IsEquip = true;
    //    MeleeSkillSO[2].IsGet = true;
    //    // WindSword
    //    MeleeSkillSO[3].AtkRatio = 1.2f;
    //    MeleeSkillSO[3].Level = 1;
    //    MeleeSkillSO[3].MaxUpgradeCount = 3;
    //    MeleeSkillSO[3].CurrentUpgradeCount = 0;
    //    MeleeSkillSO[3].IsEquip = false;
    //    MeleeSkillSO[3].IsGet = false;
    //    // BloodStrike
    //    MeleeSkillSO[4].AtkRatio = 1.8f;
    //    MeleeSkillSO[4].Level = 1;
    //    MeleeSkillSO[4].MaxUpgradeCount = 3;
    //    MeleeSkillSO[4].CurrentUpgradeCount = 0;
    //    MeleeSkillSO[4].IsEquip = false;
    //    MeleeSkillSO[4].IsGet = false;
    //    // HolyStrike
    //    MeleeSkillSO[5].AtkRatio = 1.6f;
    //    MeleeSkillSO[5].Level = 1;
    //    MeleeSkillSO[5].MaxUpgradeCount = 3;
    //    MeleeSkillSO[5].CurrentUpgradeCount = 0;
    //    MeleeSkillSO[5].IsEquip = false;
    //    MeleeSkillSO[5].IsGet = false;
    //    // MagmaStrike
    //    MeleeSkillSO[6].AtkRatio = 1.4f;
    //    MeleeSkillSO[6].Level = 1;
    //    MeleeSkillSO[6].MaxUpgradeCount = 3;
    //    MeleeSkillSO[6].CurrentUpgradeCount = 0;
    //    MeleeSkillSO[6].IsEquip = false;
    //    MeleeSkillSO[6].IsGet = false;

    //    // RangedSkill
    //    // Fireball
    //    RangedSkillSO[0].AtkRatio = 1.4f;
    //    RangedSkillSO[0].Level = 1;
    //    RangedSkillSO[0].MaxUpgradeCount = 3;
    //    RangedSkillSO[0].CurrentUpgradeCount = 1;
    //    RangedSkillSO[0].IsEquip = true;
    //    RangedSkillSO[0].IsGet = true;
    //    // Boltball
    //    RangedSkillSO[1].AtkRatio = 2.1f;
    //    RangedSkillSO[1].Level = 1;
    //    RangedSkillSO[1].MaxUpgradeCount = 3;
    //    RangedSkillSO[1].CurrentUpgradeCount = 0;
    //    RangedSkillSO[1].IsEquip = false;
    //    RangedSkillSO[1].IsGet = false;
    //    // Darkball
    //    RangedSkillSO[2].AtkRatio = 1.6f;
    //    RangedSkillSO[2].Level = 1;
    //    RangedSkillSO[2].MaxUpgradeCount = 3;
    //    RangedSkillSO[2].CurrentUpgradeCount = 0;
    //    RangedSkillSO[2].IsEquip = false;
    //    RangedSkillSO[2].IsGet = false;
    //    // BlueFireball
    //    RangedSkillSO[3].AtkRatio = 1.8f;
    //    RangedSkillSO[3].Level = 1;
    //    RangedSkillSO[3].MaxUpgradeCount = 3;
    //    RangedSkillSO[3].CurrentUpgradeCount = 0;
    //    RangedSkillSO[3].IsEquip = false;
    //    RangedSkillSO[3].IsGet = false;
    //    // Tornadoball
    //    RangedSkillSO[4].AtkRatio = 1.7f;
    //    RangedSkillSO[4].Level = 1;
    //    RangedSkillSO[4].MaxUpgradeCount = 3;
    //    RangedSkillSO[4].CurrentUpgradeCount = 0;
    //    RangedSkillSO[4].IsEquip = false;
    //    RangedSkillSO[4].IsGet = false;
    //    // Laser
    //    RangedSkillSO[5].AtkRatio = 1.5f;
    //    RangedSkillSO[5].Level = 1;
    //    RangedSkillSO[5].MaxUpgradeCount = 3;
    //    RangedSkillSO[5].CurrentUpgradeCount = 0;
    //    RangedSkillSO[5].IsEquip = false;
    //    RangedSkillSO[5].IsGet = false;

    //    // AreaSkill
    //    // BoltShower
    //    AreaSkillSO[0].AtkRatio = 1.9f;
    //    AreaSkillSO[0].Count = 7;
    //    AreaSkillSO[0].Interval = 1f;
    //    AreaSkillSO[0].Level = 1;
    //    AreaSkillSO[0].MaxUpgradeCount = 3;
    //    AreaSkillSO[0].CurrentUpgradeCount = 0;
    //    AreaSkillSO[0].IsEquip = false;
    //    AreaSkillSO[0].IsGet = false;
    //    // FireShower
    //    AreaSkillSO[1].AtkRatio = 1.3f;
    //    AreaSkillSO[1].Count = 7;
    //    AreaSkillSO[1].Interval = 1f;
    //    AreaSkillSO[1].Level = 1;
    //    AreaSkillSO[1].MaxUpgradeCount = 3;
    //    AreaSkillSO[1].CurrentUpgradeCount = 1;
    //    AreaSkillSO[1].IsEquip = true;
    //    AreaSkillSO[1].IsGet = true;
    //    // DarkRain
    //    AreaSkillSO[2].AtkRatio = 1.5f;
    //    AreaSkillSO[2].Count = 7;
    //    AreaSkillSO[2].Interval = 1f;
    //    AreaSkillSO[2].Level = 1;
    //    AreaSkillSO[2].MaxUpgradeCount = 3;
    //    AreaSkillSO[2].CurrentUpgradeCount = 0;
    //    AreaSkillSO[2].IsEquip = false;
    //    AreaSkillSO[2].IsGet = false;
    //    // BlueFireRain
    //    AreaSkillSO[3].AtkRatio = 1.7f;
    //    AreaSkillSO[3].Count = 7;
    //    AreaSkillSO[3].Interval = 1f;
    //    AreaSkillSO[3].Level = 1;
    //    AreaSkillSO[3].MaxUpgradeCount = 3;
    //    AreaSkillSO[3].CurrentUpgradeCount = 0;
    //    AreaSkillSO[3].IsEquip = false;
    //    AreaSkillSO[3].IsGet = false;
    //    // TornadoShower
    //    AreaSkillSO[4].AtkRatio = 1.6f;
    //    AreaSkillSO[4].Count = 7;
    //    AreaSkillSO[4].Interval = 1f;
    //    AreaSkillSO[4].Level = 1;
    //    AreaSkillSO[4].MaxUpgradeCount = 3;
    //    AreaSkillSO[4].CurrentUpgradeCount = 0;
    //    AreaSkillSO[4].IsEquip = false;
    //    AreaSkillSO[4].IsGet = false;
    //    // LaserBomb
    //    AreaSkillSO[5].AtkRatio = 1.4f;
    //    AreaSkillSO[5].Count = 7;
    //    AreaSkillSO[5].Interval = 1f;
    //    AreaSkillSO[5].Level = 1;
    //    AreaSkillSO[5].MaxUpgradeCount = 3;
    //    AreaSkillSO[5].CurrentUpgradeCount = 0;
    //    AreaSkillSO[5].IsEquip = false;
    //    AreaSkillSO[5].IsGet = false;

    //    // MeleeEnemy
    //    // Snail
    //    MeleeEnemySO[0].Atk = 10f;
    //    MeleeEnemySO[0].Hp = 20f;
    //    // Rock
    //    MeleeEnemySO[1].Atk = 5f;
    //    MeleeEnemySO[1].Hp = 30f;
    //    // Chicken
    //    MeleeEnemySO[2].Atk = 12f;
    //    MeleeEnemySO[2].Hp = 30f;
    //    // Mushroom
    //    MeleeEnemySO[3].Atk = 15f;
    //    MeleeEnemySO[3].Hp = 20f;
    //    // Bunny
    //    MeleeEnemySO[4].Atk = 8f;
    //    MeleeEnemySO[4].Hp = 20f;
    //    // Turtle
    //    MeleeEnemySO[5].Atk = 30f;
    //    MeleeEnemySO[5].Hp = 40f;
    //    // Rino
    //    MeleeEnemySO[6].Atk = 40f;
    //    MeleeEnemySO[6].Hp = 30f;

    //    // RangedEnemy
    //    // Plant
    //    RangedEnemySO[0].Atk = 3f;
    //    RangedEnemySO[0].Hp = 15f;
    //    // Trunk
    //    RangedEnemySO[1].Atk = 5f;
    //    RangedEnemySO[1].Hp = 18f;
    //    // Radish
    //    RangedEnemySO[2].Atk = 8f;
    //    RangedEnemySO[2].Hp = 20f;
    //    // Skull
    //    RangedEnemySO[3].Atk = 10f;
    //    RangedEnemySO[3].Hp = 25f;
    //}

}
