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
    public int TutorialCount = 1;
    //public bool IsTutorial = false;

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
    public bool IsAttribute = false;

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

    public enum SkillAttribute
    {
        None,
        Fire,
        Electricity,
        Wind,
        Light,
        Dark,
    }

    [Header("Common Stats")]
    public string Tag;
    public SkillType Type;
    public SkillAttribute Attribute;
    public SkillRank Rank;
    public string Description;
    public int Level;
    public int MaxUpgradeCount;
    public int CurrentUpgradeCount;
    public float AtkRatio;
    public float NuckbackPower;
    public bool IsGet;
    public bool IsEquip;
    public string IconPath;
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
    public int CurrentStageCoin;
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
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            string jsonData = JsonUtility.ToJson(GameData, true);
            string path = Path.Combine(Application.persistentDataPath, "GameData.json");
            File.WriteAllText(path, jsonData);
        }
        else
        {
            string jsonData = JsonUtility.ToJson(GameData, true);
            string path = Path.Combine(Application.dataPath, "GameData.json");
            File.WriteAllText(path, jsonData);
        }
    }

    [ContextMenu("From Json GameData")]
    void LoadGameDataFromJson()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            string path = Path.Combine(Application.persistentDataPath, "GameData.json");
            string jsonData = File.ReadAllText(path);
            GameData = JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            string path = Path.Combine(Application.dataPath, "GameData.json");
            string jsonData = File.ReadAllText(path);
            GameData = JsonUtility.FromJson<GameData>(jsonData);
        }
    }

    // PlayerData
    [ContextMenu("To Json PlayerData")]
    void SavePlayerDataToJson()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            string jsonData = JsonUtility.ToJson(PlayerData, true);
            string path = Path.Combine(Application.persistentDataPath/* + "/05.Json/"*/, "PlayerData.json");
            File.WriteAllText(path, jsonData);
        }
        else
        {
            string jsonData = JsonUtility.ToJson(PlayerData, true);
            string path = Path.Combine(Application.dataPath/* + "/05.Json/"*/, "PlayerData.json");
            File.WriteAllText(path, jsonData);
        }
    }

    [ContextMenu("From Json PlayerData")]
    void LoadPlayerDataFromJson()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            string path = Path.Combine(Application.persistentDataPath, "PlayerData.json");
            string jsonData = File.ReadAllText(path);
            PlayerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            string path = Path.Combine(Application.dataPath, "PlayerData.json");
            string jsonData = File.ReadAllText(path);
            PlayerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
    }

    // CastleData
    [ContextMenu("To Json CastleData")]
    void SaveCastleDataToJson()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            string jsonData = JsonUtility.ToJson(CastleData, true);
            string path = Path.Combine(Application.persistentDataPath, "CastleData.json");
            File.WriteAllText(path, jsonData);
        }
        else
        {
            string jsonData = JsonUtility.ToJson(CastleData, true);
            string path = Path.Combine(Application.dataPath, "CastleData.json");
            File.WriteAllText(path, jsonData);
        }
    }

    [ContextMenu("From Json CastleData")]
    void LoadCastleDataFromJson()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            string path = Path.Combine(Application.persistentDataPath, "CastleData.json");
            string jsonData = File.ReadAllText(path);
            CastleData = JsonUtility.FromJson<CastleData>(jsonData);
        }
        else
        {
            string path = Path.Combine(Application.dataPath, "CastleData.json");
            string jsonData = File.ReadAllText(path);
            CastleData = JsonUtility.FromJson<CastleData>(jsonData);
        }
    }

    // DataWrapper
    [ContextMenu("To Json DataWrapper")]
    void SaveDataWrapperToJson()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            string jsonData = JsonUtility.ToJson(DataWrapper, true);
            string path = Path.Combine(Application.persistentDataPath, "DataWrapper.json");
            File.WriteAllText(path, jsonData);
        }
        else
        {
            string jsonData = JsonUtility.ToJson(DataWrapper, true);
            string path = Path.Combine(Application.dataPath, "DataWrapper.json");
            File.WriteAllText(path, jsonData);
        }
    }

    [ContextMenu("From Json DataWrapper")]
    void LoadDataWrapperFromJson()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            string path = Path.Combine(Application.persistentDataPath, "DataWrapper.json");
            string jsonData = File.ReadAllText(path);
            DataWrapper = JsonUtility.FromJson<DataWrapper>(jsonData);
        }
        else
        {
            string path = Path.Combine(Application.dataPath, "DataWrapper.json");
            string jsonData = File.ReadAllText(path);
            DataWrapper = JsonUtility.FromJson<DataWrapper>(jsonData);
        }

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

    public void DataReset()
    {
        // Game Data
        GameData.Stage = 1;
        GameData.Coin = 0;
        GameData.SkillDrawCount = 0;
        GameData.TutorialCount = 1;
        PlayerPrefs.DeleteAll();
        //GameData.IsTutorial = false;

        // Player
        PlayerData.Level = 1;
        PlayerData.MaxExp = 10f;
        PlayerData.CurrentExp = 0f;
        PlayerData.Atk = 10f;
        PlayerData.Speed = 3f;

        // Castle
        CastleData.Level = 1;
        CastleData.MaxExp = 10f;
        CastleData.CurrentExp = 0f;
        CastleData.Atk = 5f;
        CastleData.Hp = 100f;
        CastleData.AttackCoolTime = 5f;

        // MeleeSkill
        // Strike
        DataWrapper.SkillData[0].AtkRatio = 1.5f;
        DataWrapper.SkillData[0].Level = 1;
        DataWrapper.SkillData[0].MaxUpgradeCount = 3;
        DataWrapper.SkillData[0].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[0].IsEquip = false;
        DataWrapper.SkillData[0].IsGet = false;
        // BoltSword
        DataWrapper.SkillData[1].AtkRatio = 1.9f;
        DataWrapper.SkillData[1].Level = 1;
        DataWrapper.SkillData[1].MaxUpgradeCount = 3;
        DataWrapper.SkillData[1].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[1].IsEquip = false;
        DataWrapper.SkillData[1].IsGet = false;
        // FireSword
        DataWrapper.SkillData[2].AtkRatio = 1.3f;
        DataWrapper.SkillData[2].Level = 1;
        DataWrapper.SkillData[2].MaxUpgradeCount = 3;
        DataWrapper.SkillData[2].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[2].IsEquip = false;
        DataWrapper.SkillData[2].IsGet = false;
        // WindSword
        DataWrapper.SkillData[3].AtkRatio = 1.2f;
        DataWrapper.SkillData[3].Level = 1;
        DataWrapper.SkillData[3].MaxUpgradeCount = 3;
        DataWrapper.SkillData[3].CurrentUpgradeCount = 1;
        DataWrapper.SkillData[3].IsEquip = true;
        DataWrapper.SkillData[3].IsGet = true;
        // BloodStrike
        DataWrapper.SkillData[4].AtkRatio = 1.8f;
        DataWrapper.SkillData[4].Level = 1;
        DataWrapper.SkillData[4].MaxUpgradeCount = 3;
        DataWrapper.SkillData[4].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[4].IsEquip = false;
        DataWrapper.SkillData[4].IsGet = false;
        // HolyStrike
        DataWrapper.SkillData[5].AtkRatio = 1.6f;
        DataWrapper.SkillData[5].Level = 1;
        DataWrapper.SkillData[5].MaxUpgradeCount = 3;
        DataWrapper.SkillData[5].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[5].IsEquip = false;
        DataWrapper.SkillData[5].IsGet = false;
        // MagmaStrike
        DataWrapper.SkillData[6].AtkRatio = 1.4f;
        DataWrapper.SkillData[6].Level = 1;
        DataWrapper.SkillData[6].MaxUpgradeCount = 3;
        DataWrapper.SkillData[6].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[6].IsEquip = false;
        DataWrapper.SkillData[6].IsGet = false;

        // RangedSkill
        // Fireball
        DataWrapper.SkillData[7].AtkRatio = 1.4f;
        DataWrapper.SkillData[7].Level = 1;
        DataWrapper.SkillData[7].MaxUpgradeCount = 3;
        DataWrapper.SkillData[7].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[7].IsEquip = false;
        DataWrapper.SkillData[7].IsGet = false;
        // Boltball
        DataWrapper.SkillData[8].AtkRatio = 2.1f;
        DataWrapper.SkillData[8].Level = 1;
        DataWrapper.SkillData[8].MaxUpgradeCount = 3;
        DataWrapper.SkillData[8].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[8].IsEquip = false;
        DataWrapper.SkillData[8].IsGet = false;
        // Darkball
        DataWrapper.SkillData[9].AtkRatio = 1.6f;
        DataWrapper.SkillData[9].Level = 1;
        DataWrapper.SkillData[9].MaxUpgradeCount = 3;
        DataWrapper.SkillData[9].CurrentUpgradeCount = 1;
        DataWrapper.SkillData[9].IsEquip = true;
        DataWrapper.SkillData[9].IsGet = true;
        // BlueFireball
        DataWrapper.SkillData[10].AtkRatio = 1.8f;
        DataWrapper.SkillData[10].Level = 1;
        DataWrapper.SkillData[10].MaxUpgradeCount = 3;
        DataWrapper.SkillData[10].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[10].IsEquip = false;
        DataWrapper.SkillData[10].IsGet = false;
        // Tornadoball
        DataWrapper.SkillData[11].AtkRatio = 1.7f;
        DataWrapper.SkillData[11].Level = 1;
        DataWrapper.SkillData[11].MaxUpgradeCount = 3;
        DataWrapper.SkillData[11].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[11].IsEquip = false;
        DataWrapper.SkillData[11].IsGet = false;
        // Laser
        DataWrapper.SkillData[12].AtkRatio = 1.5f;
        DataWrapper.SkillData[12].Level = 1;
        DataWrapper.SkillData[12].MaxUpgradeCount = 3;
        DataWrapper.SkillData[12].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[12].IsEquip = false;
        DataWrapper.SkillData[12].IsGet = false;

        // AreaSkill
        // BoltShower
        DataWrapper.SkillData[13].AtkRatio = 1.9f;
        DataWrapper.SkillData[13].Count = 7;
        DataWrapper.SkillData[13].Interval = 1f;
        DataWrapper.SkillData[13].Level = 1;
        DataWrapper.SkillData[13].MaxUpgradeCount = 3;
        DataWrapper.SkillData[13].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[13].IsEquip = false;
        DataWrapper.SkillData[13].IsGet = false;
        // FireShower
        DataWrapper.SkillData[14].AtkRatio = 1.3f;
        DataWrapper.SkillData[14].Count = 7;
        DataWrapper.SkillData[14].Interval = 1f;
        DataWrapper.SkillData[14].Level = 1;
        DataWrapper.SkillData[14].MaxUpgradeCount = 3;
        DataWrapper.SkillData[14].CurrentUpgradeCount = 1;
        DataWrapper.SkillData[14].IsEquip = true;
        DataWrapper.SkillData[14].IsGet = true;
        // DarkRain
        DataWrapper.SkillData[15].AtkRatio = 1.5f;
        DataWrapper.SkillData[15].Count = 7;
        DataWrapper.SkillData[15].Interval = 1f;
        DataWrapper.SkillData[15].Level = 1;
        DataWrapper.SkillData[15].MaxUpgradeCount = 3;
        DataWrapper.SkillData[15].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[15].IsEquip = false;
        DataWrapper.SkillData[15].IsGet = false;
        // BlueFireRain
        DataWrapper.SkillData[16].AtkRatio = 1.7f;
        DataWrapper.SkillData[16].Count = 7;
        DataWrapper.SkillData[16].Interval = 1f;
        DataWrapper.SkillData[16].Level = 1;
        DataWrapper.SkillData[16].MaxUpgradeCount = 3;
        DataWrapper.SkillData[16].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[16].IsEquip = false;
        DataWrapper.SkillData[16].IsGet = false;
        // TornadoShower
        DataWrapper.SkillData[17].AtkRatio = 1.6f;
        DataWrapper.SkillData[17].Count = 7;
        DataWrapper.SkillData[17].Interval = 1f;
        DataWrapper.SkillData[17].Level = 1;
        DataWrapper.SkillData[17].MaxUpgradeCount = 3;
        DataWrapper.SkillData[17].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[17].IsEquip = false;
        DataWrapper.SkillData[17].IsGet = false;
        // LaserBomb
        DataWrapper.SkillData[18].AtkRatio = 1.4f;
        DataWrapper.SkillData[18].Count = 7;
        DataWrapper.SkillData[18].Interval = 1f;
        DataWrapper.SkillData[18].Level = 1;
        DataWrapper.SkillData[18].MaxUpgradeCount = 3;
        DataWrapper.SkillData[18].CurrentUpgradeCount = 0;
        DataWrapper.SkillData[18].IsEquip = false;
        DataWrapper.SkillData[18].IsGet = false;

        // MeleeEnemy
        // Snail
        DataWrapper.EnemyData[0].Atk = 10f;
        DataWrapper.EnemyData[0].Hp = 20f;
        // Rock
        DataWrapper.EnemyData[1].Atk = 5f;
        DataWrapper.EnemyData[1].Hp = 30f;
        // Chicken
        DataWrapper.EnemyData[2].Atk = 12f;
        DataWrapper.EnemyData[2].Hp = 30f;
        // Mushroom
        DataWrapper.EnemyData[3].Atk = 15f;
        DataWrapper.EnemyData[3].Hp = 20f;
        // Bunny
        DataWrapper.EnemyData[4].Atk = 8f;
        DataWrapper.EnemyData[4].Hp = 20f;
        // Turtle
        DataWrapper.EnemyData[5].Atk = 30f;
        DataWrapper.EnemyData[5].Hp = 40f;
        // Rino
        DataWrapper.EnemyData[6].Atk = 40f;
        DataWrapper.EnemyData[6].Hp = 50f;

        // RangedEnemy
        // Plant
        DataWrapper.EnemyData[7].Atk = 3f;
        DataWrapper.EnemyData[7].Hp = 15f;
        // Trunk
        DataWrapper.EnemyData[8].Atk = 5f;
        DataWrapper.EnemyData[8].Hp = 18f;
        // Radish
        DataWrapper.EnemyData[9].Atk = 8f;
        DataWrapper.EnemyData[9].Hp = 20f;
        // Skull
        DataWrapper.EnemyData[10].Atk = 10f;
        DataWrapper.EnemyData[10].Hp = 25f;

        SetInventory();
        SetEquip();
        DataSave();
    }
}
